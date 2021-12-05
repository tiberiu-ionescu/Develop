using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace CountingSort1
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
                    int n = Convert.ToInt32(str[0]);
                    int[] myInts = str[1].Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                    int max = 0;
                    for (int i = 0; i < n; i++) if (myInts[i] > max) max = myInts[i];

                    int[] frequencies = new int[max];

                    myInts.ToList().ForEach(x => { frequencies[x - 1] += 1; });
                    frequencies.ToList().ForEach(x => { Console.Write(x.ToString() + " "); });
                }
                catch
                {
                    Console.WriteLine("Nu ati introdus numere!");
                }
            }
        }
    }
}