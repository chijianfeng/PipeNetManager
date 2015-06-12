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
using System.Runtime.InteropServices;
using System.Windows.Resources;
using GIS.Map;
using PipeMessage.eMap;

namespace PipeNetManager.eMap
{
    /// <summary>
    /// Mapctl.xaml 的交互逻辑
    /// </summary>
    public partial class Mapctl : UserControl
    {
        public Mapctl()
        {
            InitializeComponent();

            AddContent();
        }

        private List<BaseControl> listLayer = new List<BaseControl>();          //创建图层集合
        private void AddContent()
        {
            MapBackground background = new MapBackground();                     //创建地图背景图层
            this.MapGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.MapGrid.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.MapGrid.Children.Add(background);
            listLayer.Add(background);                                          //保存到图层中，便于管理
            RainJuncs rainjuncs = new RainJuncs();
            RainPipes rainpipes = new RainPipes(rainjuncs);                     //添加雨水管道图层
            this.MapGrid.Children.Add(rainpipes);
            listLayer.Add(rainpipes);

            WasteJuncs wastejuncs = new WasteJuncs();                           //添加污水检查井图层
            WastePipes wastepipes = new WastePipes(wastejuncs);                 //添加污水管道图层
            this.MapGrid.Children.Add(wastepipes);
            listLayer.Add(wastepipes);

            
            this.MapGrid.Children.Add(rainjuncs);                               //添加雨水检查井图层
            listLayer.Add(rainjuncs);

           
            this.MapGrid.Children.Add(wastejuncs);
            listLayer.Add(wastejuncs);

            
        }

        //message
        private Cursor CreateCur(string path)
        {
            StreamResourceInfo sri = Application.GetResourceStream(new Uri(path, UriKind.Relative));

            Cursor customCursor = new Cursor(sri.Stream);
            return customCursor;
        }
        /*
         * 移动消息处理
         */
        private void OnMoveMap(object sender, RoutedEventArgs e)
        {
            MapGrid.Cursor = CreateCur("/Assets/Move.cur");                    //表示移动状态
            View_ZoomIn.IsChecked = false;
            View_ZoomOut.IsChecked = false;
            VIew_Orignal.IsChecked = false;
            View_Select.IsChecked = false;
            View_Move.IsChecked = true;
        }

        private void OnSelect(object sender, RoutedEventArgs e)
        {
            MapGrid.Cursor = Cursors.Arrow;
            View_ZoomIn.IsChecked = false;
            View_ZoomOut.IsChecked = false;
            VIew_Orignal.IsChecked = false;
            View_Move.IsChecked = false;
            View_Select.IsChecked = true;
        }
        //放大操作
        private void OnZoomIn(object sender, RoutedEventArgs e)
        {
            MapGrid.Cursor = CreateCur("/Assets/zoomin.cur");
            //其他选项变成非选中
            View_Move.IsChecked = false;
            View_ZoomOut.IsChecked = false;
            VIew_Orignal.IsChecked = false;
            View_Select.IsChecked = false;
            View_ZoomIn.IsChecked = true;
        }

        //缩小操作
        private void OnZoomOut(object sender, RoutedEventArgs e)
        {
            //修改鼠标状态
            MapGrid.Cursor = CreateCur("/Assets/zoomout.cur");
            //其他选项变成非选中
            View_Move.IsChecked = false;
            View_ZoomIn.IsChecked = false;
            VIew_Orignal.IsChecked = false;
            View_Select.IsChecked = false;
            View_ZoomOut.IsChecked = true;
        }

        //是否显示雨水检查井
        private void View_Show_Rainjunc_Click(object sender, RoutedEventArgs e)
        {
            int index = 3;
            if(View_Show_Rainjunc.IsChecked)            //选中
            {
                View_Show_Rainjunc.IsChecked = false;
                this.MapGrid.Children[index+1].Visibility = Visibility.Collapsed;
                listLayer.ElementAt(index).IsHidden = true;
            }
            else
            {
                View_Show_Rainjunc.IsChecked = true;
                this.MapGrid.Children[index+1].Visibility = Visibility.Visible;
                listLayer.ElementAt(index).IsHidden = false;
                RainJuncs junc = listLayer.ElementAt(index) as RainJuncs;
                junc.Update();
            }
        }

        //是否显示污水检查井
        private void View_Show_Wastejunc_Click(object sender, RoutedEventArgs e)
        {
            int index = 4;
            if(View_Show_Wastejunc.IsChecked)
            {
                View_Show_Wastejunc.IsChecked = false;
                this.MapGrid.Children[index+1].Visibility = Visibility.Hidden;
                listLayer.ElementAt(index).IsHidden = true;
            }
            else
            {
                View_Show_Wastejunc.IsChecked = true;
                this.MapGrid.Children[index+1].Visibility = Visibility.Visible;
                listLayer.ElementAt(index).IsHidden = false;
                WasteJuncs junc = listLayer.ElementAt(index) as WasteJuncs;
                junc.Update();
            }
        }
        //是否显示雨水管道图层
        private void View_Show_Rainpipe_Click(object sender, RoutedEventArgs e)
        {
            int index = 1;
            if(View_Show_Rainpipe.IsChecked)
            {
                View_Show_Rainpipe.IsChecked = false;
                this.MapGrid.Children[index+1].Visibility = Visibility.Hidden;
                listLayer.ElementAt(index).IsHidden = true;
            }
            else
            {
                View_Show_Rainpipe.IsChecked = true;
                this.MapGrid.Children[index+1].Visibility = Visibility.Visible;
                listLayer.ElementAt(index).IsHidden = false;
                RainPipes pipes = listLayer.ElementAt(index) as RainPipes;
                pipes.Update();
            }
        }
        //是否显示污水管道图层
        private void View_Show_Wastepipe_Click(object sender, RoutedEventArgs e)
        {
            int index = 2;
            if(View_Show_Wastepipe.IsChecked)
            {
                View_Show_Wastepipe.IsChecked = false;
                this.MapGrid.Children[index + 1].Visibility = Visibility.Hidden;
                listLayer.ElementAt(index).IsHidden = true;
            }
            else
            {
                View_Show_Wastepipe.IsChecked = true;
                this.MapGrid.Children[index + 1].Visibility = Visibility.Visible;
                listLayer.ElementAt(index).IsHidden = false;
                WastePipes pipes = listLayer.ElementAt(index) as WastePipes;
                pipes.Update();
            }
        }

        //内部消息事件，适用于各个图层
        private void MapGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in listLayer)                                    //每个图层接收消息
            {
                item.OnMouseUp(sender, e);
            }
        }

        private void MapGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in listLayer)                                    //每个图层接收消息
            {
               // if(!item.IsHidden)
                item.OnMouseLeftDown(sender, e);
            }
        }

        private void MapGrid_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var item in listLayer)                                    //每个图层接收消息
            {
               // if (!item.IsHidden)
                item.OnMouseMove(sender, e);
            }
            //更新信息
            Point p = e.GetPosition(MapGrid);
            if (p.X < 0)
                return;
            int Column = (int)p.X / 256;
            int Row = (int)p.Y / 256;

            double dx = p.X - Column * 256;
            double dy = p.Y - Row * 256;

            Tile tile = App.Tiles[Row * Level.Total_Column + Column];
            double x = tile.X + tile.Dx * dx;
            double y = tile.Y - tile.Dy * dy;
            Coords.Point point;
            point.x = x; point.y = y;
            Coords.Point mp = Coords.Mercator2WGS84(point);
            String Msg = "";
            Msg += "经纬度坐标：\nX:" + mp.x.ToString("0.00000000") + "\nY:" + mp.y.ToString("0.00000000") + "\n";
            Lbl_Detail.Content = Msg;
        }

        //显示原始尺寸
        private void VIew_Orignal_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listLayer)                                    //每个图层接收消息
            {
                //if (!item.IsHidden)
                item.OnViewOriginal(sender, e);
            }
            MapGrid.Cursor = Cursors.Arrow;
            View_Move.IsChecked = false;
            View_ZoomOut.IsChecked = false;
            View_ZoomIn.IsChecked = false;
        }

        //添加雨水检查井
        private void Edit_Add_RainJunc_Click(object sender, RoutedEventArgs e)
        {
            int index = 3;
            if(!View_Show_Rainjunc.IsChecked)            //未选中
            {
                MessageBox.Show("雨水检查井图层为隐藏，无法操作");
                return;
            }
            MapGrid.Cursor = CreateCur("/Assets/add.cur");                    //表示添加状态
            for (int i = 0; i < listLayer.Count;i++ )
            {
                if (i == index)
                    continue;
                listLayer.ElementAt(i).SetOperationMode(IState.SELECTMODE);   //进入选择状态
            }
            listLayer.ElementAt(index).SetOperationMode(IState.ADDMODE);      //进入添加状态
            View_Move.IsChecked = false;                                      //其他状态变为不可用
            View_ZoomOut.IsChecked = false;
            View_ZoomIn.IsChecked = false;
        }

        //添加污水检查井
        private void Edit_Add_WasteJunc_Click(object sender, RoutedEventArgs e)
        {
            int index = 4;
            if(!View_Show_Wastejunc.IsChecked)
            {
                MessageBox.Show("污水检查井图层为隐藏，无法操作");
                return;
            }
            MapGrid.Cursor = CreateCur("/Assets/add.cur");                    //表示添加状态
            for (int i = 0; i < listLayer.Count; i++)
            {
                if (i == index)
                    continue;
                listLayer.ElementAt(i).SetOperationMode(IState.SELECTMODE);   //进入选择状态
            }
            listLayer.ElementAt(index).SetOperationMode(IState.ADDMODE);      //进入添加状态
            View_Move.IsChecked = false;                                      //其他状态变为不可用
            View_ZoomOut.IsChecked = false;
            View_ZoomIn.IsChecked = false;
        }

        private void Edit_Add_WastePipe_Click(object sender, RoutedEventArgs e)
        {
            int index = 2;
            if(!View_Show_Wastepipe.IsChecked)
            {
                MessageBox.Show("污水管道图层为隐藏，无法操作");
                return;
            }
            MapGrid.Cursor = CreateCur("/Assets/add.cur");                    //表示添加状态
            for (int i = 0; i < listLayer.Count; i++)
            {
                if (i == index)
                    continue;
                listLayer.ElementAt(i).SetOperationMode(IState.SELECTMODE);   //进入选择状态
            }
            listLayer.ElementAt(index).SetOperationMode(IState.ADDMODE);      //进入添加状态
            View_Move.IsChecked = false;                                      //其他状态变为不可用
            View_ZoomOut.IsChecked = false;
            View_ZoomIn.IsChecked = false;
        }

        private void Edit_Add_RainPipe_Click(object sender, RoutedEventArgs e)
        {
            int index = 1;
            if (!View_Show_Rainpipe.IsChecked)
            {
                MessageBox.Show("雨水管道图层为隐藏，无法操作");
                return;
            }
            MapGrid.Cursor = CreateCur("/Assets/add.cur");                    //表示添加状态
            for (int i = 0; i < listLayer.Count; i++)
            {
                if (i == index)
                    continue;
                listLayer.ElementAt(i).SetOperationMode(IState.SELECTMODE);   //进入选择状态
            }
            listLayer.ElementAt(index).SetOperationMode(IState.ADDMODE);      //进入添加状态
            View_Move.IsChecked = false;                                      //其他状态变为不可用
            View_ZoomOut.IsChecked = false;
            View_ZoomIn.IsChecked = false;
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Del_Click(object sender, RoutedEventArgs e)
        {
            MapGrid.Cursor = CreateCur("/Assets/Del.cur");                    //表示添加状态
            for (int i = 0; i < listLayer.Count; i++)
            {
                listLayer.ElementAt(i).SetOperationMode(IState.DELMODE);   //进入选择状态
            }
            View_Move.IsChecked = false;                                      //其他状态变为不可用
            View_ZoomOut.IsChecked = false;
            View_ZoomIn.IsChecked = false;
        }
    }
}
