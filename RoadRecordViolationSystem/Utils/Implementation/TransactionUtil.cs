using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Utils.Implementation
{
    public static class TransactionUtil
    {
        public static decimal CalculateAdditionalChargeByDays(int countOfDaysPassed, decimal charge, decimal addtionalPercent)
        {
            return countOfDaysPassed * (charge * addtionalPercent);
        }

        public static int CalculateExpirationDay(DateTime dateStart)
        {
            if (dateStart.DayOfWeek == 0)
                return 0;
            else if ((int)dateStart.DayOfWeek == 6)
                return 6;
            else
                return 7;
        }

        public static int CountValidAdditionalCharge(DateTime commencingDateForAdditional)
        {
            int dayOfAdditional = (int)(DateTime.UtcNow - commencingDateForAdditional).TotalDays;
            int totalValidDays = 0;
            int dayOfWeek = (int)commencingDateForAdditional.DayOfWeek;



            for (int i = 0; i <= dayOfAdditional; i++)
            {
                if (dayOfWeek != 0 && dayOfWeek != 6)
                {
                    totalValidDays++;
                }


                if (dayOfWeek == 6)
                {
                    dayOfWeek = 0;
                }
                else
                {
                    dayOfWeek++;
                }


            }

            return totalValidDays;


        }
    }
}
