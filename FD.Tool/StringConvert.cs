using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Tool
{
    public static class StringConvert
    {
        public static bool StringConverter(this string value)
        {
            return !string.IsNullOrEmpty(value) ? Convert.ToBoolean(value) : false;
        }
    }
}
