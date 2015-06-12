using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.Map
{
    /// <summary>
    /// 地图瓦片图
    /// 以左上角为坐标原点（X,Y）
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// 瓦片级别
        /// </summary>
        public String Level { get; set; }

        /// <summary>
        /// 瓦片URL链接
        /// </summary>
        public String Filename { get; set; }

        /// <summary>
        /// 瓦片图片左上角位置的墨卡托坐标X
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// 瓦片图片左上角位置的墨卡托坐标Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// X轴方向的偏移
        /// </summary>
        public double Dx { get; set; }

        /// <summary>
        /// X轴方向的偏移
        /// </summary>
        public double Dy { get; set; }

        public String RowColumn { set; get; }
        /// <summary>
        /// 该瓦片的行编号
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// 该瓦片的列编号
        /// </summary>
        public int Column { get; set; }

    }
}
