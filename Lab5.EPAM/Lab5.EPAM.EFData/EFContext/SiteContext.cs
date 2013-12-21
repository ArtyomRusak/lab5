using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.EPAM.Core.Entities;
using Lab5.EPAM.EFData.EFContext.Mappings;

namespace Lab5.EPAM.EFData.EFContext
{
    public class SiteContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public SiteContext(string connectionString)
            : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
