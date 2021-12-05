using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBreakfastC.Model
{
    class Bacon
    {
        public static int baconFried = 0;

        public Bacon()
        {
            baconFried++;
        }
    }
}
