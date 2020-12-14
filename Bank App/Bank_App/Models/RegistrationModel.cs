using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bank_App.Models
{
    public class RegistrationModel
    {
        /*
        accountID SERIAL,
	    username VARCHAR(255),
	    email VARCHAR(255) NOT NULL,
	    passwordHash VARCHAR NOT NULL,
	    LastName VARCHAR(255) NOT NULL,
	    FirstName VARCHAR(255) NOT NULL,
         */
        [DisplayName("Username (Optional)")]
        [StringLength(64, ErrorMessage = "Field cannot be more than 64 characters.")]
        public string username { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter a valid email address")]
        [StringLength(64, ErrorMessage = "Field cannot be more than 64 characters.")]
        [RegularExpression(@"^([a-zA-Z0-9_\!\#\$\%\&amp;\'\*\-\/\=\?\^\`\{\|\}\~\.\+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9_\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})$", ErrorMessage = "Please enter a valid email.")]
        public string email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a valid password")]
        [StringLength(64, ErrorMessage = "Field cannot be more than 64 characters.")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{12,}$", ErrorMessage = "Passwords must be at least 12 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(64, ErrorMessage = "Field cannot be more than 64 characters.")]
        [Compare("password", ErrorMessage = "Password does not match")]
        public string confirmpassword { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter your Last Name")]
        [StringLength(64, ErrorMessage = "Field cannot be more than 64 characters.")]
        public string Lastname { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter your First Name")]
        [StringLength(64, ErrorMessage = "Field cannot be more than 64 characters.")]
        public string FirstName { get; set; }
    }
}