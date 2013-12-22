using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.EPAM.Core.Entities
{
    public class Role : Entity<int>
    {
        public Role()
        {
            this.Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
