using System;
using System.Collections.Generic;
using System.Threading;

namespace Attraction
{
    // AttractionManager - main options:
    // 1. Validation rules for kid on atttraction 
    //    How it works: private method GetValidAttractionForKid will return only 1 object with Type Attraction (or null) that was RANDOM set 
    //    to kid. 
    // E.G. for Kid = Shanita (Random set to Pony Low) and Levi - no attraction for Levi because of Attraction rules/conditions (height + day, etc.)
    // --******************************************** 
    //  Pony Strong => Shanita 10 F 85
    //  Pony Low => Shanita 10 F 85
    // - - - - List of Attraction per Kid = > 2
    // Attraction => Pony Low for KID + Shanita
    // --******************************************** 
    // - - - - List of Attraction per Kid = > 0
    //     !!!!! No Attraction for Levi
    // --******************************************** 
    class AttractionManager
    {
        private KidInitializing LisfOfKids { get; }
        private AttractionInitialization ListOfOpenAttractions { get; }
        private Days WeekDay { get; }
        // Public constant for max value of cash box
        public const int MAX_VALUE_FOR_CASH_BOX = 300;

        // Public constructor Attraction manager with input paramteres as Kids + Attractions + Day
        public AttractionManager(KidInitializing lisfOfKids, AttractionInitialization listOfOpenAttractions, Days day)
        {
            LisfOfKids = lisfOfKids;
            ListOfOpenAttractions = listOfOpenAttractions;
            WeekDay = day;
        }

        //Get random valid attraction for Kid based on Attraction conditions
        // return null or 1 Attraction 
        private Attraction GetRandomValidAttractionForKid(Kid kid)
        {
            // using for store ALL available attraction ( based on rules/ condition ) related to current Kid.
            List<Attraction> listOfAvailableAttractionForKid = new List<Attraction>();
            Random rand = new Random();

            kid.SetKidAction("Trying to get list of avaiable Attractions");
            
            // Try to find available attractions
            foreach (var attraction in ListOfOpenAttractions.GetListOfRunningAttractions())
            {
                // check is attraction avaivalbe for kid based in his/her parameters + day of week
                if (attraction.DoValidation(kid, WeekDay))
                {
                    // If attraction available - add to list
                    listOfAvailableAttractionForKid.Add(attraction);
                }
            }
            // return null Or Random attraction.
            // null - if there no available attraction for Kid.
            // Random attraction calculated as Random LIST index for all available attractions for 1 kid.
            //    List index started from 0, thats why total count of available attractions should be decreased on 1 for count > 1.            
            return listOfAvailableAttractionForKid.Count <= 0?
                null:
                listOfAvailableAttractionForKid[rand.Next(0, listOfAvailableAttractionForKid.Count > 1 ? 
                                                                listOfAvailableAttractionForKid.Count : 
                                                                listOfAvailableAttractionForKid.Count - 1)];
        }

        // Method RidingOnAttractions use for riding emulation.
        // Method will call from ToRide() in separete thread based on logic from ThreadPool.QueueUserWorkItem
        private void RidingOnAttractions(Kid kids, Attraction currentAttraction)
        {
            Random rand = new Random();

            // Set Action for kid with attraction name and durations
            kids.SetKidAction("I'm staring to ride on => " + currentAttraction.Name + " for "+ currentAttraction.Duration +" sec");
            kids.SetIsBusyStatus(true);
            // Adding money to Global cash box
            currentAttraction.AddMoneyToCashBox();
            //Increase Kid satisfaction degree
            kids.ChangeSatisfactionDegree(rand.Next(10, 20));
            //Attraction payment 
            kids.DeCreaseMoney(currentAttraction.Price);
            // Riding emulation by sleepinn on attraction duration x1000 (as Seconds)
            Thread.Sleep(currentAttraction.Duration*1000);
            // Then attraction finished - update status to Finished + free
            kids.SetKidAction("I've FINISHED to ride on => " + currentAttraction.Name);
            kids.SetIsBusyStatus(false);
        }

        // Implemented method - toRide
        // Kid go to Ride on Attraction. Next action should be:
        //  1. - kid money ( price of attraction )
        //  2. + Satisfaction Level for kid
        //  3. + money in CashBox for AttractionManager 
        //  4. if no available attraction - run method Kid.ToCry => + height + Delay??
        public void ToRide()
        {
            int cashAmout = 0;
            //create separete thread for displaying kids status
            ThreadPool.QueueUserWorkItem(LisfOfKids.PrintInformationAboutKids);
            // attraction will work until CashBox < MAX_VALUE_FOR_CASH_BOX 
            do
            {
                // Attraction manager will check all kids in Queue one by one.
                // added emulation of Manager work = sleep 1000
                foreach (var kid in LisfOfKids.GetListOfKids())
                {
                    // if kid is still busy with another avtivity like Cry, Ride, etc. process will skip him
                    if (!kid.GetIsBusyStatus())
                    {
                        // get random attraction from all available for kid 
                        var attractionToRide = GetRandomValidAttractionForKid(kid);

                        if (attractionToRide == null)
                        {
                            // if no available attraction for Kid - use method to Cry. 
                            ThreadPool.QueueUserWorkItem(kid.ToCry);
                        }
                        else
                        {
                            // If Kid don't have enough money for attraction - marking action = No Money
                            if (kid.GetCurrentMoneyAmount() < attractionToRide.Price || !kid.IsEnoughMoneyForAttractions())
                            {
                                kid.SetKidAction("No Money for Attractions!");
                                kid.SetNoMoneyFlag(true);
                            }
                            else
                            {
                                // all is fine = we could ride.
                                ThreadPool.QueueUserWorkItem(o => RidingOnAttractions(kid,attractionToRide));
                            }
                        }
                    }
                    // Manager work imitation
                    Thread.Sleep(1000);

                    // get total amount of CashBox
                    cashAmout = (int) typeof(Attraction).GetProperty("CashBox")?.GetValue(null, null);

                    // Stop attractions when total money in cash box will be more than MAX_VALUE_FOR_CASH_BOX
                    if (cashAmout < MAX_VALUE_FOR_CASH_BOX) continue;
                    Console.Clear();
                    Console.WriteLine("- - - Total CashBox value " + cashAmout);
                    Console.WriteLine("- - - Stopping attractions - - - ");
                    break;
                }
            } while (cashAmout < MAX_VALUE_FOR_CASH_BOX);
        }
    }
}
