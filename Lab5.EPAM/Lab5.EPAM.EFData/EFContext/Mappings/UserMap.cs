using System.Data.Entity.ModelConfiguration;
using Lab5.EPAM.Core.Entities;

namespace Lab5.EPAM.EFData.EFContext.Mappings
{
    internal class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(e => e.Id);
            Property(e => e.UserName).HasMaxLength(30).IsRequired();
            Property(e => e.Email).HasMaxLength(40).IsRequired();
            Property(e => e.PasswordSalt).IsRequired();
            Property(e => e.Password).IsRequired();
        }
    }
}
