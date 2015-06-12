using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class CSystemBase
    {
        private string systemid;
        /// <summary>
        /// 排水系统编码
        /// </summary>
        public string SystemID
        {
            set { systemid = value; }
            get { return systemid; }
        }

        private string sysname;
        /// <summary>
        /// 排水系统的名称
        /// </summary>
        public string SysName
        {
            set { sysname = value; }
            get { return sysname; }
        }

        private int systype;
        /// <summary>
        /// 1-雨水；2-污水；3-合流；4-其他
        /// </summary>
        public int SysType
        {
            set
            {
                if (value < 1 || value > 4)
                    systype = 4;
                else
                    systype = value;
            }
            get { return systype; }
        }

        private DateTime updatedate;
        /// <summary>
        /// 最新修改日期
        /// </summary>
        public DateTime UpdateDate
        {
            set { updatedate = value; }
            get
            {
                if (updatedate == null)
                    updatedate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                return updatedate;
            }
        }

        private string estdept;
        /// <summary>
        /// 创建该记录的单位
        /// </summary>
        public string EstDept
        {
            set { estdept = value; }
            get { return estdept; }
        }

        private string orgdept;
        /// <summary>
        /// 数据维护单位
        /// </summary>
        public string OrgDept
        {
            set { orgdept = value; }
            get { return orgdept; }
        }

        private string coorsys;
        /// <summary>
        /// 数据采用的坐标系统
        /// </summary>
        public string CoorSys
        {
            set { coorsys = value; }
            get { return coorsys; }
        }

        private string elesys;
        /// <summary>
        /// 高程系统
        /// </summary>
        public string EleSys
        {
            set { elesys = value; }
            get { return elesys; }
        }

        private string drainsys;
        /// <summary>
        /// 排水系统补充说明
        /// </summary>
        public string DrainSys
        {
            set { drainsys = value; }
            get { return drainsys; }
        }
    }
}
