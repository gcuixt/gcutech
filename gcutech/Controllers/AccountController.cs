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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult OnRegister(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Register");
                }
                _accountService.RegisterUser(user);

                return RedirectToAction("Index", "Home");
            }catch(Exception e)
            {
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File("~/logs/logs.txt")
                    .CreateLogger();

                logger.Information(e.Message);

                //TODO MAKE SURE THIS ISN'T A 302 REDIRECT
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
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File("~/logs/logs.txt")
                    .CreateLogger();

                logger.Information(e.Message);
                return RedirectToAction("Login");
            }
            catch(RecordNotFoundException e)
            {
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File("~/logs/logs.txt")
                    .CreateLogger();

                logger.Information(e.Message);
                ViewBag.ErrorMessage = "User does not exist.";
                return RedirectToAction("Login");
            }
            catch(Exception e)
            {
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File("~/logs/logs.txt")
                    .CreateLogger();

                logger.Information(e.Message);
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