using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GIS.Map
{
    class Loader
    {
        String url = "map";
        public Loader() { }

        public Loader(String url)
        {
            this.url = url;
        }

        public Tile LoadTile(int x,int y)
        {
            return new Tile();
        }

        public Detail LoadMapDetail()
        {
            Detail detail = new Detail();

            String detail_file_name = Path.Combine(url, "detail.txt");
            FileStream file = new FileStream(detail_file_name, FileMode.Open);
            StreamReader reader = new StreamReader(file, Encoding.UTF8);
            String level_str = reader.ReadLine();
            String[] lvls = level_str.Split(' ');
            file.Close();
            reader.Close();

            for (int i = 0; i < lvls.Length; i++)
            {
                Level level = new Level();
                String lvl_path_name = Path.Combine(url, lvls[i]);
                string[] fileList = System.IO.Directory.GetFileSystemEntries(lvl_path_name);
                for (int j = 0; j < fileList.Length;j = j+4 )
                {
                    Tile tile = new Tile();
                    String name = Path.GetFileNameWithoutExtension(fileList[j]);
                    tile.Filename = fileList[j+1];
                    file = new FileStream(fileList[j], FileMode.Open);
                    reader = new StreamReader(file, Encoding.UTF8);
                    tile.Dx = double.Parse(reader.ReadLine());
                    reader.ReadLine();
                    reader.ReadLine();
                    tile.Dy = -double.Parse(reader.ReadLine());
                    tile.X = double.Parse(reader.ReadLine());
                    tile.Y = double.Parse(reader.ReadLine());

                    String Name = System.IO.Path.GetFileNameWithoutExtension(tile.Filename);
                    String[] strs = Name.Split('-');
                    tile.Row = int.Parse(strs[0]);
                    tile.Column =  int.Parse(strs[1]);
                    tile.RowColumn = tile.Row + "-" + tile.Column;
                    level.M_Tiles.Add(tile.RowColumn,tile);
                }
                level.FTile = level.M_Tiles[level.M_Tiles.Keys.First<String>()];
                level.LTile = level.M_Tiles[level.M_Tiles.Keys.Last<String>()];
                level.Level_Name = lvls[i];
                detail.Levels.Add(level);
            }

            return detail;
        }

        int GetRow(Tile Start , Tile end)
        {
            String StartName = Path.GetFileNameWithoutExtension(Start.Filename);
            String EndName = Path.GetFileNameWithoutExtension(end.Filename);
            String[] strs = StartName.Split('-');
            int startRow = int.Parse(strs[0]);
            int startColumn = int.Parse(strs[1]);
            strs = EndName.Split('-');
            int endRow = int.Parse(strs[0]);
            int endColumn = int.Parse(strs[1]);

            return endRow-startRow+1;
        }

        int GetColumn(Tile Start, Tile end)
        {
            String StartName = Path.GetFileNameWithoutExtension(Start.Filename);
            String EndName = Path.GetFileNameWithoutExtension(end.Filename);
            String[] strs = StartName.Split('-');
            int startRow = int.Parse(strs[0]);
            int startColumn = int.Parse(strs[1]);
            strs = EndName.Split('-');
            int endRow = int.Parse(strs[0]);
            int endColumn = int.Parse(strs[1]);

            return endColumn - startColumn+1;
        }
    }
}
