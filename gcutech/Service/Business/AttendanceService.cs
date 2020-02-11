using gcutech.Models;
using gcutech.Service.Data;
using gcutech.Service.Exceptions;
using System;
using System.Collections.Generic;

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

        public List<User> DownloadAttendance(DateTime date)
        {
            return this._attendanceData.ReadAllT(date);
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