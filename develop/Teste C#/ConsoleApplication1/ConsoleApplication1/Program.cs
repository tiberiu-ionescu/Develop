using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GridChallenge
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
                    n[0] = Convert.ToInt32(str[1]);
                    int cursor = 1;

                    for (int z = 0; z < t; z++)
                    {
                        if (z > 0) 
                        {
                            cursor += n[z - 1] + 1;
                            n[z] += Convert.ToInt32(str[cursor]);
                        }

                        string[] strings = new string[n[z]];
                        byte[][] ba = new byte[n[z]][];

                        for (int i = 2; i <= n[z] + 1; i++)
                        {
                            strings[i - 2] = str[i];
                            ba[i - 2] = Encoding.ASCII.GetBytes(strings[i - 2].ToString());
                        }

                        for (int i = 0; i < n[z]; i++)
                        {
                            Array.Sort(ba[i], StringComparer.InvariantCulture);
                            ba[i] = ba[i].Skip(1).ToArray();
                        }

                        bool result = CheckColumns(ba, n[z]);
                        Console.WriteLine(result ? "YES" : "NO");
                    }
                }
                catch
                {
                    Console.WriteLine("Ceva nu a mers!");
                }
            }
        }

        public static bool CheckColumns(byte[][] arr, int dim)
        {
            for (int k = 0; k < dim; k++)
                for (int i = 0; i < dim - 1; i++)
                    for (int j = i + 1; j < dim; j++)
                        if (arr[i][k] > arr[j][k]) return false;
            return true;
        }
    }
}
