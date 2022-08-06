using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Utils.Implementation
{
    public static class TicketUtil
    {
        public static string GenerateNo()
        {
            var nums = new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var random = new Random();
            var no = new StringBuilder();

            for(int i =0; i < 9; i++)
            {
                no.Append(nums[random.Next(0, 9)]);
            }

            return no.ToString();
        }
    }
}
