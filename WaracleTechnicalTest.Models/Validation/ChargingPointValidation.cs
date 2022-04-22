using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace WaracleTechnicalTest.Models.Validation
{
    public class ChargingPointValidator : AbstractValidator<ChargingPoint>
    {
        private readonly List<string> _allowedProtocols = new List<string> { "1.5", "1.6", "1.7", "2.0" };

        public ChargingPointValidator()
        {
            RuleFor(chargingPoint => chargingPoint.Id).NotEmpty();
            RuleFor(chargingPoint => chargingPoint.Id).Must(BeANumber).WithMessage("Id needs to be a string representing a number");
            RuleFor(chargingPoint => chargingPoint.Name).NotEmpty().MaximumLength(30);
            RuleFor(chargingPoint => chargingPoint.Comment).NotEmpty().MaximumLength(200);
            RuleFor(chargingPoint => chargingPoint.ProtocolVersion).NotEmpty().Must(AllowedProtocols)
                .WithMessage($"Allowed protocols are {string.Join(", ", _allowedProtocols.Select(x => x))}");
            RuleFor(chargingPoint => chargingPoint.GroupId).NotEmpty();
            RuleFor(chargingPoint => chargingPoint.OwnerId).NotEmpty();
            RuleFor(chargingPoint => chargingPoint.Latitude).NotEmpty();
            RuleFor(chargingPoint => chargingPoint.Longitude).NotEmpty();
        }

        private bool BeANumber(string id)
        {
            return int.TryParse(id, out _);
        }

        private bool AllowedProtocols(string protocol)
        {
            return _allowedProtocols.Contains(protocol);
        }
    }
}
