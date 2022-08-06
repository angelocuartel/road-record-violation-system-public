using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
   public interface IUserOptions
    {
        Task ChangePasswordAsync(string username, string newPassword);
        
    }
}
