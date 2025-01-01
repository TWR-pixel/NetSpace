using Microsoft.AspNetCore.Mvc;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IUserRepository userRepo) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
