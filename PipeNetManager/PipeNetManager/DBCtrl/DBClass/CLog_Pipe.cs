using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class CLog_Pipe
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        private int pipeid;
        public int PipeID
        {
            set { pipeid = value; }
            get { return pipeid; }
        }

        private string logpath;
        public string LogPath
        {
            set { logpath = value; }
            get { return logpath; }
        }
    }

    public class CPic_Pipe
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        private int pipeid;
        public int PipeID
        {
            set { pipeid = value; }
            get { return pipeid; }
        }

        private string picpath;
        public string PicPath
        {
            set { picpath = value; }
            get { return picpath; }
        }
    }

    public class CVideo_Pipe
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        private int pipeid;
        public int PipeID
        {
            set { pipeid = value; }
            get { return pipeid; }
        }

        private string videopath;
        public string VideoPath
        {
            set { videopath = value; }
            get { return videopath; }
        }
    }

    public class CReport_Pipe
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        private int pipeid;
        public int PipeID
        {
            set { pipeid = value; }
            get { return pipeid; }
        }

        private string reportpath;
        public string ReportPath
        {
            set { reportpath = value; }
            get { return reportpath; }
        }
    }
}
