using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphysm
{
    class Animal
    {
        public string Name { get; set; }
        
        public virtual void Talk()
        {
            Console.WriteLine("I am an animal");
        }
    }
}
