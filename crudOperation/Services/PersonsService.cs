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

        public PersonsService()
        {
            _persons = new(); 
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
            PersonResponse personResponse =  person.ToPersonResponse();

            return personResponse;
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(temp => temp.ToPersonResponse()).ToList();
        }

        public List<PersonResponse> GetFilteredPerson(string SearchBy, string? SearchString)
        {
            List<PersonResponse> AllPersons = GetAllPersons();
            List<PersonResponse> MatchingPersons = AllPersons;

            if (string.IsNullOrEmpty(SearchBy) || string.IsNullOrEmpty(SearchString))
                return MatchingPersons;
           
            switch(SearchBy)
            {
                case nameof(Person.PersonName):
                    MatchingPersons = AllPersons.Where(temp => 
                    string.IsNullOrEmpty(temp.PersonName) || temp.PersonName.Contains(SearchString,StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(Person.Email):
                    MatchingPersons = AllPersons.Where(temp =>
                    string.IsNullOrEmpty(temp.Email) || temp.Email.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(Person.DateOfBirth):
                    MatchingPersons = AllPersons.Where(temp =>
                    (temp.DateOfBirth != null) ? temp.DateOfBirth.Value.ToString("dd MM yyyyy").Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Gender):
                    MatchingPersons = AllPersons.Where(temp =>
                    string.IsNullOrEmpty(temp.Gender) || temp.Gender.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(Person.Address):
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

                (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allpersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

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
