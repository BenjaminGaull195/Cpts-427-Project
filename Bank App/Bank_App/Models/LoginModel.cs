using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bank_App.Models
{
    public class LoginModel
    {
        
        [DisplayName("Login")]
        [Required(ErrorMessage = "Please enter your Login User Id or Email address.")]
        [StringLength(50, ErrorMessage = "Field cannot be more than 50 characters.")]
        public string loginID { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter your Password.")]
        [StringLength(50, ErrorMessage = "Field cannot be more than 50 characters.")]
        public string passwordHash { get; set; }

        public bool loginFailed { get; set; } = false;
    }
}