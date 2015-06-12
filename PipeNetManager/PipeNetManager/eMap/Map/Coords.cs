using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.Map
{
    public class Coords
    {
        public struct Point
        {
            public double x;
            public double y;
        }
        public static Point Mercator2WGS84(Point mercator)
        {
            Point wgs84;
            double x = mercator.x / 20037508.34 * 180;
            double y = mercator.y / 20037508.34 * 180;
            y = 180 / Math.PI * (2 * Math.Atan(Math.Exp(y * Math.PI / 180)) - Math.PI / 2);
            wgs84.x = x;
            wgs84.y = y;
            return wgs84;
        }
        public static Point WGS842Mercator(Point wgs84)
        {
            Point mercator;
            double x = wgs84.x * 20037508.34 / 180;
            double y = Math.Log(Math.Tan((90 + wgs84.y) * Math.PI / 360)) / (Math.PI / 180);
            y = y * 20037508.34 / 180;
            mercator.x = x;
            mercator.y = y;
            return mercator;
        }
    }
}
