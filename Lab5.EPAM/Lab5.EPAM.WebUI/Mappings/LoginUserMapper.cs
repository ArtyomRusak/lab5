using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab5.EPAM.Core.Entities;
using Lab5.EPAM.Services.Services;
using Lab5.EPAM.WebUI.Models;

namespace Lab5.EPAM.WebUI.Mappings
{
    public class LoginUserMapper : IMapper<User, LoginUserViewModel>
    {
        public LoginUserViewModel MapEntityToViewModel(User entity)
        {
            var viewModel = new LoginUserViewModel()
            {
                Email = entity.Email,
                UserName = entity.UserName
            };

            return viewModel;
        }

        public User MapViewModelToEntity(LoginUserViewModel viewModel)
        {
            return null;
        }
    }
}