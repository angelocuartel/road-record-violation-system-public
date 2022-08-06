using RoadRecordViolationSystem.Utils.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Utils.Interface
{
 public interface IEmail 
    {
        Task SendWithTemplateAsync<T>(string to, string subject, T model, IEmailTemplate template);
        void Send<T>(string to, string subject, T model);
    }
}
