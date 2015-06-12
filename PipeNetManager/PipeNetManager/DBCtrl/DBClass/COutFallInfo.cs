using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class COutFallInfo
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
  
        private string systemid;
        /// <summary>
        /// 排水系统编码
        /// </summary>
        public string SystemID
        {
            set { systemid = value; }
            get { return systemid; }
        }

       
        private double x_coor; 
        /// <summary>
        /// x coordinate
        /// </summary>
        public double X_Coor
        {
            set { x_coor = value; }
            get { return x_coor; }
        }

       
        private double y_coor;
        /// <summary>
        ///  y coordinate
        /// </summary>
        public double Y_Coor
        {
            set { y_coor = value; }
            get { return y_coor; }
        }

        private string receivewater;
        /// <summary>
        /// 排往城市河流或者湖泊的受纳水体编码
        /// </summary>
        public string ReceiveWater
        {
            set { receivewater = value; }
            get { return receivewater; }
        }

        private int category;
        /// <summary>
        /// 管道类型 ：1-雨水；2-污水；3-合流；4-其他
        /// </summary>
        public int Category
        {
            set
            {
                if (value < 1 || value > 4)
                    category = 4;
                else
                    category = value;
            }
            get { return category; }
        }

        private bool isflap;
        /// <summary>
        /// 是否有拍门，0-否；1-是
        /// </summary>
        public bool IsFlap
        {
            set { isflap = value; }
            get { return isflap; }
        }

        private double botele;
        /// <summary>
        /// 排放口的底部高程，单位：米
        /// </summary>
        public double BotEle
        {
            set { botele = value; }
            get { return botele; }
        }

        private int outfalltype;
        /// <summary>
        /// 出流形式：1-自由出流；2-常水位淹没；3-潮汐影响
        /// </summary>
        public int OutFallType
        {
            set
            {
                if (value < 1 || value > 3)
                    outfalltype = 1;
                else
                    outfalltype = value;
            }
            get
            {
                return outfalltype;
            }
        }

        private int datasource;
        /// <summary>
        /// 数据来源：1-设计图；2-竣工图；3-现场测绘；4-人工估计；5-其他
        /// </summary>
        public int DataSource
        {
            set
            {
                if (value < 1 || value > 5)
                    datasource = 5;
                else
                    datasource = value;
            }
            get { return datasource; }
        }

        private DateTime record_date;
        /// <summary>
        /// 数据来源的具体时间格式：yyyy-mm-d
        /// </summary>
        public DateTime Record_Date
        {
            set { record_date = value; }
            get
            {
                if (record_date == null)
                    record_date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                return record_date;
            }
        }

        private string reportdept;
        /// <summary>
        /// 数据填报单位
        /// </summary>
        public string ReportDept
        {
            set { reportdept = value; }
            get { return reportdept; }
        }

        private DateTime reportdate;
        /// <summary>
        /// 数据填报时间：yyyy-mm-d;
        /// </summary>
        public DateTime ReportDate
        {
            set { reportdate = value; }
            get
            {
                if (reportdate == null)
                    reportdate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                return reportdate;
            }
        }
    }
}
