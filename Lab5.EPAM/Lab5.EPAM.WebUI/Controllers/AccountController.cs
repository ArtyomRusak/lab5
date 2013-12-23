using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AttributeRouting.Web.Mvc;
using Lab5.EPAM.EFData;
using Lab5.EPAM.EFData.EFContext;
using Lab5.EPAM.Services.Exceptions;
using Lab5.EPAM.Services.Services;
using Lab5.EPAM.WebUI.Models;
using Microsoft.Ajax.Utilities;

namespace Lab5.EPAM.WebUI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("register")]
        public ActionResult Register()
        {
            var model = new RegisterUserViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var context = new SiteContext(Resources.ConnectionString);
                var unitOfWork = new UnitOfWork(context);
                var membershipService = new MembershipService(unitOfWork, unitOfWork);
                try
                {
                    var user = membershipService.RegisterUser(model.UserName, model.Password, model.Email, model.Male);
                    unitOfWork.Dispose();
                    return RedirectToAction("Login");
                }
                catch (MembershipServiceException e)
                {
                    if (e.Message == "User exist.")
                    {
                        ModelState.AddModelError("Exist", e.Message);
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", Resources.EmailExist);
                        return View(model);
                    }
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("login")]
        public ActionResult Login()
        {
            LoginUserViewModel model = new LoginUserViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var context = new SiteContext(Resources.ConnectionString);
                    var unitOfWork = new UnitOfWork(context);
                    var membershipService = new MembershipService(unitOfWork, unitOfWork);
                    var loginUser = membershipService.LoginUser(model.Email, model.Password);
                    if (loginUser == null)
                    {
                        ModelState.AddModelError("NullUser", Resources.NullUser);
                        return View(model);
                    }

                    unitOfWork.Dispose();

                    var ticket = new FormsAuthenticationTicket(3, loginUser.Email, DateTime.Now,
                        DateTime.Now.AddMinutes(20), model.RememberMe, "");
                    var cookieString = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieString);
                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");

                    //var url = Request.QueryString["returnUrl"];
                    //Response.Redirect(url);
                }
                catch (MembershipServiceException e)
                {
                    if (e.Message == "Wrong password.")
                    {
                        ModelState.AddModelError("Password", Resources.WrongPassword);
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("Ex", e.Message);
                        return View(model);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Unknown exception");
                return View(model);
            }
        }

        public ActionResult LogOff()
        {
            var context = new SiteContext(Resources.ConnectionString);
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);
            var user = membershipService.GetUserByEmail(User.Identity.Name);
            FormsAuthentication.SignOut();
            unitOfWork.Dispose();

            return RedirectToAction("Login", "Account");
        }
    }
}