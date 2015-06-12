using GIS.Arc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PipeNetManager.eMap.State
{
    class RainPipeState:PipeState
    {
        public RainPipeState(RainPipes rp) : base(rp.RainPipeCanvas) 
        {
            rainpipes = rp;
        }
        /// <summary>
        /// 添加多个雨水管道
        /// </summary>
        /// <param name="listpipe"></param>
        /// <param name="sps"></param>
        /// <param name="eps"></param>
        public void AddRainPipes(List<RainPipe> listpipe , Point[] sps , Point[] eps)
        {
            int index = 0;
            foreach (RainPipe pipe in listpipe)
            {
                Path path = new Path();
                path.Stroke = pipe.GetColorBrush();
               /* LineGeometry lg = new LineGeometry();
                lg.StartPoint = sps[index];
                lg.EndPoint = eps[index];
                path.Data = lg;*/
                //添加带方向的管道

                path.Data = DrawPipe(sps[index], eps[index]);
                index++;
                
                path.StrokeThickness = App.StrokeThinkness;
                path.SetValue(Canvas.ZIndexProperty, -1);
                path.ToolTip = pipe;
                context.Children.Add(path);
                listpath.Add(path);
            }
        }
        public new  void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentMode == ADDMODE)
            {
                Point cp = e.GetPosition(context);                     //获取相关坐标
                cp.X = cp.X + 7 - App.StrokeThinkness / 2;
                cp.Y = cp.Y + 7 - App.StrokeThinkness / 2;             //设置为中心
                Cover c = rainpipes.rainjunc.FindClosedCover(cp);      //检测最近点位
                if (null == c)
                    return;
                if (IsDrawLine == false)
                {
                    c1 = c;
                    p1.X = ((c1.Location.X- App.Tiles[0].X) / App.Tiles[0].Dx) + App.StrokeThinkness / 2; //计算管道第一个点位置坐标
                    p1.Y = ((App.Tiles[0].Y - c1.Location.Y) / App.Tiles[0].Dy) +App.StrokeThinkness / 2;
                }
                else
                {
                    c2 = c;
                    p2.X = ((c2.Location.X - App.Tiles[0].X) / App.Tiles[0].Dx) + App.StrokeThinkness / 2;
                    p2.Y = ((App.Tiles[0].Y - c2.Location.Y) / App.Tiles[0].Dy) + App.StrokeThinkness / 2;

                    RainPipe pipe = new RainPipe(c1.Name + "-" + c2.Name, "双击查看信息", c1, c2);
                    AddPipe(pipe, p1, p2);
                    rainpipes.AddRainPipe(pipe);
                    //插入后台数据库
                }
                IsDrawLine = !IsDrawLine;
            }
            else if (CurrentMode == DELMODE)
            {
                Path path = e.Source as Path;
                if(DelPipe(path))
                {
                    RainPipe rp = path.ToolTip as RainPipe;
                    rainpipes.DelRainPipe(rp);
                }
            }
            base.OnMouseDown(sender, e);                //若都不是添加或删除命令，则交给父类进行处理
        }

        private RainPipes rainpipes = null;
    }
}
