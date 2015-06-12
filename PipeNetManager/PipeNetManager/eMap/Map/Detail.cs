using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.Map
{
    /// <summary>
    /// 地图详情
    /// </summary>
    public class Detail
    {
        /// <summary>
        /// 层级数目
        /// </summary>
        public int Level_Count { get { return Levels.Count; } }
        /// <summary>
        /// 全部层级
        /// </summary>
        public List<Level> Levels = new List<Level>();
    }
}
