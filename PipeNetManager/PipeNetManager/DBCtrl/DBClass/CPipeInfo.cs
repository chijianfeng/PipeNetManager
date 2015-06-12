using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class CPipeInfo
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        private string pipename;
        /// <summary>
        /// 管道名称，命名规则：井盖-井盖
        /// </summary>
        public string PipeName
        {
            set { pipename = value; }
            get { return pipename; }
        }

        private string systemid;
        /// <summary>
        /// 所属排水系统编码
        /// </summary>
        public string SystemID
        {
            set { systemid = value; }
            get { return "0"; }
        }

        private int pipe_category;
        /// <summary>
        /// 管道类别：1-雨水管道、2-污水管道，3-合流管道；4-其他
        /// </summary>
        public int Pipe_Category
        {
            set
            {
                if(value<1||value>4)
                    pipe_category = 4;
                else
                    pipe_category = value;
            }
            get
            {
                return pipe_category;
            }
        }

        private double pipe_len;
        /// <summary>
        /// 管道长度，单位：米
        /// </summary>
        public double Pipe_Len
        {
            set { pipe_len = value; }
            get { return pipe_len; }
        }

        private int in_junid;
        /// <summary>
        /// 起点井盖ID，提取对应井盖的信息
        /// </summary>
        public int In_JunID
        {
            set { in_junid = value; }
            get { return in_junid; }
        }

        private int out_junid;
        /// <summary>
        /// 对应终点井盖ID，同上
        /// </summary>
        public int Out_JunID
        {
            set { out_junid = value; }
            get { return out_junid; }
        }

        
        private double in_upele;
        /// <summary>
        /// 起点管顶标高，单位：米
        /// </summary>
        public double In_UpEle
        {
            set { in_upele = value; }
            get { return in_upele; }
        }

        private double in_bottomele;
        /// <summary>
        /// 起点管底标高，单位：米
        /// </summary>
        public double In_BottomEle
        {
            set { in_bottomele = value; }
            get { return in_bottomele; }
        }

        private double out_upele;
        /// <summary>
        /// 对应终点管顶标高，单位：米
        /// </summary>
        public double Out_UpEle
        {
            set { out_upele = value; }
            get { return out_upele; }
        }

        private double out_bottomele;
        /// <summary>
        /// 对应终点管底标高，单位：米
        /// </summary>
        public double Out_BottomEle
        {
            set { out_bottomele = value; }
            get { return out_bottomele; }
        }

        private int shapetype;
        /// <summary>
        /// 断面形式：1-圆形；2-梯形；3-三角形；4-椭圆形；5-矩形，6-不规则形状
        /// </summary>
        public int ShapeType
        {
            set
            {
                if (value < 1 || value > 6)
                    shapetype = 1;
                else
                    shapetype = value;
            }
            get
            {
                return shapetype;
            }
        }

        private string shapedata;
        /// <summary>
        /// 断面数据
        /// </summary>
        public string ShapeData
        {
            set { shapedata = value; }
            get {
                if (shapedata.CompareTo("\\") == 0)
                    return "-";
                else
                    return shapedata;
            }
        }

        private double shape_data1;
        /// <summary>
        /// 断面形式为圆形时填写直径，断面形式为其他形式时填深度，单位：米
        /// </summary>
        public double Shape_Data1
        {
            set { shape_data1 = value; }
            get { return shape_data1; }
        }

        private double shape_data2;
        /// <summary>
        /// 断面形式为矩形时填写宽度；断面形式为梯形时添加底部宽度；
        /// 断面形式为三角形时填写底面宽度；断面形式为椭圆时填写最大宽度
        /// </summary>
        public double Shape_Data2
        {
            set { double shape_data2 = value; }
            get { return shape_data2; }
        }

        private double shape_data3;
        /// <summary>
        /// 断面形式为梯形时填写左侧边坡的宽度和高度之比
        /// </summary>
        public double Shape_Data3
        {
            set { double shape_data3 = value; }
            get { return shape_data3; }
        }

        private double shape_data4;
        /// <summary>
        /// 断面形式为梯形时填写右侧边坡的宽度和高度之比
        /// </summary>
        public double Shape_Data4
        {
            set { double shape_data4 = value; }
            get { return shape_data4; }
        }

        private int material;
        /// <summary>
        /// 管道材质：1-混凝土管；2-钢筋混凝土管；3-陶土管；
        /// 4-PE（聚乙烯）管；5-HDPE(高密度聚乙烯）管；6-UPVC管；7-铸铁管；
        /// 8-玻璃钢夹沙管；9-钢管；10-石棉水泥管；11-其他
        /// </summary>
        public int Material
        {
            set
            {
                if (value < 1 || value > 11)
                    material = 11;
                else
                    material = value;
            }
            get { return material; }
        }

        private double roughness;
        /// <summary>
        /// 粗糙程度，
        /// </summary>
        public double Roughness
        {
            set { roughness = value; }
            get { return roughness; }
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
