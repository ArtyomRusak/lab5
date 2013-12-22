using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab5.EPAM.Core.Entities;
using Lab5.EPAM.WebUI.Models;

namespace Lab5.EPAM.WebUI.Mappings
{
    public class RegisterUserMapper : IMapper<User, RegisterUserViewModel>
    {
        public RegisterUserViewModel MapEntityYoViewModel(User entity)
        {
            return null;
        }

        public User MapViewModelToEntity(RegisterUserViewModel viewModel)
        {
            return null;
        }
    }
}