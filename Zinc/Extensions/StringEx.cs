using System;

namespace Zinc.Extensions
{
    public static class StringEx
    {
        public static bool hasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}