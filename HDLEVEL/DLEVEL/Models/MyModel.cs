using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DLEVEL.Models
{

    public class MyModel
    {
        public int staff_ID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please enter a Staff Name")]
        [DisplayName("Staff Name")]
        [StringLength(50)]
        public string staff_Name { get; set; }
        [Required(ErrorMessage = "Please enter a Company Name")]
        [DisplayName("Company Name")]
        [StringLength(50)]
        public string company_Name { get; set; }
        [Required(ErrorMessage = "Please enter an Address")]
        [DisplayName("Address")]
        [StringLength(199)]
        public string address { get; set; }
        [Required(ErrorMessage = "Please enter a phone number")]
        [DisplayName("Contact Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        [StringLength(50)]
        public string phone { get; set; }
        [Required(ErrorMessage = "Please enter an age")]
        [DisplayName("Age")]
        [Range(1, 150, ErrorMessage = "The value must be greater than 1 and less than 150")]
        public int age { get; set; }
    }
}