using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Widgets
{
    public class WidgetTable
    {
        /// <summary>
        /// takes array of strings and outputs rows
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="content"></param>
        /// <param name="footer"></param>
        public static void DisplayTable(List<string> headers, List<string> content, string footer) {
            /*
             *  Header
             *  
             *  Content
             *  1.
             *  2.
             *  3.
             *  4.
             *  5.
             *  
             *  Enter an input
             */
            foreach (var row in headers)
            {
                Console.WriteLine(row);
            }

            foreach (var row in content)
            {
                Console.WriteLine(row);
            }

            Console.WriteLine("");

            //if (String.IsNullOrEmpty(footer.Trim()))
            //{
            //    Console.WriteLine(footer);
            //}
            //else {
                Console.Write(footer);
            //}
            
        }
    }
}
