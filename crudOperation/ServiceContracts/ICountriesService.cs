using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents Business logic for manipulating Country Entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country objects to add</param>
        /// <returns>returns the country object after adding it (including newly generated country id)</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);
        
        /// <summary>
        /// Returns all countries from the list
        /// </summary>
        /// <returns>All Countries from the list as list of CountryResponse</returns>
        List<CountryResponse> GetAllCountries();
    
    }
}