using System;

namespace Zinc.Extensions
{
    public static class StringEx
    {
        public static bool hasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool ToBool(this string str)
        {
            return str.ToLower() == "true" || str.ToLower() == "1";
        }

        public static bool VerifyPhoneNumber(this string str)
        {
            /*
             is str like
             14145555555
             1-414-555-5555
             1(414)555-5555
             4145555555

            create an intuitive way to determine whether it's a number or not
             */
            return false;
        }

        public static Int16 ToInt16(this string str)
        {
            return Int16.Parse(str);
        }

        public static Int32 ToInt32(this string str)
        {
            return Int32.Parse(str);
        }

        public static Int64 ToInt64(this string str)
        {
            return Int64.Parse(str);
        }
    }
}