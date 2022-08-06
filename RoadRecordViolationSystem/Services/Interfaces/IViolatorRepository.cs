using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
  public  interface  IViolatorRepository<T>  : ICrudRepository<T> where T : class
    {
        bool NameExist(string fullName);

        int GetDbIdByName(string fullname);
    }
}
