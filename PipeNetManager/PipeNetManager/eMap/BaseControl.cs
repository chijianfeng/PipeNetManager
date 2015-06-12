using PipeMessage.eMap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PipeNetManager.eMap
{
    public abstract class BaseControl : UserControl
    {
        protected double ScreenHeight{get;set;}        //显示屏高度
        protected double ScreenWidth{get;set;}         //显示屏宽度

        public bool IsHidden = false;                   //是否隐藏图层
        public static  bool IsViewMove                  //是否是移动操作
        { set; get; }

        public static  bool IsZoomIn                     //是否放大操作
        { set; get; }

        public static bool IsViewSelect { set; get; }    //是否是选择操作

        public static  bool IsZoomOut { set; get; }      //是否缩小操作

        public void SetViewMove() {
            IsViewMove = true;

            //other is false
            IsZoomIn = false;
            IsZoomOut = false;
        }

        public void SetZoomIn()
        {
            IsZoomIn = true;

            IsViewMove = false;
            IsZoomOut = false;
        }

        public void SetZoomOut()
        {
            IsZoomOut = true;

            IsViewMove = false;
            IsZoomIn = false;
        }


        public BaseControl()                    //构造函数
        {
            ScreenWidth = SystemParameters.WorkArea.Width;//得到屏幕工作区域宽度

            ScreenHeight = SystemParameters.WorkArea.Height;//得到屏幕工作区域高度
        }

        //鼠标事件
        public abstract void OnMouseUp(object sender, MouseButtonEventArgs e);
        public abstract void OnMouseMove(object sender, MouseEventArgs e);
        public abstract void OnMouseLeftDown(object sender, MouseButtonEventArgs e);

        public abstract void OnViewOriginal(object sender, RoutedEventArgs e);

        public abstract void SetOperationMode(int mode);       //设置操作模式：添加，删除，选择

        public void Update(){}//强制更新，重写
    }
}
