using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BalancedBrackets
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
                    string[] strings = new string[str.Length - 1];
                    int k = Convert.ToInt32(str[0]);
                    byte[] ba;

                    for (int i = 0; i < str.Length - 2; i++) 
                    {
                        strings[i] = str[i + 1];
                        ba = Encoding.ASCII.GetBytes(strings[i]);
                        ba.Skip(ba.Length - 1).ToArray();
                        Console.WriteLine(CheckBalance(ba)? "YES":"NO");
                    }
                }
                catch
                {
                    Console.WriteLine("Nu ati introdus numere!");
                }
            }
        }

        public static bool CheckBalance(byte[] arr)
        {
            byte[] start = new byte[arr.Length - 1];
            byte[] end = new byte[arr.Length - 1];

            int i = 0;
            int index = 0;

            while(arr[i] != 41 && arr[i] != 93 && arr[i] != 125 && i < arr.Length - 1)
            {
                start[i] = arr[i];
                i++;
                index++;
            }

            Array.Reverse(start);

            for (int k = 0; k < start.Length; k++)
            {
                switch (start[k])
                {
                    case 40: 
                        end[k] = 41;
                        break;
                    case 91:
                        end[k] = 93;
                        break;
                    case 123:
                        end[k] = 125;
                        break;
                    default:
                        end[k] = start[k];
                        break;
                }
            }
            
            for (int j = index; j < arr.Length - 1; j++)
                if (arr[j] != end[j]) return false;

            return true;
        }
    }
}
