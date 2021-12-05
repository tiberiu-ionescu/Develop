using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace TimeConversion
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
                    try
                    {
                        DateTime d = Convert.ToDateTime(str, CultureInfo.InvariantCulture);
                        Console.WriteLine(d.ToString("HH:mm:ss"));
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
