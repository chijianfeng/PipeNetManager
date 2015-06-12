using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PipeNetManager.eMap
{
    //用于数据绑定
    public class DataControl:BaseControl
    {
        public override void OnMouseUp(object sender, MouseButtonEventArgs e){}
        public  override void OnMouseMove(object sender, MouseEventArgs e){}
        public override void OnMouseLeftDown(object sender, MouseButtonEventArgs e) { }

        public override void OnViewOriginal(object sender, RoutedEventArgs e) { }

        public override void SetOperationMode(int mode)
        {
            throw new NotImplementedException();
        }
    }
}
