using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace LonelyInteger
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
                    int k = 0;
                    int l = 0;
                    
                    for(int i = 0; i < myInts.Length; i++)
                    {
                        bool found = false;
                        for (int j = 0; j < myInts.Length; j++) if (i != j && myInts[i] == myInts[j]) found = true;
                        if (!found) Console.WriteLine(myInts[i]);
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