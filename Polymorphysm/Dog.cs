using System;


namespace Polymorphysm
{
    class Dog : Animal
    {
        public override void Talk()
        {
            Console.WriteLine("Gav - Gav. I am a dog");
        }
    }

    class Buldog : Dog
    {
        public override  void Talk()
        {
            Console.WriteLine("Im buldog");
        }
    }

}
