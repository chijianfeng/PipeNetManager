using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class COutFallExtInfo
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        //排放口ID
        private int outfallid;
        public int OutFallID
        {
            set { outfallid = value; }
            get { return outfallid; }
        }

        
        private string outfallname;
        /// <summary>
        /// 排放口名称
        /// </summary>
        public string OutFallName
        {
            set { outfallname = value; }
            get { return outfallname; }
        }

        private string outfalladdr;
        /// <summary>
        /// 排放口具体地址描述
        /// </summary>
        public string OutFallAddr
        {
            set { outfalladdr = value; }
            get { return outfalladdr; }
        }

        private int flap_material;
        /// <summary>
        /// 拍门材质：1-铸铁；2-钢；3-不锈钢；4-塑料；5-复合材料；6-其他
        /// </summary>
        public int Flap_Material
        {
            set { flap_material = value; }
            get { return flap_material; }
        }

        private double flap_diameter;
        /// <summary>
        /// 拍门直径，单位：米
        /// </summary>
        public double Flap_Diameter
        {
            set { flap_diameter = value; }
            get { return flap_material; }
        }

        private double flap_topele;
        /// <summary>
        /// 拍门顶部高程，单位：米
        /// </summary>
        public double Flap_TopEle
        {
            set { flap_topele = value; }
            get { return flap_topele; }
        }

        private double flap_botele;
        /// <summary>
        /// 拍门底部高程，单位：米
        /// </summary>
        public double Flap_BotEle
        {
            set { flap_topele = value; }
            get { return flap_topele; }
        }

        private double topele;
        /// <summary>
        /// 排放口顶部高程，单位:米
        /// </summary>
        public double TopEle
        {
            set { topele = value; }
            get { return topele; }
        }

        private double normallevel;
        /// <summary>
        /// 淹没常水位，当常水位淹没出流时，记录常水位高程，单位：米
        /// </summary>
        public double NormalLevel
        {
            set { normallevel = value; }
            get { return normallevel; }
        }

        private double tidal_curve;
        /// <summary>
        /// 潮位曲线，当出流形式为潮汐影响时，与XY曲线表管了，X代表时间，单位：小时；Y代表潮位高程，单位：米
        /// </summary>
        public double Tidal_Curve
        {
            set { tidal_curve = value; }
            get { return tidal_curve; }
        }

        private int status;
        /// <summary>
        /// 设施状态：1-拟建；2-在建；3-已建；4-待废；5-已废；6-其他
        /// </summary>
        public int Status
        {
            set
            {
                if (value < 1 || value > 6)
                    status = 6;
                else
                    status = value;
            }
            get { return status; }
        }

        private string remark;
        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }
    }
}
