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
    public class TLogPipe : BaseTable
    {
        public TLogPipe(string path, string pw)
        {
           // if (path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<CLog_Pipe> Load_Log_Pipe()
        {
            string cmd = "SELECT * FROM [Log_Pipe]";
            return Select(cmd);
        }

        /// <summary>
        /// 根据pipeid获取相应的信息
        /// </summary>
        /// <param name="pipeid"></param>
        /// <returns></returns>
        public List<CLog_Pipe> Sel_Log_Pipe(int pipeid)
        {
            string cmd = "SELECT * FROM [Log_Pipe] where [PipeID] = " + pipeid;
            return Select(cmd);
        }

        public bool Update_Log_Pipe(List<CLog_Pipe> listpipe)
        {
            if (listpipe == null || listpipe.Count <= 0)
                return false;
            MySqlCommand com = new MySqlCommand();
            try
            {
                if(ConnectionState.Closed==connect.State)
                    connect.Open();
                com.Connection = connect;
                com.CommandType = CommandType.Text;
                foreach (CLog_Pipe pipe in listpipe)
                {
                    string cmdstr = "UPDATE [Log_Pipe] SET [PipeID]="+pipe.PipeID+" , [LogPath]='"+pipe.LogPath+"' where ID="+pipe.ID;
                    com.CommandText = cmdstr;
                    com.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Log error : "+ex.Message);
                return false;
            }
            finally
            {
               connect.Close();
            }
            return true;
        }

        public bool Insert_Log_Pipe(ref CLog_Pipe pipe)
        {
            MySqlDataReader reader;
            MySqlCommand cmd = null;
            string strcmd = "INSERT INTO [Log_Pipe]([PipeID],[LogPath]) values(" + pipe.PipeID + " ,'" + pipe.LogPath + "')";
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

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [Log_Pipe]";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Log error : " + ex.Message);
                connect.Close();
                Console.WriteLine("Reinsrt log:");
                
                return Reinsert(ref pipe);
            }
            finally
            {
                //connect.Close();
            }
            return true;
        }

        public bool Delete_Log_Pipe(CLog_Pipe pipe)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE FROM [Log_Pipe] where ID = " + pipe.ID;
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Log error : " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Clear_Log_Pipe()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [Log_Pipe]";
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Log error : " + ex.Message);
                return false;
            }
            return true;
        }
        private List<CLog_Pipe> Select(string cmd)
        {
            List<CLog_Pipe> listpipe = new List<CLog_Pipe>();
            MySqlCommand com;
            MySqlDataReader reader;

            try
            {
                if(connect.State == ConnectionState.Closed)
                    connect.Open();
                com = new MySqlCommand(cmd, connect);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    CLog_Pipe pipe = new CLog_Pipe();
                    int i = 0;
                    string tmp;
                    pipe.ID = Convert.ToInt32(reader[i++].ToString());
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.PipeID = Convert.ToInt32(tmp);
                    pipe.LogPath = reader[i++].ToString();
                    listpipe.Add(pipe);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Log error : " + ex.Message);
                return null;
            }
            finally
            {
                if(connect!=null)
                connect.Close();
            }
            return listpipe;
        }

        private bool Reinsert(ref CLog_Pipe pipe)
        {
            MySqlDataReader reader;
            MySqlCommand cmd = null;
            string strcmd = "INSERT INTO [Log_Pipe]([PipeID],[LogPath]) values(" + pipe.PipeID + " ,'" + pipe.LogPath + "')";
            try
            {
                connect.Open();
                cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [Log_Pipe]";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Log error : " + ex.Message);
                return false;
            }
            return true;
        }

        private int count = 0;
    }

    public class TPicPipe : BaseTable
    {
        public TPicPipe(string path , string pw)
        {
          if (path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<CPic_Pipe> Load_Pic_Pipe()
        {
            string cmd = "SELECT * FROM [Pic_Pipe]";
            return Select(cmd);
        }

        /// <summary>
        /// 根据pipeid获取相应的信息
        /// </summary>
        /// <param name="pipeid"></param>
        /// <returns></returns>
        public List<CPic_Pipe> Sel_Pic_Pipe(int pipeid)
        {
            string cmd = "SELECT * FROM [Pic_Pipe] where [PipeID] = " + pipeid;
            return Select(cmd);
        }

        public bool Update_Pic_Pipe(List<CPic_Pipe> listpipe)
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
                foreach (CPic_Pipe pipe in listpipe)
                {
                    string cmdstr = "UPDATE [Pic_Pipe] SET [PipeID]="+pipe.PipeID+" , [PicPath]='"+pipe.PicPath+"' where ID="+pipe.ID;
                    com.CommandText = cmdstr;
                    com.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        public bool Insert_Pic_Pipe(ref CPic_Pipe pipe)
        {
            MySqlDataReader reader;
            MySqlCommand cmd = null;
            string strcmd = "INSERT INTO [Pic_Pipe]([PipeID],[PicPath]) values("+pipe.PipeID+" ,'"+pipe.PicPath+"')";
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

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [Pic_Pipe]";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
            	Console.WriteLine("Pipe of Pic error : "+ex.Message);
                Console.WriteLine("Reinsert Pic:");
                connect.Close();
                return Reinsert(ref pipe);
            }
            finally
            {
                //connect.Close();
            }
            return true;
        }

        public bool Delete_Pic_Pipe(CPic_Pipe pipe)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [Pic_Pipe] where ID = " + pipe.ID;
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Clear_Pic_Pipe()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [Pic_Pipe]";
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return false;
            }
            return true;
        }

        private List<CPic_Pipe> Select(string cmd)
        {
            List<CPic_Pipe> listpipe = new List<CPic_Pipe>();
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
                    CPic_Pipe pipe = new CPic_Pipe();
                    int i = 0;
                    string tmp;
                    pipe.ID = Convert.ToInt32(reader[i++].ToString());
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.PipeID = Convert.ToInt32(tmp);
                    pipe.PicPath = reader[i++].ToString();
                    listpipe.Add(pipe);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return null;
            }
            finally
            {
                if(connect!=null)
                connect.Close();
            }
            return listpipe;
        }

        private bool Reinsert(ref CPic_Pipe pipe)
        {
            MySqlDataReader reader;
            MySqlCommand cmd = null;
            string strcmd = "INSERT INTO [Pic_Pipe]([PipeID],[PicPath]) values(" + pipe.PipeID + " ,'" + pipe.PicPath + "')";
            try
            {
                connect.Open();
                cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [Pic_Pipe]";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return false;
            }
            Console.WriteLine("Success!");
            return true;
        }

        private int count = 0;
    }

    public class TVideoPipe : BaseTable
    {
        public TVideoPipe(string path, string pw)
        {
            if (path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<CVideo_Pipe> Load_Video_Pipe()
        {
            string cmd = "SELECT * FROM [Video_Pipe]";
            return Select(cmd);
        }

        /// <summary>
        /// 根据pipeid获取相应的信息
        /// </summary>
        /// <param name="pipeid"></param>
        /// <returns></returns>
        public List<CVideo_Pipe> Sel_Video_Pipe(int pipeid)
        {
            string cmd = "SELECT * FROM [Video_Pipe] where [PipeID] = " + pipeid;
            return Select(cmd);
        }

        public bool Update_Video_Pipe(List<CVideo_Pipe> listpipe)
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
                foreach (CVideo_Pipe pipe in listpipe)
                {
                    string cmdstr = "UPDATE [Video_Pipe] SET [PipeID]=" + pipe.PipeID + " , [VideoPath]='" + pipe.VideoPath + "' where ID=" + pipe.ID;
                    com.CommandText = cmdstr;
                    com.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        public bool Insert_Video_Pipe(ref CVideo_Pipe pipe)
        {
            MySqlDataReader reader;
            MySqlCommand cmd = null;
            string strcmd = "INSERT INTO [Video_Pipe]([PipeID],[VideoPath]) values(" + pipe.PipeID + " ,'" + pipe.VideoPath + "')";
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

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [Video_Pipe]";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Video error : " + ex.Message);
                Console.WriteLine("Reinsert Video:");
                connect.Close();
                return Reinsert(ref pipe);
            }
            finally
            {
                //connect.Close();
            }
            return true;
        }

        public bool Delete_Video_Pipe(CVideo_Pipe pipe)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [Video_Pipe] where ID = " + pipe.ID;
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Clear_Video_Pipe()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [Video_Pipe]";
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return false;
            }
            return true;
        }

        private List<CVideo_Pipe> Select(string cmd)
        {
            List<CVideo_Pipe> listpipe = new List<CVideo_Pipe>();
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
                    CVideo_Pipe pipe = new CVideo_Pipe();
                    int i = 0;
                    string tmp;
                    pipe.ID = Convert.ToInt32(reader[i++].ToString());
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.PipeID = Convert.ToInt32(tmp);
                    pipe.VideoPath = reader[i++].ToString();
                    listpipe.Add(pipe);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return null;
            }
            finally
            {
                if(connect!=null)
                connect.Close();
            }
            return listpipe;
        }

        private bool Reinsert(ref CVideo_Pipe pipe)
        {
            MySqlDataReader reader;
            MySqlCommand cmd = null;
            string strcmd = "INSERT INTO [Video_Pipe]([PipeID],[VideoPath]) values(" + pipe.PipeID + " ,'" + pipe.VideoPath + "')";
            try
            {
                connect.Open();
                cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [Video_Pipe]";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return false;
            }
            Console.WriteLine("Success!");
            return true;
        }

        private int count = 0;
    }

    public class TReportPipe : BaseTable
    {
        public TReportPipe(string path, string pw)
        {
            if (path.Length > 0)
                ConnectDB(path, pw);
        }

        public List<CReport_Pipe> Load_Report_Pipe()
        {
            string cmd = "SELECT * FROM [Report_Pipe]";
            return Select(cmd);
        }

        /// <summary>
        /// 根据pipeid获取相应的信息
        /// </summary>
        /// <param name="pipeid"></param>
        /// <returns></returns>
        public List<CReport_Pipe> Sel_Report_Pipe(int pipeid)
        {
            string cmd = "SELECT * FROM [Report_Pipe] where [PipeID] = " + pipeid;
            return Select(cmd);
        }

        public bool Update_Report_Pipe(List<CReport_Pipe> listpipe)
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
                foreach (CReport_Pipe pipe in listpipe)
                {
                    string cmdstr = "UPDATE [Report_Pipe] SET [PipeID]=" + pipe.PipeID + " , [ReportPath]='" + pipe.ReportPath + "' where ID=" + pipe.ID;
                    com.CommandText = cmdstr;
                    com.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of report error : " + ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        public bool Insert_Report_Pipe(ref CReport_Pipe pipe)
        {
            MySqlDataReader reader;
            MySqlCommand cmd = null;
            string strcmd = "INSERT INTO [Report_Pipe]([PipeID],[ReportPath]) values(" + pipe.PipeID + " ,'" + pipe.ReportPath + "')";
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

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [Report_Pipe]";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Report error : " + ex.Message);
                Console.WriteLine("Reinsert Report");
                connect.Close();
                return Reinsert(ref pipe);
            }
            finally
            {
                //connect.Close();
            }
            return true;
        }

        public bool Delete_Report_Pipe(CReport_Pipe pipe)
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [Report_Pipe] where ID = " + pipe.ID;
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Clear_Report_Pipe()
        {
            List<string> listcmd = new List<string>();
            try
            {
                string cmd = "DELETE * FROM [Report_Pipe]";
                listcmd.Add(cmd);
                ExectueCmd(listcmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return false;
            }
            return true;
        }

        private List<CReport_Pipe> Select(string cmd)
        {
            List<CReport_Pipe> listpipe = new List<CReport_Pipe>();
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
                    CReport_Pipe pipe = new CReport_Pipe();
                    int i = 0;
                    string tmp;
                    pipe.ID = Convert.ToInt32(reader[i++].ToString());
                    tmp = reader[i++].ToString();
                    if (tmp != null && tmp.Length > 0)
                        pipe.PipeID = Convert.ToInt32(tmp);
                    pipe.ReportPath = reader[i++].ToString();
                    listpipe.Add(pipe);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Pic error : " + ex.Message);
                return null;
            }
            finally
            {
                if(connect!=null)
                connect.Close();
            }
            return listpipe;
        }

        private bool Reinsert(ref CReport_Pipe pipe)
        {
            MySqlDataReader reader;
            MySqlCommand cmd = null;
            string strcmd = "INSERT INTO [Report_Pipe]([PipeID],[ReportPath]) values(" + pipe.PipeID + " ,'" + pipe.ReportPath + "')";
            try
            {
               
                connect.Open();
                cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strcmd;

                strcmd = "SELECT MAX([ID]) AS MAXID FROM [Report_Pipe]";
                cmd.ExecuteNonQuery();
                cmd.CommandText = strcmd;
                reader = cmd.ExecuteReader();
                reader.Read();
                pipe.ID = Convert.ToInt32(reader[0].ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pipe of Report error : " + ex.Message);
                return false;
            }
            return true;
        }

        private int count = 0;
    }
}
