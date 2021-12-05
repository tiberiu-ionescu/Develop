using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NoPrefixSet
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
                string[] strings = new string[str.Length - 1];

                try
                {
                    for (int i = 0; i < str.Length - 1; i++) strings[i] = str[i + 1];
                    string s = Compare(strings);

                    if (s == "-1")
                        Console.WriteLine("GOOD SET");
                    else
                    {
                        Console.WriteLine("BAD SET");
                        Console.WriteLine(s);
                    }
                }
                catch
                {
                    Console.WriteLine("Nu ati introdus numere!");
                }
            }
        }

        public static string Compare(string[] strings)
        {
            for (int i = 0; i < strings.Length - 1; i++)
            {
                for (int j = i; j >= 0; j--)
                {
                    if (strings[j].Length >= strings[i].Length && i != j && 
                        strings[j].Substring(0, strings[i].Length - 1).Contains(strings[i].Replace("\r\n", "").Replace("\r", "").Replace("\n", "")))
                        return strings[j];

                    if (strings[j].Length < strings[i].Length && i != j && 
                        strings[i].Substring(0, strings[j].Length - 1).Contains(strings[j].Replace("\r\n", "").Replace("\r", "").Replace("\n", "")))
                        return strings[i];
                }
            }

            return "-1";
        }
    }
}
