namespace Fastconnect
{
    public interface Iconnectfast
    {

        void connectfast(string configfile, string ipuser, string password);

        void connectfast(string vpnname, string fortitag, string ipuser, string password);

        void fortibackup(string rdpip);

        void openconfigcontrol(string server, string configlocation);

        void rdpcontrol(string rdpip, string rdpipuser, string rdpippassword);

        void fileconnection(string ip, string user, string password);

        bool ping(string ip);


    }

    /*
     * 
     *       void Iconnectfast.connectfast(
              string vpnname,
              string fortitag,
              string ipuser,
              string password,
              string rdpip,
              string rdpipuser,
              string rdpippassword)
            {
                int systemMetrics1 = Connect.GetSystemMetrics(0);
                int systemMetrics2 = Connect.GetSystemMetrics(1);
                int x = systemMetrics1 / 2;
                int y = systemMetrics2 / 2;
                string processName1 = "FortiClient";
                string processName2 = "FortiTray";
                string name1 = "SOFTWARE\\Fortinet\\FortiClient\\FA_VPN";
                string name2 = "connection";
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name1, true))
                    registryKey?.SetValue(name2, (object)fortitag, RegistryValueKind.String);
                Process.GetProcessesByName(processName1);
                Process[] processesByName1 = Process.GetProcessesByName(processName2);
                if (processesByName1.Length != 0)
                    processesByName1[0].Kill();
                Process.Start(this.filePath);
                Process.GetProcessesByName(processName2);
                Process[] processesByName2 = Process.GetProcessesByName(processName1);
                IntPtr mainWindowHandle = processesByName2[0].MainWindowHandle;
                bool flag = true;
                while (flag)
                {
                    Process[] processesByName3 = Process.GetProcessesByName(processName2);
                    Thread.Sleep(400);
                    if (processesByName2.Length != 0 && processesByName3.Length != 0)
                    {
                        flag = false;
                        Thread.Sleep(2500);
                        Connect.SetCursorPos(x, y);
                        Connect.SetForegroundWindow(mainWindowHandle);
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
                        while (processesByName2.Length != 0)
                        {
                            processesByName2 = Process.GetProcessesByName(processName1);
                            Thread.Sleep(1000);
                        }
                    if (processesByName2.Length != 0) ;
                            
                    }
                }
            }*/


/*
 * 
 * 
 *        void Iconnectfast.connectfast(
          string configfile,
          string ipuser,
          string password,
          string rdpip,
          string rdpipuser,
          string rdpippassword)
        {
            string path = "C:\\Program Files\\OpenVPN\\config\\password.txt";
            bool flag = false;
            if (configfile.EndsWith(".ovpn"))
                configfile = configfile.Substring(0, configfile.Length - 5);
            string[] contents = new string[2] { ipuser, password };
            File.WriteAllText(path, string.Empty);
            File.WriteAllLines(path, contents);
            if (Process.GetProcessesByName("openvpn").Length != 0)
                return;
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
            while (!flag)
            {
                try
                {
                    flag = true;
                    File.WriteAllText(path, string.Empty);
                }
                catch (IOException ex)
                {
                    Thread.Sleep(1000);
                }
            }
        }


    */

}