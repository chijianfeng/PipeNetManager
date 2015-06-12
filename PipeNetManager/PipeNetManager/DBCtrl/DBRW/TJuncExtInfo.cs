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
    public class TJuncExtInfo:BaseTable
    {

        public TJuncExtInfo(string path, string pw)
        {
            //if (path.Length > 0)
                ConnectDB(path, pw);
        }

        /// <summary>
        /// 提取表中所有JuncExt信息
        /// </summary>
        /// <returns></returns>
        public List<CJuncExtInfo> Load_JuncExtInfo()
        {
            string cmd = "SELECT * FROM JunExtInfo";
            return Select(cmd);
        }

        /// <summary>
        /// 根据井盖ID检索
        /// </summary>
        /// <param name="juncid"></param>
        /// <returns></returns>
        public List<CJuncExtInfo> Sel_JuncExtInfo(int juncid)
        {
            string cmd = "SELECT * FROM JunExtInfo where JuncID="+juncid;
            return Select(cmd);
        }

        /// <summary>
        /// 更新juncExtinfo 信息 ，其ID必须在表中已存在
        /// </summary>
        /// <param name="listjunc"></param>
        /// <returns></returns>
        public bool Update_JuncExtInfo(List<CJuncExtInfo> listjunc)
        {
            if (listjunc == null || listjunc.Count <= 0)
                return false;
            MySqlCommand com = new MySqlCommand();

            try
            {
                connect.Open();
                com.Connection = connect;
                com.CommandType = CommandType.Text;
                foreach (CJuncExtInfo junc in listjunc)
                {
                    string cmdstr = "UPDATE JunExtInfo SET JuncID= " + junc.JuncID + ", Anci_Fac='" + junc.Anci_Fac + "' , Lane_Way='" +
                        junc.Lane_Way + "' , Cov_Material = " + junc.Cov_Material + " , Cov_Shape=" + junc.Cov_Shape + " , Cov_Dimen1= '" +
                        junc.Cov_Dimen1 + "' , Cov_Dimen2='" + junc.Cov_Dimen2 + "' , Cov_Dimen3='" + junc.Cov_Dimen3 + "', Chamber_Type='" +
                        junc.Chamber_Type + "',Chamber_Length='" + junc.Chamber_Length + "',Chamber_Width='" + junc.Chamber_Width + "',Chamber_Height='" +
                        junc.Chamber_Height + "',Survery_WaterDeep='" + junc.Survery_WaterDeep + "',Survery_SediDeep='" + junc.Survery_SediDeep +
                        "',Survery_Date='" + Convert.ToString(junc.Survery_Date) + "',Bottom_Style=" + junc.Bottom_Style + ",Junc_Class=" +
                        junc.Junc_Class + ",Status=" + junc.Status + ",Remark='" + junc.Remark + "' where ID=" + junc.ID;
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
        /// 在表中插入juncExtIndfo，并返回插入的ID
        /// </summary>
        /// <param name="junc"></param>
        /// <returns></returns>
        public bool Insert_JuncExtInfo(ref CJuncExtInfo junc)
        {
            MySqlDataReader reader;
            string strcmd = "INSERT INTO JunExtInfo (JuncID,Anci_Fac,Lane_Way,Cov_Material,Cov_Shape,Cov_Dimen1,Cov_Dimen2,"+
            "Cov_Dimen3,Chamber_Type,Chamber_Length,Chamber_Width,Chamber_Height,Survery_WaterDeep,Survery_SediDeep,Survery_Date,"+
            "Bottom_Style,Junc_Class,Status,Remark) values("+junc.JuncID+",'"+junc.Anci_Fac+"','"+junc.Lane_Way+"', "+junc.Cov_Material
            +" , "+junc.Cov_Shape+" ,'"+junc.Cov_Dimen1+"','"+junc.Cov_Dimen2+"','"+junc.Cov_Dimen3+"','"+junc.Chamber_Type+"','"+junc.Chamber_Length
            + "','" + junc.Chamber_Width + "','" + junc.Chamber_Height + "','" + junc.Survery_WaterDeep+"','"+junc.Survery_SediDeep+
            "','"+Convert.ToString(junc.Survery_Date)+"', "+junc.Bottom_Style+" , "+junc.Junc_Class+" , "+junc.Status+" ,'"+junc.Remark+"')";
            try
            {
                connect.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                //strcmd = "SELECT MAX(ID) AS MAXID FROM JunExtInfo";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT MAX(ID) AS MAXID FROM JunExtInfo";
                reader = cmd.ExecuteReader();
                reader.Read();
                junc.ID = Convert.ToInt32(reader[0].ToString());
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

        public bool Insert_JuncExtInfo(List<CJuncExtInfo> junclist)
        {
            string strcmd = "";
            MySqlTransaction trans = null;
            try
            {
                if (ConnectionState.Closed == connect.State)
                     connect.Open();

                MySqlCommand cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.Text;
                trans = connect.BeginTransaction();
                cmd.Transaction = trans;
                foreach (CJuncExtInfo junc in junclist)
                {
                    strcmd = "INSERT INTO JunExtInfo (ID,JuncID,Anci_Fac,Lane_Way,Cov_Material,Cov_Shape,Cov_Dimen1,Cov_Dimen2," +
             "Cov_Dimen3,Chamber_Type,Chamber_Length,Chamber_Width,Chamber_Height,Survery_WaterDeep,Survery_SediDeep,Survery_Date," +
             "Bottom_Style,Junc_Class,Status,Remark) values("+junc.ID+"," + junc.JuncID + ",'" + junc.Anci_Fac + "','" + junc.Lane_Way + "', " + junc.Cov_Material
             + " , " + junc.Cov_Shape + " ,'" + junc.Cov_Dimen1 + "','" + junc.Cov_Dimen2 + "','" + junc.Cov_Dimen3 + "','" + junc.Chamber_Type + "','" + junc.Chamber_Length
             + "','" + junc.Chamber_Width + "','" + junc.Chamber_Height + "','" + junc.Survery_WaterDeep + "','" + junc.Survery_SediDeep +
             "','" + Convert.ToString(junc.Survery_Date) + "', " + junc.Bottom_Style + " , " + junc.Junc_Class + " , " + junc.Status + " ,'" + junc.Remark + "')";

                    cmd.CommandText = strcmd;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                trans.Rollback();
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        /// <summary>
        /// 删除指定juncExt info
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public bool Delete_JuncExtInfo(CJuncExtInfo junc)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE  FROM JunExtInfo where ID = " + junc.ID;
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

        public bool Clear_JuncExtInfo()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE  FROM JunExtInfo where id>0";
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
        private List<CJuncExtInfo> Select(string cmd)
        {
            List<CJuncExtInfo> listjunc = new List<CJuncExtInfo>();
            MySqlCommand com;
            MySqlDataReader reader;

            try
            {
                connect.Open();
                com = new MySqlCommand(cmd, connect);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    CJuncExtInfo junc = new CJuncExtInfo();
                    int i = 0;
                    string tmp;
                    junc.ID = Convert.ToInt32(reader[i++].ToString());
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.JuncID = Convert.ToInt32(tmp);
                    junc.Anci_Fac = reader[i++].ToString();
                    junc.Lane_Way = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                         junc.Cov_Material = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Cov_Shape = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Cov_Dimen1 = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Cov_Dimen2 = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Cov_Dimen3 = Convert.ToDouble(tmp);
                    junc.Chamber_Type = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Chamber_Length = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Chamber_Width = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Chamber_Height = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Survery_WaterDeep = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Survery_SediDeep = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Survery_Date = Convert.ToDateTime(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Bottom_Style = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Junc_Class = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        junc.Status = Convert.ToInt32(tmp);
                    junc.Remark = reader[i++].ToString();
                    listjunc.Add(junc);
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

            return listjunc;
        }
    } 
}
