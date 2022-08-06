using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Utils.Implementation
{
    public static class QrHash
    {
        public static string Generate()
        {
            var hash = new StringBuilder();
            var random = new Random();

            for(int i = 0; i < 35; i++)
            {
                hash.Append((char)random.Next(65, 90));
            }

            return hash.ToString();
        }
    }
}
