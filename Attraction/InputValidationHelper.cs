using System;
using System.Configuration;

namespace Attraction
{
    static class InputValidationHelper
    {
        //check is correct day was eneter
        private static bool IsInWeekRange(int value)
        {
            return value >= Convert.ToInt32(ConfigurationManager.AppSettings["MinDayOfWeek"]) 
                   && value <= Convert.ToInt32(ConfigurationManager.AppSettings["MaxDayOfWeek"]);
        }

        //checking if correct day was enetered. ( integer + in list of days )
        private static bool IsCorrectDayOfWeek(string value)
        {
            int outValue;
            return int.TryParse(value, out outValue) && IsInWeekRange(outValue);
        }

        // Checking if correct value for day was set up?
        // if yes -> return Day as type "Days"
        public static Days GetValidadedDayOfWeek()
        {
            Console.Write("Enter day of week (number): ");
            string inputValue = Console.ReadLine();

            while (!IsCorrectDayOfWeek(inputValue))
            {
                Console.Write("     Warning - Enter valid day of week: ");
                inputValue = Console.ReadLine();
            }
            // get correct day of week
            return (Days)int.Parse(inputValue);
        }

    }
}
