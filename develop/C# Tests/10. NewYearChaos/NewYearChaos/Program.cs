using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NewYearChaos
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
                try
                {
                    int t = Convert.ToInt32(str[0]);
                    int[] n = new int[t];
                    int cursor = 0;

                    for (int z = 1; z <= t; z++)
                    {
                        n = str[2*z].Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

                        Console.WriteLine(CheckBribes(n, 0));
                    }
                }
                catch
                {
                    Console.WriteLine("Ceva nu a mers!");
                }
            }
        }

        public static string CheckBribes(int[] arr, int number)
        {
            int[] sort = new int[arr.Length];

            arr.CopyTo(sort, 0);
            Array.Sort(sort);
            int sum = number;
            int dim = 0;
            int item = 0;
            int index = 0;

            for (int i = sort.Length - 1; i >= 0; i--)
            {
                if (arr[i] != sort[i])
                {
                    item = sort[i];
                    if (Math.Abs(arr.ToList().IndexOf(item) - sort.ToList().IndexOf(item)) > 2) return "Too chaotic";
                    else
                    {
                        dim = Math.Abs(arr.ToList().IndexOf(item) - sort.ToList().IndexOf(item));
                        sum += dim;
                        int[] aux = new int[dim];
                        index = arr.ToList().IndexOf(item);
                        for (int j = 0; j < dim; j++) aux[j] = arr[index + j + 1];
                        arr[i] = sort[i];
                        for (int j = 0; j < dim; j++) arr[index + j] = aux[j];
                        return CheckBribes(arr, sum);
                    }
                }
            }

            return sum.ToString();
        }
    }
}
