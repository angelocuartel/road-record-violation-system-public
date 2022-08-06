using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static RoadRecordViolationSystem.ViewModels.EmailViewModel;

namespace RoadRecordViolationSystem.Utils.Implementation
{
    public class ViolationEmailTemplate : IEmailTemplate
    {

        public string GetRazorPath()
        {
            return $"{Directory.GetCurrentDirectory()}/wwwroot/EmailTemplates/violation.cshtml";
        }
    }
}
