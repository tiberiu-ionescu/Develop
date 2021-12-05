using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace DiagonalDifference
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var sr = new StreamReader(Console.OpenStandardInput()))
            using (Stream stdout = Console.OpenStandardOutput())
            {
                var lines = sr.ReadToEnd().Split('\n');
                string[] str = lines.ToArray();
                try
                {
                    int m = Convert.ToInt32(str[0]);
                    int[][] myInts = new int[m][];
                    int left = 0;
                    int right = 0;

                    for (int i = 1; i <= m; i++)
                    {
                        myInts[i - 1] = str[i].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
                        left += myInts[i - 1][i - 1];
                    }

                    for (int i = m; i > 0; i--)
                        right += myInts[m - i][i - 1];

                    Console.WriteLine(Math.Abs(left - right));
                }
                catch
                {
                    Console.WriteLine("Nu ati introdus numere!");
                }
            }
        }
    }
}