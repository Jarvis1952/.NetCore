using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;
using ServiceContracts.Enums;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;

        public PersonsService(bool initialize = true)
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
            if (initialize)
            {
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("6BD65612-C114-44C9-8E20-134513546ED7"),
                    PersonName = "Jedediah",
                    Email = "jcotta1@oracle.com",
                    DateOfBirth = DateTime.Parse("2013-02-21"),
                    Gender = "Male",
                    Address = "625 Fairview Road",
                    ReceiveNewsLetters = false,
                    CountryID = Guid.Parse("6B69260C-3516-46A4-B7A2-E6DF45E303AC")

                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("DDAB3D49-F5B6-46CC-858D-3AF5BD5A8827"),
                    PersonName = "Ellary",
                    Email = "einggall0@ed.gov",
                    DateOfBirth = DateTime.Parse("2014-05-18"),
                    Gender = "Female",
                    Address = "6 Weeping Birch Lane",
                    ReceiveNewsLetters = true,
                    CountryID = Guid.Parse("D919D5B4-832E-44D9-80B0-1DD5A2D5E081")

                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("4545BF56-D313-4872-A506-3B081D8AB008"),
                    PersonName = "Sybilla",
                    Email = "sousley2@china.com.cn",
                    DateOfBirth = DateTime.Parse("2000-08-01"),
                    Gender = "Female",
                    Address = "30703 Chinook Center",
                    ReceiveNewsLetters = true,
                    CountryID = Guid.Parse("600E27C4-18E8-4487-9151-FE7020DE943E")

                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("42FF717E-324D-43AC-9EE3-D8785077DCEB"),
                    PersonName = "Wilbur",
                    Email = "walbrighton3@rakuten.co.jp",
                    DateOfBirth = DateTime.Parse("2011-12-10"),
                    Gender = "Male",
                    Address = "28022 Blaine Alley",
                    ReceiveNewsLetters = false,
                    CountryID = Guid.Parse("A1C80EEE-69EF-49B1-A41C-9359E77DD111")

                });
            }
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            // check if PersonRequest is not null
            if (personAddRequest == null)
                throw new ArgumentNullException(nameof(personAddRequest));

            ValidationHelpers.ModelValidation(personAddRequest);

            //Convert personAddRequest into person type
            Person person = personAddRequest.ToPerson();

            // Generate new PersonID
            person.PersonID = Guid.NewGuid();

            // Add person object to person list
            _persons.Add(person);

            //Convert the Person Object into PersonResponse type
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(temp => ConvertPersonToPersonResponse(temp)).ToList();
        }

        public List<PersonResponse> GetFilteredPerson(string SearchBy, string? SearchString)
        {
            List<PersonResponse> AllPersons = GetAllPersons();
            List<PersonResponse> MatchingPersons = AllPersons;

            if (string.IsNullOrEmpty(SearchBy) || string.IsNullOrEmpty(SearchString))
                return MatchingPersons;
           
            switch(SearchBy)
            {
                case nameof(PersonResponse.PersonName):
                    MatchingPersons = AllPersons.Where(temp => 
                    string.IsNullOrEmpty(temp.PersonName) || temp.PersonName.Contains(SearchString,StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonResponse.Email):
                    MatchingPersons = AllPersons.Where(temp =>
                    string.IsNullOrEmpty(temp.Email) || temp.Email.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonResponse.DateOfBirth):
                    MatchingPersons = AllPersons.Where(temp =>
                    (temp.DateOfBirth != null) ? temp.DateOfBirth.Value.ToString("dd MM yyyyy").Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(PersonResponse.Gender):
                    MatchingPersons = AllPersons.Where(temp =>
                    string.IsNullOrEmpty(temp.Gender) || temp.Gender.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonResponse.CountryID):
                    MatchingPersons = AllPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ?
                    temp.Country.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.Address):
                    MatchingPersons = AllPersons.Where(temp =>
                    string.IsNullOrEmpty(temp.Address) || temp.Address.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                default: MatchingPersons = AllPersons;
                    break;
            }
            return MatchingPersons;
        }

        public PersonResponse? GetPersonbyPersonID(Guid? personID)
        {
            if (personID == null)
                return null;

            Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);

            if (person == null)
                return null;

            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allpersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allpersons;

            List<PersonResponse> sortedPerson = (sortBy, sortOrder)
            switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allpersons.OrderBy(temp=> temp.PersonName,StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allpersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allpersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allpersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allpersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.Gender).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allpersons.OrderByDescending(temp => temp.Gender).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allpersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.DESC) => allpersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) => allpersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                _ => allpersons
            };
            return sortedPerson;
        }

        public PersonResponse UpdatePerson(PersonUpdateRequest personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(Person));

            ValidationHelpers.ModelValidation(personUpdateRequest);

            Person? MatchingPerson = _persons.FirstOrDefault(temp=>temp.PersonID == personUpdateRequest.PersonID);

            if(MatchingPerson == null)
                throw new ArgumentException("Given person id doesn't exist");

            MatchingPerson.PersonName = personUpdateRequest.PersonName;
            MatchingPerson.Email = personUpdateRequest.Email;
            MatchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            MatchingPerson.Gender = personUpdateRequest.Gender.ToString();
            MatchingPerson.CountryID = personUpdateRequest.CountryID;
            MatchingPerson.Address = personUpdateRequest.Address;
            MatchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            return MatchingPerson.ToPersonResponse();
        }
        
        public bool DeletePerson(Guid? personID)
        {
            if (personID == null)
                throw new ArgumentNullException(nameof(personID));

            Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);

            if (person == null)
                return false;

            _persons.RemoveAll(temp => temp.PersonID == personID);

            return true;
        }
    }
}
