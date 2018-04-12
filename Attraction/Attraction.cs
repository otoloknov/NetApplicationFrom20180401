namespace Attraction
{
    // abstract class Attraction using as a "template" for further Attration creation 
    abstract class Attraction
    {
        // Attraction Price 
        public int Price { get; }
        // Attraction duration 
        public int Duration { get; }
        // Attraction name. 
        // Currently could be set up 3 type of Attractions ( name is unique )
        public string Name { get; }
        // Static int variable for collecting money ( Kid pay for attraction )
        public static int CashBox {get ; set; }

        // Protected constructor for abstracted class Attraction.
        protected Attraction(string name, int price, int duration)
        {
            Price = price;
            Duration = duration;
            Name = name;
            CashBox = 0;
        }

        // "Tempalte" of method for all inheritence class. 
        // This method will make validation process for (Kid + Day) and return Bool value. 
        public abstract bool DoValidation(Kid kid, Days day);

        //Adding money to CashBox
        public void AddMoneyToCashBox()
        {
            CashBox += Price;
        }

        // Return static amount of CashBox
        public int CheckCashBox()
        {
            return CashBox;
        }
    }

    class Pony : Attraction
    {
        public Pony(string name, int price, int duration) : base(name, price, duration) { }

        public override bool DoValidation(Kid kid, Days day)
        {
            return Days.Sun != day && kid.Height > 65;
        }
    }

    class Swan : Attraction
    {
        public Swan(string name, int price, int duration) : base(name, price, duration) { }

        public override bool DoValidation(Kid kid, Days day)
        {
            return ((Days.Tue == day || Days.Thu == day || Days.Sat == day)
                    &&
                    (
                        (GenderType.F == kid.Gender && kid.Height > 120 && kid.Height < 140)
                        || (GenderType.M == kid.Gender && kid.Height < 190)
                     )
                    );
        }
    }

    class Batman : Attraction
    {
        public Batman(string name, int price, int duration) : base(name, price, duration)
        {
        }

        public override bool DoValidation(Kid kid, Days day)
        {
            return (//(Days.Mon == day || Days.Wed == day || Days.Fri == day)
                    //&& 
                    GenderType.M == kid.Gender
                    && kid.Height > 150);
        }
    }
}
