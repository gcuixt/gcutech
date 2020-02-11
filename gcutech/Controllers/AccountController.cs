using gcutech.Models;
using gcutech.Service.Business;
using gcutech.Service.Exceptions;
using Microsoft.VisualBasic.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Log = Serilog.Log;

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

        public ViewResult AccountInfo()
        {
            User principal = (User)HttpContext.Session["principal"];
            return View(principal);
        }

        public ViewResult Update()
        {
            User principal = (User)HttpContext.Session["principal"];
            return View(principal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnRegister(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Register");
                }
                _accountService.RegisterUser(user);

                return View("~/Views/Home/Index.cshtml");
            }catch(Exception e)
            {
                ViewBag.Error = e.Message;
                Console.WriteLine(e.StackTrace);

                return View("Register");
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
            catch(Exception e)
            {
                ViewBag.Error = e.Message;
                Console.WriteLine(e.StackTrace);
                return RedirectToAction("Login");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult OnUpdate(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Update");
                }

                _accountService.ProcessUpdate(user);

                HttpContext.Session.Remove("principal");
                HttpContext.Session.Add("principal", user);

                return View("AccountInfo", user);
            }catch(Exception e)
            {
                ViewBag.Error = e.Message;
                Console.WriteLine(e.StackTrace);
                return View("Update");
            }

            
        }

        public ActionResult OnDelete()
        {
            User principal = (User)HttpContext.Session["principal"];

            try
            {
                _accountService.ProcessDelete(principal);
                HttpContext.Session.Clear();
                return View("~/Views/Home/Index.cshtml");
            }
            catch(Exception e)
            {
                ViewBag.Error = e.Message;
                Console.WriteLine(e.StackTrace);
                return View("AccountInfo", principal);
            }
        }

        public RedirectToRouteResult OnLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}