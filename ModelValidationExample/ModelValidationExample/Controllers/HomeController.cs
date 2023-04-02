using Microsoft.AspNetCore.Mvc;
using ModelValidation.Models;
using System;

namespace ModelValidation.Controllers
{
    public class HomeController : Controller
    {
        [Route("registration")]
        public IActionResult Index(Person person)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join("\n", ModelState.Values.SelectMany(Value => Value.Errors).Select(error => error.ErrorMessage));

                return BadRequest(errorMessage);
            }

            return Content($"{person}");
        }
    }
}
