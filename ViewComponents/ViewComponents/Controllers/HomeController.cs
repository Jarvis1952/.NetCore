using Microsoft.AspNetCore.Mvc;
using ViewComponents.Models;

namespace ViewComponents.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            return View();
        }

        [Route("emplist")]
        public IActionResult ShowEmpList()
        {

            PersonGridModels personGridModel = new()
            {
                GridName = "Employee List",
                Persons = new()
                {
                    new Person() {PersonName = "john", JobTitle = "Developer"},
                    new Person() {PersonName = "Adam", JobTitle = "Manager"},
                    new Person() {PersonName = "smith", JobTitle = "QA"}
                }
            };
            return ViewComponent("Grid", new { grid = personGridModel });
        }
    }
}
