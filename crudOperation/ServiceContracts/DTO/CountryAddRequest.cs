using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Data Transfer Object class for adding a new Country
    /// </summary>
    public class CountryAddRequest
    {
        public string? CountryName { get; set; }
        
        public Country ToCountry()
        {
            return new Country { CountryName = CountryName };
        }
    }
}
