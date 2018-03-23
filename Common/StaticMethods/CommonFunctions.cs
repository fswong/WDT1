using System;
using System.Collections.Generic;
using System.Text;

namespace Common.StaticMethods
{
    public class CommonFunctions
    {
        public static bool TryParseInt (string integer, out int parsedInteger){
            return Int32.TryParse(integer, out parsedInteger);
        }
    }
}
