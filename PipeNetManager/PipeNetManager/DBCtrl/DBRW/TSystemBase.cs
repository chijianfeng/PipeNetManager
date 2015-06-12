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
    public class TSystemBase:BaseTable
    {
        public TSystemBase(string path, string pw)
        {
           // if (path != null && path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<CSystemBase> Load_SystemBase()
        {
            string cmd = "SELECT * FROM [SysBaseInfo]";
            return Select(cmd);
        }

        public bool Update_SystemBase(List<CSystemBase> listsys)
        {
            if (listsys == null || listsys.Count <= 0)
                return false;
            MySqlCommand com = new MySqlCommand();
            try
            {
                connect.Open();
                com.Connection = connect;
                com.CommandType = CommandType.Text;
                foreach (CSystemBase sys in listsys)
                {
                    string cmdstr = "UPDATE [SysBaseInfo] SET [SysName]='" + sys.SysName + "',[SysType]=" + sys.SysType + " ,[UpdateDate]=#" +
                        Convert.ToString(sys.UpdateDate) + "#,[EstDept]='" + sys.EstDept + "',[OrgDept]='" + sys.OrgDept + "',[CoorSys]='"+
                        sys.CoorSys + "',[EleSys]='" + sys.EleSys + "',[DrainSys]='"+sys.DrainSys+"' where [SystemID]='"+sys.SystemID+"'";

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
            return false;
        }

        public bool Insert_SystemBase(ref CSystemBase sys)
        {
            string strcmd = "INSERT INTO [SysBaseInfo] ([SystemID],[SysName],[SysType],[UpdateDate],[EstDept]," +
                "[OrgDept],[CoorSys],[EleSys],[DrainSys]" +
                ")values('" +
                sys.SystemID + "','" + sys.SysName + "'," + sys.SysType + ",#" + Convert.ToString(sys.UpdateDate) + "#,'" + sys.EstDept + "','" +
                sys.OrgDept + "','" + sys.CoorSys + "','" + sys.EleSys + "','" + sys.DrainSys + "')";
            try
            {
                connect.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                cmd.ExecuteNonQuery();
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

        public bool Delete_SystemBase(CSystemBase sys)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [SysBaseInfo] where [SystemID] = '" + sys.SystemID + "'";
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

        public bool Clear_SystemBase()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [SysBaseInfo]";
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

        private List<CSystemBase> Select(string cmd)
        {
            List<CSystemBase> listsys = new List<CSystemBase>();
            MySqlCommand com;
            MySqlDataReader reader;
            try
            {
                connect.Open();
                com = new MySqlCommand(cmd, connect);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    CSystemBase sys = new CSystemBase();
                    int i = 0;
                    string tmp;
                    sys.SystemID = reader[i++].ToString();
                    sys.SysName = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        sys.SysType = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        sys.UpdateDate = Convert.ToDateTime(tmp);
                    sys.EstDept = reader[i++].ToString();
                    sys.OrgDept = reader[i++].ToString();
                    sys.CoorSys = reader[i++].ToString();
                    sys.EleSys = reader[i++].ToString();
                    sys.DrainSys = reader[i++].ToString();
                    listsys.Add(sys);
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
            return listsys;
        }
    }
}
