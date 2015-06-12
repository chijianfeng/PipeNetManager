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
    public class TPipeInfo : BaseTable
    {
        public TPipeInfo(string path, string pw)
        {
           // if (path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<CPipeInfo> Load_PipeInfo()
        {
            string cmd = "SELECT * FROM PipeInfo";
            return Select(cmd);
        }

        /// <summary>
        /// 根据管道名称，获取数据
        /// </summary>
        /// <param name="pipename"></param>
        /// <returns></returns>
        public List<CPipeInfo> Sel_PipeInfo(string pipename)
        {
            string cmd = "SELECT * FROM PipeInfo where PipeName='" + pipename + "'";
            return Select(cmd);
        }

        /// <summary>
        /// 根据管道类型进行选择
        /// </summary>
        /// <param name="catelogy"></param>
        /// <returns></returns>
        public List<CPipeInfo> Sel_PipeInfo(int catelogy)
        {
            string cmd = "SELECT * FROM PipeInfo where Pipe_Category=" + catelogy;
            return Select(cmd);
        }

        /// <summary>
        /// 更新Pipe信息
        /// </summary>
        /// <param name="listpipe"></param>
        /// <returns></returns>
        public bool Update_PipeInfo(List<CPipeInfo> listpipe)
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
                foreach (CPipeInfo pipe in listpipe)
                {
                    string cmdstr = "UPDATE PipeInfo SET PipeName ='" + pipe.PipeName + "',SystemID='" + pipe.SystemID +
                        "',Pipe_Category=" + pipe.Pipe_Category + " ,Pipe_Len='" + pipe.Pipe_Len + "',In_JunID= " + pipe.In_JunID +
                        " ,Out_JunID= " + pipe.Out_JunID + " ,In_UpEle='" + pipe.In_UpEle +"',In_BottomEle='"+ pipe.In_BottomEle+
                        "',Out_UpEle='"+pipe.Out_UpEle+"',Out_BottomEle='" + pipe.Out_BottomEle + "',ShapeType="+
                        pipe.ShapeType+" ,ShapeData='"+pipe.ShapeData+"' ,Shape_Data1='"+pipe.Shape_Data1+"',Shape_Data2='"+pipe.Shape_Data2+"',Shape_Data3='"+
                        pipe.Shape_Data3 + "',Shape_Data4='" + pipe.Shape_Data4 + "',Material=" + pipe.Material + " ,Roughness='"+
                        pipe.Roughness + "',DataSource=" + pipe.DataSource + " ,Record_Data='"+Convert.ToString(pipe.Record_Date)+
                        "',ReportDept='" + pipe.ReportDept + "',ReportDate='"+pipe.ReportDate+"' where ID="+pipe.ID;
                    com.CommandText = cmdstr;
                    com.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pipe error : " + ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        public bool Insert_PipeInfo(ref CPipeInfo pipe)
        {
            MySqlDataReader reader;
            MySqlCommand cmd;
            string strcmd = "INSERT INTO PipeInfo (PipeName,SystemID,Pipe_Category,Pipe_Len,In_JunID,Out_JunID," +
                "In_UpEle,In_BottomEle,Out_UpEle,Out_BottomEle,ShapeType,ShapeData,Shape_Data1,Shape_Data2,Shape_Data3,"+
                "Shape_Data4,Material,Roughness,DataSource,Record_Data,ReportDept,ReportDate" +
                ")values(" +
                "'"+pipe.PipeName+"','"+pipe.SystemID+"',"+pipe.Pipe_Category+" ,'"+pipe.Pipe_Len+"', "+pipe.In_JunID+" , "+pipe.Out_JunID
                +" ,'"+pipe.In_UpEle+"','"+pipe.In_BottomEle+"','"+pipe.Out_UpEle+"','"+pipe.Out_BottomEle+"', "+pipe.ShapeType+" ,'"+
                pipe.ShapeData+"','"+pipe.Shape_Data1+"','"+pipe.Shape_Data2+"','"+pipe.Shape_Data3
                +"','"+pipe.Shape_Data4+"', "+pipe.Material+" ,'"+pipe.Roughness+"', "+pipe.DataSource+" ,'"+Convert.ToString(pipe.Record_Date)
                +"','"+pipe.ReportDept+"','"+Convert.ToString(pipe.ReportDate)+"')";
            try
            {
                if (ConnectionState.Closed == connect.State)
                {
                    connect.Open();
                    cmd = new MySqlCommand();
                }
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
                  //  cmd = olecmd;
                    count++;
                    cmd = mysqlcmd.Clone();
                }
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX(ID) AS MAXID FROM PipeInfo";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pipe error : " + ex.Message);
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

        public bool Insert_PipeInfo(List<CPipeInfo> listpipe , ref List<int> listid)
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
                foreach (CPipeInfo pipe in listpipe)
                {
                     strcmd = "INSERT INTO PipeInfo (ID,PipeName,SystemID,Pipe_Category,Pipe_Len,In_JunID,Out_JunID," +
                "In_UpEle,In_BottomEle,Out_UpEle,Out_BottomEle,ShapeType,ShapeData,Shape_Data1,Shape_Data2,Shape_Data3," +
                "Shape_Data4,Material,Roughness,DataSource,Record_Data,ReportDept,ReportDate" +
                ")values(" +pipe.ID+","+
                "'" + pipe.PipeName + "','" + pipe.SystemID + "'," + pipe.Pipe_Category + " ,'" + pipe.Pipe_Len + "', " + pipe.In_JunID + " , " + pipe.Out_JunID
                + " ,'" + pipe.In_UpEle + "','" + pipe.In_BottomEle + "','" + pipe.Out_UpEle + "','" + pipe.Out_BottomEle + "', " + pipe.ShapeType + " ,'" +
                pipe.ShapeData + "','" + pipe.Shape_Data1 + "','" + pipe.Shape_Data2 + "','" + pipe.Shape_Data3
                + "','" + pipe.Shape_Data4 + "', " + pipe.Material + " ,'" + pipe.Roughness + "', " + pipe.DataSource + " ,'" + Convert.ToString(pipe.Record_Date)
                + "','" + pipe.ReportDept + "','" + Convert.ToString(pipe.ReportDate) + "')";
                    cmd.CommandText = strcmd;

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"select @@identity"; ;
                    int value = Int32.Parse(cmd.ExecuteScalar().ToString());
                    listid.Add(value);
                    
                }
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pipe error : " + ex.Message);
                trans.Rollback();
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }
        public bool Delete_PipeInfo(CPipeInfo pipe)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE  FROM [PipeInfo] where ID = " + pipe.ID;
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pipe error : " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Clear_PipeInfo()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE  FROM PipeInfo where id>0";
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pipe error : " + ex.Message);
                return false;
            }
            return true;
        }

        private List<CPipeInfo> Select(string cmd)
        {
            List<CPipeInfo> listpipe = new List<CPipeInfo>();
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
                    CPipeInfo pipe = new CPipeInfo();
                    int i = 0;
                    string tmp;
                    pipe.ID = Convert.ToInt32(reader[i++].ToString());
                    pipe.PipeName = reader[i++].ToString();
                    pipe.SystemID = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Pipe_Category = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Pipe_Len = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.In_JunID = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Out_JunID = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.In_UpEle = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.In_BottomEle = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Out_UpEle = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Out_BottomEle = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.ShapeType = Convert.ToInt32(tmp);

                    pipe.ShapeData = reader[i++].ToString(); ;
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Shape_Data1 = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Shape_Data2 = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Shape_Data3 = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Shape_Data4 = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Material = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Roughness = Convert.ToDouble(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.DataSource = Convert.ToInt32(tmp);
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.Record_Date = Convert.ToDateTime(tmp);
                    pipe.ReportDept = reader[i++].ToString();
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.ReportDate = Convert.ToDateTime(tmp);
                    listpipe.Add(pipe);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pipe error : " + ex.Message);
                return null;
            }
            finally
            {
                connect.Close();
            }
            return listpipe;
        }

        private bool ReInsert(ref CPipeInfo pipe)
        {
            MySqlDataReader reader;
            string strcmd = "INSERT INTO PipeInfo (PipeName,SystemID,Pipe_Category,Pipe_Len,In_JunID,Out_JunID," +
                "In_UpEle,In_BottomEle,Out_UpEle,Out_BottomEle,ShapeType,ShapeData,Shape_Data1,Shape_Data2,Shape_Data3," +
                "Shape_Data4,Material,Roughness,DataSource,Record_Data,ReportDept,ReportDate" +
                ")values(" +
                "'" + pipe.PipeName + "','" + pipe.SystemID + "'," + pipe.Pipe_Category + " ,'" + pipe.Pipe_Len + "', " + pipe.In_JunID + " , " + pipe.Out_JunID
                + " ,'" + pipe.In_UpEle + "','" + pipe.In_BottomEle + "','" + pipe.Out_UpEle + "','" + pipe.Out_BottomEle + "', " + pipe.ShapeType + " ,'" +
                pipe.ShapeData + "','" + pipe.Shape_Data1 + "','" + pipe.Shape_Data2 + "','" + pipe.Shape_Data3
                + "','" + pipe.Shape_Data4 + "', " + pipe.Material + " ,'" + pipe.Roughness + "', " + pipe.DataSource + " ,'" + Convert.ToString(pipe.Record_Date)
                + "','" + pipe.ReportDept + "','" + Convert.ToString(pipe.ReportDate) + "')";
            try
            {   
                connect.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX(ID) AS MAXID FROM PipeInfo";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ReInsert : " + ex.Message);
                connect.Close();
                return false;
            }
            Console.WriteLine("Success!");
            return true;
        }

        private int count = 0;
    }
}
