using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
   public interface ILicenseDetailsRepository<T> : ICrudRepository<T> where T : class
    {
        bool LicenseExist(string license);

        int GetLicenseDbID(string license);

        T GetLicenseByViolatorId(int id);
    }
}
