using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.EPAM.Core;
using Lab5.EPAM.Core.Entities;
using Lab5.EPAM.Core.Exceptions;
using Lab5.EPAM.Infrastructure.Guard;
using Lab5.EPAM.Services.Exceptions;

namespace Lab5.EPAM.Services.Services
{
    public class MembershipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _factoryOfRepositories;

        public MembershipService(IUnitOfWork unitOfWork, IRepositoryFactory factoryOfRepositories)
        {
            Guard.AgainstNullReference(unitOfWork, "unitOfWork");
            Guard.AgainstNullReference(factoryOfRepositories, "factoryOfRepositories");

            _unitOfWork = unitOfWork;
            _factoryOfRepositories = factoryOfRepositories;
        }

        public User RegisterUser(string userName, string password, string email, bool male)
        {
            var user = GetUserByEmail(email);

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    Male = male,
                    PasswordSalt = DateTime.Now.ToString(),
                    UserName = userName
                };
            }
            else
            {
                throw new MembershipServiceException("User exist.");
            }

            user.SetPassword(password);
            var userRepository = _factoryOfRepositories.GetUserRepository();
            userRepository.Create(user);
            var memberRole = GetRoleByName("Member");
            user.Roles.Add(memberRole);
            if (user.Male)
            {
                var boyRole = GetRoleByName("Boy");
                user.Roles.Add(boyRole);
            }
            else
            {
                var girlRole = GetRoleByName("Girl");
                user.Roles.Add(girlRole);
            }

            try
            {
                _unitOfWork.PreSave();
            }
            catch (Exception e)
            {
                throw new MembershipServiceException(e.Message);
            }

            return user;
        }

        public User LoginUser(string email, string password)
        {
            try
            {
                var user = GetUserByEmail(email);
                if (user == null)
                {
                    //throw new MembershipServiceException("User doesn't exist");
                    return null;
                }
                if (user.VerifyPassword(password))
                {
                    user.IsLogged = true;
                    UpdateUser(user);
                    return user;
                }
                else
                {
                    throw new MembershipServiceException("Wrong password.");
                }
            }
            catch (RepositoryException e)
            {
                throw new MembershipServiceException(e.Message);
            }
        }

        public void LogOut(int userId)
        {
            var user = GetUserById(userId);
            user.IsLogged = false;
            UpdateUser(user);
        }

        public User GetUserByEmail(string email)
        {
            var userRepository = _factoryOfRepositories.GetUserRepository();
            try
            {
                return userRepository.Find(e => e.Email == email);
            }
            catch (RepositoryException e)
            {
                throw new MissingMemberException(e.Message);
            }
        }

        public Role GetRoleByName(string name)
        {
            var roleRepository = _factoryOfRepositories.GetRoleRepository();
            try
            {
                return roleRepository.Find(e => e.Name == name);
            }
            catch (RepositoryException e)
            {
                throw new MembershipServiceException(e.Message);
            }
        }

        public User GetUserById(int userId)
        {
            var userRepository = _factoryOfRepositories.GetUserRepository();
            try
            {
                return userRepository.GetEntityById(userId);
            }
            catch (RepositoryException e)
            {
                throw new MembershipServiceException(e.Message);
            }
        }

        public void UpdateUser(User user)
        {
            var userRepository = _factoryOfRepositories.GetUserRepository();
            userRepository.Update(user);
        }
    }
}
