using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using GIS.Arc;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using GIS.Map;
using BLL.Command;
using PipeNetManager;

namespace PipeMessage.eMap
{
    public abstract class IState
    {

        private Path path = null;

        protected Canvas context = null;

        public  int CurrentMode = SELECTMODE;                           //当前的模式

        protected  List<Path> listpath = new List<Path>();             //保存显示原始
        public Path SelectPath
        {
            get { return path; }
            set { path = value; }
        }

        public static readonly int SELECTMODE = 1;                      //选择模式
        public static readonly int ADDMODE = 2;                         //添加模式
        public static readonly int DELMODE = 3;                         //删除模式
        public static readonly int RELATEDMODE = 4;                     //关联模式

        public ColorCenter colorCenter = ColorCenter.GetInstance();

        public IState(Canvas canvas)
        {
            this.context = canvas;
        }

        /// <summary>
        /// 选择对象
        /// </summary>
        /// <param name="path"></param>
        public abstract void SelectShape(Path path);

        /// <summary>
        /// 坐标转换
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Point GetMercator(Point p)
        {
            Point point = new Point();
            int Column = (int)p.X / 256;
            int Row = (int)p.Y / 256;

            double dx = p.X - Column * 256;
            double dy = p.Y - Row * 256;

            Tile tile = PipeNetManager.App.Tiles[Row * Level.Total_Column + Column];
            point.X = tile.X + tile.Dx * dx;
            point.Y = tile.Y - tile.Dy * dy;
            return point;
        }

        /// <summary>
        /// 鼠标响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void OnMouseDown(object sender, MouseButtonEventArgs e);

        /// <summary>
        /// 响应鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void OnMouseMove(Object sender, MouseEventArgs e);

    }
}
