using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using WaracleTechnicalTest.API.Services;
using WaracleTechnicalTest.Models;
using WaracleTechnicalTest.Models.Canonical;

namespace WaracleTechnicalTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChargingPointController : ControllerBase
    {
        private readonly IChargingPointStoreService _chargingPointStoreService;

        public ChargingPointController(IChargingPointStoreService chargingPointStoreService)
        {
            _chargingPointStoreService = chargingPointStoreService;
        }

        /// <summary>
        /// Returns all Charging Points.
        /// </summary>
        /// <returns>All Charging Points</returns>
        /// <response code="200">Charging points returned successfully</response>
        /// <response code="400">An error has occurred</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok((await _chargingPointStoreService.GetChargingPoints()).Select(c => new ChargingPoint(c)));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Adds or updates Charging Point with specified Id.
        /// </summary>
        /// <returns>A newly created or updated Charging Point</returns>
        /// <response code="200">Update successful</response>
        /// <response code="400">An error has occurred</response>
        [HttpPost]
        public async Task<IActionResult> Post(ChargingPoint chargingPoint)
        {
            try
            {
                return Ok(new ChargingPoint(await _chargingPointStoreService.UpdateChargingPoint(new DbChargingPoint(chargingPoint))));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Deletes a Charging Point by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deletion successful</response>
        /// <response code="400">An error has occurred</response>
        /// <response code="404">An item with specified Id does not exist</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _chargingPointStoreService.DeleteChargingPoint(id);
                return Ok();
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
