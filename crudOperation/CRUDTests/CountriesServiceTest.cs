﻿using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;
        public CountriesServiceTest()
        {
            _countriesService = new CountriesService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options));
        }

        #region TestCases AddCountry

        //When CountryAddRequests is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Assert 
            await Assert.ThrowsAsync<ArgumentNullException>(async () => {
                //Act
                await _countriesService.AddCountry(request);
            });
        }

        //When the CountryName is null, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_CountryNameIsNullAsync()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = null };

            //Assert 
            await Assert.ThrowsAsync<ArgumentException>(async() => {
                //Act
                await _countriesService.AddCountry(request);
            });
        }

        //When the CountryName is duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            //Arrange
            CountryAddRequest? request1 = new () { CountryName = "USA" };
            CountryAddRequest? request2 = new () { CountryName = "USA" };

            //Assert 
            Assert.Throws<ArgumentException>(() => {
                //Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }
        //When you supply proper countryName, it should insert the country to the existing list of countries 
        [Fact]
        public async Task AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest? request = new () { CountryName = "Japan" };

            //Act
            CountryResponse response = await _countriesService.AddCountry(request);

            //Assert 
            Assert.True(response.CountryID != Guid.Empty);
        }

        #endregion

        #region TestCase GetAllCountries
        [Fact]
        //The list of countries should be empty by default (before adding any countries)
        public async Task GetAllCountries_EmptyList()
        {
            List<CountryResponse> actual_countryResponse = await _countriesService.GetAllCountries();

            Assert.Empty(actual_countryResponse);
        }

        #endregion
    }
}
