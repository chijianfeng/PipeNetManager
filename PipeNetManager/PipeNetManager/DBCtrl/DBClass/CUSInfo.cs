using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class CUSInfo
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        private int pipeid;
        /// <summary>
        /// 排水管ID
        /// </summary>
        public int PipeID
        {
            set { pipeid = value; }
            get { return pipeid; }
        }

        private string jobid;
        /// <summary>
        /// 作业编号
        /// </summary>
        public string JobID
        {
            set { jobid = value; }
            get {
                if (jobid.Equals("\\"))
                    return "-";
                else
                    return jobid;
            }
        }   

        private DateTime detectdate;
        /// <summary>
        /// 检测日期，格式：yyyy-m-d
        /// </summary>
        public DateTime DetectDate
        {
            set { detectdate = value; }
            get
            {
                if (detectdate == null)
                    detectdate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                return detectdate;
            }
        }


        private string detectdep;
        /// <summary>
        /// 检测单位
        /// </summary>
        public string DetectDep
        {
            set { detectdep = value; }
            get { return detectdep; }
        }

        private string detect_person;
        /// <summary>
        /// 检测操作人员
        /// </summary>
        public string Detect_Person
        {
            set { detect_person = value; }
            get { return detect_person; }
        }

        private string contacts;
        /// <summary>
        /// 检测单位联系方式：地址、电话、电子邮箱
        /// </summary>
        public string Contacts
        {
            set { contacts = value; }
            get { return contacts; }
        }

        private int detect_method;
        /// <summary>
        /// 检测方法：1-CCTV；2-声纳；3-潜望镜；4-其他
        /// </summary>
        public int Detect_Method
        {
            set 
            {
                if(value<1||value>4)
                    detect_method = 4;
                else
                    detect_method = value; 
            }
            get { return detect_method; }
        }

        private int detect_dir;
        /// <summary>
        /// 检测方向：1-与水流向一致；2-与水流方向不一致
        /// </summary>
        public int Detect_Dir
        {
            set
            {
                if (value < 1 || value > 2)
                    detect_dir = 1;
                else
                    detect_dir = value;
            }
            get { return detect_dir; }
        }

        private string pipe_stop;
        /// <summary>
        /// 封堵情况
        /// </summary>
        public string Pipe_Stop
        {
            set { pipe_stop = value; }
            get { return pipe_stop; }
        }

        private int func_defect;
        /// <summary>
        /// 功能性缺失：0-无缺陷；1-沉积；2-结垢；3-障碍物；
        /// 4-残墙、坝根；5-树根；6-浮渣；7-封堵；8-其他
        /// </summary>
        public int Func_Defect
        {
            set
            {
                if (value < 1 || value > 8)
                    func_defect = 8;
                else
                    func_defect = value;
            }
            get { return func_defect; }
        }


        public int func_class;
        /// <summary>
        /// 根据城镇排水管道检测与评估技术规程确定
        /// </summary>
        public int Func_Class
        {
            set { func_class = value; }
            get { return func_class; }
        }

        private int strcut_defect;
        /// <summary>
        /// 结构性缺陷：0-无缺陷；1-破裂；2-变形；3-腐蚀；4-错口；
        /// 5-起伏；6-脱节；7-接口材料脱落；8-支管暗接；9-异物穿入；10-渗漏；11-其他
        /// </summary>
        public int Strcut_Defect
        {
            set
            {
                if (value < 1 || value > 11)
                    strcut_defect = 11;
                else
                    strcut_defect = value;
            }
            get { return strcut_defect; }
        }

        private int struct_class;
        /// <summary>
        /// 根据城镇排水管道检测与评估技术规程确定
        /// </summary>
        public int Struct_Class
        {
            set { struct_class = value; }
            get { return struct_class; }
        }

        private double repair_index;
        /// <summary>
        /// 修复指数：根据城镇排水管道检测与评估技术规程确定
        /// </summary>
        public double Repair_Index
        {
            set { repair_index = value; }
            get { return repair_index; }
        }

        private double matain_index;
        /// <summary>
        /// 养护指数：根据城镇排水管道检测与评估技术规程确定
        /// </summary>
        public double Matain_Index
        {
            set { matain_index = value; }
            get { return matain_index; }
        }

        private string problem;
        /// <summary>
        /// 缺陷描述
        /// </summary>
        public string Problem
        {
            set { problem = value; }
            get { return problem; }
        }

        private string video_filename;
        /// <summary>
        /// 检测影像文件的文件名，针对当前报告的影像名
        /// </summary>
        public string Video_Filename
        {
            set { video_filename = value; }
            get {
                if (video_filename.Equals("\\"))
                    return "-";
                else
                    return video_filename;
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
        /// 填报日期，格式：yyyy-m-d
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

        private bool dataisfull;
        /// <summary>
        /// 数据是否完整
        /// </summary>
        public bool DataIsFull
        {
            set { dataisfull = value; }
            get { return dataisfull; }
        }

        private string losereason;
        /// <summary>
        /// 数据缺失原因
        /// </summary>
        public string LoseReason
        {
            set { losereason = value; }
            get {
                if (losereason.Equals("\\"))
                    return "-";
                else
                    return losereason;
            }
        }

        private string remark;
        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }
    }
}
