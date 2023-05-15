using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;

        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();
            if (initialize)
            {
                _countries.AddRange(new List<Country>()
                {
                    new Country() { CountryID = Guid.Parse("D919D5B4-832E-44D9-80B0-1DD5A2D5E081"), CountryName = "USA" },

                    new Country() { CountryID = Guid.Parse("600E27C4-18E8-4487-9151-FE7020DE943E"), CountryName = "UK" },

                    new Country() { CountryID = Guid.Parse("A1C80EEE-69EF-49B1-A41C-9359E77DD111"), CountryName = "Canada" },

                    new Country() { CountryID = Guid.Parse("6B69260C-3516-46A4-B7A2-E6DF45E303AC"), CountryName = "India" }
                });

            }
            
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

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
                return null;

            Country? country_response_from_list = _countries.FirstOrDefault(temp => temp.CountryID == countryID);

            if (country_response_from_list == null)
                return null;

            return country_response_from_list.ToCountryResponse();
        }
    }
}