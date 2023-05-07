using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
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
            if(_countries.Where(country=>country.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }
            // Convert oject from CountryAddRequest to Country type 
            Country country = countryAddRequest.ToCountry();

            //generate CountryID
            country.CountryID = Guid.NewGuid();

            //Add Country object into _countries
            _countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country=>country.ToCountryResponse()).ToList();
        }
    }
}