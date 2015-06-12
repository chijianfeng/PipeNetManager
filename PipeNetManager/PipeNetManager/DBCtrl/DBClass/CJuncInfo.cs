using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class CJuncInfo
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        private string juncname;
        /// <summary>
        /// 井盖名称
        /// </summary>
        public string JuncName
        {
            set { juncname = value; }
            get { return juncname; }
        }

        private string systemid;
        /// <summary>
        /// 排水系统ID,唯一标识
        /// </summary>
        public string SystemID
        {
            set { systemid = value; }
            get {
                if (systemid != null)
                    return "0";
                else
                    return "0";
            }
        }

        private string workingid;
        /// <summary>
        /// 施工图ID，
        /// </summary>
        public string WorkingID
        {
            set { workingid = value; }
            get {
                if (workingid != null)
                    return workingid;
                else
                    return "0";
            }
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

        private int junc_category;
        /// <summary>
        /// 检查井类别：1-雨水；2-污水井；3-合流井；4-其他
        /// </summary>
        public int Junc_Category
        {
            set
            {
                if (value > 4 || value < 1)
                    junc_category = 4;
                else
                    junc_category = value;
            }

            get { return junc_category; }
        }

        private int junc_type;
        /// <summary>
        /// //检查井类型：1-排水井；2-接户井；3-闸阀井；4-溢流井；5-倒虹井；6-透气井；
        ///7压力井；8-检测井；9-拍门井；10-截流井；11-水封井；12-跌水井；13其他
        /// </summary>
        public int Junc_Type
        {
            set
            {
                if (value < 1 || value > 13)
                    junc_type = 13;
                else
                    junc_type = value;
            }
            get { return junc_type; }
        }

        private int junc_style;
        /// <summary>
        /// 检查井形式：1-一通；2-二通直；3-二通转；4-三通；5-四通；6-五通；7-五通以上；8-暗井；9-侧立型；10-平面型；11-其他；
        /// </summary>
        public int Junc_Style
        {
            set
            {
                if (value < 1 || value > 11)
                    junc_style = 11;
                else
                    junc_style = value;
            }
            get { return junc_style; }
        }

        private double depth;
        /// <summary>
        /// 井深，单位：米
        /// </summary>
        public double Depth
        {
            set { depth = value; }
            get { return depth; }
        }

        private double surface_ele;
        /// <summary>
        /// 井盖所处的地面高程，单位：米
        /// </summary>
        public double Surface_Ele
        {
            set { surface_ele = value; }
            get { return surface_ele; }
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

        private bool junc_darkjoint;
        /// <summary>
        /// 管道是否暗接(检查井内有没有企业违规私自暗接的管道)：是-1，否-0
        /// </summary>
        public bool Junc_Darkjoint
        {
            set { junc_darkjoint = value; }
            get { return junc_darkjoint; }
        }

        private bool sewage_line;
        /// <summary>
        /// 污水是否直排(有没有企业通过暗接管道排污水)：是-1，否-0
        /// </summary>
        public bool Sewage_Line
        {
            set { sewage_line = value; }
            get { return sewage_line; }
        }

        private bool junc_error;
        /// <summary>
        /// 井盖错误(检查井的井盖有没有错误，比方说雨水井的井盖错成污水井)：是-1，否-0
        /// </summary>
        public bool Junc_Error
        {
            set { junc_error = value; }
            get { return junc_error; }
        }

        /// <summary>
        /// 校验的CCTV检测编号
        /// </summary>
        private string cctv_ckeckcode;
        public string CCTV_CheckCode
        {
            set
            {
                cctv_ckeckcode = value;
            }
            get { 
                if(cctv_ckeckcode.CompareTo("\\")==0)
                    return "-";
                return cctv_ckeckcode;
            }
        }

        /// <summary>
        /// 数据是否完整
        /// </summary>
        private bool fulldata;
        public bool FullData
        {
            set { fulldata = value; }
            get { return fulldata; }
        }

        /// <summary>
        /// 数据缺失原因
        /// </summary>
        private string losereson;
        public string LoseReson
        {
            set { losereson = value; }
            get { return losereson; }
        }

        /// <summary>
        ///A上游井口至管顶距离
        ///A上游井口至管底距离
        ///A下游井口至管顶距离
        ///A下游井口至管底距离
        ///B上游井口至管顶距离
        ///B上游井口至管底距离
        ///B下游井口至管顶距离
        ///B下游井口至管底距离
        /// </summary>
        public double[] Dis  = new double[8];       
    }
}
