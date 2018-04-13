using System;
using System.Collections.Generic;

namespace Polymorphysm
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();
            var edinorig = new Animal();
            // animal.Talk();
            animals.Add(edinorig);

            var kon = new Horse();
            // kon.Talk();
            animals.Add(kon);

            var sobaka = new Dog();
            //sobaka.Talk();
            animals.Add(sobaka);

            var buld = new Buldog();
            animals.Add(buld);

            MakeAnimalToTalk(animals);

            Console.ReadLine();
        }

        private static void MakeAnimalToTalk(IEnumerable<Animal> animals)
        {
            foreach (var animal in animals)
            {
                animal.Talk();
            }
        }
    }
}
