using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace GIS.Arc
{
    public class IShape
    {
        /// <summary>
        /// 名称（编号）
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public String Info { get; set; }

        public virtual SolidColorBrush GetColorBrush()
        {
            return ColorCenter.GetInstance().Seleted_Fill_Color;
        }
    }
    /// <summary>
    /// 固定位置图形
    /// </summary>
    public class IConcreteShape : IShape
    {
        /// <summary>
        /// 处在位置
        /// </summary>
        public Point Location { get; set; }

        public override string ToString()
        {
            String msg = "";
            msg += "名称：" + this.Name + "\n";
            msg += "位置：(" + this.Location.X + "," + this.Location.Y + ")\n";
            msg += "说明：" + this.Info + "\n";
            return msg;
        }

    }
    /// <summary>
    /// 相对位置图形
    /// </summary>
    public class IRelatedShape : IShape
    {
        /// <summary>
        /// 起点
        /// </summary>
        public IConcreteShape Start { get; set; }

        public IConcreteShape End { get; set; }

        public override string ToString()
        {
            String msg = "";
            msg += "名称：" + this.Name + "\n";
            msg += "起点：" + Start.Name + "(" + Start.Location.X + "," + Start.Location.Y + ")\n";
            msg += "终点：" + End.Name + "(" + End.Location.X + "," + End.Location.Y + ")\n";
            msg += "说明：" + this.Info + "\n";
            return msg;
        }
    }
}
