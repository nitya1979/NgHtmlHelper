using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWebApp.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage ="First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of brith is required.")]
        [Display(Description = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage ="Email is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Gender is required.")]
        [Display(Name  = "Please enter Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Address1 is required.")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Preference is required.")]
        public string Prefereces { get; set; }

        public bool check1;

        [Display(Name ="Second Check")]
        public bool check2;

    }
}