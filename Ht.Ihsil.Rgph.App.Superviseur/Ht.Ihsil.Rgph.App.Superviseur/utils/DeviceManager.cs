using Ht.Ihsi.Rgph.DataAccess.Entities;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Exceptions;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using RegawMOD.Android;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class DeviceManager
    {
        AndroidController android;
        private Device device;
        private bool _isConnected;
        private string _serial;
        Logger log;

        public bool IsConnected
        {
            get
            {
                initializeAndroid();
                android.UpdateDeviceList();
                if (android.HasConnectedDevices)
                {
                    return true;
                }
                else
                    return false;
            }
            set { _isConnected = value; }
        }

        public string Serial
        {
            get { return _serial; }
            set { _serial = value; }
        }

        public DeviceManager()
        {
            log = new Logger();
        }

        public void initializeAndroid()
        {
            android = AndroidController.Instance;
        }

        public Device getConnectedDevice()
        {
            initializeAndroid();
            android.UpdateDeviceList();
            if (android.HasConnectedDevices)
            {
                _serial = android.ConnectedDevices[0];
                device = android.GetConnectedDevice(_serial);
                Process[] procs = Process.GetProcessesByName("adb");
                foreach (var proc in procs)
                {
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
                return device;
            }
            return null;
        }

        public DeviceInformation getDeviceInformation()
        {
            DeviceInformation deviceInfo = new DeviceInformation();
            initializeAndroid();
            if (android.HasConnectedDevices)
            {
                Process[] procs = Process.GetProcessesByName("adb");
                foreach (var proc in procs)
                {
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.StartInfo.Arguments = Constant.CMD_ADB_VERSION;
                process.Start();

                string[] split = new string[] { "\r\n" };
                string output = null;
                string[] messages = null;

                process.StartInfo.Arguments = Constant.CMD_SERIAL;
                process.Start();
                StreamReader reader = process.StandardOutput;
                if (reader != null)
                {
                    output = reader.ReadToEnd().ToString();
                    messages = output.Split(split, StringSplitOptions.RemoveEmptyEntries);
                    deviceInfo.Serial = messages.ToString();
                    int size = messages.Count();
                    deviceInfo.Serial = messages[size - 1].ToString();
                    //for (int i = 0; i < messages.Length; i++)
                    //{
                    //    if (messages[i].Contains("Device"))
                    //    {
                    //        split = new string[] { "=" };
                    //        string[] mes = messages[i].Split(split, StringSplitOptions.RemoveEmptyEntries);
                    //        deviceInfo.Imei = mes[1].ToString();
                    //    }
                    //}
                }

                process.StartInfo.Arguments = Constant.CMD_IMEI;
                process.Start();
                reader = process.StandardOutput;
                if (reader != null)
                {
                    output = reader.ReadToEnd().ToString();
                    messages = output.Split(split, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < messages.Length; i++)
                    {
                        if (messages[i].Contains("Device"))
                        {
                            split = new string[] { "=" };
                            string[] mes = messages[i].Split(split, StringSplitOptions.RemoveEmptyEntries);
                            deviceInfo.Imei = mes[1].ToString();
                        }
                    }
                }

                process.StartInfo.Arguments = Constant.CMD_MODEL;
                process.Start();
                reader = process.StandardOutput;
                if (reader != null)
                {
                    output = reader.ReadToEnd();
                    messages = output.Split(split, StringSplitOptions.RemoveEmptyEntries);
                    if (messages.Length > 1)
                    {
                        for (int i = 0; i < messages.Length; i++)
                        {
                            if (messages[i].Contains("SM"))
                            {
                                deviceInfo.Model = messages[i].ToString();
                            }
                        }
                    }
                    else
                    {
                        deviceInfo.Model = messages[0].ToString();
                    }
                }

                process.StartInfo.Arguments = Constant.CMD_VERSION;
                process.Start();
                reader = process.StandardOutput;
                if (reader != null)
                {
                    output = reader.ReadToEnd();
                    messages = output.Split(split, StringSplitOptions.RemoveEmptyEntries);
                    if (messages.Length > 1)
                    {
                        for (int i = 0; i < messages.Length; i++)
                        {
                            if (messages[i].Contains("4") || messages[i].Contains("5"))
                            {
                                deviceInfo.Model = messages[i].ToString();
                            }
                        }
                    }
                    else
                    {
                        deviceInfo.OsVersion = messages[0].ToString();
                    }
                }
                procs = Process.GetProcessesByName("adb");
                foreach (var proc in procs)
                {
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
            }
            android.Dispose();
            return deviceInfo;
        }
        public bool pullFile(string path)
        {
            bool pulled = false;
            initializeAndroid();
            android.UpdateDeviceList();
            if (android.HasConnectedDevices)
            {
                Process[] procs = Process.GetProcessesByName("adb");
                foreach (var proc in procs)
                {
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                StreamReader reader = null;
                process.StartInfo.Arguments = Constant.CMD_PULL_DB + "  " + "\"" + path + "";
                process.Start();
                reader = process.StandardOutput;
                string message = reader.ReadToEnd();
                if (Directory.GetDirectories(path).Length != 0)
                {
                    string pathCopied="";
                    string[] folder = (Directory.GetDirectories(path));
                    if (folder.Length != 0)
                    {
                        foreach (string fol in folder)
                        {
                            pathCopied = System.IO.Path.GetFullPath(fol);
                            break;
                        }
                        string[] fichiers = Directory.GetFiles(pathCopied);
                        if (fichiers.Length != 0)
                            pulled = true;
                        else
                            pulled = false;
                    }
                }
                else
                {
                    string[] files = Directory.GetFiles(path);
                    if (files.Length != 0)
                    {
                        log.Info("FILE COPIED:" + message);
                        pulled = true;
                    }
                    else
                    {
                        pulled = false;
                    }
                }
                procs = Process.GetProcessesByName("adb");
                foreach (var proc in procs)
                {
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
            }
            android.Dispose();
            return pulled;
        }
        public bool pushFile(string path)
        {
            bool pushed = false;
            initializeAndroid();
            android.UpdateDeviceList();
            if (android.HasConnectedDevices)
            {
                Process[] procs = Process.GetProcessesByName("adb");
                foreach (var proc in procs)
                {
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                StreamReader reader = null;
                process.StartInfo.Arguments = Constant.CMD_PUSH_DB + " " + path + " " + Constant.DEVICE_DIRECTORY_DATA;
                process.Start();
                reader = process.StandardOutput;
                string message = reader.ReadToEnd();
                pushed = true;
                procs = Process.GetProcessesByName("adb");
                foreach (var proc in procs)
                {
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
            }
            android.Dispose();
            return pushed;
        }


    }
}
