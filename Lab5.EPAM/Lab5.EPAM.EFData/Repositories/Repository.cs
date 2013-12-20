using System.Data.Entity;
using Lab5.EPAM.Core.InterfaceRepository;

namespace Lab5.EPAM.EFData.Repositories
{
    public class Repository : IRepository
    {
        protected DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }
    }
}
