using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Command
{
    public class ClearCmd : BasicCmd
    {

        public override void Execute()
        {
            if (rec != null)
            {
                string cmd = "Clear";
                rec.Docmd(cmd);
            }
        }
    }
}
