using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kindergarten
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sr = new StreamReader(Console.OpenStandardInput()))
            using (Stream stdout = Console.OpenStandardOutput())
            {
                int bufSize = 1024;
                Stream inStream = Console.OpenStandardInput(bufSize);
                Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, bufSize));

                var lines = sr.ReadToEnd().Split('\n');
                string[] str = lines.ToArray();

                int n = Convert.ToInt32(str[0]);
                string[] strings = new string[n];

                for (int i = 0; i < n; i++)
                    strings[i] = str[i + 1];

                int[] ints = strings.Select(x => Convert.ToInt32(x)).ToArray();
                int[] candies = new int[n];

                for (int i = 0; i < ints.Length; i++)
                {
                    candies[i] = 1;
                    if (i > 0 && ints[i] > ints[i - 1]) candies[i] = candies[i - 1] + 1;
                    if (i > 0 && ints[i] == 2 && candies[i - 1] > 2) candies[i] = 2;
                }

                int sum = 0;

                candies.ToList().ForEach(x => sum += x);
                Console.WriteLine(sum);
            }
        }
    }
}
