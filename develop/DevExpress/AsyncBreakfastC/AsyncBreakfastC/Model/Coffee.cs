using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBreakfastC.Model
{
    class Coffee
    {
        public static int coffeesPoured = 0;

        public Coffee()
        {
            coffeesPoured++;
        }
    }
}