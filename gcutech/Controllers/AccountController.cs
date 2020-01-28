using gcutech.Models;
using gcutech.Service.Business;
using gcutech.Service.Exceptions;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gcutech.Controllers
{
    public class AccountController : Controller
    {
        private IAccountBusiness<User> _accountService;

        public AccountController(IAccountBusiness<User> accountService)
        {
            this._accountService = accountService;
        }
        public ViewResult Login()
        {
            return View();
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult OnRegister(User user)
        {
            try
            {
                _accountService.RegisterUser(user);

                return RedirectToAction("Index", "Home");
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Register");
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult OnLogin(Credentials credentials)
        {
            try
            {
                User principal = _accountService.AuthenticateUser(credentials);

                HttpContext.Session.Add("principal", principal);
                HttpContext.Session.Add("isLoggedIn", true);

                return RedirectToAction("Index", "Home");
            }
            catch (LoginFailedException e)
            {
                Console.WriteLine(e.Message);
                ViewBag.ErrorMessage = "Your password was incorrect.";
                return RedirectToAction("Login");
            }
            catch(RecordNotFoundException e)
            {
                ViewBag.ErrorMessage = "User does not exist.";
                return RedirectToAction("Login");
            }
            catch(Exception e)
            {
                return RedirectToAction("Login");
            }

        }


        public ViewResult AccountInfo()
        {
            User principal = (User) HttpContext.Session["principal"];
            return View(principal);
        }
        public RedirectToRouteResult OnLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}