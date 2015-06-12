using GIS.Arc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PipeNetManager.eMap.State
{
    class WasteJuncState:JuncState
    {
        public WasteJuncState(WasteJuncs wj) : base(wj.WasteCanvas) 
        {
            wastejunc = wj;
        }

        /// <summary>
        /// 添加多个污水检查井
        /// </summary>
        /// <param name="listjuncs"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        public void AddWasteJunc(List<WasteCover> listjuncs , float[] px , float[] py)
        {
            int index = 0;
            foreach (WasteCover cover in listjuncs)
            {
                AddJunc(cover, new Point(px[index], py[index]));
                index++;
            }
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentMode == SELECTMODE)
                return;
            Point cp = e.GetPosition(context);                               //获取相对位置
            cp.X = cp.X + 7 - App.StrokeThinkness / 2;
            cp.Y = cp.Y + 7 - App.StrokeThinkness / 2;                       //设置为中心
            Cover c = wastejunc.FindClosedCover(cp);
            if (null == c)
            {
                animationcanvas.Children.Clear();
                return;
            }
            //转换坐标
            cp.X = ((c.Location.X - App.Tiles[0].X) / App.Tiles[0].Dx);
            cp.Y = ((App.Tiles[0].Y - c.Location.Y) / App.Tiles[0].Dy);
            //创建选中矩形框
            if (animationcanvas.Children.Count > 0)
            {
                //改变位置
                animationcanvas.Children.Clear();
                animationcanvas.Children.Add(CreatRect(cp, App.StrokeThinkness));
            }
            else
            {
                animationcanvas.Children.Add(CreatRect(cp, App.StrokeThinkness));
            }
            base.OnMouseMove(sender, e);
        }

        public new  void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentMode == ADDMODE)
            {
                Point cp = e.GetPosition(context);      //获取相关坐标
                cp.X = cp.X + 7;
                cp.Y = cp.Y + 7;                        //设置为中心
                WasteCover c = new WasteCover("污水检查井", GetMercator(cp), "双击查看详细信息");
                //添加其他相关信息
                AddJunc(c, cp);                         //添加到视图中
                wastejunc.AddWasteJunc(c);              //添加到数据中

                //插入后台数据库

            }
            else if (CurrentMode == DELMODE)
            {
                Path path = e.Source as Path;
                if(DelJunc(path))
                {
                    WasteCover c = path.ToolTip as WasteCover;
                    wastejunc.DelWasteJunc(c);
                }
            }
            base.OnMouseDown(sender, e);                //若都不是添加或删除命令，则交给父类进行处理
        }

        private WasteJuncs wastejunc = null;
    }
}
