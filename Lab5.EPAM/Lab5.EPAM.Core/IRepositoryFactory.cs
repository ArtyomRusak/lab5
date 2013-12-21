using System.Security.Cryptography.X509Certificates;
using Lab5.EPAM.Core.Entities;
using Lab5.EPAM.Core.InterfaceRepository;

namespace Lab5.EPAM.Core
{
    public interface IRepositoryFactory
    {
        IRepository<User, int> GetUserRepository();
        IRepository<Role, int> GetRoleRepository();
    }
}
