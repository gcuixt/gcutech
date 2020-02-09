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
        private ICrud<User> _attendanceData;

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
                this._attendanceData.CreateT(user);
                
            }
            else
            {
                throw new IncorrectCodeException("The code you used was not correct for todays code.");
            }
        }

        public void DownloadAttendance(DateTime date)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public string RecieveToken()
        {
            throw new NotImplementedException();
        }
    }
}