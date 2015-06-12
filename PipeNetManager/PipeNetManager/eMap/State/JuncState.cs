using BLL.Command;
using BLL.Receiver;
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
    class JuncState:IState
    {
        public JuncState(Canvas canvas):base(canvas){

            animationcanvas = new Canvas();
            canvas.Children.Add(animationcanvas);           //用于显示动画特效
        }

        protected Canvas animationcanvas = null;
        /// <summary>
        /// 选择对象
        /// </summary>
        /// <param name="path"></param>
        public override void SelectShape(Path path)
        {
            if (path == null) return;
            if(SelectPath!=null)
            {
                SelectPath.Stroke = colorCenter.UnSelected_Border_Color;
            }
            if (path != SelectPath)
            {
                path.Stroke = colorCenter.Selected_Border_Color;
                SelectPath = path;
            }
            else
                SelectPath = null;
        }

        //添加单个检查井
        /// <summary>
        /// 增加井盖
        /// </summary>
        /// <param name="cover"></param>
        /// <param name="cp"></param>
        public  void AddJunc(Cover cover, Point cp)
        {
            Path path = new Path();
            path.Fill = cover.GetColorBrush();
            path.Stroke = colorCenter.UnSelected_Border_Color;
            EllipseGeometry eg = new EllipseGeometry();
            eg.Center = cp;
            eg.RadiusX = App.StrokeThinkness;
            eg.RadiusY = App.StrokeThinkness;
            path.Data = eg;
            path.ToolTip = cover;
            context.Children.Add(path);
            listpath.Add(path);
        }

        /// <summary>
        /// 创建正方形
        /// </summary>
        /// <param name="p"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        protected Path CreatRect(Point p , double r)
        {
            Path path = new Path();
            RectangleGeometry rg = new RectangleGeometry();
            rg.Rect = new Rect(p.X - r, p.Y - r, 2*r, 2*r);
            path.StrokeThickness = App.StrokeThinkness / 2;
            path.Stroke = colorCenter.Selected_Border_Color;
            path.Data = rg;
            return path;
        }

        /// <summary>
        /// 删除检查井
        /// </summary>
        /// <param name="path"></param>
        public bool DelJunc(Path path)
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
                Cover c = obj as Cover;
                if (c == null) return false;
                JuncRev jrev = new JuncRev();
                jrev.ListJunc = new List<DBCtrl.DBClass.CJuncInfo>();
                jrev.ListJunc.Add(c.juncInfo);
                //dcmd.SetReceiver(jrev);
                //dcmd.Execute();*/
                context.Children.Remove(path);
                return true;
            }
            else
            {
                path.Stroke = colorCenter.UnSelected_Border_Color;
                return false; 
            }
        }

        //更新检查井位置
        public void UpdateJuncPos(float[] px, float[] py)
        {
            for (int i = 0; i < listpath.Count; i++)
            {
                ((EllipseGeometry)(listpath[i].Data)).Center = new Point(px[i], py[i]);
                ((EllipseGeometry)(listpath[i].Data)).RadiusX = App.StrokeThinkness;
                ((EllipseGeometry)(listpath[i].Data)).RadiusY = App.StrokeThinkness;
            }
        }

        //鼠标按下后，出现基本信息
        public override void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Path path = e.Source as Path;
            if (path==null)
                return;

            SelectShape(path);
            object obj = path.ToolTip;
           /* Cover c = (Cover)obj;
            SelectCmd scmd = new SelectCmd();
            JuncRev jrev = new JuncRev();
            jrev.JuncName = c.juncInfo.JuncName;
            scmd.SetReceiver(jrev);
            scmd.Execute();*/
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            
        }
    }
}
