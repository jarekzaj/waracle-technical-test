using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using WaracleTechnicalTest.API.Services;
using WaracleTechnicalTest.Models.Canonical;

namespace WaracleTechnicalTest.API.Tests.Services
{
    public class ChargingPointStoreServiceTests
    {
        private ICosmosDbService _fakeCosmosDbService;
        private ChargingPointStoreService _chargingPointStoreService;

        [SetUp]
        public void Setup()
        {
            _fakeCosmosDbService = A.Fake<ICosmosDbService>();
            _chargingPointStoreService = new ChargingPointStoreService(_fakeCosmosDbService);
        }

        [Test]
        public async Task GetChargingPointsReturnsItemsFromDb()
        {
            var fakeItems = new List<DbChargingPoint>()
            {
                new DbChargingPoint
                {
                    Id = "123"
                },
                new DbChargingPoint
                {
                    Id = "124"
                }
            };
            A.CallTo(() => _fakeCosmosDbService.GetItems()).Returns(fakeItems);

            var result = await _chargingPointStoreService.GetChargingPoints();

            Assert.AreEqual(fakeItems, result);
        }

        [Test]
        public async Task UpdateChargingPointCallsUpdateItemAsyncOnDbService()
        {
            var chargingPoint = new DbChargingPoint
            {
                Id = "123"
            };

            A.CallTo(() => _fakeCosmosDbService.UpdateItemAsync(chargingPoint)).Returns(chargingPoint);

            var result = await _chargingPointStoreService.UpdateChargingPoint(chargingPoint);

            Assert.AreEqual(chargingPoint, result);
            A.CallTo(() => _fakeCosmosDbService.UpdateItemAsync(chargingPoint)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task DeleteChargingPointCallsDeleteItemAsyncOnDbService()
        {
            await _chargingPointStoreService.DeleteChargingPoint("1234");

            A.CallTo(() => _fakeCosmosDbService.DeleteItemAsync("1234")).MustHaveHappenedOnceExactly();
        }
    }
}
