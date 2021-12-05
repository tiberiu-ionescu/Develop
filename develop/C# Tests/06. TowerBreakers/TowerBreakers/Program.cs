using System.IO;
using System.Linq;
using System.Text;
using System;

namespace TowerBreakers
{
    class Program
    {
        public static void Main(string[] args)
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
                    int m = Convert.ToInt32(str[0]);
                    int[][] myInts = new int[m][];

                    for (int i = 1; i <= m; i++) myInts[i - 1] = str[i].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    int[] winners = new int[m];

                    for (int i = 0; i < m; i++) 
                    {
                        winners[i] = Play(myInts[i], 1);
                        Console.WriteLine(winners[i] + " ");
                    }
                }
                catch
                {
                    Console.WriteLine("Nu ati introdus numere!");
                }
            }
        }

        public static int Play(int[] array, int player)
        {
            int next = player == 1 ? 2 : 1;
            
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != 1)
                {
                    if (array[i] % 2 == 0) array[i] /= 2;
                    else array[i] -= array[i] - 1; 
                    return Play(array, next);
                }
            }

            return next;
        }
    }
}
