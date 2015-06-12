using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Command
{
    public class DeleteCmd : BasicCmd
    {
        public override void Execute()
        {
            if (rec != null)
            {
                string cmd = "Delete";
                rec.Docmd(cmd);
            }
        }
    }
}
