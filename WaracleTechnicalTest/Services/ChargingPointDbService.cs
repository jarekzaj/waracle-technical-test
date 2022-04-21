using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using WaracleTechnicalTest.Models.Canonical;

namespace WaracleTechnicalTest.API.Services
{
    public interface ICosmosDbService
    {
        Task<DbChargingPoint> GetItem(string id, string partitionKey);
        Task<IEnumerable<DbChargingPoint>> GetItems();
        Task<DbChargingPoint> UpdateItemAsync(DbChargingPoint chargingPoint);
        Task DeleteItemAsync(string id);
    }

    public class ChargingPointDbService : ICosmosDbService
    {
        private readonly Container _container;

        public ChargingPointDbService(CosmosClient dbClient, string databaseName, string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task<DbChargingPoint> GetItem(string id, string partitionKey)
        {
            try
            {
                ItemResponse<DbChargingPoint> response = await _container.ReadItemAsync<DbChargingPoint>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<DbChargingPoint>> GetItems()
        {
            var q = _container.GetItemLinqQueryable<DbChargingPoint>();
            var iterator = q.ToFeedIterator();
            return await iterator.ReadNextAsync();
        }

        public async Task<DbChargingPoint> UpdateItemAsync(DbChargingPoint chargingPoint)
        {
            var existingItem = await GetItem(chargingPoint.Id, chargingPoint.Id);
            if (existingItem != null)
            {
                return await _container.UpsertItemAsync(chargingPoint, new PartitionKey(chargingPoint.Id));
            }

            return await _container.CreateItemAsync(chargingPoint, new PartitionKey(chargingPoint.Id));
        }

        public async Task DeleteItemAsync(string id)
        { 
            await _container.DeleteItemAsync<DbChargingPoint>(id, new PartitionKey(id));
        }
    }
}
