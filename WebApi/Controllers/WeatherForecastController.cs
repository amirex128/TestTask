using System.Threading.Tasks;
using Application.Weather;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private WeatherApplication _weatherApplication;

        public WeatherForecastController(WeatherApplication weatherApplication)
        {
            _weatherApplication = weatherApplication;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Location(WeatherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var httpResponse = await _weatherApplication.GetApiWeather(model);

            if (httpResponse.IsSuccessStatusCode)
            {
                var jobId = await _weatherApplication.ScheduleTask(model, httpResponse);
                return new JsonResult(new
                {
                    Status = "success",
                    JobId = jobId
                });
            }

            return BadRequest("process failed");
        }


    }
}