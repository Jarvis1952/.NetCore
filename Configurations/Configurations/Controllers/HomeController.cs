using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Configurations.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherAPIOptions _option;

        public HomeController(IOptions<WeatherAPIOptions> weatherApiOption)
        {
            _option = weatherApiOption.Value;
        }

        [Route("/")]
        public IActionResult Index()
        {
            //IConfigurationSection WeatherAPISection = _configuration.GetSection("WeatherAPI");

            // when we use it as cofiguration as a service then we dont required any of this
            //WeatherAPIOptions weatheroptions = _option.GetSection("WeatherAPI").Get<WeatherAPIOptions>();

            ViewBag.ClientID = _option.ClientID;
            ViewBag.ClientSecret = _option.ClientSecret;

            return View();
        }
    }
}
