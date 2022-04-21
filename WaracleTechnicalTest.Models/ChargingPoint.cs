using System;

namespace WaracleTechnicalTest.Models
{
    public class ChargingPoint
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }
        public string ProtocolVersion { get; set; }
        public int GroupId { get; set; }
        public int OwnerId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
