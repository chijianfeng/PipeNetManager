using BLL.Command;
using BLL.Receiver;
using DBCtrl.DBClass;
using GIS.Arc;
using PipeMessage.eMap;
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
    class PipeState:IState
    {
        public PipeState(Canvas canvas) : base(canvas) { }

        /// <summary>
        /// 管道选择
        /// </summary>
        /// <param name="path"></param>
        public override void SelectShape(System.Windows.Shapes.Path path)
        {
            if (path == null)
            {
                return;
            }
            if (SelectPath != null)
            {
                Pipe p = (Pipe)SelectPath.ToolTip;
                SelectPath.Stroke = p.GetColorBrush();
            }
            if (path != SelectPath)
            {
                path.Stroke = colorCenter.Selected_Border_Color;
                SelectPath = path;
            }
            else
                SelectPath = null;
        }
        /// <summary>
        /// 增加管道
        /// </summary>
        /// <param name="pipe"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        public void AddPipe(Pipe pipe, Point Start, Point End)
        {
            Path path = new Path();
            path.Stroke = pipe.GetColorBrush();
            LineGeometry lg = new LineGeometry();
            lg.StartPoint = Start;
            lg.EndPoint = End;
            path.Data = lg;
            path.StrokeThickness = App.StrokeThinkness;
            path.SetValue(Canvas.ZIndexProperty, -1);
            path.ToolTip = pipe;
            context.Children.Add(path);
            listpath.Add(path);
        }

        /// <summary>
        /// 更新管道
        /// </summary>
        /// <param name="sps"></param>
        /// <param name="eps"></param>
        public void UpdatePipes(Point[] sps, Point[] eps)
        {
            HeadHeight = App.StrokeThinkness;
            HeadWidth = App.StrokeThinkness * 2;
            for (int i = 0; i < listpath.Count; i++)
            {
                /*((LineGeometry)(listpath[i].Data)).StartPoint = sps[i];
                ((LineGeometry)(listpath[i].Data)).EndPoint = eps[i];*/
                using (StreamGeometryContext context = ((StreamGeometry)(listpath[i].Data)).Open())
                {
                    InternalDrawArrowGeometry(context, sps[i], eps[i]);
                }
                listpath[i].StrokeThickness = App.StrokeThinkness*2/3;
            }
        }

        /// <summary>
        /// 删除管道
        /// </summary>
        /// <param name="path"></param>
        public bool DelPipe(Path path)
        {
             if (path == null) return false;

            path.Stroke = colorCenter.Selected_Border_Color;
            string msg = "是否删除选中对象?";
            string title = "删除";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result =
            MessageBox.Show(msg, title, buttons, icon);
            if (result == MessageBoxResult.Yes)
            {

                /*object obj = path.ToolTip;
                Pipe p = obj as Pipe;
                if (p == null) return false;
                PipeRev piperev = new PipeRev();
                piperev.ListPipe = new List<DBCtrl.DBClass.CPipeInfo>();
                piperev.ListPipe.Add(p.pipeInfo);*/
                //dcmd.SetReceiver(piperev);
                //dcmd.Execute();
                context.Children.Remove(path);
                return true;
            }
            else
            {
                path.Stroke = colorCenter.UnSelected_Border_Color;
                return false;
            }

        }

        /// <summary>
        /// 响应按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Path path = e.Source as Path;
            if (path == null)
                return;  

            SelectShape(path);
            object obj = path.ToolTip;
            Pipe p = obj as Pipe;
            if (p == null)
                return;

            SelectCmd scmd = new SelectCmd();
            PipeRev piperev = new PipeRev();
            piperev.ListPipe = new List<CPipeInfo>();
            piperev.PipeName = p.pipeInfo.PipeName;
            scmd.SetReceiver(piperev);
            scmd.Execute();
            if (piperev.ListPipeExt != null && piperev.ListPipeExt.Count > 0)
                p.pipeExtInfo = piperev.ListPipeExt[0];
        }

        /// <summary>
        /// 响应鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            
        }

        protected Geometry DrawPipe(Point sp , Point ep)
        {
            StreamGeometry geometry = new StreamGeometry();
            geometry.FillRule = FillRule.EvenOdd;

            using (StreamGeometryContext context = geometry.Open())
            {
                InternalDrawArrowGeometry(context , sp , ep);
            }

            return geometry;
        }

        private void InternalDrawArrowGeometry(StreamGeometryContext context , Point sp , Point ep)
        {
            double theta = Math.Atan2(sp.Y-ep.Y , sp.X-ep.X);
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);

            Point pm = new Point((sp.X+ep.X)/2 ,  (sp.Y + ep.Y) / 2 );

            Point pt3 = new Point(
                pm.X + (HeadWidth * cost - HeadHeight * sint),
                pm.Y+ (HeadWidth * sint + HeadHeight * cost));

            Point pt4 = new Point(
                 pm.X + (HeadWidth * cost + HeadHeight * sint),
                pm.Y - (HeadHeight * cost - HeadWidth * sint));

            //begin draw line
            context.BeginFigure(sp, true, false);
            context.LineTo(ep, true, true);

            context.BeginFigure(pt3, true, false);
            context.LineTo(pm, true, true);
            context.LineTo(pt4, true, true);
        }

        protected bool IsDrawLine = false;              //是否划线
        protected Cover c1, c2;
        protected Point p1, p2;
        protected double HeadWidth = App.StrokeThinkness*2;
        protected double HeadHeight = App.StrokeThinkness;
    }
}
