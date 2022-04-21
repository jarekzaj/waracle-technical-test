using System;
using WaracleTechnicalTest.Models.Canonical;

namespace WaracleTechnicalTest.Models
{
    public class ChargingPoint
    {
        public ChargingPoint()
        {
            
        }

        public ChargingPoint(DbChargingPoint dbChargingPoint)
        {
            Id = dbChargingPoint.Id;
            Name = dbChargingPoint.Name;
            Comment = dbChargingPoint.Comment;
            ProtocolVersion = dbChargingPoint.ProtocolVersion;
            GroupId = dbChargingPoint.GroupId;
            OwnerId = dbChargingPoint.OwnerId;
            Latitude = dbChargingPoint.Latitude;
            Longitude = dbChargingPoint.Longitude;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }
        public string ProtocolVersion { get; set; }
        public int GroupId { get; set; }
        public int OwnerId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
