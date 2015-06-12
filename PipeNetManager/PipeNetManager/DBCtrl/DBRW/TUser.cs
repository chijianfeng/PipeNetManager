using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.DBClass;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;

namespace DBCtrl.DBRW
{
    public class TUser : BaseTable
    {
        public TUser(string path, string pw)
        {
            //if(path.Length>0)
                ConnectDB(path, pw);
        }

        /// <summary>
        /// 获取表中所有用户
        /// </summary>
        /// <returns></returns>
        public List<CUser> Load_User()
        {
            List<CUser> userlist = new List<CUser>();

            MySqlCommand com;
            MySqlDataReader reader;
            string cmd = "SELECT * FROM user";

            try
            {
                connect.Open();
                com = new MySqlCommand(cmd, connect);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    CUser user = new CUser();
                    user.ID = Convert.ToInt32(reader[0].ToString());
                    user.UserName = reader[1].ToString();
                    user.PassWord = reader[2].ToString();
                    if (reader[3].ToString().Length > 0)
                    {
                        user.UserType = Convert.ToInt32(reader[3].ToString());
                    }
                    userlist.Add(user);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                connect.Close();
            }
            return userlist;
        }

        /// <summary>
        /// select the user by the user name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<CUser> Sel_User(string name)
        {
            if (name == null || name.Length <= 0)
                return null;
            List<CUser> userlist = new List<CUser>();

            MySqlCommand com;
            MySqlDataReader reader;
            string cmd = "SELECT * FROM user where UserName ='"+name+"'";
            try
            {
                connect.Open();
                com = new MySqlCommand(cmd, connect);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    CUser user = new CUser();
                    user.ID = Convert.ToInt32(reader[0].ToString());
                    user.UserName = reader[1].ToString();
                    user.PassWord = reader[2].ToString();
                    if (reader[3].ToString().Length > 0)
                    {
                        user.UserType = Convert.ToInt32(reader[3].ToString());
                    }
                    userlist.Add(user);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                connect.Close();
            }
            return userlist;
        }

        /// <summary>
        /// 更新用户数据，id必须和数据库中相符
        /// </summary>
        /// <param name="listuser"></param>
        /// <returns></returns>
        public bool Update_User(List<CUser> listuser)
        {
            if (listuser == null || listuser.Count <= 0)
                return false;
            MySqlCommand com = new MySqlCommand();
            try
            {
                connect.Open();
                com.Connection = connect;
                com.CommandType = CommandType.Text;
                foreach (CUser user in listuser)
                {
                    string cmdstr = "UPDATE user SET UserName='" + user.UserName +"', PassWord='"+user.PassWord+"',UserType="+
                        user.UserType +" where ID=" + user.ID;
                    com.CommandText = cmdstr;
                    com.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        /// <summary>
        /// insert a CUser to the table
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// user.ID
        /// true/false
        /// </returns>
        public bool Insert_User(ref CUser user)
        {
            MySqlDataReader reader;
            try
            {
                string strcmd = "INSERT INTO user(UserName,PassWord,UserType) values('"+user.UserName+"','"+
                    user.PassWord+"', "+user.UserType+")";
                connect.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX([ID]) AS MAXID FROM User";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                user.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
            	Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        /// <summary>
        /// 根据user的id进行删除
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Delete_User(CUser user)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE FROM user where ID = " + user.ID;
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// clear up all the user
        /// </summary>
        /// <returns></returns>
        public bool Clear_User()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM user";
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }


    }
}
