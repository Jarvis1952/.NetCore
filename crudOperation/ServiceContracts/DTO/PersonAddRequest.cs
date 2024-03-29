﻿using Entities;
using ServiceContracts.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Acts as a DTO for inserting a new person
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "Person Name can't be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Enter Valid EmailAddress")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please Select Gender")]
        public GenderOptions? Gender { get; set; }
        
        [Required(ErrorMessage="Please Select Country")]
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }


        /// <summary>
        /// Converts the current object PersonAddRequest into new object of person type
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            return new Person() { PersonName  = PersonName, Email = Email, DateOfBirth = DateOfBirth,Gender = Gender.ToString(), CountryID = CountryID, Address = Address, ReceiveNewsLetters = ReceiveNewsLetters };
        }
    }
}
