using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WaracleTechnicalTest.Models;

namespace WaracleTechnicalTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChargingPointController : ControllerBase
    {

        private readonly ILogger<ChargingPointController> _logger;
        private readonly IChargingPointStoreService _chargingPointStoreService;

        public ChargingPointController(ILogger<ChargingPointController> logger, IChargingPointStoreService chargingPointStoreService)
        {
            _logger = logger;
            _chargingPointStoreService = chargingPointStoreService;
        }

        [HttpGet]
        public IEnumerable<ChargingPoint> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new ChargingPoint())
            .ToArray();
        }
    }
}
