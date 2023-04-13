using Microsoft.AspNetCore.Mvc;
using RazorViews.Models;

namespace RazorViews.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Asp.Net Core Demo";
            List<Person> people = new()
            {
                new Person() { Name = "John", DateofBirth = Convert.ToDateTime("2000-05-19"), PersonGender = Gender.Male },
                new Person() { Name = "Tanya", DateofBirth = Convert.ToDateTime("2002-12-14"), PersonGender = Gender.Female },
                new Person() { Name = "Robert", DateofBirth = Convert.ToDateTime("2001-07-29"), PersonGender = Gender.Male }
            };

            //ViewData["people"] = people;
            ViewBag.people = people;
            
            return View();
        }

        [Route("person-details/{name}")]
        public IActionResult Details(string? name)
        {
            if(name == null)
            {
                return Content("PersonName can not be null");
            }

            List<Person> people = new()
            {
                new Person() { Name = "John", DateofBirth = Convert.ToDateTime("2000-05-19"), PersonGender = Gender.Male },
                new Person() { Name = "Tanya", DateofBirth = Convert.ToDateTime("2002-12-14"), PersonGender = Gender.Female },
                new Person() { Name = "Robert", DateofBirth = Convert.ToDateTime("2001-07-29"), PersonGender = Gender.Male }
            };
            Person? MatchingName = people.Where(match=>match.Name == name).FirstOrDefault();
            return View(MatchingName);
        }

        [Route("product-details")]
        public IActionResult ProductDetails()
        {
            Person person = new()
            {
                Name = "sara", PersonGender = Gender.Female, DateofBirth = Convert.ToDateTime("2001-05-07")
            };

            Product product = new()
            {
                ProductID = 1,
                ProductName = "TV"
            };

            PersonAndProductWrapper personAndProductWrapper = new()
            {
                personData = person,
                productData = product
            };
            return View(personAndProductWrapper);
        }
    }
}
