using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using DBCtrl.DBClass;

namespace GIS.Arc
{
    /// <summary>
    /// 井盖
    /// </summary>
    public class Cover : IConcreteShape
    {
        public static int NUM;

        public CJuncInfo juncInfo { get; set; }
        public CJuncExtInfo juncExtInfo { get; set; }

        public Pipe In_Pipe { get; set; }
        public Pipe Out_Pipe { get; set; }

        public Cover()
        {
            this.Name = "井盖" + NUM++;
            this.Info = "无";
        }

        public Cover(Point point)
        {
            this.Name = "井盖" + NUM++;
            this.Info = "无";
            this.Location = point;
        }

        public Cover(String name, Point point, String info)
        {
            this.Name = name;
            this.Location = point;
            this.Info = info;
        }
        
    }
    /// <summary>
    /// 雨水井盖
    /// </summary>
    public class RainCover : Cover 
    {
        public static SolidColorBrush Cover_Fill_Color = new SolidColorBrush();

        public RainCover(String name, Point point, String info):base(name, point, info) 
        {
            //Cover_Fill_Color.Color = Colors.DimGray;
        }

        public override SolidColorBrush GetColorBrush()
        {
            return ColorCenter.GetInstance().Cover_Rain_Fill_Color;
        }
    }

    /// <summary>
    /// 污水井盖
    /// </summary>
    public class WasteCover : Cover
    {
        public static SolidColorBrush Cover_Fill_Color = new SolidColorBrush();
        public WasteCover(String name, Point point, String info) : base(name, point, info) 
        {
            //Cover_Fill_Color.Color = Colors.SaddleBrown;
        }

        public override SolidColorBrush GetColorBrush()
        {
            return ColorCenter.GetInstance().Cover_Waste_Fill_Color;
        }
    }

}
