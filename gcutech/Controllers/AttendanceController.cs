using ClosedXML.Excel;
using gcutech.Models;
using gcutech.Service.Business;
using System;
using System.Collections.Generic;
using System.IO;
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
                ViewBag.Error = e.Message;
                Console.WriteLine(e.StackTrace);
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
                if (!ModelState.IsValid)
                {
                    return View();
                }
                User principal = (User)HttpContext.Session["principal"];
                this._attendanceService.CheckIn(code, principal);
                return View("~/Views/Home/Index.cshtml");
            }catch(Exception e)
            {
                ViewBag.Error = e.Message;
                Console.WriteLine(e.StackTrace);
                return View();
            }
        }

        [HttpGet]
        public ActionResult ShowToken()
        {
            try
            {
                ViewBag.Token = this._attendanceService.RecieveToken();
                return View();
            }catch(Exception e)
            {
                ViewBag.Error = e.Message;
                Console.WriteLine(e.StackTrace);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Attendance()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult getAttendance(ChallengeCode code)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Attendance");
                }
                ViewData["attendance"] = this._attendanceService.GetAttendance(code._date);
                return View("Attendance");
            }catch(Exception e)
            {
                ViewBag.Error = e.Message;
                Console.WriteLine(e.StackTrace);
                return View("Attendance");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult downloadAttendance(ChallengeCode code)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Attendance");
                }
                List<User> attendance = this._attendanceService.DownloadAttendance(code._date);
                XLWorkbook workbook = new XLWorkbook();
                IXLWorksheet worksheet = workbook.Worksheets.Add("pinetech");
                worksheet.Cell(1, 1).SetValue("Full Name");
                worksheet.Cell(1, 2).SetValue("User Name");
                worksheet.Cell(1, 3).SetValue("Email");

                int i = 1;
                foreach(var u in attendance)
                {
                    i++;
                    worksheet.Cell(i, 1).SetValue(u._fullName);
                    worksheet.Cell(i, 2).SetValue(u._credentials._userName);
                    worksheet.Cell(i, 3).SetValue(u._email);
                }

                MemoryStream ms = new MemoryStream();
                workbook.SaveAs(ms);
                ms.Position = 0;

                return new FileStreamResult(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                { FileDownloadName = "Attendance.xlsx" };
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                Console.WriteLine(e.StackTrace);
                return View("Attendance");
            }
        }
    }
}