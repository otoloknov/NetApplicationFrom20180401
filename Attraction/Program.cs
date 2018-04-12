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
            var day = InputValidationHelper.GetValidadedDayOfWeek();

            // Initializing (generating) random parameters for 10 Kids.
            KidInitializing groupKidInitializing = new KidInitializing();

            // Initializing (generating) random parameters for attractions (Pony, Swan, Batman)
            AttractionInitialization openAttractions = new AttractionInitialization();

            //Create new Attraction Manager with already generated(prepared) data for Kids and Attractions
            AttractionManager openAttractionManager = new AttractionManager(groupKidInitializing, openAttractions, day);
            // open attraction
            openAttractionManager.ToRide();

            Console.ReadLine();
        }
    }
}
