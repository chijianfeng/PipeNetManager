using GIS.Arc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PipeNetManager.eMap.State
{
    class RainJuncState:JuncState
    {
        public RainJuncState(RainJuncs rj) : base(rj.RainJuncsCanvas) {

            rainjuncs = rj;
        }

        /// <summary>
        /// 添加多个检查井
        /// </summary>
        /// <param name="listjuncs"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        public void AddRainJunc(List<RainCover> listjuncs , float[] px, float[] py)
        {
            int index = 0;
            foreach(RainCover cover in listjuncs)
            {
                AddJunc(cover, new Point(px[index], py[index]));
                index++;
            }
        }
        
        /// <summary>
        /// 响应鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public new  void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(CurrentMode==ADDMODE)
            {
                Point cp = e.GetPosition(context);      //获取相关坐标
                cp.X = cp.X + 7-App.StrokeThinkness/2;
                cp.Y = cp.Y + 7-App.StrokeThinkness/2;  //设置为中心
                RainCover c = new RainCover("雨水检查井", GetMercator(cp), "双击查看详细信息");
                //添加其他相关信息
                AddJunc(c, cp);                         //添加到视图中
                rainjuncs.AddJunc(c);

                //插入后台数据库

            }
            else if(CurrentMode==DELMODE)
            {
                Path path = e.Source as Path;
                if(DelJunc(path))
                {
                    RainCover c = path.ToolTip as RainCover;
                    rainjuncs.DelJunc(c);
                }
            }
            base.OnMouseDown(sender, e);                //若都不是添加或删除命令，则交给父类进行处理
        }

        /// <summary>
        /// 鼠标移动事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentMode == SELECTMODE)                                  //若是选择模式，不进行位置标记
                return;
            Point cp = e.GetPosition(context);                              //获取相对位置
            cp.X = cp.X + 7 - App.StrokeThinkness / 2;
            cp.Y = cp.Y + 7 - App.StrokeThinkness / 2;                      //设置为中心
            Cover c = rainjuncs.FindClosedCover(cp);
            if (null == c)
            {
                animationcanvas.Children.Clear();
                return; 
            }
            //转换坐标
            cp.X = ((c.Location.X - App.Tiles[0].X) / App.Tiles[0].Dx);
            cp.Y = ((App.Tiles[0].Y - c.Location.Y) / App.Tiles[0].Dy);
            //创建选中矩形框
            if(animationcanvas.Children.Count>0)
            {
                //改变位置
                animationcanvas.Children.Clear();
                animationcanvas.Children.Add(CreatRect(cp, App.StrokeThinkness));
            }
            else
            {
                animationcanvas.Children.Add(CreatRect(cp, App.StrokeThinkness));
            }
        }

        private RainJuncs rainjuncs = null;
    }
}
