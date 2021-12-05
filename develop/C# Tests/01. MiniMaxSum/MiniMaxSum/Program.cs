using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MiniMaxSum
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Stream stdin = Console.OpenStandardInput())
            using (Stream stdout = Console.OpenStandardOutput())
            {
                byte[] buffer = new byte[2048];
                int bytes;
                while ((bytes = stdin.Read(buffer, 0, buffer.Length)) > 0)
                {
                    var str = System.Text.Encoding.Default.GetString(buffer);
                    string[] tokens = str.Split(' ');
                    try
                    {
                        Int64[] myInts = tokens.Select(Int64.Parse).ToArray();

                        Array.Sort(myInts);
                        Int64 minim = 0, maxim = 0;

                        for (int i = 0; i < 4; i++) minim += myInts[i];
                        for (int i = myInts.Length - 1; i >= myInts.Length - 4; i--) maxim += myInts[i];
                        string s = minim.ToString() + ' ' + maxim.ToString();
                        Console.WriteLine(s);
                    }
                    catch
                    {
                        Console.WriteLine("Nu ati introdus numere!");
                    }
                }
            }
        }
    }
}
