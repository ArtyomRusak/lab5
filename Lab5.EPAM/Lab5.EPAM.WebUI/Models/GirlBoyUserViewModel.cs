using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5.EPAM.WebUI.Models
{
    public class GirlBoyUserViewModel : ViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int CountOfBoys { get; set; }
        public int CountOfGirls { get; set; }
    }
}