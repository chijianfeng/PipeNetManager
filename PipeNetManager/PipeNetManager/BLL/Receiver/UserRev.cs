using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.DBClass;
using DBCtrl.DBRW;

namespace BLL.Receiver
{
    public class UserRev : BasicRev
    {
        public List<CUser> ListUser
        {
            set;
            get;
        }

        public string UserName
        {
            set;
            get;
        }
//        private string _dbpath = DBpath;
//         public UserRev()
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
            TUser user = new TUser(_dbpath, PassWord);
            ListUser = user.Load_User();
            if (ListUser == null || ListUser.Count <= 0)
                return false;
            else
                return true;
        }

        private bool DoSelect()
        {
            TUser user = new TUser(_dbpath, PassWord);
            if (UserName == null || UserName.Length <= 0)
                return false;
            ListUser = user.Sel_User(UserName);
            if (ListUser == null || ListUser.Count <= 0)
                return false;
            else
                return true;
        }

        private bool DoUpdate()
        {
            TUser user = new TUser(_dbpath, PassWord);
            return user.Update_User(ListUser);
        }

        private bool DoInsert()
        {
            if (ListUser == null || ListUser.Count <= 0)
                return false;
            TUser user = new TUser(_dbpath, PassWord);
            int count = 0;
            foreach (CUser u in ListUser)
            {
                CUser tmp = u;
                if (user.Insert_User(ref tmp))
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
            if (ListUser == null || ListUser.Count <= 0)
                return false;
            TUser user = new TUser(_dbpath, PassWord);
            int count = 0;
            foreach (CUser u in ListUser)
            {
                if (user.Delete_User(u))
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
            TUser user = new TUser(_dbpath, PassWord);
            return user.Clear_User();
        }
    }
}
