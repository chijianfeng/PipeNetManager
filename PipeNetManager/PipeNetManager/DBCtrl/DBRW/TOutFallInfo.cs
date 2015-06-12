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
    public class TOutFallInfo:BaseTable
    {
        public TOutFallInfo(string path, string pw)
        {
            //if (path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<COutFallInfo> Load_OutFallInfo()
        {
            string cmd = "SELECT * FROM [OutFallInfo]";
            return Select(cmd);
        }

        public bool Update_OutFallInfo(List<COutFallInfo> listout)
        {
            if (listout == null || listout.Count <= 0)
                return false;
            MySqlCommand com = new MySqlCommand();

            try
            {
                connect.Open();
                com.Connection = connect;
                com.CommandType = CommandType.Text;
                foreach (COutFallInfo outfall in listout)
                {
                    string cmdstr = "UPDATE [OutFallInfo] SET [SystemID]='" + outfall.SystemID + "',[X_Coor]='" + outfall.X_Coor + "',[Y_Coor]='" +
                        outfall.Y_Coor + "',[ReceiveWater]='" + outfall.ReceiveWater + "',[Category]= " + outfall.Category + " , [IsFlap]=" +
                        Convert.ToInt32(outfall.IsFlap) + " ,[BotEle]='" + outfall.BotEle + "',[OutFallType]=" + outfall.OutFallType + " ,[DataSource]="
                        + outfall.DataSource + " ,[Record_date]='" + Convert.ToString(outfall.Record_Date) + "',[ReportDept]='" + outfall.ReportDept +
                        "',[ReportDate]='" + Convert.ToString(outfall.ReportDate) + "' where ID=" + outfall.ID;
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

        public bool Insert_OutFallInfo(ref COutFallInfo outfall)
        {
            MySqlDataReader reader;
            string strcmd = "INSERT INTO [OutFallInfo] ([SystemID],[X_Coor],[Y_Coor],[ReceiveWater],[Category],[IsFlap],[BotEle]," +
                "[OutFallType],[DataSource],[Record_Date],[ReportDept],[ReportDate])" +
                "values(" +
                "'" + outfall.SystemID + "','" + outfall.X_Coor + "','" + outfall.Y_Coor + "','" + outfall.ReceiveWater + "'," + outfall.Category + " , " +
                Convert.ToInt32(outfall.IsFlap) + " ,'" + outfall.BotEle + "', " + outfall.OutFallType + " , " + outfall.DataSource + " ,#" +
                Convert.ToString(outfall.Record_Date) + "#,'" + outfall.ReportDept + "',#" + Convert.ToString(outfall.ReportDate) + "#)";
            try
            {
                connect.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [OutFallInfo]";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                outfall.ID = Convert.ToInt32(reader[0].ToString());
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

        public bool Delete_OutFallExtInfo(COutFallInfo outfall)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [OutFallInfo] where ID = " + outfall.ID;
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

        public bool Clear_OutFallExtInfo()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [OutFallInfo]";
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

        private List<COutFallInfo> Select(string cmd)
        {
            List<COutFallInfo> listout = new List<COutFallInfo>();
            MySqlCommand com;
            MySqlDataReader reader;

            try
            {
                connect.Open();
                com = new MySqlCommand(cmd, connect);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    COutFallInfo outfall = new COutFallInfo();
                    int i = 0;
                    string tmp;
                    outfall.ID = Convert.ToInt32(reader[i++].ToString());
                    outfall.SystemID = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.X_Coor = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.Y_Coor = Convert.ToDouble(tmp);
                    outfall.ReceiveWater = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.Category = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.IsFlap = Convert.ToBoolean(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.BotEle = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.OutFallType = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.DataSource = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.Record_Date = Convert.ToDateTime(tmp);
                    outfall.ReportDept = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.ReportDate = Convert.ToDateTime(tmp);
                    listout.Add(outfall);
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
            return listout;
        }
    }
}
