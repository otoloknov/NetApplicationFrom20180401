using System;

namespace Attraction
{
    // List of days
    enum Days : byte { Mon = 1, Tue, Wed, Thu, Fri, Sat, Sun }
    // Set gender type ( M = male, F = female )
    enum GenderType : byte { M, F}

    public static class Program
    {
        static void Main()
        {
            // method for setting day of week from console input.
            var day = ConsoleInputValidation.GetValidadedDayOfWeek();

            // Initializing (generating) random parameters for 10 Kids.
            var groupOfRandomKids = new RandomKidGenerator();

            // Initializing (generating) random parameters for attractions (Pony, Swan, Batman)
            var groupOfRandomAttractions = new RandomAttractionGenerator();

            //Create new Attraction Manager with already generated(prepared) data for Kids and Attractions
            var generalAttractionManager = new AttractionManager(groupOfRandomKids, groupOfRandomAttractions, day);
            // open attraction
            generalAttractionManager.ToRide();

            Console.ReadLine();
        }
    }
}
