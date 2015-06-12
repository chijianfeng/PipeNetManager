using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GIS.Arc
{
    /// <summary>
    /// 色彩中心
    /// 用于统一管理系统使用色彩的配置
    /// </summary>
    public class ColorCenter
    {
        List<Color> Rain_Defect_Pipe_Colors = new List<Color>();

        List<Color> Waste_Defect_Pipe_Colors = new List<Color>();

        //雨水井盖
        public SolidColorBrush Cover_Rain_Fill_Color = new SolidColorBrush();
        //污水井盖
        public SolidColorBrush Cover_Waste_Fill_Color = new SolidColorBrush();
        //雨水管道
        public SolidColorBrush Pipe_Rain_Fill_Color = new SolidColorBrush();
        //污水管道
        public SolidColorBrush Pipe_Waste_Fill_Color = new SolidColorBrush();
        //边框色彩 - 未选中
        public SolidColorBrush UnSelected_Border_Color = new SolidColorBrush();
        //边框色彩 - 选中
        public SolidColorBrush Selected_Border_Color = new SolidColorBrush();
        //填充色彩 - 选中
        public SolidColorBrush Seleted_Fill_Color = new SolidColorBrush();

        private ColorCenter() 
        {
            initDefectPipeColors();
            initNormalColorBrush();
        }

        private void initNormalColorBrush()
        {
            Cover_Rain_Fill_Color.Color = Rain_Defect_Pipe_Colors[0];//Colors.LightGreen;
            Cover_Waste_Fill_Color.Color = Waste_Defect_Pipe_Colors[0];//Colors.DimGray;
            Pipe_Rain_Fill_Color.Color = Rain_Defect_Pipe_Colors[0];//Colors.LightGreen;
            Pipe_Waste_Fill_Color.Color = Waste_Defect_Pipe_Colors[0];//Colors.DimGray;
            UnSelected_Border_Color.Color = Colors.Black;
            Selected_Border_Color.Color = Colors.Red;
            Seleted_Fill_Color.Color = Colors.LightCoral;
        }

        private void initDefectPipeColors()
        {
            for(int i=0;i<=4;i++)
            {
                Rain_Defect_Pipe_Colors.Add(Color.FromArgb(255, 0, (byte)(255-i*60), 255));
                Waste_Defect_Pipe_Colors.Add(Color.FromArgb(255, 255, (byte)(255-i*60), 0));
            }
        }

        private static ColorCenter instance = null;

        public Color GetRainDefectPipeColor(int i)
        {
            if (i < 0 || i > 4)
                return Rain_Defect_Pipe_Colors[0];
            return Rain_Defect_Pipe_Colors[i];
        }
        public Color GetWasteDefectPipeColor(int i)
        {
            if (i < 0 || i > 4)
                return Waste_Defect_Pipe_Colors[0];
            return Waste_Defect_Pipe_Colors[i];
        }
        public static ColorCenter GetInstance()
        {
            if (instance == null)
                instance = new ColorCenter();
            return instance;
        }
    }
}
