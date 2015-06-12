using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class CPumpStationInfo
    {
        private int id;
        public int ID
        {
            set{id = value;}
            get{return id;}
        }

        private string systemid;
        /// <summary>
        /// 排水系统ID,唯一标识
        /// </summary>
        public string SystemID
        {
            set { systemid = value; }
            get { return systemid; }
        }

        private double x_coor;
        /// <summary>
        /// 井盖x轴坐标
        /// </summary>
        public double X_Coor
        {
            set { x_coor = value; }
            get { return x_coor; }
        }

        private double y_coor;
        public double Y_Coor
        {
            set { y_coor = value; }
            get { return y_coor; }
        }

        private string pumpname;
        /// <summary>
        /// 泵站名称
        /// </summary>
        public string PumpName
        {
            set { pumpname = value; }
            get { return pumpname; }
        }

        private string pumpaddr;
        /// <summary>
        /// 泵站地址
        /// </summary>
        public string PumpAddr
        {
            set { pumpaddr = value; }
            get { return pumpaddr; }
        }

        private int ps_category1;
        /// <summary>
        /// 泵站大类：1-雨水；2-污水；3-合流；4-其他
        /// </summary>
        public int PS_Category1
        {
            set
            {
                if (value < 1 || value > 4)
                    ps_category1 = 4;
                else
                    ps_category1 = value;
            }
            get { return ps_category1; }
        }

        private int ps_category2;
        /// <summary>
        /// 泵站小类：1-雨水泵站；2-污水泵站；3-合流泵站；
        /// 4-地道泵站；5-泵闸；6-干线输送泵站；7-支线输送泵站；
        /// 8-合建泵站；9-污水处理厂泵站；10-其他
        /// </summary>
        public int PS_Category2
        {
            set
            {
                if (value < 1 || value > 10)
                    ps_category2 = 10;
                else
                    ps_category2 = value;
            }
            get { return ps_category2; }
        }

        private int ps_num;
        /// <summary>
        /// 泵总台数
        /// </summary>
        public int PS_Num
        {
            set { ps_num = value; }
            get { return ps_num; }
        }

        private double design_storm;
        /// <summary>
        /// 设计的雨水排水能力，单位：立方米/秒
        /// </summary>
        public double Design_Storm
        {
            set { design_storm = value; }
            get { return design_storm; }
        }

        private double design_sewer;
        /// <summary>
        /// 设计的污水排水能力，单位：立方米/秒
        /// </summary>
        public double Design_Sewer
        {
            set { design_sewer = value; }
            get { return design_sewer; }
        }

        private double min_level;
        /// <summary>
        /// 泵站允许的最低控制水位，单位：米
        /// </summary>
        public double Min_Level
        {
            set { min_level = value; }
            get { return min_level; }
        }

        private double control_level;
        /// <summary>
        /// 泵站的常规控制水位，单位：立方米/秒
        /// </summary>
        public double Control_Level
        {
            set { control_level = value; }
            get { return control_level; }
        }

        private double warnning_level;
        /// <summary>
        /// 泵站允许的最高运行水位，单位米
        /// </summary>
        public double Warnning_Level
        {
            set { warnning_level = value; }
            get { return warnning_level; }
        }

        private int datasource;
        /// <summary>
        /// 数据来源：1-设计图；2-竣工图；3-现场测绘；4-人工估计；5-其他
        /// </summary>
        public int DataSource
        {
            set
            {
                if (value > 5 || value < 1)
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
