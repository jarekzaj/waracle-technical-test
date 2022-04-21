using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _chargingPointStoreService.DeleteChargingPoint(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
