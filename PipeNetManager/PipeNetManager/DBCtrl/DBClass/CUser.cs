using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class CUser
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

       
        private string username;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }


        private string password;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string PassWord
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

 
        private int usertype;
        /// <summary>
        /// 用户类型：1普通用户、2：管理员
        /// </summary>
        public int UserType
        {
            get
            {
                if (usertype <= 0 || usertype > 2)
                    return 1;
                else
                    return usertype;
            }
            set
            {
                if (value >= 1 && value <= 2)
                    usertype = value;
                else
                    usertype = 1;
            }
        }
    }
}
