using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using Xunit;
using ServiceContracts.Enums;
using Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _countriesService = new CountriesService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options));
            _personsService = new PersonsService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options), _countriesService);
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson
        [Fact]
        public async Task AddPerson_NullPersonObject()
        {
            PersonAddRequest? personAddRequest = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async() =>
            {
                await _personsService.AddPerson(personAddRequest);
            });
        }

        [Fact]
        public async Task AddPerson_PersonNameNull()
        {
            PersonAddRequest? personAddRequest = new()
            {
                PersonName = null
            };

            await Assert.ThrowsAsync<ArgumentException>(async() =>
            {
                await _personsService.AddPerson(personAddRequest);
            });
        }

        [Fact]
        public async Task AddPerson_ProperPersonDetails()
        {
            PersonAddRequest? personAddRequest = new()
            {
                PersonName = "John", Address = "St.101", Email = "abc@gmail.com",
                CountryID = Guid.NewGuid(), Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2000-01-01"), ReceiveNewsLetters = true
            };

            PersonResponse personResponse = await _personsService.AddPerson(personAddRequest);

            Assert.True(personResponse.PersonID != Guid.Empty);
        }
        #endregion

        #region GetPersonByPersonID
        // If we supply null as PersonID, it should return null as PersonResponse
        [Fact]
        public async Task GetPersonByPersonID_NullPersonID()
        {
            Guid? personID = null;
             
            PersonResponse? personResponse = await _personsService.GetPersonbyPersonID(personID);

            Assert.Null(personResponse);
        }

        [Fact]
        public async Task GetPersonByPersonID_WithProperID()
        {
            PersonAddRequest personAddRequest = new()
            {
                PersonName = "John",
                Address = "St.101",
                Email = "abc@gmail.com",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true,
            };
            PersonResponse personResposnefromAdd = await _personsService.AddPerson(personAddRequest);
            
            PersonResponse? personResposnefromGet = await _personsService.GetPersonbyPersonID(personResposnefromAdd.PersonID);

            Assert.Equal(personResposnefromAdd, personResposnefromGet);
        }
        #endregion

        #region GetAllPersons
        
        //GetAllPersons() should return an empty list by default
        [Fact]
        public async Task GetAllPersons_EmptyList()
        {
            List<PersonResponse> personResponses = await _personsService.GetAllPersons();
            
            Assert.Empty(personResponses);
        }

        // First, add few persons, and then when we call GetAllPersons(), it should return the same persons that were added
        [Fact]
        public async Task GetAllPersons_AddFewPersons()
        {
            PersonAddRequest personAddRequest1 = new() { 
                PersonName = "john", Email = "abc@gmail.com", Gender = GenderOptions.Male, Address = "demo address", DateOfBirth = DateTime.Parse("2000-01-01"), ReceiveNewsLetters = true
            };
            PersonAddRequest personAddRequest2 = new()
            {
                PersonName = "smith",
                Email = "smith@gmail.com",
                Gender = GenderOptions.Male,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2001-11-01"),
                ReceiveNewsLetters = true
            };
            PersonAddRequest personAddRequest3 = new()
            {
                PersonName = "Nat",
                Email = "nat@gmail.com",
                Gender = GenderOptions.Female,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2002-01-11"),
                ReceiveNewsLetters = true
            };

            List<PersonAddRequest> personAddRequests_list = new()
            {
                personAddRequest1, personAddRequest2, personAddRequest3
            };
            
            List<PersonResponse> personResponsesList = new();

            foreach(PersonAddRequest person_request in personAddRequests_list)
            {
                PersonResponse personResponse = await _personsService.AddPerson(person_request);
                personResponsesList.Add(personResponse);
            }

            List<PersonResponse> personResponses = await _personsService.GetAllPersons();

            foreach (PersonResponse person in personResponsesList)
            {
                Assert.Contains(person, personResponses);
            }
        }
        #endregion

        #region GetFilteredPersons

        // If the search test is empty and search by is "PersonName" it should return all persons
        [Fact]
        public async Task GetFilteredPersons_EmptySearchText()
        {
            PersonAddRequest personAddRequest1 = new()
            {
                PersonName = "john",
                Email = "abc@gmail.com",
                Gender = GenderOptions.Male,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true
            };
            PersonAddRequest personAddRequest2 = new()
            {
                PersonName = "smith",
                Email = "smith@gmail.com",
                Gender = GenderOptions.Male,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2001-11-01"),
                ReceiveNewsLetters = true
            };
            PersonAddRequest personAddRequest3 = new()
            {
                PersonName = "Nat",
                Email = "nat@gmail.com",
                Gender = GenderOptions.Female,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2002-01-11"),
                ReceiveNewsLetters = true
            };

            List<PersonAddRequest> personAddRequests_list = new()
            {
                personAddRequest1, personAddRequest2, personAddRequest3
            };

            List<PersonResponse> personResponsesList = new();

            foreach (PersonAddRequest person_request in personAddRequests_list)
            {
                PersonResponse personResponse = await _personsService.AddPerson(person_request);
                personResponsesList.Add(personResponse);
            }

            List<PersonResponse> personResponses_fromSearch = await _personsService.GetFilteredPerson(nameof(Person.PersonName),"");

            foreach (PersonResponse person in personResponsesList)
            {
                Assert.Contains(person, personResponses_fromSearch);
            }
        }

        // Add Few Persons and search based on person name with some search string,it should return the matching persons.
        [Fact]
        public async Task GetFilteredPersons_SearchByPersonName()
        {
            PersonAddRequest personAddRequest1 = new()
            {
                PersonName = "john",
                Email = "abc@gmail.com",
                Gender = GenderOptions.Male,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true
            };
            PersonAddRequest personAddRequest2 = new()
            {
                PersonName = "smith",
                Email = "smith@gmail.com",
                Gender = GenderOptions.Male,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2001-11-01"),
                ReceiveNewsLetters = true
            };
            PersonAddRequest personAddRequest3 = new()
            {
                PersonName = "Nat",
                Email = "nat@gmail.com",
                Gender = GenderOptions.Female,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2002-01-11"),
                ReceiveNewsLetters = true
            };

            List<PersonAddRequest> personAddRequests_list = new()
            {
                personAddRequest1, personAddRequest2, personAddRequest3
            };

            List<PersonResponse> personResponsesList = new();

            foreach (PersonAddRequest person_request in personAddRequests_list)
            {
                PersonResponse personResponse = await _personsService.AddPerson(person_request);
                personResponsesList.Add(personResponse);
            }

            List<PersonResponse> personResponses_fromSearch = await _personsService.GetFilteredPerson(nameof(Person.PersonName), "ma");

            foreach (PersonResponse person in personResponsesList)
            {
                if(person.PersonName != null)
                {
                    if (person.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
                        Assert.Contains(person, personResponses_fromSearch);
                }
            }
        }
        #endregion

        #region GetSortedPersons
        //When we sort based on PersonName in DESC it should return persons list in descending on personName
        [Fact]
        public async Task GetSortedPersons()
        {
            PersonAddRequest personAddRequest1 = new()
            {
                PersonName = "john",
                Email = "abc@gmail.com",
                Gender = GenderOptions.Male,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true
            };
            PersonAddRequest personAddRequest2 = new()
            {
                PersonName = "smith",
                Email = "smith@gmail.com",
                Gender = GenderOptions.Male,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2001-11-01"),
                ReceiveNewsLetters = true
            };
            PersonAddRequest personAddRequest3 = new()
            {
                PersonName = "Nat",
                Email = "nat@gmail.com",
                Gender = GenderOptions.Female,
                Address = "demo address",
                DateOfBirth = DateTime.Parse("2002-01-11"),
                ReceiveNewsLetters = true
            };

            List<PersonAddRequest> personAddRequests_list = new()
            {
                personAddRequest1, personAddRequest2, personAddRequest3
            };

            List<PersonResponse> personResponsesList = new();

            foreach (PersonAddRequest person_request in personAddRequests_list)
            {
                PersonResponse personResponse = await _personsService.AddPerson(person_request);
                personResponsesList.Add(personResponse);
            }

            List<PersonResponse> AllPerson = await _personsService.GetAllPersons();

            List<PersonResponse> personResponses_fromSort = await _personsService.GetSortedPersons(AllPerson, nameof(Person.PersonName), SortOrderOptions.DESC);

            personResponsesList = personResponsesList.OrderByDescending(temp=>temp.PersonName).ToList();

            for(int i = 0;  i < personResponsesList.Count; i++)
            {
                Assert.Equal(personResponsesList[i], personResponses_fromSort[i]);
            }
        }
        #endregion

        #region UpdatePerson

        //when we supply null as PersonupdateRequest, it should throw ArgumentNullException 
        [Fact]
        public async Task UpdatePerson_NullPerson()
        {
            PersonUpdateRequest? personUpdateRequest = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async() => {
                //Act
               await _personsService.UpdatePerson(personUpdateRequest);
            });
        }

        //When we supply invalid person id, it should throw ArgumentException
        [Fact]
        public async Task UpdatePerson_InvalidPersonID()
        {
            //Arrange
            PersonUpdateRequest? person_update_request = new () { PersonID = Guid.NewGuid() };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>( async () => {
                //Act
                await _personsService.UpdatePerson(person_update_request);
            });
        }

        //When PersonName is null, it should throw ArgumentException
        [Fact]
        public async Task UpdatePerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest person_add_request = new PersonAddRequest() { PersonName = "John", Address = "Abc road", DateOfBirth = DateTime.Parse("2000-01-01"), Email = "abc@example.com", Gender = GenderOptions.Male, ReceiveNewsLetters = true };

            PersonResponse person_response_from_add = await _personsService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async() => {
                //Act
                await _personsService.UpdatePerson(person_update_request);
            });
        }

        //First, add a new person and try to update the person name and email
        [Fact]
        public async Task UpdatePerson_PersonFullDetailsUpdation()
        {
            //Arrange
            PersonAddRequest person_add_request = new () { PersonName = "John", Address = "Abc road", DateOfBirth = DateTime.Parse("2000-01-01"), Email = "abc@example.com", Gender = GenderOptions.Male, ReceiveNewsLetters = true };

            PersonResponse person_response_from_add = await _personsService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = "William";
            person_update_request.Email = "william@example.com";

            //Act
            PersonResponse person_response_from_update = await _personsService.UpdatePerson(person_update_request);

            PersonResponse? person_response_from_get = await _personsService.GetPersonbyPersonID(person_response_from_update.PersonID);

            //Assert
            Assert.Equal(person_response_from_get, person_response_from_update);
        }
        #endregion

        #region DeletePerson
        //If you supply an valid personID, it should return True

        [Fact]
        public async Task DeletePerson_ValidPersonID()
        {
            PersonAddRequest person_add_request = new () { PersonName = "John", Address = "Abc road", DateOfBirth = DateTime.Parse("2000-01-01"), Email = "abc@example.com", Gender = GenderOptions.Male, ReceiveNewsLetters = true };

            PersonResponse personsService = await _personsService.AddPerson(person_add_request);

            bool isDeleted = await _personsService.DeletePerson(personsService.PersonID);

            Assert.True(isDeleted);
        }

        //If you supply an Invalid personID, it should return false

        [Fact]
        public async Task DeletePerson_InvalidPersonID()
        {
            bool isDeleted = await _personsService.DeletePerson(Guid.NewGuid());

            Assert.False(isDeleted);
        }
        #endregion
    }
}
