using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;

namespace DBCtrl.DBRW
{
    public class BaseTable
    {
        protected MySqlConnection connect;
        protected MySqlCommand mysqlcmd = null;
        protected static int NUMBER
        {
            get{
                return 50;
            }
        }

        public bool OpenDB()
        {
            if (connect == null)
                return false;
            if (ConnectionState.Closed == connect.State)
            {
                connect.Open();
            }
            if (ConnectionState.Open == connect.State)
            {
                mysqlcmd = new MySqlCommand();
                return true;
            }
            else
            {
                connect.Close();
                connect.Dispose();
                return false;
            }
        }

        public void CloseDB()
        {
            if (connect == null)
                return ;
            connect.Close();
            connect.Dispose();
        }

        protected void ConnectDB(string path, string pw)
        {
            string constr = "Database='pipedb';Data Source='localhost';User Id='root';Password='"+pw+"'";
            connect = new MySqlConnection(constr);
        }

        /// <summary>
        /// not for reader
        /// </summary>
        /// <param name="strcmd"></param>
        /// <returns></returns>
        protected int ExectueCmd(List<string> liststrcmd)
        {
            int num = 0;
            try
            {
                if(connect.State==ConnectionState.Closed)
                    connect.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                foreach (string stringcmd in liststrcmd)
                {
                    cmd.CommandText = stringcmd;
                    num = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                connect.Close();
            }
            return num;
        }


    }
}
