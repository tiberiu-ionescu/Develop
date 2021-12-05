using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace PlusMinus
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sr = new StreamReader(Console.OpenStandardInput()))
            using (Stream stdout = Console.OpenStandardOutput())
            {
                var lines = sr.ReadToEnd().Split('\n');
                string[] str = lines.ToArray();
                try
                {
                    int[] myInts = str[1].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    int positives = 0, negatives = 0, zeros = 0;

                    for (int i = 0; i < myInts.Length; i++)
                    {
                        if (myInts[i] < 0) negatives++;
                        if (myInts[i] > 0) positives++;
                        if (myInts[i] == 0) zeros++;
                    }
                    if (myInts.Length - 1 > 0)
                    {
                        Console.WriteLine("{0:N6}", (double)positives / (double)(myInts.Length));
                        Console.WriteLine("{0:N6}", (double)negatives / (double)(myInts.Length));
                        Console.WriteLine("{0:N6}", (double)zeros / (double)(myInts.Length));
                    }
                }
                catch
                {
                    Console.WriteLine("Nu ati introdus numere!");
                }
            }
        }
    }
}