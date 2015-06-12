using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.DBClass;
using System.Reflection;
using BLL.Command;
using BLL.Receiver;

namespace BLL
{
    class Program
    {
        private static readonly string filepath = "DBCtrl";
        static void Main(string[] args)
        {
//             CUser user = (CUser)(Assembly.Load(filepath).CreateInstance(filepath + ".DBClass." + "CUser"));
//             if (user != null)
//             {
//                 System.Console.WriteLine(user.ToString());
//                 System.Console.ReadLine();
//             }

            InsertCmd icmd = new InsertCmd();
            LoadCmd lcmd = new LoadCmd();
            SelectCmd scmd = new SelectCmd();
            UpdateCmd ucmd = new UpdateCmd();
            DeleteCmd dcmd = new DeleteCmd();
            ClearCmd ccmd = new ClearCmd();

#region test TUser
             
//             UserRev userrev = new UserRev();
//             CUser u = new CUser();
//             u.UserName = "Peson1";
//             u.UserType = 1;
//             u.PassWord = "000001";
//             List<CUser> listuser = new List<CUser>();
//             listuser.Add(u);
//             userrev.ListUser = listuser;
//             icmd.SetReceiver(userrev);
//             icmd.Execute();

//             lcmd.SetReceiver(userrev);
//             lcmd.Execute();
//             System.Console.WriteLine("Element number:{0}", userrev.ListUser.Count);

//             userrev.UserName = "Peson1";
//             scmd.SetReceiver(userrev);
//             scmd.Execute();
//             System.Console.WriteLine("Element number:{0}", userrev.ListUser.Count);

//             userrev.ListUser.ElementAt(0).PassWord = "127890";
//             ucmd.SetReceiver(userrev);
//             ucmd.Execute();

//             dcmd.SetReceiver(userrev);
//             dcmd.Execute();

//             ccmd.SetReceiver(userrev);
//             ccmd.Execute();
 
#endregion

#region test TSystemBase

            SystemRev sysrev = new SystemRev();

//             CSystemBase sysbase = new CSystemBase();
//             sysbase.SystemID = "2014722002";
//             sysbase.SysName = "test system name";
//             List<CSystemBase> list = new List<CSystemBase>();
//             list.Add(sysbase);
//             sysrev.SysList = list;
//             icmd.SetReceiver(sysrev);
//             icmd.Execute();

//             lcmd.SetReceiver(sysrev);
//             lcmd.Execute();
//             System.Console.WriteLine(sysrev.SysList.Count);
// 
//             dcmd.SetReceiver(sysrev);
//             dcmd.Execute();
#endregion

#region test PumpRev
            PumpRev prev = new PumpRev();
//             CPumpStationInfo pump = new CPumpStationInfo();
//             pump.PumpName = "testpum";
//             List<CPumpStationInfo> list = new List<CPumpStationInfo>();
//             list.Add(pump);
//             prev.ListPump = list;
//             icmd.SetReceiver(prev);
//             icmd.Execute();

//             lcmd.SetReceiver(prev);
//             lcmd.Execute();
//             System.Console.WriteLine(prev.ListPump.Count);

//             prev.PumpName = "testpum";
//             scmd.SetReceiver(prev);
//             scmd.Execute();
//             System.Console.WriteLine(prev.ListPump.Count);

//             prev.ListPump.ElementAt(0).PumpName = "Newname";
//             ucmd.SetReceiver(prev);
//             ucmd.Execute();

//             dcmd.SetReceiver(prev);
//             dcmd.Execute();

//             ccmd.SetReceiver(prev);
//             ccmd.Execute();

#endregion

#region test OutFallinfo
            OutFallRev orev = new OutFallRev();
//             COutFallInfo outfall = new COutFallInfo();
//             COutFallExtInfo outext = new COutFallExtInfo();
//             List<COutFallInfo> listoutfall = new List<COutFallInfo>();
//             List<COutFallExtInfo> listoutext = new List<COutFallExtInfo>();
//             outfall.Category = 1;
//             outfall.SystemID = "2014722001";
//             listoutfall.Add(outfall);
//             outext.OutFallName = "outfallname";
//             listoutext.Add(outext);
//             orev.OutList = listoutfall;
//             orev.OutExtList = listoutext;
//             icmd.SetReceiver(orev);
//             icmd.Execute();

//             lcmd.SetReceiver(orev);
//             lcmd.Execute();
//             System.Console.WriteLine("OutList Number: {0},OutlistExt NUmber : {1}",orev.OutList.Count,orev.OutExtList.Count);

//             orev.OutExtList.ElementAt(0).OutFallName = "New Name";
//             orev.OutList.ElementAt(0).ReportDept = "adasda";
//             ucmd.SetReceiver(orev);
//             ucmd.Execute();
//             dcmd.SetReceiver(orev);
//             dcmd.Execute();
            
#endregion

#region test junction
            JuncRev jrev = new JuncRev();
            //lcmd.SetReceiver(jrev);
            //lcmd.Execute();
            //Console.WriteLine(jrev.ListJunc.Count);
            //CJuncInfo junc = new CJuncInfo();
            //CJuncExtInfo junext = new CJuncExtInfo();
            //List<CJuncInfo> listjunc = new List<CJuncInfo>();
            //List<CJuncExtInfo> listjuncext = new List<CJuncExtInfo>();
            //junc.Junc_Style = 1;
            //junc.X_Coor = 119.1232443;
            //junc.Y_Coor = 21.123023123;
            //junext.Lane_Way = "way name";
            //listjuncext.Add(junext);
            //listjunc.Add(junc);
            //listjuncext.Add(junext);
            //jrev.ListJunc = listjunc;
            //icmd.SetReceiver(jrev);
            //icmd.Execute();

            jrev.JuncName = "ShiN-W1 / LongQ-W1";
            
            scmd.SetReceiver(jrev);
            scmd.Execute();
            System.Console.WriteLine(jrev.ListJunc.Count);

            //jrev.ListJunc.ElementAt(0).SystemID = "2102323";
            //jrev.ListJuncExt.ElementAt(0).Junc_Class = 2;
            //ucmd.SetReceiver(jrev);
            //ucmd.Execute();
            //dcmd.SetReceiver(jrev);
            //dcmd.Execute();
            //ccmd.SetReceiver(jrev);
            //ccmd.Execute();
#endregion

#region test pipe
            PipeRev piperev = new PipeRev();

//             lcmd.SetReceiver(piperev);
//             lcmd.Execute();
//             System.Console.WriteLine(piperev.ListPipe.Count);
//             CPipeInfo pipe = new CPipeInfo();
//             pipe.PipeName = "Y2-Y3";
//             List<CPipeInfo> listpipe = new List<CPipeInfo>();
//             listpipe.Add(pipe);
//             piperev.ListPipe = listpipe;
//             icmd.SetReceiver(piperev);
//             icmd.Execute();

            //piperev.PipeName = "Y1-Y2";
            //scmd.SetReceiver(piperev);
            //scmd.Execute();
            //System.Console.WriteLine(piperev.ListPipe.Count);
            //System.Console.WriteLine(piperev.ListPipeExt.Count);
            //System.Console.WriteLine(piperev.ListUS.Count);
            //System.Console.WriteLine(piperev.ListLog.Count);
            //System.Console.WriteLine(piperev.ListVideo.Count);

//             ucmd.SetReceiver(piperev);
//             ucmd.Execute();
            //piperev.ListLog.Clear();
            //piperev.ListPipeExt.Clear();
            //dcmd.SetReceiver(piperev);
            //dcmd.Execute();
#endregion

            System.Console.WriteLine("Done");
            System.Console.ReadLine();
        }
    }
}
