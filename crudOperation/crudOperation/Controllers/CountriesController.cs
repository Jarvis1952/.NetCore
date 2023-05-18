using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace crudOperation.Controllers
{
    [Route("[Controller]")]
    public class CountriesController : Controller
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService )
        {
            _countriesService = countriesService;
        }

        [Route("UploadFromExcel")]
        public IActionResult UploadFromExcel()
        {
            return View();
        }

        [HttpPost]
        [Route("UploadFromExcel")]
        public async Task<IActionResult> UploadToExcel(IFormFile excelFile) 
        {
            if(excelFile == null || excelFile.Length == 0)
            {
                ViewBag.ErrorMessage = "Please Select an Excel File";
                return View();
            }    

            if(!Path.GetFileName(excelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.ErrorMessage = "Unsupported File, Please Select an Excel File";
                return View();
            }

            int countriesCount = await _countriesService.UploadCountriesFromExcel(excelFile);
            ViewBag.Message = $"{countriesCount} Countries Uploaded";
            return View();
        }
    }
}
