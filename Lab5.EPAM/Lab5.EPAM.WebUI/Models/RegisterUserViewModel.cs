using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Lab5.EPAM.Core.Entities;

namespace Lab5.EPAM.WebUI.Models
{
    public class RegisterUserViewModel : ViewModel
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(40)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool Male { get; set; }
    }
}