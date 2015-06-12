using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.Map
{
    public class Level
    {
        public static int Total_Row = 4;
        public static int Total_Column = 6;
        /// <summary>
        /// 缩放等级名称
        /// </summary>
        public String Level_Name { get; set; }

        /// <summary>
        /// 该层级下最左上角的X坐标
        /// </summary>
        public double X { set; get; }
        /// <summary>
        /// 该层级下最左上角的Y坐标
        /// </summary>
        public double Y { set; get; }

        /// <summary>
        /// 瓦片行数
        /// </summary>
        public int Row { get; set; }
        
        /// <summary>
        /// 瓦片列数
        /// </summary>
        public int Column { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }
        
        public int Buttom { get; set; }
        
        public int Right { get; set; }

        public Tile FTile { get; set; }
        public Tile LTile { get; set; }

        /// <summary>
        /// 该等级下的全部瓦片信息（不包括位图信息） 
        /// </summary>
        public Dictionary<String,Tile> M_Tiles = new Dictionary<String,Tile>();

        public List<Tile> GetTiles_M(int row, int column)
        {
            Tile FTile = M_Tiles[M_Tiles.Keys.First<String>()];
            Tile LTile = M_Tiles[M_Tiles.Keys.Last<String>()];
            List<Tile> list = new List<Tile>();
            for (int i = row; i < row + Total_Row;i++ )
                for(int j=column;j<column + Total_Column ; j++)
                {
                    Tile t = null;
                    if (i < FTile.Row || i > LTile.Row || j < FTile.Column || j > LTile.Column)
                    {
                        t = new Tile();
                        t.Dx = FTile.Dx;
                        t.Dy = FTile.Dy;
                        t.X = FTile.X + (j - FTile.Column) * 256 * t.Dx;
                        t.Y = FTile.Y - (i - FTile.Row) * 256 * t.Dy;
                        t.Filename = "map/default.jpg";
                    }
                    else
                        t = M_Tiles[i + "-" + j];
                    list.Add(t);
                }
            return list;
        }
    }
}
