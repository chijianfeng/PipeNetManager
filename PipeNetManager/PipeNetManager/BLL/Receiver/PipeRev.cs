using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.DBRW;
using DBCtrl.DBClass;

namespace BLL.Receiver
{
    public class PipeRev : BasicRev
    {
        /// <summary>
        /// 管道基本信息
        /// </summary>
        public List<CPipeInfo> ListPipe
        {
            set;
            get;
        }
        /// <summary>
        /// 管道附加信息
        /// </summary>
        public List<CPipeExtInfo> ListPipeExt
        {
            set;
            get;
        }

        /// <summary>
        /// 管道内壁检查信息
        /// </summary>
        public List<CUSInfo> ListUS
        {
            set;
            get;
        }
        /// <summary>
        /// 管道日志信息
        /// </summary>
        public List<CLog_Pipe> ListLog
        {
            set;
            get;
        }
        /// <summary>
        /// 管道图像信息
        /// </summary>
        public List<CPic_Pipe> ListPic
        {
            set;
            get;
        }
        /// <summary>
        /// 管道文档报告信息
        /// </summary>
        public List<CReport_Pipe> ListRpt
        {
            set;
            get;
        }
        /// <summary>
        /// 管道视频信息
        /// </summary>
        public List<CVideo_Pipe> ListVideo
        {
            set;
            get;
        }

        /// <summary>
        /// 管道名称
        /// </summary>
        public string PipeName
        {
            set;
            get;
        }

        public PipeRev()
        {
            ListPipe = null;
            ListPipeExt = null;
            ListPic = null;
            ListLog = null;
            ListUS = null;
            ListRpt = null;
            ListVideo = null;
        }

        public override bool Docmd(string cmd)
        {
            if (cmd.Equals("Load"))
            {
                return DoLoad();
            }
            else if (cmd.Equals("Select"))
            {
                return DoSelect();
            }
            else if (cmd.Equals("Update"))
            {
                return DoUpdate();
            }
            else if (cmd.Equals("Insert"))
            {
                return DoInsert();
            }
            else if (cmd.Equals("Delete"))
            {
                return DoDelete();
            }
            else if (cmd.Equals("Clear"))
            {
                return DoClear();
            }
            else if (cmd.Equals("QuickInsert"))
            {
                return DoQuickInsert();
            }
            return true;
        }

      

        /// <summary>
        /// 导入管道信息，附加信息;但不会导入管道检测信息，管道日志，图片，报告，视频信息
        /// </summary>
        /// <returns></returns>
        private bool DoLoad()
        {
            TPipeInfo pipeinfo = new TPipeInfo(_dbpath, PassWord);
            TPipeExtInfo pipextinfo = new TPipeExtInfo(_dbpath, PassWord);
            TUSInfo usinfo = new TUSInfo(_dbpath, PassWord);
            pipeinfo.OpenDB();
            pipextinfo.OpenDB();
            usinfo.OpenDB();

            ListPipe = pipeinfo.Load_PipeInfo();
            ListPipeExt = pipextinfo.Load_PipeExtInfo();
            ListUS = usinfo.Load_USInfo();

            pipeinfo.CloseDB();
            pipextinfo.CloseDB();
            usinfo.CloseDB();
            if (ListPipe == null || ListPipeExt == null||ListUS==null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据管道名称获取管道信息，附加信息;管道检测信息，管道日志，图片，报告，视频信息
        /// </summary>
        /// <returns></returns>
        private bool DoSelect()
        {
            if (PipeName == null || PipeName.Length <= 0)
                return false;
            TPipeInfo pipeinfo = new TPipeInfo(_dbpath, PassWord);
            TPipeExtInfo pipextinfo = new TPipeExtInfo(_dbpath, PassWord);
            TUSInfo usinfo = new TUSInfo(_dbpath, PassWord);
            TPicPipe pic = new TPicPipe(_dbpath, PassWord);
            TVideoPipe video = new TVideoPipe(_dbpath, PassWord);
            TReportPipe repott = new TReportPipe(_dbpath, PassWord);

            ListPipe = pipeinfo.Sel_PipeInfo(PipeName);
            if (ListPipe == null || ListPipe.Count <= 0)
                return false;

            ListPipeExt = new List<CPipeExtInfo>();
            ListUS = new List<CUSInfo>();
            ListLog = new List<CLog_Pipe>();
            ListPic = new List<CPic_Pipe>();
            ListVideo = new List<CVideo_Pipe>();
            ListRpt = new List<CReport_Pipe>();

            foreach(CPipeInfo pipe in ListPipe)
            {
                int id = pipe.ID;
                List<CPipeExtInfo> list1 = pipextinfo.Sel_PipeExtInfo(id);
                if (list1 != null && list1.Count > 0)
                {
                    ListPipeExt.Add(list1.ElementAt(0));
                }
                List<CUSInfo> list2 = usinfo.Sel_USInfo(id);
                if (list2 != null && list2.Count > 0)
                {
                    ListUS.Add(list2.ElementAt(0));
                }

                List<CPic_Pipe> list4 = pic.Sel_Pic_Pipe(id);
                if (list4 != null && list4.Count > 0)
                {
                    ListPic.Add(list4.ElementAt(0));
                }

                List<CVideo_Pipe> list5 = video.Sel_Video_Pipe(id);
                if (list5 != null && list5.Count > 0)
                {
                    ListVideo.Add(list5.ElementAt(0));
                }
                List<CReport_Pipe> list6 = repott.Sel_Report_Pipe(id);
                if (list6 != null && list6.Count > 0)
                {
                    ListRpt.Add(list6.ElementAt(0));
                }
            }
            return true;
        }

        /// <summary>
        /// 更新管道信息，对现有数据更新，如不更新其他数据则设为null
        /// </summary>
        /// <returns></returns>
        private bool DoUpdate()
        {
            TPipeInfo pipeinfo = new TPipeInfo(_dbpath, PassWord);
            TPipeExtInfo pipextinfo = new TPipeExtInfo(_dbpath, PassWord);
            TUSInfo usinfo = new TUSInfo(_dbpath, PassWord);
            TLogPipe log = new TLogPipe(_dbpath, PassWord);
            TPicPipe pic = new TPicPipe(_dbpath, PassWord);
            TVideoPipe video = new TVideoPipe(_dbpath, PassWord);
            TReportPipe repott = new TReportPipe(_dbpath, PassWord);

            pipeinfo.Update_PipeInfo(ListPipe);
            pipextinfo.Update_PipeExtInfo(ListPipeExt);
            usinfo.Update_USInfo(ListUS);
            log.Update_Log_Pipe(ListLog);
            pic.Update_Pic_Pipe(ListPic);
            video.Update_Video_Pipe(ListVideo);
            repott.Update_Report_Pipe(ListRpt);

            return true;
        }

        /// <summary>
        /// 插入管道信息，和附加信息，管道检测信息，管道日志，图片，报告，视频信息
        /// </summary>
        /// <returns></returns>
        private bool DoInsert()
        {
            if (ListPipe == null)
                return false;
            TPipeInfo pipeinfo = new TPipeInfo(_dbpath, PassWord);
            TPipeExtInfo pipextinfo = new TPipeExtInfo(_dbpath, PassWord);
            TUSInfo usinfo = new TUSInfo(_dbpath, PassWord);
            /*TLogPipe log = new TLogPipe(_dbpath, PassWord);
            TPicPipe pic = new TPicPipe(_dbpath, PassWord);
            TVideoPipe video = new TVideoPipe(_dbpath, PassWord);
            TReportPipe repott = new TReportPipe(_dbpath, PassWord);*/

            pipeinfo.OpenDB();
            pipextinfo.OpenDB();
            usinfo.OpenDB();
            

            int i = 0;
            foreach (CPipeInfo pipe in ListPipe)
            {
                CPipeInfo tmp = pipe;
                //插入附加信息
                CPipeExtInfo extmp = null;
                if (!pipeinfo.Insert_PipeInfo(ref tmp))
                {
                    continue;
                }
                if (ListPipeExt == null || ListPipeExt.Count == 0)
                {
                    extmp = new CPipeExtInfo();
                }
                else
                {
                    if (i < ListPipeExt.Count)
                        extmp = ListPipeExt.ElementAt(i);
                    else
                        extmp = new CPipeExtInfo();
                }
                extmp.PipeID = tmp.ID;
                pipextinfo.Insert_PipeExtInfo(ref extmp);

                //插入管道检测信息
                CUSInfo ustmp = null;
                if (ListUS == null || ListUS.Count == 0)
                {
                    ustmp = new CUSInfo();
                }
                else
                {
                    if (i < ListUS.Count)
                        ustmp = ListUS.ElementAt(i);
                    else
                        ustmp = new CUSInfo();
                }
                ustmp.PipeID = tmp.ID;
                usinfo.Insert_USInfo(ref ustmp);

                //插入管道日志
               /* CLog_Pipe logtmp = null;
                if (ListLog == null)
                {
                    logtmp = new CLog_Pipe();
                }
                else
                {
                    if (i < ListLog.Count)
                        logtmp = ListLog.ElementAt(i);
                    else
                        logtmp = new CLog_Pipe();
                }
                logtmp.PipeID = tmp.ID;
                log.Insert_Log_Pipe(ref logtmp);*/

                //插入图片
              /*  CPic_Pipe pictmp = null;
                if (ListPic == null)
                {
                    pictmp = new CPic_Pipe();
                }
                else
                {
                    if (i < ListPic.Count)
                        pictmp = ListPic.ElementAt(i);
                    else
                        pictmp = new CPic_Pipe();
                }
                pictmp.PipeID = tmp.ID;
                pic.Insert_Pic_Pipe(ref pictmp);*/

                //插入报告
               /* CReport_Pipe rptmp = null;
                if (ListRpt == null)
                {
                    rptmp = new CReport_Pipe();
                }
                else
                {
                    if (i < ListRpt.Count)
                        rptmp = ListRpt.ElementAt(i);
                    else
                        rptmp = new CReport_Pipe();
                }
                rptmp.PipeID = tmp.ID;
                repott.Insert_Report_Pipe(ref rptmp);*/

                //插入视频信息
              /*  CVideo_Pipe vtmp = null;
                if (ListVideo == null)
                {
                    vtmp = new CVideo_Pipe();
                }
                else
                {
                    if (i < ListVideo.Count)
                        vtmp = ListVideo.ElementAt(i);
                    else
                        vtmp = new CVideo_Pipe();
                }
                vtmp.PipeID = tmp.ID;
                video.Insert_Video_Pipe(ref vtmp);*/

                i++;
            }

            //close the db connection
            pipeinfo.CloseDB();
            pipextinfo.CloseDB();
            usinfo.CloseDB();
           

            return true;
        }
        private bool DoQuickInsert()
        {
            if (ListPipe == null||ListPipe.Count==0)
            {
                if (ListUS != null && ListUS.Count > 0)
                    return InsertUs();
                else
                    return false;
            }
            TPipeInfo pipeinfo = new TPipeInfo(_dbpath, PassWord);
            TPipeExtInfo pipextinfo = new TPipeExtInfo(_dbpath, PassWord);
            pipeinfo.OpenDB();
            pipextinfo.OpenDB();

            List<int> listid = new List<int>();

            if (!pipeinfo.Insert_PipeInfo(ListPipe, ref listid))
                return false;

            int nCount = 0;
            List<CPipeExtInfo> newllist = new List<CPipeExtInfo>();
            foreach (CPipeExtInfo pipe in ListPipeExt)
            {
                pipe.PipeID = listid.ElementAt(nCount++);
                newllist.Add(pipe);
            }
            if (!pipextinfo.Insert_PipeExtInfo(ListPipeExt))
                return false;
            pipeinfo.CloseDB();
            pipextinfo.CloseDB();
            return true;
        }

        private bool InsertUs()
        {
            TUSInfo usinfo = new TUSInfo(_dbpath, PassWord);

            usinfo.OpenDB();
            bool ret = usinfo.Insert_USInfo(ListUS);
            usinfo.CloseDB();
            return ret;
        }
        
        /// <summary>
        /// 删除管道的基本信息，同时删除其他关联信息,
        /// :附加信息，管道检测信息，管道日志，图片，报告，视频信息
        /// </summary>
        /// <returns></returns>
        private bool DoDelete()
        {
            TPipeInfo pipeinfo = new TPipeInfo(_dbpath, PassWord);
            TPipeExtInfo pipextinfo = new TPipeExtInfo(_dbpath, PassWord);
            TUSInfo usinfo = new TUSInfo(_dbpath, PassWord);
            TLogPipe log = new TLogPipe(_dbpath, PassWord);
            TPicPipe pic = new TPicPipe(_dbpath, PassWord);
            TVideoPipe video = new TVideoPipe(_dbpath, PassWord);
            TReportPipe repott = new TReportPipe(_dbpath, PassWord);

            if (ListPipe == null || ListPipe.Count == 0)
                return false;

            foreach (CPipeInfo pipe in ListPipe)
            {
                pipeinfo.Delete_PipeInfo(pipe);
                
                CPipeExtInfo ext = null;
                ListPipeExt = pipextinfo.Sel_PipeExtInfo(pipe.ID);
                if (ListPipeExt != null && ListPipeExt.Count > 0)
                    ext = ListPipeExt.ElementAt(0);
                pipextinfo.Delete_PipeExtInfo(ext);

                CUSInfo us = null;
                ListUS = usinfo.Sel_USInfo(pipe.ID);
                if ( ListUS.Count > 0)
                    us = ListUS.ElementAt(0);
                usinfo.Delete_USInfo(us);

                CLog_Pipe l = null;
                ListLog = log.Sel_Log_Pipe(pipe.ID);
                if ( ListLog.Count > 0)
                    l = ListLog.ElementAt(0);
                log.Delete_Log_Pipe(l);

                CPic_Pipe p = null;
                ListPic = pic.Sel_Pic_Pipe(pipe.ID);
                if (ListPic.Count > 0)
                    p = ListPic.ElementAt(0);
                pic.Delete_Pic_Pipe(p);

                CVideo_Pipe v = null;
                ListVideo = video.Sel_Video_Pipe(pipe.ID);
                if ( ListVideo.Count > 0)
                    v = ListVideo.ElementAt(0);
                video.Delete_Video_Pipe(v);

                CReport_Pipe r = null;
                ListRpt = repott.Sel_Report_Pipe(pipe.ID);
                if (ListRpt.Count > 0)
                    r = ListRpt.ElementAt(0);
                repott.Delete_Report_Pipe(r);
            }

            return true;
        }

        private bool DoClear()
        {
            TPipeInfo pipeinfo = new TPipeInfo(_dbpath, PassWord);
            TPipeExtInfo pipextinfo = new TPipeExtInfo(_dbpath, PassWord);
            TUSInfo usinfo = new TUSInfo(_dbpath, PassWord);
            TLogPipe log = new TLogPipe(_dbpath, PassWord);
            TPicPipe pic = new TPicPipe(_dbpath, PassWord);
            TVideoPipe video = new TVideoPipe(_dbpath, PassWord);
            TReportPipe repott = new TReportPipe(_dbpath, PassWord);

            pipeinfo.Clear_PipeInfo();
            pipextinfo.Clear_PipeExtInfo();
            usinfo.Clear_USInfo();
            log.Clear_Log_Pipe();
            pic.Clear_Pic_Pipe();
            video.Clear_Video_Pipe();
            repott.Clear_Report_Pipe();


            return true;
        }
    }
}
