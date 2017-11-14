using System;

namespace Zinc.Extensions
{
    public static class TimeEx
    {
        //take in a number of minutes and convert it to hours and minutes
        public static string HumanizeMinutes(this int minutes)
        {
            string hoursOrMinutes = "minutes";
            if (minutes > 59)
            {
                hoursOrMinutes = "hours";
                //fix this, parse time properly
                minutes = minutes / 60;

            }
            //ex.
            return "2 hours 30 " + hoursOrMinutes;
        }
    }
}