using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.EPAM.Core.Entities
{
    public class User : Entity<int>
    {
        public string UserName { get; set; }
        public int Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public bool Male { get; set; }
        public bool IsLogged { get; set; }
        public ICollection<Role> Roles { get; set; }

        public void SetPassword(string password)
        {
            Password = (password + PasswordSalt).GetHashCode();
        }

        public bool VerifyPassword(string password)
        {
            return Password == (password + PasswordSalt).GetHashCode();
        }
    }
}
