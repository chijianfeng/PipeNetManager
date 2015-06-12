using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.DBClass;
using DBCtrl.DBRW;

namespace BLL.Receiver
{
    public class JuncRev : BasicRev
    {
        /// <summary>
        /// 检测井信息
        /// </summary>
        public List<CJuncInfo> ListJunc
        {
            set;
            get;
        }

        /// <summary>
        /// 检查井附加信息
        /// </summary>
        public List<CJuncExtInfo> ListJuncExt
        {
            set;
            get;
        }

        /// <summary>
        /// 检查进信息
        /// </summary>
        public string JuncName
        {
            set;
            get;
        }

        public int ID
        {
            set;
            get;
        }

//        private string _dbpath = DBpath;

//         public JuncRev()
//         {
//             _dbpath = System.IO.Directory.GetCurrentDirectory()+"/"+_dbpath;
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
            else if (cmd.Equals("QuickInsert"))
            {
                return DoQuickInsert();
            }
            return true;
        }

       
        /// <summary>
        /// load command
        /// </summary>
        /// <returns></returns>
        private bool DoLoad()
        {
            TJuncInfo juncinfo = new TJuncInfo(_dbpath, PassWord);
            TJuncExtInfo juncextinfo = new TJuncExtInfo(_dbpath, PassWord);

            ListJunc = juncinfo.Load_JuncInfo();
            if (ListJunc == null)
                return false;
            ListJuncExt = juncextinfo.Load_JuncExtInfo();
            if (ListJuncExt != null)
                return false;
           
            return true;
        }

        /// <summary>
        /// select command.
        /// </summary>
        /// <returns></returns>
        private bool DoSelect()
        {
            TJuncInfo juncinfo = new TJuncInfo(_dbpath, PassWord);
            TJuncExtInfo juncextinfo = new TJuncExtInfo(_dbpath, PassWord);

            if (JuncName != null && JuncName.Length > 0)
            {
                ListJunc = juncinfo.Sel_JuncInfo(JuncName);
                if (ListJunc == null)
                    return false;
                ListJuncExt = new List<CJuncExtInfo>();
                foreach (CJuncInfo junc in ListJunc)
                {
                    List<CJuncExtInfo> tmplist = juncextinfo.Sel_JuncExtInfo(junc.ID);
                    if (tmplist != null&&tmplist.Count>0)
                        ListJuncExt.Add(tmplist.ElementAt(0));
                }
            }
            else
            {
                CJuncInfo ji= juncinfo.Sel_JuncInfo(ID);
                ListJunc = new List<CJuncInfo>();
                if (ji != null)
                    ListJunc.Add(ji);
            }

            return true;
        }

        private bool DoUpdate()
        {
            TJuncInfo juncinfo = new TJuncInfo(_dbpath, PassWord);
            TJuncExtInfo juncextinfo = new TJuncExtInfo(_dbpath, PassWord);

            if (ListJunc == null||ListJuncExt==null)
                return false;
           bool b1 =  juncinfo.Update_JuncInfo(ListJunc);
           bool b2 =  juncextinfo.Update_JuncExtInfo(ListJuncExt);

           return b1 & b2;
        }

        /// <summary>
        /// 插入检查井信息，同时插入对应的检查井附加信息，若不存在附加信息
        /// 则创建新的信息
        /// </summary>
        /// <returns></returns>
        private bool DoInsert()
        {
            TJuncInfo juncinfo = new TJuncInfo(_dbpath, PassWord);
            TJuncExtInfo juncextinfo = new TJuncExtInfo(_dbpath, PassWord);

            if (ListJunc == null)
                return false;
            int i =0;
            foreach (CJuncInfo junc in ListJunc)
            {
                CJuncInfo tmp = junc;
                if (!juncinfo.Insert_JuncInfo(ref tmp))
                    continue;
                CJuncExtInfo extmp = null;
                if (ListJuncExt == null || ListJuncExt.Count == 0)
                {
                    extmp = new CJuncExtInfo();
                }
                else
                {
                    if (i < ListJuncExt.Count)
                        extmp = ListJuncExt.ElementAt(i);
                    else
                        extmp = new CJuncExtInfo();
                }
                extmp.JuncID = tmp.ID;
                juncextinfo.Insert_JuncExtInfo(ref extmp);
                i++;
            }
            if (i <= 0)
                return false;
            return true;
        }

        private bool DoQuickInsert()
        {
            TJuncInfo juncinfo = new TJuncInfo(_dbpath, PassWord);
            TJuncExtInfo juncextinfo = new TJuncExtInfo(_dbpath, PassWord);

            if (ListJunc == null)
                return false;
            List<int> listid = new List<int>();         //for junction id list

            //asume that the junclist size equal juncextlist size
            if (!juncinfo.Insert_JuncInfo(ListJunc, ref listid))
                return false;
            int nCount = 0;
            List<CJuncExtInfo> newlistext = new List<CJuncExtInfo>();
            foreach(CJuncExtInfo junc in ListJuncExt)
            {
                junc.JuncID = listid.ElementAt(nCount++);
                newlistext.Add(junc);
            }
            if (!juncextinfo.Insert_JuncExtInfo(newlistext))
                return false;
            return true;
        }


        /// <summary>
        /// 删除检查井信息，同时删除检查井附加信息
        /// </summary>
        /// <returns></returns>
        private bool DoDelete()
        {
            if (ListJunc == null)
                return false;

            TJuncInfo juncinfo = new TJuncInfo(_dbpath, PassWord);
            TJuncExtInfo juncextinfo = new TJuncExtInfo(_dbpath, PassWord);

            foreach (CJuncInfo junc in ListJunc)
            {
                juncinfo.Delete_JuncInfo(junc);
                List<CJuncExtInfo> list = juncextinfo.Sel_JuncExtInfo(junc.ID);
                if (list != null && list.Count > 0)
                    juncextinfo.Delete_JuncExtInfo(list.ElementAt(0));
            }
            return true;
        }

        private bool DoClear()
        {
            TJuncInfo juncinfo = new TJuncInfo(_dbpath, PassWord);
            TJuncExtInfo juncextinfo = new TJuncExtInfo(_dbpath, PassWord);
            juncinfo.Clear_JuncInfo();
            juncextinfo.Clear_JuncExtInfo();
            return true;
        }

    }
}
