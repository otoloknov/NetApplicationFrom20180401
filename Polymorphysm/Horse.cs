using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphysm
{
    class Horse : Animal
    {
        public override void Talk()
        {
            Console.WriteLine("I - go - go. I am a horse");
        }
    }
}
