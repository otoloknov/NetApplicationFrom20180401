using System;
using System.Collections.Generic;

namespace Attraction
{
    // use only for internal Attraction generation 
    class RandomAttractionGenerator
    {
        private readonly List<Attraction> _attraction = new List<Attraction>();
        public RandomAttractionGenerator()
        {
            var rand = new Random();
            _attraction.Add(new Pony("Pony Strong",rand.Next(15, 20),rand.Next(6, 10)));
            _attraction.Add(new Pony("Pony Low", rand.Next(10, 15), rand.Next(2, 6)));
            _attraction.Add(new Batman("Batman Strong", rand.Next(15, 20), rand.Next(6, 10)));
            _attraction.Add(new Batman("Batman Low", rand.Next(10, 15), rand.Next(2, 6)));
            _attraction.Add(new Swan("Swan Strong", rand.Next(15, 20), rand.Next(6, 10)));
            _attraction.Add(new Swan("Swan Low", rand.Next(10, 15), rand.Next(2, 6)));
        }

        public List<Attraction> GetListOfGeneratedAttractions()
        {
            return _attraction;
        }
    }
}
