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

        public static string ToZincTime(this string datetime)
        {
            return DateTime.Parse(datetime).ToUniversalTime().ToString("o");
        }

        public static DateTime ToDateTime(this string datetime)
        {
            return DateTime.Parse(datetime);
        }

        public static int GetDateTimeHours(this string datetime)
        {
            return DateTime.Parse(datetime).Hour;
        }

        public static string GenerateUuid(this string new_date)
        {
            DateTime date = DateTime.Parse(new_date);
            string uuid = date.Year + "" + date.Month + "" + date.Day + new Random().Next(0, 99999);
            return uuid;
        }
    }
}