using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace Hosts
{
    class Program
    {
        static void Main(string[] args)
        {
            var filestream = new System.IO.FileStream(@"C:\Hosts_access_log_00.txt",
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            string s;
            string[] t;
            while ((s = file.ReadLine()) != null)
            {
                t = s.Split(' ').ToArray();
                for (int i = 0; i < t.Length; i++)
                {
                    if (t[i].Count(f => f == '.') == 2) Console.WriteLine(t[i]);
                }
            }
        }
    }
}
