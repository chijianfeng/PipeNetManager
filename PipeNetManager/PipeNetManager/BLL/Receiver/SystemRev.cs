using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.DBRW;
using DBCtrl.DBClass;

namespace BLL.Receiver
{
    class SystemRev : BasicRev
    {

        public List<CSystemBase> SysList
        {
            set;
            get;
        }

        public override bool Docmd(string cmd)
        {
            if (cmd.Equals("Load"))
            {
                return DoLoad();
            }
            else if (cmd.Equals("Insert"))
            {
                return DoInsert();
            }
            else if (cmd.Equals("Delete"))
            {
                return DoDelete();
            }
            else if (cmd.Equals("Clear"))
            {
                return DoClear();
            }
            return true;
        }

        private bool DoLoad()
        {
            TSystemBase sysbase = new TSystemBase(_dbpath, PassWord);
            SysList = sysbase.Load_SystemBase();
            if (SysList == null || SysList.Count <= 0)
                return false;
            else
                return true;
        }

        private bool DoInsert()
        {
            TSystemBase sysbase = new TSystemBase(_dbpath, PassWord);
            if (SysList == null || SysList.Count <= 0)
                return false;
            int count = 0;
            foreach(CSystemBase s in SysList)
            {
                CSystemBase tmp = s;
                if (sysbase.Insert_SystemBase(ref tmp))
                {
                    count++;
                }
            }
            if (count <= 0)
                return false;
            else
                return true;
        }

        private bool DoDelete()
        {
            TSystemBase sysbase = new TSystemBase(_dbpath, PassWord);
            if (SysList == null || SysList.Count <= 0)
                return false;
            int count = 0;
            foreach (CSystemBase s in SysList)
            {
                if (sysbase.Delete_SystemBase(s))
                {
                    count++;
                }
            }
            if (count <= 0)
                return false;
            else
                return true;
        }

        private bool DoClear()
        {
            TSystemBase sysbase = new TSystemBase(_dbpath, PassWord);
            return sysbase.Clear_SystemBase();
        }
    }
}
