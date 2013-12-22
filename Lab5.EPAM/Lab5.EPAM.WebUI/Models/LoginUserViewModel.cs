using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Lab5.EPAM.WebUI.Models
{
    public class LoginUserViewModel : ViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool RememberMe { get; set; }
    }
}