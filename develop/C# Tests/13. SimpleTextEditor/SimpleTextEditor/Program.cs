using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleTextEditor
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

                string s = "";
                
                var lines = sr.ReadToEnd().Split('\n');
                string[] str = lines.ToArray();
                string[] strings = new string[str.Length - 2];
                string[] ops = new string[strings.Length + 1];
                int[] opint = new int[strings.Length];
                
                try
                {
                    int q = Convert.ToInt32(str[0]);
                    for (int i = 0; i < str.Length - 2; i++) 
                    {
                        strings[i] = str[i + 1];
                        opint[i] = Convert.ToInt32(strings[i].Substring(0, 1)); 
                    }

                    ops[0] = "";

                    for (int i = 0; i < strings.Length; i++)
                    {
                        switch (Convert.ToInt32(strings[i].Substring(0, 1)))
                        {
                            case 1:
                                ops[i + 1] = s + strings[i].Substring(2);
                                break;
                            case 2:
                                if (ops[i].Length >= Convert.ToInt32(strings[i].Substring(2, 1))) 
                                    ops[i + 1] = ops[i].Substring(0, ops[i].Length - 1 - Convert.ToInt32(strings[i].Substring(2, 1)));
                                break;
                            case 3:
                                Console.WriteLine(ops[i].Substring(Convert.ToInt32(strings[i].Substring(2, 1)) - 1, 1));
                                ops[i + 1] = ops[i];
                                break;
                            case 4:
                                ops[i + 1] = Undo(ops, opint, i);
                                break;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Nu ati introdus numere!");
                }
            }
        }

        public static string Undo(string[] ops, int[] opint, int index)
        {
            int undos = 0;
            for (int i = index - 1; i >= 0; i--) if (opint[i] == 4) undos++;

            for (int i = index; i >= 0; i--)
            {
                if (opint[i] == 1 || opint[i] == 2) 
                {
                    if (undos == 0) return ops[i];
                    else undos--;
                }
            }

            return "-1";
        }
    }
}
