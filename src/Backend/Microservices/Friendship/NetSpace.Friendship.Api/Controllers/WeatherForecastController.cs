using Microsoft.AspNetCore.Mvc;
using NetSpace.Friendship.Infrastructure;

namespace NetSpace.Friendship.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(NetSpaceDbContext dbContext) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult Get()
        {
            var follower = new Domain.UserEntity(Guid.NewGuid(), "iejof", "oiwjefiowje", null, Domain.Gender.NotSet);
            dbContext.Users.Add(follower);

            follower.Followers.Add(new Domain.UserEntity(Guid.NewGuid(), "eijf", "iejo", null, Domain.Gender.Female));

            dbContext.SaveChangesAsync();
              
            return Ok();
        }
    }
}
