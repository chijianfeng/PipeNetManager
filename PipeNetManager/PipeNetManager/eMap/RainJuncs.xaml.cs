using DBCtrl.DBClass;
using DBCtrl.DBRW;
using GIS.Arc;
using PipeMessage.eMap;
using PipeNetManager.eMap.State;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

namespace PipeNetManager.eMap
{
    /// <summary>
    /// RainJuncs.xaml 的交互逻辑
    /// </summary>
    public partial class RainJuncs : BaseControl
    {
        public RainJuncs()
        {
            InitializeComponent();
            InitRainJuncs();
            //UpdateRainJuncs();
            AddJuncs();
        }

        //初始化相关变量---》》》》》》》》》》进行坐标转换加速
        private void InitRainJuncs()
        {
            if(listRains==null)
            {
                listRains = new List<RainCover>();
                //加载雨水检查井
                TJuncInfo juninfo = new TJuncInfo(App._dbpath , App.PassWord);
                List<CJuncInfo> tmplist = juninfo.Sel_JuncInfoByCaty(1);            //仅仅加载雨水检查井
                //进行坐标转换
                foreach(CJuncInfo junc in tmplist)
                {
                    if (junc.X_Coor == 0)                                           //无座标
                        continue;
                    RainCover cover = null;
                    Point p = new Point(junc.X_Coor + 0.0045, junc.Y_Coor - 0.0034);

                    cover = new RainCover(junc.JuncName, GISConverter.WGS842Merator(p), junc.SystemID);
                    cover.juncInfo = junc;
                    listRains.Add(cover);
                }
            }
            RainGrid.Margin = new Thickness(-0, -0, 0, 0);                          //初始化相对位置
            state = new RainJuncState(this);
            
            //将点坐标进行保存
            Rainpx = new float[listRains.Count+500];
            Rainpy = new float[listRains.Count+500];
        }

        //向图层添加检查井
        private unsafe void AddJuncs()
        {
            Task.Factory.StartNew((Obj) =>
            {
                Parallel.For(0, (int)Obj, i =>              //并行计算
                {
                    Rainpx[i] = (float)((listRains[i].Location.X - App.Tiles[0].X) / App.Tiles[0].Dx);
                    Rainpy[i] = (float)((App.Tiles[0].Y - listRains[i].Location.Y) / App.Tiles[0].Dy);
                });
            }, listRains.Count).ContinueWith(ant => {
                state.AddRainJunc(listRains, Rainpx, Rainpy);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void AddJunc(RainCover c)           //添加雨水检查井
        {
            listRains.Add(c);
            //计算点的坐标
            Rainpx[listRains.Count] = (float)((c.Location.X - App.Tiles[0].X) / App.Tiles[0].Dx);
            Rainpy[listRains.Count] = (float)((App.Tiles[0].Y - c.Location.Y) / App.Tiles[0].Dy);
        }

        public void DelJunc(RainCover c)
        {
            int index = 0;
            foreach(RainCover tmpc in listRains)
            {
                if(c.Name.Equals(tmpc.Name))
                {
                    break;
                }
                index++;
            }
            if (index < listRains.Count)
                listRains.RemoveAt(index);
        }

        /// <summary>
        ///在现有的检查井中寻找最接近的点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public RainCover FindClosedCover(Point p)
        {
            RainCover cover = null;
            double dis = App.StrokeThinkness;
            for (int i = 0; i < listRains.Count;i++ )
            {
                if (Math.Abs(Rainpx[i] - p.X) > dis || Math.Abs(Rainpy[i] - p.Y) > dis)
                    continue;
                double d = Math.Sqrt((Rainpx[i] - p.X) * (Rainpx[i] - p.X) +
                    (Rainpy[i] - p.Y) * (Rainpy[i] - p.Y));                       //计算距离
                if (dis > d)
                {
                    dis = d;
                    cover = listRains[i];
                }
            }
            return cover;
        }

        //更新检查井--》》》》》》》进行加速
        private void UpdateRainJuncs()
        {
            //计算相对位置
            /*Point Start = new Point();
            Point End = new Point();

            if (App.Tiles.Count <= 0)
                return;

            Start.X = App.Tiles[0].X; Start.Y = App.Tiles[0].Y;
            End.X = App.Tiles[App.Tiles.Count - 1].X + 256 * App.Tiles[App.Tiles.Count - 1].Dx;
            End.Y = App.Tiles[App.Tiles.Count - 2].Y - 256 * App.Tiles[App.Tiles.Count - 2].Dy;*/
            
            Task.Factory.StartNew<int>((Obj) =>
            {
                Parallel.For(0, (int)Obj, i =>              //并行计算
                {
                    Rainpx[i] = (float)((listRains[i].Location.X - App.Tiles[0].X) / App.Tiles[0].Dx);
                    Rainpy[i] = (float)((App.Tiles[0].Y - listRains[i].Location.Y) / App.Tiles[0].Dy);
                });
                return 0;
            }, listRains.Count).ContinueWith(ant =>
            {
                state.UpdateJuncPos(Rainpx, Rainpy);
            }, TaskScheduler.FromCurrentSynchronizationContext());
            this.RainGrid.Margin = App.MoveRect;
        }
        public override void OnViewOriginal(object sender, RoutedEventArgs e)
        {
            UpdateRainJuncs();
        }

        public override void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsViewMove)
            {
                IsMousedown = false;
            }
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsViewMove || !IsMousedown)
            {
                state.OnMouseMove(sender, e);                       //默认处理
                return; 
            }
            Grid CurGrid = this.RainGrid;
            CurGrid.Margin = App.MoveRect;
        }

        public override void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            if(IsViewMove)
            {
                IsMousedown = true;
                return;
            }
            if (IsZoomIn)            //放大操作
            {
                if (App.Cur_Level_Index > App.TotalLevels||IsHidden)    //达到最大放大级别或者该图层为隐藏
                    return;
                UpdateRainJuncs();
                return;
            }
            if (IsZoomOut)          //缩小
            {
                if (App.Cur_Level_Index < 1||IsHidden)
                    return;
                UpdateRainJuncs();
                return;
            }
            state.OnMouseDown(sender, e);                               //其他操作，删除，选择操作等
            return;
        }

        public new void Update()                               //强制更新检查井位置
        {
            UpdateRainJuncs();
        }
        public override void SetOperationMode(int mode)
        {
            if (state == null)
                return;
            state.CurrentMode = mode;
        }

        public List<RainCover> listRains = null;               //雨水检查井集合

        RainJuncState state = null;                            //操作 

        bool IsMousedown = false;                              //鼠标是否按下

        unsafe float[] Rainpx = null;                           //检查井坐标
        unsafe float[] Rainpy = null;
    }
}
