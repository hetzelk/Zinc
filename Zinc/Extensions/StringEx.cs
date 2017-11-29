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
            return str.ToLower().Equals("true");
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
    }
}