using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.DBClass;
using DBCtrl.DBRW;

namespace BLL.Receiver
{
    public class OutFallRev:BasicRev
    {
        /// <summary>
        /// 出水口
        /// </summary>
        public List<COutFallInfo> OutList
        {
            set;
            get;
        }

        public List<COutFallExtInfo> OutExtList
        {
            set;
            get;
        }

//        private string _dbpath = DBpath;

//         public OutFallRev()
//         {
//             _dbpath = System.IO.Directory.GetCurrentDirectory() + "/" + _dbpath;
//         }

        public override bool Docmd(string cmd)
        {
            if (cmd.Equals("Load"))
            {
                return DoLoad();
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
            TOutFallInfo outinfo = new TOutFallInfo(_dbpath , PassWord);
            TOutFallExtInfo outextinfo = new TOutFallExtInfo(_dbpath, PassWord);

            OutList = outinfo.Load_OutFallInfo();
            OutExtList = outextinfo.Load_OutFallExtInfo();

            if (OutList == null || OutExtList == null)
                return false;

            return true;
        }

        private bool DoUpdate()
        {
            TOutFallInfo outinfo = new TOutFallInfo(_dbpath, PassWord);
            TOutFallExtInfo outextinfo = new TOutFallExtInfo(_dbpath, PassWord);
            if (OutExtList == null || OutList == null)
                return false;
            bool b1 = outinfo.Update_OutFallInfo(OutList);
            bool b2 = outextinfo.Update_OutFallExtInfo(OutExtList);

            return b1 & b2;
        }

        /// <summary>
        /// 插入出水口信息，同时添加出水口附加信息，若不存在则创建新信息
        /// </summary>
        /// <returns></returns>
        private bool DoInsert()
        {
            TOutFallInfo outinfo = new TOutFallInfo(_dbpath, PassWord);
            TOutFallExtInfo outextinfo = new TOutFallExtInfo(_dbpath, PassWord);

            if (OutList == null || OutList.Count <= 0)
                return false;
            int count = 0;
            
            foreach (COutFallInfo info in OutList)
            {
                COutFallInfo tmp = info;
                if (!outinfo.Insert_OutFallInfo(ref tmp))
                    continue;
                COutFallExtInfo extmp = null;
                if (OutExtList == null)
                {
                    extmp = new COutFallExtInfo();
                }
                else
                {
                    if (count < OutExtList.Count)
                        extmp = OutExtList.ElementAt(count);
                    else
                        extmp = new COutFallExtInfo();
                }
                extmp.OutFallID = tmp.ID;
                outextinfo.Insert_OutFallExtInfo(ref extmp);
                count++;
            }
            if (count <= 0)
                return false;
            return true;
        }


        private bool DoDelete()
        {
            if (OutList == null)
                return false;
            TOutFallInfo outinfo = new TOutFallInfo(_dbpath, PassWord);
            TOutFallExtInfo outextinfo = new TOutFallExtInfo(_dbpath, PassWord);

            foreach (COutFallInfo info in OutList)
            {
                outinfo.Delete_OutFallExtInfo(info);
                List<COutFallExtInfo> list = outextinfo.Sel_OutFallExtInfo(info.ID);
                if (list != null && list.Count > 0)
                    outextinfo.Delete_OutFallExtInfo(list.ElementAt(0));
            }

            return true;
        }

        private bool DoClear()
        {
            TOutFallInfo outinfo = new TOutFallInfo(_dbpath, PassWord);
            TOutFallExtInfo outextinfo = new TOutFallExtInfo(_dbpath, PassWord);

            bool b1 = outinfo.Clear_OutFallExtInfo();
            bool b2 = outextinfo.Clear_OutFallExtInfo();

            return b1 & b2;
        }
    }
}
