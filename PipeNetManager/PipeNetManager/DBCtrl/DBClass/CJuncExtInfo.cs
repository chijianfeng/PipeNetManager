using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class CJuncExtInfo
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        private int juncid;
        /// <summary>
        /// 检查井ID
        /// </summary>
        public int JuncID
        {
            set { juncid = value; }
            get { return juncid; }
        }

        private string anci_fac;
        /// <summary>
        /// 对井内的梯子、压力盖等附属物进行描述
        /// </summary>
        public string Anci_Fac
        {
            set { anci_fac = value; }
            get { return anci_fac; }
        }

        private string lane_way;
        /// <summary>
        /// 设施所在道路的名称
        /// </summary>
        public string Lane_Way
        {
            set { lane_way = value; }
            get { return lane_way; }
        }

        private int cov_material;
        /// <summary>
        /// 检查井的材质：1-铸铁；2-钢；3-混凝土；4-塑料；5-玻璃钢；6-大理石
        /// </summary>
        public int Cov_Material
        {
            set 
            {
                if (value < 1 || value > 6)
                    cov_material = 1;
                else
                    cov_material = value; 
            }
            get { return cov_material; }
        }

        private int cov_shape;
        /// <summary>
        /// 井盖的形状：1-圆形；2-三角形；3-矩形；4-其他
        /// </summary>
        public int Cov_Shape
        {
            set 
            {
                if (value < 1 || value > 4)
                    cov_shape = 4;
                else
                    cov_shape = value; 
            }
            get { return cov_shape; }
        }

        private double cov_dimen1;
        /// <summary>
        /// 井盖为圆形时填写井盖直径；井盖为三角形时填写任意边长；井盖为矩形时填写边长，单位：米
        /// </summary>
        public double Cov_Dimen1
        {
            set { cov_dimen1 = value; }
            get { return cov_dimen1; }
        }

        private double cov_dimen2;
        /// <summary>
        /// 井盖为三角形时填写任意边长；井盖为矩形时填写短边长，单位：米
        /// </summary>
        public double Cov_Dimen2
        {
            set { cov_dimen2 = value; }
            get { return cov_dimen2; }
        }

        private double cov_dimen3;
        /// <summary>
        /// 井盖为三角形时填写任意边长；单位：米
        /// </summary>
        public double Cov_Dimen3
        {
            set { cov_dimen3 = value; }
            get { return cov_dimen3; }
        }

        private string chamber_type;
        /// <summary>
        /// 井室的类型说明
        /// </summary>
        public string Chamber_Type
        {
            set { chamber_type = value; }
            get { return chamber_type; }
        }

        private double chamber_length;
        /// <summary>
        /// 井室的长度，单位：米
        /// </summary>
        public double Chamber_Length
        {
            set { chamber_length = value; }
            get { return chamber_length; }
        }

        private double chamber_width;
        /// <summary>
        /// 井室的宽度，单位：米
        /// </summary>
        public double Chamber_Width
        {
            set { chamber_width = value; }
            get { return chamber_width; }
        }

        private double chamber_height;
        /// <summary>
        /// 井室的高度，单位：米
        /// </summary>
        public double Chamber_Height
        {
            set { chamber_height = value; }
            get { return chamber_height; }
        }

        private double survery_waterdeep;
        /// <summary>
        /// 现场测绘时，检查井水深，单位：米
        /// </summary>
        public double Survery_WaterDeep
        {
            set { survery_waterdeep = value; }
            get { return survery_waterdeep; }
        }

        private double survery_sedideep;
        /// <summary>
        /// 现场测绘时，检查井底部淤积物的深度，单位：米
        /// </summary>
        public double Survery_SediDeep
        {
            set { survery_sedideep = value; }
            get { return survery_sedideep; }
        }

        private DateTime survery_date;
        /// <summary>
        /// 现场测绘的具体日期：格式：yyyy-mm-d
        /// </summary>
        public DateTime Survery_Date
        {
            set { survery_date = value; }
            get
            {
                if (survery_date == null)
                    survery_date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                return survery_date; 
            }
        }

        private int bottom_style;
        /// <summary>
        /// 井底形式：1-平底；2-流槽；3-落底；4-其他
        /// </summary>
        public int Bottom_Style
        {
            set
            {
                if (value > 4 || value < 1)
                    bottom_style = 4;
                else
                    bottom_style = value;
            }
            get
            {
                return bottom_style;
            }
        }

        private int junc_class;
        /// <summary>
        /// 检查井等级：1-主井（主管上的井）；2-附井（接户井；过度井）；3-其他
        /// </summary>
        public int Junc_Class
        {
            set
            {
                if(value<1||value>3)
                    junc_class = 3;
                else
                    junc_class = value;
            }
            get { return junc_class; }
        }

        private int status;
        /// <summary>
        /// 设施状态：1-拟建；2-在建；3-已建；4-待废；5-已废；6-其他
        /// </summary>
        public int Status
        {
            set
            {
                if(value<1||value>6)
                    status = 6;
                else
                    status = value;
            }
            get
            {
                return status;
            }
        }

        private string remark;
        /// <summary>
        /// 备注，相关事项说明
        /// </summary>
        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }
    }
}
