using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAS.WEB.Models
{
    public class UserProfileModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string UserName { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [Display(Name = "Contact email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }

        [Display(Name = "Mobile phone")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid phone number")]
        public string MobilePhone { get; set; }

        [Display(Name = "Home phone")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid phone number")]
        public string HomePhone { get; set; }

        [Display(Name = "Work phone")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid phone number")]
        public string WorkPhone { get; set; }
    
        [HiddenInput(DisplayValue = false)]
        public string ContactPhone { get; set; }
    }
}