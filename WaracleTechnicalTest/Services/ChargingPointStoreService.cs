using System.Collections.Generic;
using System.Threading.Tasks;
using WaracleTechnicalTest.Models.Canonical;

namespace WaracleTechnicalTest.API.Services
{
    public interface IChargingPointStoreService
    {
        public Task<IEnumerable<DbChargingPoint>> GetChargingPoints();
        public Task<DbChargingPoint> UpdateChargingPoint(DbChargingPoint chargingPoint);
        public Task DeleteChargingPoint(string id);
    }

    public class ChargingPointStoreService : IChargingPointStoreService
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ChargingPointStoreService(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<DbChargingPoint>> GetChargingPoints()
        {
            var dbChargingPoints =  await _cosmosDbService.GetItems();

            return dbChargingPoints;
        }

        public async Task<DbChargingPoint> UpdateChargingPoint(DbChargingPoint chargingPoint)
        {
            return await _cosmosDbService.UpdateItemAsync(chargingPoint);
        }

        public async Task DeleteChargingPoint(string id)
        {
             await _cosmosDbService.DeleteItemAsync(id);
        }
    }

}
