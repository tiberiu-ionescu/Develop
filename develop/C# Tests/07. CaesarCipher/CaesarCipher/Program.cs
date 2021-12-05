using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace CaesarCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            const int letters = 26;

            int[] upper = new int[letters];
            int[] lower = new int[letters];

            for (int i = 0; i < letters; i++)
            {
                upper[i] = i + 65;
                lower[i] = i + 97;
            }

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
                    string s = str[1];
                    int k = Convert.ToInt32(str[2]) % letters;

                    byte[] ba = Encoding.ASCII.GetBytes(s);
                    int[] input = ba.Select(x => (int)x).ToArray();
                    int[] index = new int[input.Length];
                    int[] result = new int[input.Length];

                    for (int i = 0; i < input.Length; i++)
                        if ((input[i] < 65) || (input[i] > 90 && input[i] < 97) || (input[i] > 122))
                            result[i] = input[i];

                    bool found;

                    for (int i = 0; i < input.Length; i++)
                    {
                        found = false;

                        for (int j = 0; j < letters; j++)
                        {
                            if (input[i] == lower[j])
                            {
                                index[i] = j;
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            for (int j = 0; j < letters; j++)
                            {
                                if (input[i] == upper[j])
                                {
                                    index[i] = j + letters;
                                    found = true;
                                    break;
                                }
                            }
                        }

                        if (!found) index[i] = -1;
                    }

                    int[] lkeys = new int[k];
                    int[] ukeys = new int[k];

                    for (int i = 0; i < k; i++) 
                    {
                        lkeys[i] = lower[i];
                        ukeys[i] = upper[i];
                    }

                    for (int i = 0; i < letters - k; i++)
                    {
                        lower[i] = lower[i + k];
                        upper[i] = upper[i + k];
                    }

                    for (int i = 0; i < k; i++)
                    {
                        lower[letters - i - 1] = lkeys[k - i - 1];
                        upper[letters - i - 1] = ukeys[k - i - 1];
                    }
                    
                    for (int i = 0; i < index.Length; i++)
                    {
                        if (index[i] > letters - 1 && index[i] <= 2 * letters - 1)
                            result[i] = upper[index[i] - letters];
                        if (index[i] >= 0 && index[i] <= letters - 1)
                            result[i] = lower[index[i]];
                    }

                    string cuc = new string(Array.ConvertAll(result, x => (char)x));
                    Console.WriteLine(cuc);
                }
                catch
                {
                    Console.WriteLine("Nu ati introdus numere!");
                }
            }
        }
    }
}