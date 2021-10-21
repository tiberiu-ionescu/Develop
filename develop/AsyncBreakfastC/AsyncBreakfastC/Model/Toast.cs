using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBreakfastC.Model
{
    class Toast
    {
        public static int toastsMade = 0;

        public Toast()
        {
            toastsMade++;
        }
    }
}
