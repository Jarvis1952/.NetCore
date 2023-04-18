using Microsoft.AspNetCore.Mvc;

namespace PartialViews.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["ListTitle"] = "Cities";
            ViewData["ListItems"] = new List<string>()
            {
                "Paris",
                "NYC",
                "Rome"
            };
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            return View();
        }
    }
}
