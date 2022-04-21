using Newtonsoft.Json;

namespace WaracleTechnicalTest.Models.Canonical
{
    public class DbChargingPoint
    {
        public DbChargingPoint()
        {
            
        }

        public DbChargingPoint(ChargingPoint chargingPoint)
        {
            Id = chargingPoint.Id;
            Name = chargingPoint.Name;
            Comment = chargingPoint.Comment;
            ProtocolVersion = chargingPoint.ProtocolVersion;
            GroupId = chargingPoint.GroupId;
            OwnerId = chargingPoint.OwnerId;
            Latitude = chargingPoint.Latitude;
            Longitude = chargingPoint.Longitude;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string PartitionKey => Id;

        public string Name { get; set; }

        public string Comment { get; set; }
        public string ProtocolVersion { get; set; }
        public int GroupId { get; set; }
        public int OwnerId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
