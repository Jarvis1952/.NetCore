﻿namespace RazorViews.Models
{
    public class Person
    {
        public string? Name { get; set; }
        public DateTime? DateofBirth { get; set; }
        public Gender PersonGender { get; set; }  
    }
    public enum Gender
    {
        Male, Female, Other
    }
}