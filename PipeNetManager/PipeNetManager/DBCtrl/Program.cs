using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DBCtrl.DBClass;
using System.Text.RegularExpressions;

namespace DBCtrl
{
    class Program
    {
        static void Main(string[] args)
        {
            string cmd = "FeiY-Y17";
            List<string> l = new List<string>();
            l.Add("ShiN-Y1 / LongQ-Y1");
            l.Add("LongQ-Y155/TongJ-Y1");
            l.Add("ShiN-Y7 / FeiY-Y17 ");
            l.Add("ShiN-Y39 / YangD-Y21");
            l.Add("BaiF-GY1");
            l.Add("LongQ-Y15");
            System.Console.WriteLine(CheckID(cmd , l));
           // System.Console.WriteLine(GetChar(cmd));
        }
        private int GetPipeID(string pipename, List<CPipeInfo> list)
        {
            string pattern = @"(.{0,}" + pipename + @".{0,}";
            foreach (CPipeInfo pipe in list)
            {
                if (Regex.IsMatch(pipe.PipeName, pattern))
                {
                    return pipe.ID;
                }
            }
            return -1;
        }
       static  private int GetClass(string str)
        {
            string[] pat = { @"Ⅰ|I", @"Ⅱ", @"Ⅲ", @"Ⅳ", @"Ⅴ", @"Ⅵ", @"Ⅶ", @"Ⅷ",
                           @"Ⅸ"};
            int i = 0;
            foreach (string pattern in pat)
            { 
                i++;
                string p = @".{0,}" + @pattern + @".{0,}$";
                if (Regex.IsMatch(str, p))
                    return i;
               
            }
            if (i >=pat.Count())
                return 0;
            return i;
        }
        static int CheckID(string str, List<string> list)
        {
            string pattern = @"(.{0,}"+@str + @"\D+)|(.*"+@str+"$)";
            int n = 0;
            foreach (string junc in list)
            {
                n++;
                if (Regex.IsMatch(junc, pattern))
                {
                    return n;
                }
            }
            return -1;
        }
        private static int GetNumber(string str)
        {
            string pattern = @"\D*";
            string res = System.Text.RegularExpressions.Regex.Replace(str, pattern, "");
            return Convert.ToInt32(res);
        }

        private static double GetFloat(string str)
        {
            Regex r  =new Regex(@"\d*[\d\.]\d*");
            string res = r.Match(str).Value.ToString();
            return Convert.ToDouble(res);
        }

        private static int GetChar(string str)
        {
            string pattern = @"\d+\w*$";
            string res = System.Text.RegularExpressions.Regex.Replace(str, pattern, "");
            if (res != null && res.Length == 1)
            {
                char c = res.ElementAt(0);
                return Convert.ToInt32(c);
            }
            return -1;
        }

        private static int GetCategory(string str)
        {
            string[] pat = { @"雨水", @"污水", @"合流" };
            int i = 0;
            foreach (string pattern in pat)
            {
                i++;
                if (Regex.IsMatch(str, pattern))
                    return i;
            }

            return i;
        }
    }
}
