using Microsoft.AspNetCore.Mvc;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

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
            var follower = new UserEntity(Guid.NewGuid(), "iejof", "oiwjefiowje", null, Gender.NotSet);

            userRepo.AddAsync(follower);

            return Ok();
        }
    }
}
