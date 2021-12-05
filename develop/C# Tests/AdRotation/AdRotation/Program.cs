using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdRotation
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

                int value = Convert.ToInt32(str[0]);
                string binary = Convert.ToString(value, 2);

                char[] charArray = binary.ToCharArray();

                for (int i = 0; i < charArray.Length; i++)
			    {
                    if (charArray[i] == '0')
                    {
                        charArray[i] = '1';
                    }
                    else if (charArray[i] == '1')
                    {
                        charArray[i] = '0';
                    }                    
			    }

                string s = new string(charArray);

                Console.Write(Convert.ToInt32(s, 2).ToString());
            }
        }
    }
}
