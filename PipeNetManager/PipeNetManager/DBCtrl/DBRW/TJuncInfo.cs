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
    public class TJuncInfo : BaseTable
    {
        public TJuncInfo(string path, string pw)
        {
          //  if (path.Length > 0)
                ConnectDB(path, pw);
        }

        /// <summary>
        /// 获取所有junction
        /// </summary>
        /// <returns></returns>
        public List<CJuncInfo> Load_JuncInfo()
        {
            string cmd = "SELECT * FROM JuncInfo";
            return Select(cmd);
        }

        /// <summary>
        /// 根据管道名称 查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<CJuncInfo> Sel_JuncInfo(string name)
        {
            string cmd = "SELECT * FROM JuncInfo where JuncName='"+name+"'";
            return Select(cmd);
        }

        public CJuncInfo Sel_JuncInfo(int  id)
        {
            string cmd = "SELECT * FROM JuncInfo where ID= " + id ;
            List<CJuncInfo> list = Select(cmd);
            if(list==null||list.Count<=0)
                return null;
            return list.ElementAt(0);
        }

        public List<CJuncInfo> Sel_JuncInfoByCaty(int caty)
        {
            string cmd = "SELECT * FROM JuncInfo where Junc_Category=" + caty;
            return Select(cmd);
        }

        /// <summary>
        /// 更新junction数据，其id必须和数据库相符
        /// </summary>
        /// <param name="listjunc"></param>
        /// <returns>bool</returns>
        public bool Update_JuncInfo(List<CJuncInfo> listjunc)
        {
            if (listjunc == null || listjunc.Count <= 0)
                return false;
            MySqlCommand com = new MySqlCommand();
            try
            {
                connect.Open();
                com.Connection = connect;
                com.CommandType = CommandType.Text;
                foreach (CJuncInfo junc in listjunc)
                {
                    string cmdstr = "UPDATE JuncInfo SET JuncName='" + junc.JuncName + "', SystemID='" + junc.SystemID +"',WorkingID='"+junc.WorkingID+
                        "',X_Coor='" +junc.X_Coor+"',Y_Coor='"+junc.Y_Coor+"',Junc_Category="+junc.Junc_Category+",Junc_Type="+
                        junc.Junc_Type+",Junc_Style="+junc.Junc_Style+",Depth='"+junc.Depth+"',Surface_Ele='"+junc.Surface_Ele+
                        "',DataSource="+junc.DataSource+",Record_Date='"+Convert.ToString(junc.Record_Date)+"',ReportDept='"+
                        junc.ReportDept+"',ReportDate='"+Convert.ToString(junc.ReportDate)+"',Junc_Darkjoint=" +
                        Convert.ToString(junc.Junc_Darkjoint) + " , Sewage_Line=" + Convert.ToString(junc.Sewage_Line) + " ,Junc_Error="
                        +junc.Junc_Error+" ,CCTV_CheckCode='"+junc.CCTV_CheckCode+"',FullData="+junc.FullData+",LoseReson='"+
                        junc.LoseReson + "',AUpTop='" + junc.Dis[0] + "',AUpBottom='" + junc.Dis[1] + "',ADownTop='"+junc.Dis[2]
                        + "',ADownBottom='" + junc.Dis[3] + "',BUpTop='" + junc.Dis[4] + "',BUpBottom='" + junc.Dis[5] + "',BDownTop='"+
                        junc.Dis[6] + "',BDownBottom='"+junc.Dis[7] + "' where ID=" + junc.ID;
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
        /// 插入junction 并返回其在数据库中的ID
        /// </summary>
        /// <param name="junc"></param>
        /// <returns></returns>
        public bool Insert_JuncInfo(ref CJuncInfo junc)
        {
            MySqlDataReader reader;
            string strcmd = "INSERT INTO JuncInfo(JuncName,SystemID,WorkingID,X_Coor,Y_Coor,Junc_Category,Junc_Type," +
            "Junc_Style,Depth,Surface_Ele,DataSource,Record_Date,ReportDept,ReportDate,Junc_Darkjoint,"+
            "Sewage_Line,Junc_Error,CCTV_CheckCode,FullData,LoseReson,AUpTop,AUpBottom,ADownTop,ADownBottom,"
            + "BUpTop,BUpBottom,BDownTop,BDownBottom) values('" + junc.JuncName + "','" + junc.SystemID + "','"+junc.WorkingID+"','"
            + junc.X_Coor + "','" 
            + junc.Y_Coor + "', " + junc.Junc_Category + " , " + junc.Junc_Type + " , " +junc.Junc_Style + " ,'" + junc.Depth 
            + "','" + junc.Surface_Ele + "', " + junc.DataSource + " ,'" + Convert.ToString(junc.Record_Date) + "','" +
            junc.ReportDept + "','" + Convert.ToString(junc.ReportDate) + "', "+Convert.ToString(junc.Junc_Darkjoint)+","
            +junc.Sewage_Line+","+junc.Junc_Error+",'"+junc.CCTV_CheckCode+"',"+junc.FullData+",'"+junc.LoseReson+"','"+junc.Dis[0]
            +"','"+junc.Dis[1]+"','"+junc.Dis[2]+"','"+junc.Dis[3]+"','"+junc.Dis[4]+"','"+junc.Dis[5]+"','"+junc.Dis[6]+"','"+junc.Dis[7]+"')";
            try
            {
                connect.Open();
                MySqlCommand cmd = new  MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                //strcmd = "SELECT MAX(ID) AS MAXID FROM JuncInfo";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT MAX(ID) AS MAXID FROM JuncInfo";
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

        public bool Insert_JuncInfo(List<CJuncInfo> listjunc , ref List<int> listid)
        {
            if (listjunc == null || listjunc.Count <= 0)
                return false;

            string strcmd;
            MySqlTransaction trans = null;
            MySqlCommand cmd  = null;
            try
            {
                if (ConnectionState.Closed == connect.State)
                {
                    connect.Open();
                    cmd = connect.CreateCommand();
                }
                else
                    cmd = connect.CreateCommand();
                trans = connect.BeginTransaction();
                cmd.Transaction = trans;
                foreach(CJuncInfo junc in listjunc)
                {
                    cmd.CommandType = CommandType.Text;
                    strcmd = "INSERT INTO JuncInfo(ID,JuncName,SystemID,WorkingID,X_Coor,Y_Coor,Junc_Category,Junc_Type," +
                    "Junc_Style,Depth,Surface_Ele,DataSource,Record_Date,ReportDept,ReportDate,Junc_Darkjoint,"+
                    "Sewage_Line,Junc_Error,CCTV_CheckCode,FullData,LoseReson,AUpTop,AUpBottom,ADownTop,ADownBottom,"
                    + "BUpTop,BUpBottom,BDownTop,BDownBottom) values("+junc.ID+",'"+ junc.JuncName + "','" + junc.SystemID + "','" +junc.WorkingID+"','"+ junc.X_Coor + "','" 
                    + junc.Y_Coor + "', " + junc.Junc_Category + " , " + junc.Junc_Type + " , " +junc.Junc_Style + " ,'" + junc.Depth 
                    + "','" + junc.Surface_Ele + "', " + junc.DataSource + " ,'" + Convert.ToString(junc.Record_Date) + "','" +
                    junc.ReportDept + "','" + Convert.ToString(junc.ReportDate) + "', "+Convert.ToString(junc.Junc_Darkjoint)+","
                    +junc.Sewage_Line+","+junc.Junc_Error+",'"+junc.CCTV_CheckCode+"',"+junc.FullData+",'"+junc.LoseReson+"','"+junc.Dis[0]
                    +"','"+junc.Dis[1]+"','"+junc.Dis[2]+"','"+junc.Dis[3]+"','"+junc.Dis[4]+"','"+junc.Dis[5]+"','"+junc.Dis[6]+"','"+junc.Dis[7]+"')";
                    cmd.CommandText = strcmd;

                    //strcmd = "SELECT MAX([ID]) AS MAXID FROM [JuncInfo]";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"select @@identity"; ;
                    int value = Int32.Parse(cmd.ExecuteScalar().ToString());
                    listid.Add(value);
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

        public bool Delete_JuncInfo(CJuncInfo junc)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE  FROM JuncInfo where ID = " + junc.ID;
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

        public bool Clear_JuncInfo()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE  FROM JuncInfo where id>0";
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

        private List<CJuncInfo> Select(string cmd)
        {
            List<CJuncInfo> listjunc = new List<CJuncInfo>();
            MySqlCommand com;
            MySqlDataReader reader;

            try
            {
                connect.Open();
                com = new MySqlCommand(cmd, connect);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    CJuncInfo juncinfo = new CJuncInfo();
                    int i = 0;
                    string tmp;
                    juncinfo.ID = Convert.ToInt32(reader[i++].ToString());
                    juncinfo.JuncName = reader[i++].ToString();
                    juncinfo.SystemID = reader[i++].ToString();
                    juncinfo.WorkingID = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.X_Coor = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.Y_Coor = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.Junc_Category = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.Junc_Type = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.Junc_Style = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.Depth = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.Surface_Ele = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.DataSource = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.Record_Date = Convert.ToDateTime(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.ReportDept = tmp;
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.ReportDate = Convert.ToDateTime(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.Junc_Darkjoint = Convert.ToBoolean(Convert.ToInt32(tmp));  //for mysql
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.Sewage_Line = Convert.ToBoolean(Convert.ToInt32(tmp));
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.Junc_Error = Convert.ToBoolean(Convert.ToInt32(tmp));
                    juncinfo.CCTV_CheckCode = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        juncinfo.FullData = Convert.ToBoolean(Convert.ToInt32(tmp));
                    juncinfo.LoseReson = reader[i++].ToString();
                    for (int j = 0; j < 8;j++ )
                    {
                        tmp = reader[i++].ToString();
                        if (tmp != null && tmp.Length > 0)
                            juncinfo.Dis[j] = Convert.ToDouble(tmp);
                    }
                        listjunc.Add(juncinfo);
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
