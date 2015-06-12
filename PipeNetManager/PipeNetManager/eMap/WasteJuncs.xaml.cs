using DBCtrl.DBClass;
using DBCtrl.DBRW;
using GIS.Arc;
using PipeMessage.eMap;
using PipeNetManager.eMap.State;
using System;
using System.Collections.Generic;
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
    /// WasteJuncs.xaml 的交互逻辑
    /// </summary>
    public partial class WasteJuncs : BaseControl
    {
        public WasteJuncs()
        {
            InitializeComponent();
            InitWasteJuncs();               //初始化
            AddJuncs();                     //添加污水井
        }

        private void InitWasteJuncs()
        {
            if (listWaste == null)
            {
                listWaste = new List<WasteCover>();
                //加载雨水检查井
                TJuncInfo juninfo = new TJuncInfo(App._dbpath, App.PassWord);
                List<CJuncInfo> tmplist = juninfo.Sel_JuncInfoByCaty(2);            //仅仅加载污水检查井
                //进行坐标转换
                foreach (CJuncInfo junc in tmplist)
                {
                    if (junc.X_Coor == 0)                                           //无座标
                        continue;
                    WasteCover cover = null;
                    Point p = new Point(junc.X_Coor + 0.0045, junc.Y_Coor - 0.0034);

                    cover = new WasteCover(junc.JuncName, GISConverter.WGS842Merator(p), junc.SystemID);
                    cover.juncInfo = junc;
                    listWaste.Add(cover);
                }
            }
            WasteGrid.Margin = new Thickness(-0, -0, 0, 0);
            state = new WasteJuncState(this);

            //将点坐标进行保存
            Wastepx = new float[listWaste.Count];
            Wastepy = new float[listWaste.Count];
        }

        private  void AddJuncs()
        {
            Task.Factory.StartNew<int>((Obj) =>
           {
               Parallel.For(0, (int)Obj, i =>              //并行计算
               {
                   Wastepx[i] = (float)((listWaste[i].Location.X - App.Tiles[0].X) / App.Tiles[0].Dx);
                   Wastepy[i] = (float)((App.Tiles[0].Y - listWaste[i].Location.Y) / App.Tiles[0].Dy);
               });
               return 0;
           }, listWaste.Count).ContinueWith(ant => {
               state.AddWasteJunc(listWaste, Wastepx, Wastepy);
           }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void AddWasteJunc(WasteCover wc)
        {
            listWaste.Add(wc);
            Wastepx = new float[listWaste.Count];
            Wastepy = new float[listWaste.Count];
        }

        public void DelWasteJunc(WasteCover c)
        {
            int index = 0;
            foreach (WasteCover tmpc in listWaste)
            {
                if (c.Name.Equals(tmpc.Name))
                {
                    break;
                }
                index++;
            }
            if (index < listWaste.Count)
                listWaste.RemoveAt(index);
        }

        public WasteCover FindClosedCover(Point p)
        {
            WasteCover cover = null;
            double dis = App.StrokeThinkness;
            for (int i = 0; i < listWaste.Count; i++)
            {
                if (Math.Abs(Wastepx[i] - p.X) > dis || Math.Abs(Wastepy[i] - p.Y) > dis)
                    continue;
                double d = Math.Sqrt((Wastepx[i] - p.X) * (Wastepx[i] - p.X) +
                    (Wastepy[i] - p.Y) * (Wastepy[i] - p.Y));                       //计算距离
                if (dis > d)
                {
                    dis = d;
                    cover = listWaste[i];
                }
            }
            return cover;
        }

        private void UpdateWasteJuncs()
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
                    Wastepx[i] = (float)((listWaste[i].Location.X - App.Tiles[0].X) / App.Tiles[0].Dx);
                    Wastepy[i] = (float)((App.Tiles[0].Y - listWaste[i].Location.Y) / App.Tiles[0].Dy);
                });
                return 0;
            }, listWaste.Count).ContinueWith(ant =>
            {
                state.UpdateJuncPos(Wastepx, Wastepy);
            }, TaskScheduler.FromCurrentSynchronizationContext());
            this.WasteGrid.Margin = App.MoveRect;
        }

        public override void OnViewOriginal(object sender, RoutedEventArgs e)
        {
            UpdateWasteJuncs();
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
            Grid CurGrid = this.WasteGrid;
            CurGrid.Margin = App.MoveRect;
        }
        public override void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (IsViewMove)
            {
                IsMousedown = true;
                return;
            }
            
            if (IsZoomIn)            //放大或缩小操作
            {
                if (App.Cur_Level_Index  > App.TotalLevels||IsHidden)
                        return;
                 UpdateWasteJuncs();
                    return;
            }
           if (IsZoomOut)
           {
              if (App.Cur_Level_Index < 1||IsHidden)
                    return;
              UpdateWasteJuncs();
              return;
           }
            state.OnMouseDown(sender, e);                   //其他操作，删除，选择操作等
        }

        public new void Update()
        {
            UpdateWasteJuncs();
        }

        public override void SetOperationMode(int mode)
        {
            if (null == state)
                return;
            state.CurrentMode = mode;
        }

        public List<WasteCover> listWaste = null;
        WasteJuncState  state = null;                           //操作 

        bool IsMousedown = false;                              //鼠标是否按下

        float[] Wastepx = null;                                 //污水检查井位置
        float[] Wastepy = null;

      
    }
}
