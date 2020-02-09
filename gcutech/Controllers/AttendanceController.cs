﻿using gcutech.Models;
using gcutech.Service.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gcutech.Controllers
{
    public class AttendanceController : Controller
    { 

        private IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            this._attendanceService = attendanceService;
        }

        [HttpGet]
        public ActionResult CreateToken()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateToken()
        {
            try
            {
                this._attendanceService.GenerateToken();
                return View("~/Views/Home/Index.cshtml");
            }
            catch(Exception e)
            {
                return View("CreateToken");
            }
            
        }

        [HttpGet]
        public ActionResult CheckIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn(ChallengeCode code)
        {
            try
            {
                User principal = (User)HttpContext.Session["principal"];
                this._attendanceService.CheckIn(code, principal);
                return View();
            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return View();
            }
        }
    }
}