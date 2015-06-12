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
    public class TPipeExtInfo : BaseTable
    {
        public TPipeExtInfo(string path, string pw)
        {
            //if (path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<CPipeExtInfo> Load_PipeExtInfo()
        {
            string cmd = "SELECT * FROM PipeExtInfo";
            return Select(cmd);
        }

        public List<CPipeExtInfo> Sel_PipeExtInfo(int pipeid)
        {
            string cmd = "SELECT * FROM PipeExtInfo where PipeID=" + pipeid;
            return Select(cmd);
        }

        /// <summary>
        /// 更新管道信息，管道ID必须在数据库中存在
        /// </summary>
        /// <param name="listpipe"></param>
        /// <returns></returns>
        public bool Update_PipeExtInfo(List<CPipeExtInfo> listpipe)
        {
            if (listpipe == null || listpipe.Count <= 0)
                return false;
            MySqlCommand com = new MySqlCommand();
            try
            {
                if (ConnectionState.Closed == connect.State)
                    connect.Open();
                com.Connection = connect;
                com.CommandType = CommandType.Text;
                foreach (CPipeExtInfo pipe in listpipe)
                {
                    string cmdstr = "UPDATE PipeExtInfo SET PipeID = " + pipe.PipeID + " ,Lane_Way='" + pipe.Lane_Way + "',Pressure_Type= " +
                        pipe.Pressure_Type + " ,Wall_Thick='" + pipe.Wall_Thick + "',Liner_Material= " + pipe.Liner_Material + " ,Conn_Type= " +
                        pipe.Conn_Type + " ,Pipe_Slop='" + pipe.Pipe_Slop + "',Invert_Silphon=" + Convert.ToInt32(pipe.Invert_Silphon) + " ,Origin_Strue=" +
                        pipe.Origin_Strue + " ,Constr_Method= " + pipe.Constr_Method + " ,Status= " + pipe.Status + " ,DataIsFull=" + Convert.ToInt32(pipe.DataIsFull)
                       + " ,LoseReason='"+pipe.LoseReason + "' ,Remark='" + pipe.Remark + "' where ID=" +
                        pipe.ID;
                    com.CommandText = cmdstr;
                    com.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of PipeExt error : " + ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        /// <summary>
        /// 插入一个管道信息，并返回管道插入的ID
        /// </summary>
        /// <param name="pipe"></param>
        /// <returns></returns>
        public bool Insert_PipeExtInfo(ref CPipeExtInfo pipe)
        {
            MySqlDataReader reader;
            MySqlCommand cmd;
            string strcmd = "INSERT INTO PipeExtInfo (PipeID,Lane_Way,Pressure_Type,Wall_Thick,Liner_Material,Conn_Type" +
                ",Pipe_Slop,Invert_Silphon,Origin_Strue,Constr_Method,Status,DataIsFull,LoseReason,Remark)" +
                "values(" +
                pipe.PipeID + " ,'" + pipe.Lane_Way + "', " + pipe.Pressure_Type + " ,'" + pipe.Wall_Thick + "', " + pipe.Liner_Material + " , " +
                pipe.Conn_Type + " ,'" + pipe.Pipe_Slop + "', " + Convert.ToInt32(pipe.Invert_Silphon) + " , " + pipe.Origin_Strue + " , " +
                pipe.Constr_Method + " , " + pipe.Status +" , "+Convert.ToInt32(pipe.DataIsFull)+ " ,'"+pipe.LoseReason+"','" + pipe.Remark + "')";
            try
            {
                if (ConnectionState.Closed == connect.State)
                {
                    connect.Open();
                    cmd = new MySqlCommand();
                    count = 0;
                }
                else if (count >= NUMBER)
                {
                    count = 0;
                    connect.Close();
                    mysqlcmd = new MySqlCommand();
                    cmd = mysqlcmd;
                    connect.Open();
                }
                else
                {
                    count++;
                    cmd = mysqlcmd.Clone();
                }
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX(ID) AS MAXID FROM PipeExtInfo";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of PipeExt error : " + ex.Message);
                Console.WriteLine("Reinsert PipeExt");
                connect.Close();
                return ReInsert(ref pipe);
            }
            finally
            {
                //connect.Close();
            }
            return true;
        }

        public bool Insert_PipeExtInfo(List<CPipeExtInfo>  listpipe)
        {
            if (listpipe == null || listpipe.Count <= 0)
                return false;
            MySqlTransaction trans = null;
            MySqlCommand cmd = null;
            string strcmd;
            try
            {
                if (ConnectionState.Closed == connect.State)
                {
                    connect.Open();
                }

                cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.Text;
                trans = connect.BeginTransaction();
                cmd.Transaction = trans;
                foreach(CPipeExtInfo pipe in listpipe)
                {
                     strcmd = "INSERT INTO PipeExtInfo (ID,PipeID,Lane_Way,Pressure_Type,Wall_Thick,Liner_Material,Conn_Type" +
               ",Pipe_Slop,Invert_Silphon,Origin_Strue,Constr_Method,Status,DataIsFull,LoseReason,Remark)" +
               "values(" +pipe.ID+","+
               pipe.PipeID + " ,'" + pipe.Lane_Way + "', " + pipe.Pressure_Type + " ,'" + pipe.Wall_Thick + "', " + pipe.Liner_Material + " , " +
               pipe.Conn_Type + " ,'" + pipe.Pipe_Slop + "', " + Convert.ToInt32(pipe.Invert_Silphon) + " , " + pipe.Origin_Strue + " , " +
               pipe.Constr_Method + " , " + pipe.Status + " , " + Convert.ToInt32(pipe.DataIsFull) + " ,'" + pipe.LoseReason + "','" + pipe.Remark + "')";
                    cmd.CommandText = strcmd;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of PipeExt error : " + ex.Message);
                trans.Rollback();
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }
        public bool Delete_PipeExtInfo(CPipeExtInfo pipe)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE  FROM PipeExtInfo where ID = " + pipe.ID;
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of PipeExt error : " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Clear_PipeExtInfo()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE  FROM PipeExtInfo where id>0";
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of PipeExt error : " + ex.Message);
                return false;
            }
            return true;
        }
        private List<CPipeExtInfo> Select(string cmd)
        {
            List<CPipeExtInfo> listpipe = new List<CPipeExtInfo>();
            MySqlCommand com;
            MySqlDataReader reader;

            try
            {
                if (ConnectionState.Closed == connect.State)
                    connect.Open();
                com = new MySqlCommand(cmd, connect);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    CPipeExtInfo pipe = new CPipeExtInfo();
                    int i = 0;
                    string tmp;
                    pipe.ID = Convert.ToInt32(reader[i++].ToString());
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.PipeID = Convert.ToInt32(tmp);
                    pipe.Lane_Way = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Pressure_Type = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Wall_Thick = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Liner_Material = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Conn_Type = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Pipe_Slop = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Invert_Silphon = Convert.ToBoolean(Convert.ToInt32(tmp));
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Origin_Strue = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Constr_Method = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Status = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.DataIsFull = Convert.ToBoolean(Convert.ToInt32(tmp));
                    pipe.LoseReason = reader[i++].ToString();
                    pipe.Remark = reader[i++].ToString();
                    listpipe.Add(pipe); 
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of PipeExt error : " + ex.Message);
                return null;
            }
            finally
            {
                connect.Close();
            }
            return listpipe;
        }

        private bool ReInsert(ref CPipeExtInfo pipe)
        {
            MySqlDataReader reader;
            string strcmd = "INSERT INTO PipeExtInfo (PipeID,Lane_Way,Pressure_Type,Wall_Thick,Liner_Material,Conn_Type" +
                ",Pipe_Slop,Invert_Silphon,Origin_Strue,Constr_Method,Status,DataIsFull,LoseReason,Remark)" +
                "values(" +
                pipe.PipeID + " ,'" + pipe.Lane_Way + "', " + pipe.Pressure_Type + " ,'" + pipe.Wall_Thick + "', " + pipe.Liner_Material + " , " +
                pipe.Conn_Type + " ,'" + pipe.Pipe_Slop + "', " + Convert.ToInt32(pipe.Invert_Silphon) + " , " + pipe.Origin_Strue + " , " +
                pipe.Constr_Method + " , " + pipe.Status + " , " + Convert.ToInt32(pipe.DataIsFull) + " ,'" + pipe.LoseReason + "','" + pipe.Remark + "')";
            try
            {
                connect.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX(ID) AS MAXID FROM PipeExtInfo";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ReINsert PipeExt Error : " + ex.Message);
                return false;
            }
            Console.WriteLine("Success!");
            return true;
        }

        protected int count = 0;
    }
}
