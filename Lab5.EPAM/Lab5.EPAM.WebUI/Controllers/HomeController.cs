using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Antlr.Runtime;
using AttributeRouting.Web.Mvc;
using Lab5.EPAM.EFData;
using Lab5.EPAM.EFData.EFContext;
using Lab5.EPAM.Services.Services;
using Lab5.EPAM.WebUI.Mappings;
using Lab5.EPAM.WebUI.Models;
using Microsoft.Ajax.Utilities;

namespace Lab5.EPAM.WebUI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("boys")]
        [Authorize(Roles = "Boy")]
        public ActionResult BoysPage()
        {
            var email = HttpContext.User.Identity.Name;
            var context = new SiteContext(Resources.ConnectionString);
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);
            var user = membershipService.GetUserByEmail(email);
            var viewModel = new GirlBoyUserViewModel
            {
                Email = user.Email,
                UserName = user.UserName,
                CountOfBoys = membershipService.GetCountOfBoys(),
                CountOfGirls = membershipService.GetCountOfGirls()
            };
            unitOfWork.Dispose();

            return View(viewModel);
        }

        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("girls")]
        [Authorize(Roles = "Girl")]
        public ActionResult GirlsPage()
        {
            var email = HttpContext.User.Identity.Name;
            var context = new SiteContext(Resources.ConnectionString);
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);
            var user = membershipService.GetUserByEmail(email);
            var viewModel = new GirlBoyUserViewModel
            {
                Email = user.Email,
                UserName = user.UserName,
                CountOfBoys = membershipService.GetCountOfBoys(),
                CountOfGirls = membershipService.GetCountOfGirls()
            };
            unitOfWork.Dispose();

            return View(viewModel);
        }
    }
}