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
    public class TOutFallExtInfo : BaseTable
    {
        public TOutFallExtInfo(string path, string pw)
        {
            //if (path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<COutFallExtInfo> Load_OutFallExtInfo()
        {
            string cmd = "SELECT * FROM [OutFallExtInfo]";
            return Select(cmd);
        }

        /// <summary>
        /// 根据OutFallID 获取数据
        /// </summary>
        /// <param name="outfallid"></param>
        /// <returns></returns>
        public List<COutFallExtInfo> Sel_OutFallExtInfo(int outfallid)
        {
            string cmd = "SELECT * FROM [OutFallExtInfo] where [OutFallID]=" + outfallid;
            return Select(cmd);
        }

        /// <summary>
        /// 根据OutFallNName 获取数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<COutFallExtInfo> Sel_OutFallExtInfo(string name)
        {
            string cmd = "SELECT * FROM [OutFallExtInfo] where [OutFallName]='" + name + "'";
            return Select(cmd);
        }


        public bool Update_OutFallExtInfo(List<COutFallExtInfo> listout)
        {
            if (listout == null || listout.Count <= 0)
                return false;
            MySqlCommand com = new MySqlCommand();
            try
            {
                connect.Open();
                com.Connection = connect;
                com.CommandType = CommandType.Text;
                foreach (COutFallExtInfo outfall in listout)
                {
                    string cmdstr = "UPDATE [OutFallExtInfo] SET [OutFallID]=" + outfall.OutFallID + " ,[OutFallName]='" + outfall.OutFallName +
                        "',[OutFallAddr]='" + outfall.OutFallAddr + "',[Flap_Material]=" + outfall.Flap_Material + " ,[Flap_Diameter]='" +
                        outfall.Flap_Diameter + "',[Flap_TopEle]='" + outfall.Flap_TopEle + "',[Flap_BotEle]='" + outfall.Flap_BotEle +
                        "',[TopEle]='" + outfall.TopEle + "',[NormalLevel]='" + outfall.NormalLevel + "',[Tidal_Curve]='" + outfall.Tidal_Curve
                        + "',[Status]= " + outfall.Status + " ,[Remark]='" + outfall.Remark + "' where ID =" + outfall.ID;
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

        public bool Insert_OutFallExtInfo(ref COutFallExtInfo outfall)
        {
            MySqlDataReader reader;
            string strcmd = "INSERT INTO [OutFallExtInfo]([OutFallID],[OutFallName],[OutFallAddr],[Flap_Material],[Flap_Diameter]," +
                "[Flap_TopEle],[Flap_BotEle],[TopEle],[NormalLevel],[Tidal_Curve],[Status],[Remark]) values(" + outfall.OutFallID + ",'" +
                outfall.OutFallName + "','" + outfall.OutFallAddr + "', " + outfall.Flap_Material + " ,'" + outfall.Flap_Diameter + "','" + outfall.Flap_TopEle
                + "','" + outfall.Flap_BotEle + "','" + outfall.TopEle + "','" + outfall.NormalLevel + "','" + outfall.Tidal_Curve + "'," + outfall.Status +
                " ,'" + outfall.Remark + "')";
            try
            {
                connect.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [OutFallExtInfo]";
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

        public bool Delete_OutFallExtInfo(COutFallExtInfo outfall)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [OutFallExtInfo] where ID = " + outfall.ID;
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
                string cmd = "DELETE * FROM [OutFallExtInfo]";
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

        private List<COutFallExtInfo> Select(string cmd)
        {
            List<COutFallExtInfo> listout = new List<COutFallExtInfo>();
            MySqlCommand com;
            MySqlDataReader reader;

            try
            {
                connect.Open();
                com = new MySqlCommand(cmd, connect);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    COutFallExtInfo  outfall =  new COutFallExtInfo();
                    int i = 0;
                    string tmp;
                    outfall.ID = Convert.ToInt32(reader[i++].ToString());
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.OutFallID = Convert.ToInt32(tmp);
                    outfall.OutFallName = reader[i++].ToString();
                    outfall.OutFallAddr = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.Flap_Material = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.Flap_Diameter = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.Flap_TopEle = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.Flap_BotEle = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.TopEle = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.NormalLevel = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.Tidal_Curve = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        outfall.Status = Convert.ToInt32(tmp);
                    outfall.Remark = reader[i++].ToString();
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
