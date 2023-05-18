using System;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    public interface IPersonsService
    {
        /// <summary>
        /// Adds a new person into the list of persons
        /// </summary>
        /// <param name="personAddRequest">Person to Add</param>
        /// <returns>Returns the same person Details along with newly generated PersonID</returns>
        Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Returns all persons
        /// </summary>
        /// <returns>Returns a list of objects of PersonResponse type</returns>
        Task<List<PersonResponse>> GetAllPersons();

        /// <summary>
        /// Returns Person Object based on the given personID
        /// </summary>
        /// <param name="personID">personID to search</param>
        /// <returns></returns>
        Task<PersonResponse?> GetPersonbyPersonID(Guid? personID);

        /// <summary>
        /// Returns all person Objects that matches with the given search field and search string
        /// </summary>
        /// <param name="SearhBy">Search Fields to search for</param>
        /// <param name="SearchString">search string to search for</param>
        /// <returns>Returns all matching persons based on the given search field and search string </returns>
        Task<List<PersonResponse>> GetFilteredPerson(string SearhBy, string? SearchString);

        /// <summary>
        /// Returns sorted list of persons
        /// </summary>
        /// <param name="allpersons"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortOrder"></param>
        /// <returns>Returns sorted persons as PersonResponse list</returns>
        Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> allpersons, string sortBy, SortOrderOptions sortOrder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personUpdateRequest"></param>
        /// <returns>Returns the personResponse oject after updation</returns>
        Task<PersonResponse> UpdatePerson(PersonUpdateRequest personUpdateRequest);

        /// <summary>
        /// Deletes a person based on the given personID
        /// </summary>
        /// <param name="PersonID">PersonID to Delete</param>
        /// <returns>Returns true if the deletion is true, otherwise false</returns>
        Task<bool> DeletePerson(Guid? personID);
    }
}
