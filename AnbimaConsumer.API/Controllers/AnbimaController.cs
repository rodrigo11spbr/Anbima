using AnbimaConsumer.Application;
using AnbimaConsumer.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnbimaConsumer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnbimaController : ControllerBase
    {
        public AnbimaController(IAnbimaApplication anbimaApplication)
        {
            AnbimaApplication = anbimaApplication;
        }
        readonly IAnbimaApplication AnbimaApplication;

        /// <summary>
        /// Recover anbima file
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Array of anbima file</returns>
        [HttpGet]
        public async Task<IActionResult> GetByDate([FromQuery]DateTime date)
        {
            IEnumerable<Anbima> result;
            try
            {
                result = await AnbimaApplication.GetByDate(date);
            }
            catch (ArgumentException exception)
            {
                return StatusCode(400, exception.Message);
            }
            catch (HttpRequestException exception)
            {
                return StatusCode(400, exception.Message);
            }
            return StatusCode(200, result);
        }
    }
}