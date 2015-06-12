using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Command
{
    public class LoadCmd : BasicCmd
    {
        /// <summary>
        /// execute the comamnd.
        /// </summary>
        public override void Execute()
        {
            if (rec != null)
            {
                string cmd = "Load";
                rec.Docmd(cmd);
            }
        }
    }
}
