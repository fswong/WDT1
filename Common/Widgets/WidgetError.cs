using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Widgets
{
    public class WidgetError
    {
        /// <summary>
        /// Generic display message
        /// </summary>
        /// <param name="error"></param>
        public static void DisplayError(string error) {
            try {
                Console.WriteLine(" ");
                Console.WriteLine(error);
                Console.WriteLine(" ");
            } catch (Exception) {
                // really there should be no errors that happen here
                Console.WriteLine(" ");
                Console.WriteLine("An unexpected error has occured");
                Console.WriteLine(" ");
            }
        }
    }
}
