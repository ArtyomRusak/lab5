using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Lab5.EPAM.Core.Entities;

namespace Lab5.EPAM.EFData.EFContext.Initializers
{
    public class RecreateIfModelChanges : IDatabaseInitializer<SiteContext>
    {
        #region Implementation of IDatabaseInitializer<in SiteContext>

        public void InitializeDatabase(SiteContext context)
        {
            bool databaseExists;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                databaseExists = context.Database.Exists();
            }
            if (databaseExists)
            {
                if (context.Database.CompatibleWithModel(true))
                {
                    return;
                }
                context.Database.Delete();
            }
            context.Database.Create();
            context.Database.ExecuteSqlCommand("ALTER TABLE Users ADD CONSTRAINT EmailDataUnique UNIQUE (Email)");
            context.Database.ExecuteSqlCommand("ALTER TABLE Roles ADD CONSTRAINT NameDataUnique UNIQUE (Name)");
            try
            {
                context.SaveChanges();
                Seed(context);
            }
            catch (Exception ex)
            {
                context.Dispose();
                throw;
            }
        }

        #endregion

        public void Seed(SiteContext context)
        {
            var userRole = new Role { Name = "User" };
            var memberRole = new Role { Name = "Member" };
            var boyRole = new Role { Name = "Boy" };
            var girlRole = new Role { Name = "Girl" };
            var listOfRoles = new List<Role> { userRole, memberRole, boyRole, girlRole };
            listOfRoles.ForEach(e => context.Roles.Add(e));
            context.SaveChanges();
        }
    }
}
