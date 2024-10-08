using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using System.Timers;
namespace Discord_RPC_Identity_V
{
    class MainProcess
    {
        public static int tempProcessCheck = 0;
        public static bool processFound = false;
        public static string logPath = string.Empty;
        private static System.Timers.Timer timer;
        private static HashSet<string> existingLines = new HashSet<string>();
        public static DiscordRPC drpc;
        public void Initialize()
        {
            Debug.Info("Main", "Start process watcher");
            ManagementEventWatcher startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            startWatch.EventArrived += new EventArrivedEventHandler(startWatch_EventArrived);
            startWatch.Start();
            ManagementEventWatcher stopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));
            stopWatch.EventArrived += new EventArrivedEventHandler(stopWatch_EventArrived);
            stopWatch.Start();
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
            DiscordRPC rpc = new DiscordRPC();
            rpc.Initialize();
            drpc = rpc;
        }
        public void ResetCounter()
        {
            Debug.Info("Main", "Reseting count for detect dwrg process");
            Debug.Info("Main", "Old count value : " + tempProcessCheck);
            Debug.Info("Main", "Force Find Identity V");
            Process[] dwrgs = Process.GetProcessesByName("dwrg");
            if(dwrgs.Length > 0)
            {
                Process dw = dwrgs.FirstOrDefault();
                Debug.Info("Main", "Founded Process Identity V");
                Debug.Info("Main", $"PID : {dw.Id} Thread : {dw.MainModule.BaseAddress} Path : {dw.MainModule.FileName}");
                string IdentityPath = Path.GetDirectoryName(dw.MainModule.FileName);
                if (IdentityPath == null)
                {
                    Debug.Info("Main", "Path Identity V not found");
                    return;
                }
                if (!Directory.Exists(IdentityPath))
                {
                    Debug.Info("Main", "Path Identity V not found");
                }
                Debug.Info("Main", $"Directory : {dw.MainModule.FileName}");
                Debug.Info("Main", $"Finding log.txt");
                var files = new DirectoryInfo(IdentityPath).GetFiles("*.txt");
                var latestFile = files.OrderByDescending(f => f.LastWriteTime).First();
                Debug.Info("Main", "Latest modified log file: " + latestFile.FullName);
                logPath = latestFile.FullName;
                processFound = true;
                tempProcessCheck = 2;
            }
            else
            {
                processFound = false;
                tempProcessCheck = 0;
            }
        }
        public DiscordRPC getRPC()
        {
            return drpc;
        }
        static void stopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string procName = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (procName != null && procName == "dwrg.exe")
            {
                if (tempProcessCheck == 1)
                {
                    tempProcessCheck = 2;
                }
                else if (tempProcessCheck == 2) {
                    Debug.Info("Main", "Identity V has been exit");
                    tempProcessCheck = 0;
                    processFound = false;
                }
            }
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                if (logPath.Length > 0 && File.Exists(logPath) && processFound)
                {
                    using (FileStream fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        var lines = sr.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                        foreach (var line in lines)
                        {
                            if (existingLines.Add(line))
                            {
                                if(line.Contains("INFO - WorldMatchRoom show")){
                                    drpc.Update("Selecting Character", "Normal Match", "nlm", "Normal Match");
                                }else if(line.Contains("init_app_pack_config, called_from: init")){
                                    drpc.Update("In game menu", "Waiting for join game", "gamemenu", "Game menu");
                                }else if (line.Contains("INFO - WorldBattle init_scene")) {
                                    drpc.Update("In Battle", "Normal Match", "in_match", "In Battle");
                                }else if (line.Contains("---------create csb path -ui/dm65ui/res/custom.csb, UICustomGame---------")) {
                                    drpc.Update("Custom Match Lobby", "Waiting host start match", "customgame", "Custom Match");
                                }else if(line.Contains("-ui/dm65ui/res/hall_new.csb, UIHall---------")){
                                    drpc.Update("In Lobby", "Custom Lobby", "silenceofpalace", "Custom Lobby");
                                }else if(line.Contains("[copycat] pre_start_battle")){
                                    drpc.Update("In Battle", "Copycat", "copycatgamemodeannouncement", "CopyCat");
                                }
                            }
                        }
                    }
                }
                else
                {
                    if(logPath.Length > 0)
                    {
                        Debug.Error("Main", $"Log not found: {logPath}");
                    }
                }
            }
            catch (IOException ex)
            {
                Debug.Error("Main",$"Error reading the file: {ex.Message}");
                System.Threading.Thread.Sleep(1000);
            }
        }
        static void startWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string procName = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (procName != null && procName == "dwrg.exe")
            {
                if (tempProcessCheck == 0) { 
                    tempProcessCheck = 1;
                }
                string procIdStr = e.NewEvent.Properties["ProcessId"].Value.ToString();
                int procId;
                if (tempProcessCheck == 2 && procIdStr != null && int.TryParse(procIdStr, out procId))
                {
                    Process dwrgProcess = Process.GetProcessById(procId);
                    if (dwrgProcess == null) {
                        return;
                    }
                    Debug.Info("Main", "Founded Process Identity V");
                    Debug.Info("Main", $"PID : {procId} Thread : {dwrgProcess.MainModule.BaseAddress} Path : {dwrgProcess.MainModule.FileName}");
                    string IdentityPath = Path.GetDirectoryName(dwrgProcess.MainModule.FileName);
                    if (IdentityPath == null) {
                        Debug.Info("Main", "Path Identity V not found");
                        return;
                    }
                    if (!Directory.Exists(IdentityPath)) {
                        Debug.Info("Main", "Path Identity V not found");
                    }
                    Debug.Info("Main", $"Directory : {dwrgProcess.MainModule.FileName}");
                    Debug.Info("Main", $"Finding log.txt");
                    var files = new DirectoryInfo(IdentityPath).GetFiles("*.txt");
                    var latestFile = files.OrderByDescending(f => f.LastWriteTime).First();
                    Debug.Info("Main","Latest modified log file: " + latestFile.FullName);
                    logPath = latestFile.FullName;
                    processFound = true;
                }
            }
        }
    }
}
