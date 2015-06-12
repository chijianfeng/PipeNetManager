using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Receiver
{
    public abstract class BasicRev
    {
        protected static string DBpath = "";//"PipeDB.accdb";
        protected static string PassWord = "1234";
        protected static string _dbpath = "";//System.IO.Directory.GetCurrentDirectory()+"\\"+DBpath;

        public abstract bool Docmd(string cmd);
    }
}
