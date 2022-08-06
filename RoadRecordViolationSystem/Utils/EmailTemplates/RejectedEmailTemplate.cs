using RoadRecordViolationSystem.Utils.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Utils.EmailTemplates
{
    public class RejectedEmailTemplate : IEmailTemplate
    {
        public string GetRazorPath()
        {
            return $"{Directory.GetCurrentDirectory()}/wwwroot/EmailTemplates/rejected.cshtml";
        }
    }
}
