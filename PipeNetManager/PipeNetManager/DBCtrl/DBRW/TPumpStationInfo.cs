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
    public class TPumpStationInfo : BaseTable
    {
        public TPumpStationInfo(string path, string pw)
        {
           // if (path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<CPumpStationInfo> Load_PumpStationInfo()
        {
            string cmd = "SELECT * FROM [PumpStationInfo]";
            return Select(cmd);
        }

        public List<CPumpStationInfo> Sel_PumpStationInfo(string pumpname)
        {
            string cmd = "SELECT * FROM [PumpStationInfo] where [PumpName]='" + pumpname + "'";
            return Select(cmd);
        }

        public bool Update_PumpStationInfo(List<CPumpStationInfo> listpump)
        {
            if (listpump == null || listpump.Count <= 0)
                return false;
            MySqlCommand com = new MySqlCommand();
            try
            {
                connect.Open();
                com.Connection = connect;
                com.CommandType = CommandType.Text;
                foreach (CPumpStationInfo pump in listpump)
                {
                    
                    string cmdstr = "UPDATE [PumpStationInfo] SET [SystemID]='" + pump.SystemID + "',[X_Coor]='" + pump.X_Coor + "',[Y_Coor]='" +
                        pump.Y_Coor + "',[PumpName]='" + pump.PumpName + "',[PumpAddr]='" + pump.PumpAddr + "',[PS_Category1]=" + pump.PS_Category1 +
                        " ,[PS_Category2]=" + pump.PS_Category2 + " ,[PS_Num]= " + pump.PS_Num + " ,[Design_Storm]='" + pump.Design_Storm +
                        "',[Design_Sewer]='" + pump.Design_Sewer + "',[Min_Level]='" + pump.Min_Level + "',[Control_Level]='" + pump.Control_Level +
                        "',[Warnning_Level]='" + pump.Warnning_Level + "',[DataSource]=" + pump.DataSource + " ,[Record_Data]=#" + pump.Record_Date +
                        "#,[ReportDept]='" + pump.ReportDept + "',[ReportDate]=#" + pump.ReportDate + "# where ID=" + pump.ID;
                         
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

        public bool Insert_PumpStationInfo(ref CPumpStationInfo pump)
        {
            MySqlDataReader reader;
            string strcmd = "INSERT INTO [PumpStationInfo] ([SystemID],[X_Coor],[Y_Coor],[PumpName],[PumpAddr],[PS_Category1]," +
                "[PS_Category2],[PS_Num],[Design_Storm],[Design_Sewer],[Min_Level],[Control_Level],[Warnning_Level],[DataSource]," +
                "[Record_Data],[ReportDept],[ReportDate]" +
                ")values('" +
                pump.SystemID + "','" + pump.X_Coor + "','" + pump.Y_Coor + "','" + pump.PumpName + "','" + pump.PumpAddr + "', " + pump.PS_Category1 +
                " , " + pump.PS_Category2 + " , " + pump.PS_Num + " ,'" + pump.Design_Storm + "','" + pump.Design_Sewer + "','" + pump.Min_Level + "','" +
                pump.Control_Level + "','" + pump.Warnning_Level + "', " + pump.DataSource + " ,#" + pump.Record_Date + "#,'" + pump.ReportDept + "',#" +
                pump.ReportDate + "#)";
            try
            {
                connect.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [PumpStationInfo]";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pump.ID = Convert.ToInt32(reader[0].ToString());
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

        public bool Delete_PumpStationInfo(CPumpStationInfo pump)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [PumpStationInfo] where ID = " + pump.ID;
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

        public bool Clear_PumpStationInfo()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [PumpStationInfo]";
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

        private List<CPumpStationInfo> Select(string cmd)
        {
            List<CPumpStationInfo> listpump = new List<CPumpStationInfo>();
            MySqlCommand com;
            MySqlDataReader reader;
            try
            {
                connect.Open();
                com = new MySqlCommand(cmd, connect);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    CPumpStationInfo pump = new CPumpStationInfo();
                    int i = 0;
                    string tmp;
                    pump.ID = Convert.ToInt32(reader[i++].ToString());
                    pump.SystemID = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.X_Coor = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.Y_Coor = Convert.ToDouble(tmp);
                    pump.PumpName = reader[i++].ToString();
                    pump.PumpAddr = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.PS_Category1 = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.PS_Category2 = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.PS_Num = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.Design_Storm = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.Design_Sewer = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.Min_Level = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.Control_Level = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.Warnning_Level = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.DataSource = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.Record_Date = Convert.ToDateTime(tmp);
                    pump.ReportDept = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null || tmp.Length > 0)
                        pump.ReportDate = Convert.ToDateTime(tmp);
                    listpump.Add(pump);
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
            return listpump;
        }
    }
}
