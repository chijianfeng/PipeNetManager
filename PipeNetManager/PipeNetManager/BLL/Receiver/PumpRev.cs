using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.DBRW;
using DBCtrl.DBClass;

namespace BLL.Receiver
{
    public class PumpRev:BasicRev
    {
        public List<CPumpStationInfo> ListPump
        {
            set;
            get;
        }

        public string PumpName
        {
            set;
            get;
        }

//        private string _dbpath = DBpath;
// 
//         public PumpRev()
//         {
//             _dbpath = System.IO.Directory.GetCurrentDirectory() + "/" + _dbpath;
//         }

        public override bool Docmd(string cmd)
        {
            if (cmd.Equals("Load"))
            {
                return DoLoad();
            }
            else if (cmd.Equals("Select"))
            {
                return DoSelect();
            }
            else if (cmd.Equals("Update"))
            {
                return DoUpdate();
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
            TPumpStationInfo pumpinfo = new TPumpStationInfo(_dbpath, PassWord);

            ListPump = pumpinfo.Load_PumpStationInfo();

            if (ListPump == null || ListPump.Count <= 0)
                return false;

            return true;
        }

        private bool DoSelect()
        {
            TPumpStationInfo pumpinfo = new TPumpStationInfo(_dbpath, PassWord);
            ListPump = pumpinfo.Sel_PumpStationInfo(PumpName);
            if (ListPump == null || ListPump.Count <= 0)
                return false;
            else
                return true;
        }

        private bool DoUpdate()
        {
            TPumpStationInfo pumpinfo = new TPumpStationInfo(_dbpath, PassWord);
            bool b = pumpinfo.Update_PumpStationInfo(ListPump);
            return b;
        }

        private bool DoInsert()
        {
            if (ListPump == null)
                return false;
            TPumpStationInfo pumpinfo = new TPumpStationInfo(_dbpath, PassWord);
            int i = 0;
            foreach (CPumpStationInfo pump in ListPump)
            {
                CPumpStationInfo tmp = pump;
                if(pumpinfo.Insert_PumpStationInfo(ref tmp))
                    i++;
            }
            if (i <= 0)
                return false;
            else
                return true;
        }

        private bool DoDelete()
        {
            TPumpStationInfo pumpinfo = new TPumpStationInfo(_dbpath, PassWord);
            int i = 0;
            foreach (CPumpStationInfo pump in ListPump)
            {
                if (pumpinfo.Delete_PumpStationInfo(pump))
                    i++;
            }
            if (i <= 0)
                return false;
            else
                return true;
        }

        private bool DoClear()
        {
            TPumpStationInfo pumpinfo = new TPumpStationInfo(_dbpath, PassWord);
            return pumpinfo.Clear_PumpStationInfo();
        }
    }
}
