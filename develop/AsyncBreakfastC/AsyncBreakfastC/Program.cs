using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncBreakfastC.Model;
using NUnit.Framework;
using System.Runtime.CompilerServices;

namespace AsyncBreakfastC
{
    class Program
    {
        public const int coffeeToPour = 1;
        public const int eggsToBoil = 2;
        public const int baconToFry = 3;
        public const int toastToMake = 2;

        public static List<Coffee> coffees = new List<Coffee>();
        public static List<Egg> eggs = new List<Egg>();
        public static List<Bacon> bacon = new List<Bacon>();
        public static List<Toast> toasts = new List<Toast>();

        static async Task Main(string[] args)
        {
            Coffee cup = PourCoffee(coffeeToPour);
            Console.WriteLine("coffee is ready");

            var eggsTask = BoilEggsAsync(eggsToBoil);
            var baconTask = FryBaconAsync(baconToFry);
            var toastTask = MakeToastWithButterAndJamAsync(toastToMake);

            var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (finishedTask == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Console.WriteLine("toast is ready");
                }
                breakfastTasks.Remove(finishedTask);
            }

            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
            
        }

        static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Toast t = new Toast();
                toasts.Add(t);
                Console.WriteLine("Putting a slice of bread in the toaster");
                Console.WriteLine("Start toasting...");
            }
            
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return toasts[0];
        }

        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Bacon b = new Bacon();
                bacon.Add(b);
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return bacon[0];
        }

        private static async Task<Egg> BoilEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);

            for (int egg = 0; egg < howMany; egg++)
            {
                Egg e = new Egg();
                eggs.Add(e);
                Console.WriteLine("cracking one egg");
            }

            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return eggs[0];
        }

        private static Coffee PourCoffee(int howMany)
        {
            for (int coffee = 0; coffee < howMany; coffee++)
            {
                Coffee c = new Coffee();
                coffees.Add(c);
                Console.WriteLine("Pouring coffee");
            }
            return coffees[0];
        }
    }
}