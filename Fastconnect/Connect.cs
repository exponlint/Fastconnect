
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace Fastconnect
{

    
        public class Connect : Iconnectfast
        {
            private const int MOUSEEVENTF_LEFTDOWN = 2;
            private const int MOUSEEVENTF_LEFTUP = 4;
            private InputSimulator simulator = new InputSimulator();
            private string filePath = @"C:\Program Files\Fortinet\FortiClient\FortiClientConsole.exe";

        [DllImport("user32.dll")]
            private static extern bool SetForegroundWindow(IntPtr hWnd);

            [DllImport("user32.dll")]
            private static extern void mouse_event(
              uint dwFlags,
              int dx,
              int dy,
              uint dwData,
              int dwExtraInfo);

            [DllImport("user32.dll")]
            public static extern int GetSystemMetrics(int nIndex);

            [DllImport("user32.dll")]
            private static extern bool SetCursorPos(int x, int y);
            private static void LeftClick()
            {
                Connect.mouse_event(2U, 0, 0, 0U, 0);
                Connect.mouse_event(4U, 0, 0, 0U, 0);
            }
        //rdp
            void Iconnectfast.rdpcontrol(string rdpip, string rdpipuser, string rdpippassword)
            {
                bool flag = true;
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c ping " + rdpip;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    if (process.StandardOutput.ReadLine().Contains("TTL") && flag)
                    {
                        this.Rdpconnect(rdpip, rdpipuser, rdpippassword);
                        flag = false;
                    }
                }
                process.WaitForExit();
            }
        //rdp
            private void Rdpconnect(string rdpip, string rdpipuser, string rdpippassword)
            {
                string str1 = "cmdkey /generic:TERMSRV/" + rdpip + " /user:" + rdpipuser + " /pass:" + rdpippassword;
                string str2 = "mstsc /v:" + rdpip;
                Process process1 = new Process();
                process1.StartInfo.FileName = "cmd.exe";
                process1.StartInfo.Arguments = "/C " + str1;
                Process process2 = new Process();
                process2.StartInfo.FileName = "cmd.exe";
                process2.StartInfo.Arguments = "/C " + str2;
                try
                {
                    process1.Start();
                    process1.WaitForExit();
                    process2.Start();
                    process2.WaitForExit();
                }
                catch (Exception ex)
                {
                    
                }
            }
        //openvpn
            void Iconnectfast.connectfast(string configfile, string ipuser, string password)
        {
            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string openVpnPath = System.IO.Path.Combine(userProfilePath, "OpenVPN\\config");

            string path = "" + openVpnPath + "\\OpenVPNConfig\\password.txt";
            if (File.Exists(path))
            {
                File.WriteAllText("password.txt", path);

            }
            if (configfile.EndsWith(".ovpn"))
                configfile = configfile.Substring(0, configfile.Length - 5);
            string[] contents = new string[2] { ipuser, password };
            File.WriteAllText(path, string.Empty);
            File.WriteAllLines(path, contents);
            if (Process.GetProcessesByName("openvpn").Length == 0)
            {
                string str = "\"C:\\Program Files\\OpenVPN\\bin\\openvpn-gui.exe\" --connect " + configfile + ".ovpn";
                new Process()
                {
                    StartInfo = {
            FileName = "cmd.exe",
            Arguments = ("/C " + str),
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
          }
                }.Start();
            }
            Thread.Sleep(1500);
            File.WriteAllText(path, string.Empty);

        }
        //fortivpn
        void Iconnectfast.connectfast(string vpnname, string fortitag, string ipuser, string password)
            {
            if(File.Exists(filePath))
            {

            }
            else { filePath = @"C:\Program Files\Fortinet\FortiClient\FortiClient VPN\FortiClientConsole.exe"; }

                int systemMetrics1 = Connect.GetSystemMetrics(0);
                int systemMetrics2 = Connect.GetSystemMetrics(1);
                int x = systemMetrics1 / 2;
                int y = systemMetrics2 / 2;
                string processName1 = "FortiClient";
                string processName2 = "FortiTray";
                Process[] processesByName1 = Process.GetProcessesByName(processName1);
                Process[] processesByName2 = Process.GetProcessesByName(processName2);
                if (processesByName2.Length != 0) {
                processesByName2[0].Kill();
                Thread.Sleep(50);
            }
                    
                if (processesByName1.Length != 0)
                {
                for (int i = 0; i < processesByName1.Length; i++)
                {
                    processesByName1[i].Kill();
                }
                       
                Thread.Sleep(50);
            }
                string name1 = "SOFTWARE\\Fortinet\\FortiClient\\FA_VPN";
                string name2 = "connection";
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name1, true))
                    registryKey?.SetValue(name2, (object)fortitag, RegistryValueKind.String);
            Thread.Sleep(50);
            Process.Start(this.filePath);

            Process.GetProcessesByName(processName2);
            Process[] processesByName3 = Process.GetProcessesByName(processName1);

            bool flag = true;

            while (flag)
            {
                processesByName3 = Process.GetProcessesByName(processName1);
                Process[] processesByName4 = Process.GetProcessesByName(processName2);
                // Thread.Sleep(1500);
                for (int i = 0; i < 16; i++)
                {
                    Thread.Sleep(100);
                    Connect.SetCursorPos(x, y);


                }
               
                if (processesByName3.Length > 2 && processesByName4.Length != 0)
                {
                    //  Thread.Sleep(500);
                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(100);
                        Connect.SetCursorPos(x, y);


                    }
                    
                    flag = false;

                    Connect.SetCursorPos(x, y);

                    Connect.LeftClick();
                    this.simulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                    Thread.Sleep(50);
                    this.simulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                    Thread.Sleep(50);
                    this.simulator.Keyboard.TextEntry(ipuser);
                    Thread.Sleep(50);
                    this.simulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                    Thread.Sleep(50);
                    this.simulator.Keyboard.TextEntry(password);
                    Thread.Sleep(50);
                    this.simulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                    Thread.Sleep(50);
                    this.simulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                    Thread.Sleep(50);
                    this.simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                    /* while (processesByName3.Length != 0)
                     {
                         processesByName3 = Process.GetProcessesByName(processName1);
                         Thread.Sleep(1000);
                     }
                    */
                }
            }
            }
        //openconfig
            void Iconnectfast.openconfigcontrol(string server, string configlocation)
            {
            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string openVpnPath = System.IO.Path.Combine(userProfilePath, "OpenVPN\\config");
            bool flag = true;
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c ping " + server;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                int num = 0;
                while (!process.StandardOutput.EndOfStream)
                {
                    ++num;
                    if (process.StandardOutput.ReadLine().Contains("TTL") && flag)
                    {
                        string str1 = "\\\\" + configlocation;
                        string path1 = openVpnPath;
                        foreach (string file in Directory.GetFiles(str1, "*", SearchOption.AllDirectories))
                        {
                            string relativePath = Connect.GetRelativePath(str1, file);
                            string str2 = Path.Combine(path1, relativePath);
                            string directoryName = Path.GetDirectoryName(str2);
                            if (!Directory.Exists(directoryName))
                                Directory.CreateDirectory(directoryName);
                            File.Copy(file, str2, true);
                        }
                        flag = false;
                    }
                }
            }

            private static string GetRelativePath(string fromPath, string toPath) => Uri.UnescapeDataString(new Uri(fromPath).MakeRelativeUri(new Uri(toPath)).ToString());

        //fortibackup
            void Iconnectfast.fortibackup(string rdpip)
            {
                bool flag = true;
                Process process1 = new Process();
                process1.StartInfo.FileName = "cmd.exe";
                process1.StartInfo.Arguments = "/c ping " + rdpip;
                process1.StartInfo.UseShellExecute = false;
                process1.StartInfo.RedirectStandardOutput = true;
                process1.StartInfo.CreateNoWindow = true;
                process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process1.Start();
                int num = 0;
                while (!process1.StandardOutput.EndOfStream)
                {
                    ++num;
                    if (process1.StandardOutput.ReadLine().Contains("TTL") && flag)
                    {
                        try
                        {
                        string str4;
                            string str1 = "\\\\192.168.75.7\\it companents\\Güncel Vpn Programları\\fortibackupyeni.conf";
                            string userName = Environment.UserName;
                            string str2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FortiClient");
                            if (!Directory.Exists(str2))
                                Directory.CreateDirectory(str2);
                            string fileName = Path.GetFileName(str1);
                            string destFileName = Path.Combine(str2, fileName);
                            File.Copy(str1, destFileName, true);
                            string str3 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FortiClient\\fortibackupyeni.conf");
                        if (File.Exists("C:\\Program Files\\Fortinet\\FortiClient\\FCConfig.exe"))
                        {
                           str4 = "C:\\Program Files\\Fortinet\\FortiClient\\FCConfig.exe";
                        }
                        else { str4 = @"C:\Program Files\Fortinet\FortiClient\FortiClient VPN\FCConfig.exe"; }
                        
                            string str5 = "-m vpn -f " + str3 + " -o import -i 1 -p 12345678";
                            try
                            {
                                ProcessStartInfo processStartInfo = new ProcessStartInfo()
                                {
                                    FileName = str4,
                                    Arguments = str5,
                                    Verb = "runas",
                                    UseShellExecute = true
                                };
                                Process process2 = new Process()
                                {
                                    StartInfo = processStartInfo
                                };
                                process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process2.Start();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        flag = false;
                    }
                    else if (!flag || num <= 4)
                        ;
                }
            }

        //file connection

            void Iconnectfast.fileconnection(string ip, string user, string password)
            {
                string str1 = "cmdkey /add:" + ip + " /user:" + user + " /pass:" + password;
                string str2 = "start rundll32 url.dll,FileProtocolHandler \\\\" + ip;
                Process process1 = new Process();
                process1.StartInfo.FileName = "cmd.exe";
                process1.StartInfo.Arguments = "/C " + str1;
                Process process2 = new Process();
                process2.StartInfo.FileName = "cmd.exe";
                process2.StartInfo.Arguments = "/C " + str2;
                try
                {
                    process1.Start();
                    process1.WaitForExit();
                    process2.Start();
                    process2.WaitForExit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Komut çalıştırılırken bir hata oluştu: " + ex.Message);
                }
            }

        //ping
        bool Iconnectfast.ping(string ip)
        {
            bool flag = false;
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c ping " + ip;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            for (int i = 0; i < 3; i++)
            {
                if (process.StandardOutput.ReadLine().Contains("TTL"))
                {
                    flag = true;
                }
            }
            process.Close();
            return flag;
        }
    }

   
}

