using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AsyncBreakfastC.Model;
using Moq;

namespace AsyncBreakfastC
{
    [TestFixture]
    public class TestAsync
    {
        [TestCase]
        public static void CheckCoffees(int coffeeToPour)
        {
            try { Assert.AreEqual(coffeeToPour, Coffee.coffeesPoured); }
            catch (AssertionException ex) { Console.Write(ex.Message); }
        }

        [TestCase]
        public static void CheckEggs(int eggsToBoil)
        {
            try { Assert.AreEqual(eggsToBoil, Egg.eggsBoiled); }
            catch (AssertionException ex) { Console.Write(ex.Message); }
        }

        [TestCase]
        public static void CheckBacon(int baconToFry)
        {
            try { Assert.AreEqual(baconToFry, Bacon.baconFried); }
            catch (AssertionException ex) { Console.Write(ex.Message); }
        }

        [TestCase]
        public static void CheckToasts(int toastsToMake)
        {
            try { Assert.AreEqual(toastsToMake, Toast.toastsMade); }
            catch (AssertionException ex) { Console.Write(ex.Message); }
        }
    }
}
