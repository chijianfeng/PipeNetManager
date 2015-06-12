using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.DBClass
{
    public class CPipeExtInfo
    {
        private int id;
        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        private int pipeid;
        /// <summary>
        /// 与PipeInfo关联键，管道ID
        /// </summary>
        public int PipeID
        {
            set { pipeid = value; }
            get { return pipeid; }
        }

        private string lane_way;
        /// <summary>
        /// 管道所在的道路名称
        /// </summary>
        public string Lane_Way
        {
            set { lane_way = value; }
            get { return lane_way; }
        }

        private int pressure_type;
        /// <summary>
        /// 压力类型：1-重力；2-压力；3-其他
        /// </summary>
        public int Pressure_Type
        {
            set
            {
                if (value < 1 || value > 3)
                    pressure_type = 3;
                else
                    pressure_type = value;
            }
            get { return pressure_type; }
        }

        private double wall_thick;
        /// <summary>
        /// 壁厚，单位：毫米
        /// </summary>
        public double Wall_Thick
        {
            set { wall_thick = value; }
            get { return wall_thick; }
        }

        private int liner_material;
        /// <summary>
        /// 管道衬里材质：1-水泥砂浆；2-塑料；3-金属；4-复合材料；5-其他
        /// </summary>
        public int Liner_Material
        {
            set
            {
                if (value < 1 || value > 5)
                    liner_material = 5;
                else
                    liner_material = value;
            }
            get { return liner_material; }
        }

        private int conn_type;
        /// <summary>
        /// 连接方式：1-平口；2-企口；3-承插口；4-焊接；5-其他
        /// </summary>
        public int Conn_Type
        {
            set
            {
                if (value < 1 || value > 5)
                    conn_type = 5;
                else
                    conn_type = value;
            }
            get { return conn_type; }
        }

        private double pipe_slop;
        /// <summary>
        /// 管道坡度：（起点管底标高-终点管底标高）/管道投影长度
        /// </summary>
        public double Pipe_Slop
        {
            set { pipe_slop = value; }
            get { return pipe_slop; }
        }

        private bool invert_silphon;
        /// <summary>
        /// 是否是倒虹管：0-否；1-是
        /// </summary>
        public bool Invert_Silphon
        {
            set { invert_silphon = value; }
            get { return invert_silphon; }
        }

        private int origin_strue;
        /// <summary>
        /// 原始结构状态：1-管暗接；2-暗井；3-弯斗；4-倒虹；5-轴线偏移；6-井盖埋没；7-变形；8-变径；9-其他
        /// </summary>
        public int Origin_Strue
        {
            set
            {
                if (value < 1 || value > 9)
                    origin_strue = 9;
                else
                    origin_strue = value;
            }
            get { return origin_strue; }
        }

        private int constr_method;
        /// <summary>
        /// 敷设方式：1-开槽埋管；2-顶管；3-盾构；4-拖拉管；5-其他
        /// </summary>
        public int Constr_Method
        {
            set
            {
                if (value < 1 || value > 5)
                    constr_method = 5;
                else
                    constr_method = value;
            }
            get { return constr_method; }
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
            get
            {
                return status;
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
            set {  losereason = value; }
            get { return losereason; }
        }

        private string remark;
        /// <summary>
        /// //备注，相关事项说明
        /// </summary>
        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }
    }
}
