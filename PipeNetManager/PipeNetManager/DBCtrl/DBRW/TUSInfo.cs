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
    public class TUSInfo : BaseTable
    {
        public TUSInfo(string path, string pw)
        {
           // if (path != null && path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<CUSInfo> Load_USInfo()
        {
            string cmd = "SELECT * FROM USInfo";
            return Select(cmd);
        }

        public List<CUSInfo> Sel_USInfo(int pipeid)
        {
            string cmd = "SELECT * FROM USInfo where PipeID =" + pipeid;
            return Select(cmd);
        }

        public bool Update_USInfo(List<CUSInfo> listus)
        {
            if (listus == null || listus.Count <= 0)
                return false;
            MySqlCommand com = new MySqlCommand();
            try
            {
                if (ConnectionState.Closed == connect.State)
                    connect.Open();
                com.Connection = connect;
                com.CommandType = CommandType.Text;
                foreach (CUSInfo us in listus)
                {
                    string cmdstr = "UPDATE USInfo SET PipeID=" + us.PipeID + " ,JobID='" +us.JobID+ "',DetectDate='" + Convert.ToString(us.DetectDate)
                        + "',DetectDep='" + us.DetectDep + "',Detect_Person='" + us.Detect_Person + "',Contacts='" + us.Contacts + "',Detect_Method=" + us.Detect_Method +
                        " ,Detect_Dir=" + us.Detect_Dir + " ,Pipe_Stop='" + us.Pipe_Stop + "',Func_Defect="+us.Func_Defect+" ,Func_Class="+
                        us.Func_Class + ",Strcut_Defect=" + us.Strcut_Defect + ",Struct_Class=" + us.Struct_Class + " ,Repire_Index='"+
                        us.Repair_Index + "',Matain_Index='" + us.Matain_Index + "',Problem='" + us.Problem + "',Video_Filename='"+
                        us.Video_Filename + "',ReportDept='" + us.ReportDept + "',ReportDate='"+Convert.ToString(us.ReportDate)+
                        "',DataIsFull=" + Convert.ToString(us.DataIsFull) + ",LoseReason='" +us.LoseReason+ "',Remark='" + us.Remark + "'where ID=" + us.ID;

                    com.CommandText = cmdstr;
                    com.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Usinfo error : " + ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        public bool Insert_USInfo(ref CUSInfo us)
        {
            MySqlDataReader reader;
            MySqlCommand cmd = null;
            string strcmd = "INSERT INTO USInfo (PipeID,JobID,DetectDate,DetectDep,Detect_Person,Contacts,Detect_Method,Detect_Dir" +
                ",Pipe_Stop,Func_Defect,Func_Class,Strcut_Defect,Struct_Class,Repire_Index,Matain_Index,Problem," +
                "Video_Filename,ReportDept,ReportDate,DataIsFull,LoseReason,Remark" +
                ")values(" +
                us.PipeID+",'"+us.JobID+"','"+Convert.ToDateTime(us.DetectDate)+"','"+us.DetectDep+"','"+us.Detect_Person+"','"+us.Contacts+"',"+us.Detect_Method
                +","+us.Detect_Dir+",'"+us.Pipe_Stop+"',"+us.Func_Defect+","+us.Func_Class+","+us.Strcut_Defect+","+us.Struct_Class+",'"+
                us.Repair_Index+"','"+us.Matain_Index+"','"+us.Problem+"','"+us.Video_Filename+"','"+us.ReportDept+"','"+
                Convert.ToString(us.ReportDate)+"',"+Convert.ToString(us.DataIsFull)+",'"+us.LoseReason+"','"+us.Remark+
                 "')";
            try
            {
                if (ConnectionState.Closed == connect.State)
                {
                    connect.Open();
                    cmd = new MySqlCommand();
                    count = 0;
                }
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX(ID) AS MAXID FROM USInfo";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                us.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Usinfo error : " + ex.Message);
                Console.WriteLine("Reinsert Usinfo");
                connect.Close();
                return ReInsert(ref us);
            }
            finally
            {
               // connect.Close();
            }
            return true;
        }

        public bool Insert_USInfo(List<CUSInfo> listus)
        {
            if (listus == null || listus.Count <= 0)
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
                foreach(CUSInfo us in listus)
                {
                    strcmd = "INSERT INTO USInfo (ID , PipeID,JobID,DetectDate,DetectDep,Detect_Person,Contacts,Detect_Method,Detect_Dir" +
                ",Pipe_Stop,Func_Defect,Func_Class,Strcut_Defect,Struct_Class,Repire_Index,Matain_Index,Problem," +
                "Video_Filename,ReportDept,ReportDate,DataIsFull,LoseReason,Remark" +
                ")values(" +us.ID+","+
                us.PipeID + ",'" + us.JobID + "','" + Convert.ToDateTime(us.DetectDate) + "','" + us.DetectDep + "','" + us.Detect_Person + "','" + us.Contacts + "'," + us.Detect_Method
                + "," + us.Detect_Dir + ",'" + us.Pipe_Stop + "'," + us.Func_Defect + "," + us.Func_Class + "," + us.Strcut_Defect + "," + us.Struct_Class + ",'" +
                us.Repair_Index + "','" + us.Matain_Index + "','" + us.Problem + "','" + us.Video_Filename + "','" + us.ReportDept + "','" +
                Convert.ToString(us.ReportDate) + "'," + Convert.ToString(us.DataIsFull) + ",'" + us.LoseReason + "','" + us.Remark +
                 "')";
                    cmd.CommandText = strcmd;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
               
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Usinfo error : " + ex.Message);
                trans.Rollback();
                return false;
            }
            finally
            {
                 connect.Close();
            }
            return true;
        }
        public bool Delete_USInfo(CUSInfo us)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE  FROM USInfo where ID = " + us.ID;
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Usinfo error : " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Clear_USInfo()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE  FROM USInfo where id>0";
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Usinfo error : "+ex.Message);
                return false;
            }
            return true;
        }

        private List<CUSInfo> Select(string cmd)
        {
            List<CUSInfo> listus = new List<CUSInfo>();
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
                    CUSInfo us = new CUSInfo();
                    int i = 0;
                    string tmp;
                    us.ID = Convert.ToInt32(reader[i++].ToString());
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.PipeID = Convert.ToInt32(tmp);
                    us.JobID = reader[i++].ToString(); 

                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.DetectDate = Convert.ToDateTime(tmp);
                    us.DetectDep = reader[i++].ToString();
                    us.Detect_Person = reader[i++].ToString();
                    us.Contacts = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.Detect_Method = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.Detect_Dir = Convert.ToInt32(tmp);
                    us.Pipe_Stop = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.Func_Defect = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.Func_Class = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.Strcut_Defect = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.Struct_Class = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.Repair_Index = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.Matain_Index = Convert.ToInt32(tmp);
                    us.Problem = reader[i++].ToString();
                    us.Video_Filename = reader[i++].ToString();
                    us.ReportDept = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.ReportDate = Convert.ToDateTime(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        us.DataIsFull = Convert.ToBoolean(Convert.ToInt32(tmp));
                    us.LoseReason = reader[i++].ToString();
                    us.Remark = reader[i++].ToString();
                    listus.Add(us);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Usinfo error : " + ex.Message);
                return null;
            }
            finally
            {
                //connect.Close();
            }
            return listus;
        }

        private bool ReInsert(ref CUSInfo us)
        {
            MySqlDataReader reader;
            string strcmd = "INSERT INTO USInfo (PipeID,JobID,DetectDate,DetectDep,Detect_Person,Contacts,Detect_Method,Detect_Dir" +
                ",Pipe_Stop,Func_Defect,Func_Class,Strcut_Defect,Struct_Class,Repire_Index,Matain_Index,Problem," +
                "Video_Filename,ReportDept,ReportDate,DataIsFull,LoseReason,Remark" +
                ")values(" +
                us.PipeID + ",'" + us.JobID + "','" + Convert.ToDateTime(us.DetectDate) + "','" + us.DetectDep + "','" + us.Detect_Person + "','" + us.Contacts + "'," + us.Detect_Method
                + "," + us.Detect_Dir + ",'" + us.Pipe_Stop + "'," + us.Func_Defect + "," + us.Func_Class + "," + us.Strcut_Defect + "," + us.Struct_Class + ",'" +
                us.Repair_Index + "','" + us.Matain_Index + "','" + us.Problem + "','" + us.Video_Filename + "','" + us.ReportDept + "','" +
                Convert.ToString(us.ReportDate) + "'," + Convert.ToString(us.DataIsFull) + ",'" + us.LoseReason + "','" + us.Remark +
                 "')";
            try
            {
                
                connect.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX(ID) AS MAXID FROM USInfo";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                us.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Usinfo error : " + ex.Message);
                return false;
            }
            Console.WriteLine("Success!");
            return true;
        }

        private int count = 0;
    }
}
