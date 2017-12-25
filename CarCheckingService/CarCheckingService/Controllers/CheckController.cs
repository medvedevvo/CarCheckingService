using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarCheckingService.Services;
using CarCheckingService.Models;

namespace CarCheckingService.Controllers
{
    [Produces("application/json")]
    [Route("api/Check")]
    public class CheckController : Controller
    {
        private CarRequsetService carRequsetService;

        public CheckController(CarRequsetService carRequsetService)
        {
            this.carRequsetService = carRequsetService;
            //this.realCarService = realCarService;
        }

        [HttpGet("cars/{IdCar}/accus")]
        public async Task<IActionResult> GetCarAccus([FromRoute] int IdCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
   
            AccuListWithTime response = await carRequsetService.GetAccuState(IdCar);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("cars/{IdCar}/accus/{IdAccu}")]
        public async Task<IActionResult> GetCarAccu([FromRoute] int IdCar, [FromRoute] int IdAccu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccuListWithTime response = await carRequsetService.GetAccuState(IdCar, IdAccu);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut("cars/{IdCar}/init")]
        public async Task<IActionResult> PutAccuInit([FromRoute] int IdCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int response = await carRequsetService.PutAccuInit(IdCar);

            if (response != 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}