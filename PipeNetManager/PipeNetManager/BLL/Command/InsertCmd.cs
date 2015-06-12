using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Command
{
    public class InsertCmd : BasicCmd
    {
        public override void Execute()
        {
            if (rec != null)
            {
                string cmd = "Insert";
                rec.Docmd(cmd);
            }
        }
    }
}
