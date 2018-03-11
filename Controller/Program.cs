using Common.Enum;
using Repository;
using System;

namespace Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var Driver = new Driver();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally {
                Console.WriteLine("Thank you for using the system");
            }
        }

        
    }
}
