using System;
using System.Collections.Generic;
using WaracleTechnicalTest.Models;

namespace WaracleTechnicalTest.API.Services
{
    public interface IChargingPointStoreService
    {
        public IEnumerable<ChargingPoint> GetChargingPoints();
        public IEnumerable<ChargingPoint> GetChargingPoint(string id);
        public IEnumerable<ChargingPoint> UpdateChargingPoint(ChargingPoint chargingPoint);
        public IEnumerable<ChargingPoint> DeleteChargingPoint(string id);
    }

    public class ChargingPointStoreService : IChargingPointStoreService
    {
        public IEnumerable<ChargingPoint> GetChargingPoints()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChargingPoint> GetChargingPoint(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChargingPoint> UpdateChargingPoint(ChargingPoint chargingPoint)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChargingPoint> DeleteChargingPoint(string id)
        {
            throw new NotImplementedException();
        }
    }

}
