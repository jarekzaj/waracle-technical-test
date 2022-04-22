using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using NUnit.Framework;
using WaracleTechnicalTest.API.Controllers;
using WaracleTechnicalTest.API.Services;
using WaracleTechnicalTest.Models;
using WaracleTechnicalTest.Models.Canonical;

namespace WaracleTechnicalTest.API.Tests
{
    public class Tests
    {
        private ChargingPointController _controller;
        private IChargingPointStoreService _fakeChargingPointStoreService;

        [SetUp]
        public void Setup()
        {
            _fakeChargingPointStoreService = A.Fake<IChargingPointStoreService>();
            _controller = new ChargingPointController(_fakeChargingPointStoreService);
        }

        [Test]
        public async Task GetReturnsChargingPointsFromChargingPointStore()
        {
            var dbChargingPoints = new List<DbChargingPoint>()
            {
                new DbChargingPoint {Id = "234"},
                new DbChargingPoint {Id = "345"}
            };

            A.CallTo(() => _fakeChargingPointStoreService.GetChargingPoints()).Returns(dbChargingPoints);

            var result = (OkObjectResult) await _controller.Get();
            Assert.AreEqual(200, result.StatusCode);

            var chargingPoints = (IEnumerable<ChargingPoint>)result.Value;
            Assert.IsNotNull(chargingPoints.FirstOrDefault(c => c.Id == "234"));
            Assert.IsNotNull(chargingPoints.FirstOrDefault(c => c.Id == "345"));
            Assert.AreEqual(2, chargingPoints.Count());
        }

        [Test]
        public async Task GetReturnsBadRequestIfExceptionThrown()
        {
            A.CallTo(() => _fakeChargingPointStoreService.GetChargingPoints()).Throws<Exception>();

            var result = (BadRequestResult)await _controller.Get();

            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task PostCallsUpdateChargingPointAndReturnsChargingPoint()
        {
            var chargingPoint = new ChargingPoint
            {
                Id = "12234"
            };

            A.CallTo(() => _fakeChargingPointStoreService.UpdateChargingPoint(A<DbChargingPoint>.Ignored))
                .Returns(new DbChargingPoint {Id = "12234"});

            var result = (OkObjectResult)await _controller.Post(chargingPoint);

            A.CallTo(() => _fakeChargingPointStoreService.UpdateChargingPoint(A<DbChargingPoint>.Ignored))
                .MustHaveHappenedOnceExactly();

            Assert.AreEqual(200, result.StatusCode);

            Assert.AreEqual("12234", ((ChargingPoint)result.Value).Id);
        }

        [Test]
        public async Task PostReturnsBadRequestIfExceptionThrown()
        {
            A.CallTo(() => _fakeChargingPointStoreService.UpdateChargingPoint(A<DbChargingPoint>.Ignored)).Throws<Exception>();

            var result = (BadRequestResult)await _controller.Post(new ChargingPoint());

            Assert.AreEqual(400, result.StatusCode);
        }


        [Test]
        public async Task DeleteCallsDeleteChargingPointAndReturnsOk()
        {
            var result = (OkResult)await _controller.Delete("123");

            A.CallTo(() => _fakeChargingPointStoreService.DeleteChargingPoint("123")).MustHaveHappenedOnceExactly();

            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task DeleteReturnsBadRequestIfExceptionThrown()
        {
            A.CallTo(() => _fakeChargingPointStoreService.DeleteChargingPoint("123")).Throws<Exception>();

            var result = (BadRequestResult)await _controller.Delete("123");

            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task DeleteReturnsNotFoundIfCosmosNotFoundExceptionThrown()
        {
            var cosmosNotFoundException = new CosmosException("Test", HttpStatusCode.NotFound, 404, "", 1);
            A.CallTo(() => _fakeChargingPointStoreService.DeleteChargingPoint("123")).Throws(cosmosNotFoundException);

            var result = (NotFoundResult)await _controller.Delete("123");

            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async Task DeleteReturnsBadRequestFOrAnyOtherCosmosExceptions()
        {
            var cosmosNotFoundException = new CosmosException("Test", HttpStatusCode.FailedDependency, 404, "", 1);
            A.CallTo(() => _fakeChargingPointStoreService.DeleteChargingPoint("123")).Throws(cosmosNotFoundException);

            var result = (BadRequestResult)await _controller.Delete("123");

            Assert.AreEqual(400, result.StatusCode);
        }
    }
}