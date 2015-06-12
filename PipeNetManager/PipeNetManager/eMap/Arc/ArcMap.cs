using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BLL.Command;
using BLL.Receiver;
using DBCtrl.DBClass;

namespace GIS.Arc
{
    public class ArcMap
    {
        InsertCmd icmd = new InsertCmd();
        LoadCmd lcmd = new LoadCmd();
        SelectCmd scmd = new SelectCmd();
        UpdateCmd ucmd = new UpdateCmd();
        DeleteCmd dcmd = new DeleteCmd();
        ClearCmd ccmd = new ClearCmd();

        public List<Pipe> PipeList { get; set; }
        public List<Cover> CoverList { get; set; }
        public ArcMap()
        {
            PipeList = new List<Pipe>();
            CoverList = new List<Cover>();
        }

        public void Load() 
        {
            //////////////////////////////////////////////////////////////////////////
            //加载井盖
            JuncRev jrev = new JuncRev();
            lcmd.SetReceiver(jrev);
            lcmd.Execute();
            foreach (CJuncInfo c in jrev.ListJunc)
            {
                if (c.X_Coor == 0)
                    continue;
                Cover cover = null;
                Point p = new Point(c.X_Coor+0.0045,c.Y_Coor-0.0034);
                if(c.Junc_Category == 1)
                    cover = new RainCover(c.JuncName, GISConverter.WGS842Merator(p), c.SystemID);
                else if(c.Junc_Category == 2)
                    cover = new WasteCover(c.JuncName, GISConverter.WGS842Merator(p), c.SystemID);
                cover.juncInfo = c;
                CoverList.Add(cover);
                Cover.NUM++;
            }
            //////////////////////////////////////////////////////////////////////////
            //加载管道
            PipeRev piperev = new PipeRev();
            lcmd.SetReceiver(piperev);
            lcmd.Execute();
            foreach (CPipeInfo cp in piperev.ListPipe)
            {
                Pipe pipe = null;
                Cover StartCover = FindStartCover(cp);
                Cover EndCover = FindEndCover(cp);
                if (cp.Pipe_Category == 1)
                    pipe = new RainPipe(StartCover,EndCover);
                else if (cp.Pipe_Category == 2)
                    pipe = new WastePipe(StartCover, EndCover);
                pipe.pipeInfo = cp;
                if (StartCover!=null)
                    StartCover.Out_Pipe = pipe;
                if (EndCover!=null)
                    EndCover.In_Pipe = pipe;
                pipe.Name = cp.PipeName;
                pipe.UsInfo = FindUSInfo(piperev.ListUS, cp.ID);
                PipeList.Add(pipe);
                Pipe.NUM++;
            }
            Console.WriteLine("Load data complete");
        }

        private CUSInfo FindUSInfo(List<CUSInfo> usinfolist,int pipeId)
        {
            CUSInfo info = null;
            info = usinfolist.Find(us => us.PipeID == pipeId);
            return info;
        }

        public void Save()
        { 
        
        }

        public Cover FindStartCover(CPipeInfo cp)
        {
            Cover c = null;
            c = CoverList.Find( cc => cc.juncInfo.ID == cp.In_JunID);
            return c;
        }

        public Cover FindEndCover(CPipeInfo cp)
        {
            Cover c = null;
            c = CoverList.Find(cc => cc.juncInfo.ID == cp.Out_JunID);
            return c;
        }

        /// <summary>
        /// 查找矩形坐标范围内的井盖集合
        /// </summary>
        /// <param name="Start">左上角坐标</param>
        /// <param name="End">右下角坐标</param>
        /// <returns></returns>
        public List<Cover> FindCover(Point Start,Point End)
        {
            List<Cover> list = new List<Cover>();
            foreach (Cover c in CoverList)
            {
                if (c.Location.X < Start.X || c.Location.X > End.X ||
                    c.Location.Y > Start.Y || c.Location.Y < End.Y)
                    continue;
                else
                    list.Add(c);
            }
            return list;
        }

        /// <summary>
        /// 查找与指定井盖集合相关的管道集合
        /// </summary>
        /// <param name="Covers">井盖集合</param>
        /// <returns></returns>
        public List<Pipe> FindPipe(List<Cover> Covers)
        {
            List<Pipe> list = new List<Pipe>();
            foreach (Cover c in Covers)
            {
                if (c.Out_Pipe != null)
                    list.Add(c.Out_Pipe);
            }
            return list;
        }

        public List<Cover> FindCover(List<Pipe> Pipes)
        {
            List<Cover> covers = new List<Cover>();
            foreach (Pipe p in Pipes)
            {
                if (!covers.Contains(p.Start))
                    covers.Add((Cover)p.Start);
                if (!covers.Contains(p.End))
                    covers.Add((Cover)p.End);
            }
            return covers;
        }
    }
}
