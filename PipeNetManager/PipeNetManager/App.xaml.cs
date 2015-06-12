using GIS.Map;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PipeNetManager
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        // 瓦片图
        public static List<Tile> Tiles = null;

        public static Level Cur_Level = null;
        public static int Cur_Level_Index = 0;   //当前系统层级
        public static int Cur_Row;
        public static int Cur_Column;

        public static Thickness MoveRect;

        public static int TotalLevels;          //总的层级数

        public static int StrokeThinkness = 1;  //元素的初始大小


        public static Point ReferencePoint;     //地图参考点
        public static Point ReferenceDPoint;    //地图便宜距离

        //数据库访问
        public static string PassWord = "1234";
        public static string _dbpath = "";
    }
}
