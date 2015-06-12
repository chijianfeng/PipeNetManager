using PipeNetManager.eMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PipeNetManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            double x = SystemParameters.WorkArea.Width;//得到屏幕工作区域宽度

            double y = SystemParameters.WorkArea.Height;//得到屏幕工作区域高度

            this.Width = x;//设置窗体宽度

            this.Height = y;//设置窗体高度

            //just for test
            this.Grid1.Children.Remove(textBlock1);     //移除textblock
            this.stackpanl.Children.Clear();
            this.stackpanl.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.stackpanl.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            Mapctl eMap = new Mapctl();
            this.stackpanl.Children.Add(eMap);
        }
    }
}
