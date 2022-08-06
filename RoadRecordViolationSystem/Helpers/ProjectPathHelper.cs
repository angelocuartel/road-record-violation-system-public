using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Helpers
{
    public static class ProjectPathHelper
    {
        public static string ToImageContentPath(this string path)
        {
            return $"~/Images/{path.Substring(path.LastIndexOf("/") + 1)}";
        }
    }
}
