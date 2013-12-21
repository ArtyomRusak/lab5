using System.Data.Entity.ModelConfiguration;
using Lab5.EPAM.Core.Entities;

namespace Lab5.EPAM.EFData.EFContext.Mappings
{
    internal class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Name).IsRequired();
            HasMany(e => e.Users).WithMany(e => e.Roles);
        }
    }
}
