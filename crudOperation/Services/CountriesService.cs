using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using OfficeOpenXml;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly PersonsDbContext _db;

        public CountriesService(PersonsDbContext personsDbContext, bool initialize = true)
        {
            _db = personsDbContext;
        }

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            //Validations
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest)); 
            }
            if(countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }
            if(await _db.Countries.CountAsync(country=>country.CountryName == countryAddRequest.CountryName) > 0)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }
            // Convert oject from CountryAddRequest to Country type 
            Country country = countryAddRequest.ToCountry();

            //generate CountryID
            country.CountryID = Guid.NewGuid();

            //Add Country object into _countries
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();

            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return await _db.Countries.Select(country=>country.ToCountryResponse()).ToListAsync();
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
                return null;

            Country? country_response_from_list = await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryID == countryID);

            if (country_response_from_list == null)
                return null;

            return country_response_from_list.ToCountryResponse();
        }

        public async Task<int> UploadCountriesFromExcel(IFormFile formFile)
        {
            MemoryStream memoryStream = new();
            await formFile.CopyToAsync(memoryStream);
            int CountriesInserted = 0;

            using ExcelPackage excelPackage = new ExcelPackage(memoryStream);
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Countires"];

            int rowCount = worksheet.Dimension.Rows;
            for (int row = 2; row <= rowCount; row++)
            {
                string? cellValue = Convert.ToString(worksheet.Cells[row, 1].Value);

                if (!string.IsNullOrEmpty(cellValue))
                {
                    string countryName = cellValue;

                    if(_db.Countries.Where(temp=>temp.CountryName == countryName).Count() == 0)
                    {
                        Country country = new() { CountryName = countryName };
                        _db.Countries.Add(country);
                        await _db.SaveChangesAsync();
                        CountriesInserted++;
                    }
                }
            }
            return CountriesInserted;
        }
    }
}