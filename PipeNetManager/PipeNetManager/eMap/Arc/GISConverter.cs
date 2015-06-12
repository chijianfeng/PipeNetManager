using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GIS.Arc
{
    public class GISConverter
    {
        public static readonly double PI = Math.PI;

        public static Point Mercator2WGS84(Point mercator)
        {
            Point wgs84 = new Point();
            double x = mercator.X / 20037508.34 * 180;
            double y = mercator.Y / 20037508.34 * 180;
            y = 180 / PI * (2 * Math.Atan(Math.Exp(y * PI / 180)) - PI / 2);
            wgs84.X = x;
            wgs84.Y = y;
            return wgs84;
        }
        public static Point WGS842Merator(Point wgs84)
        {
            Point mercator = new Point();
            double x = wgs84.X * 20037508.34 / 180;
            double y = Math.Log(Math.Tan((90 + wgs84.Y) * PI / 360)) / (PI / 180);
            y = y * 20037508.34 / 180;
            mercator.X = x;
            mercator.Y = y;
            return mercator;
        }
    }
}
