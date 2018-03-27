using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.StaticMethods
{
    public class CommonFunctions
    {
        public static bool TryParseInt (string integer, out int parsedInteger){
            //trim the sides
            return Int32.TryParse(integer.Trim(), out parsedInteger);
        }

        //public static bool TryParseUser(string integer, out UserType parsedUser) {
        //    int parsedInteger;
        //    if (Enum.TryParse<UserType>(integer, true, out parsedInteger))
        //    {

        //    }
        //    else {

        //    }
        //}
    }
}
