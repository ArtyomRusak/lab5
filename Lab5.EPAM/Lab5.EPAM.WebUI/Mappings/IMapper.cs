using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab5.EPAM.Core;
using Lab5.EPAM.WebUI.Models;

namespace Lab5.EPAM.WebUI.Mappings
{
    public interface IMapper<TEntity, TViewModel>
        where TEntity : Entity
    {
        TViewModel MapEntityYoViewModel(TEntity entity);
        TEntity MapViewModelToEntity(TViewModel viewModel);
    }
}