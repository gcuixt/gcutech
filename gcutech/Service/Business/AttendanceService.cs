using gcutech.Models;
using gcutech.Service.Data;
using gcutech.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gcutech.Service.Business
{
    public class AttendanceService : IAttendanceService
    {
        private ICrud<ChallengeCode> _challengeCodeData;
        private AttendanceData _attendanceData;

        public AttendanceService(ICrud<ChallengeCode> challengeCodeData)
        {
            this._challengeCodeData = challengeCodeData;
            this._attendanceData = new AttendanceData();
        }
        public void CheckIn(ChallengeCode code, User user)
        {
            string correctCode = this._challengeCodeData.ReadT(code)._code;

            if(code._code == correctCode)
            {
                if(this._attendanceData.ReadT(user)._userId != -1)
                {
                    throw new AlreadyCheckedInException("You have already been checked in.");
                }
                this._attendanceData.CreateT(user);
            }
            else
            {
                throw new IncorrectCodeException("The code you used was not correct for todays code.");
            }
        }

        public void DownloadAttendance(DateTime date)
        {
            var excApp = new Microsoft.Office.Interop.Excel.Application();

            excApp.Visible = true;

            excApp.Workbooks.Add();

            Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)excApp.ActiveSheet;


            worksheet.Cells[1, "A"] = "Full Name";
            worksheet.Cells[1, "B"] = "UserName";
            worksheet.Cells[1, "C"] = "Email";

            List<User> users = this._attendanceData.ReadAllT(date);

            var row = 1;
            foreach(var u in users)
            {
                row++;
                worksheet.Cells[row, "A"] = u._fullName;
                worksheet.Cells[row, "B"] = u._credentials._userName;
                worksheet.Cells[row, "C"] = u._email;
            }

            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();
            worksheet.Columns[3].AutoFit();
        }

        public void GenerateToken()
        {
            //Generate the token
            ChallengeCode challengeCode = new ChallengeCode();

            //Pass the token to be added to the database
            this._challengeCodeData.CreateT(challengeCode);
        }

        public List<User> GetAttendance(DateTime date)
        {
            return this._attendanceData.ReadAllT(date);
        }

        public string RecieveToken()
        {
            //Get today's date as a code model
            ChallengeCode challengeCode = new ChallengeCode();
            //Pass model to get today's token and return today's token.
            return this._challengeCodeData.ReadT(challengeCode)._code;
        }
    }
}