using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RecursiveDigitSum
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
                    int k = Convert.ToInt32(str[1]);
                    UInt64 p = 0;
                    string s= "";

                    for (int i = 0; i < k; i++)
                        s += n.ToString();

                    p = Convert.ToUInt64(s);

                    Console.WriteLine(SuperDigit(p).ToString());
                }
                catch
                {
                    Console.WriteLine("Nu ati introdus numere!");
                }
            }
        }

        public static UInt64 SuperDigit(UInt64 i)
        {
            if (Math.Floor(Math.Log10(i) + 1) == 1) return i;
            else
            {
                UInt64 sum = 0;
                UInt64 n = i;
                while (n != 0)
                {
                    sum += n % 10;
                    n /= 10;
                }

                return SuperDigit(sum);
            }
        }
    }
}
