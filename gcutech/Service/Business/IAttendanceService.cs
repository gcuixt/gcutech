using gcutech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gcutech.Service.Business
{
    public interface IAttendanceService
    {
        void GenerateToken();
        string RecieveToken();
        void CheckIn(ChallengeCode code, User user);
        List<User> GetAttendance(DateTime date);
        void DownloadAttendance(DateTime date);

    }
}
