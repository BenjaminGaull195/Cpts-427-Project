using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bank_App.Models
{
    public class AccountFormModel
    {
        public AccountFormModel()
        {
            //possibleTpyes = new List<SelectListItem>();
            
        }

        [DisplayName("Account Name (Optional)")]
        [StringLength(64, ErrorMessage = "Field cannot be more than 64 characters.")]
        public string name { get; set; }

        [DisplayName("Account Type")]
        [Required(ErrorMessage = "Please Select an Account Type.")]
        public string type { get; set; }
        //public List<string> possibleTpyes { get; set; }
               
        public List<SelectListItem> possibleTypes { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Savings", Value = "Savings"},
            new SelectListItem() { Text = "Checking", Value = "Checking"},

        };

    }
}