using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Receiver;

namespace BLL.Command
{
    public abstract class BasicCmd
    {

        protected BasicRev rec;
        public abstract void Execute();

        public void SetReceiver(BasicRev r)
        {
            rec = r;
        }
    }
}
