using System;
using System.Diagnostics;           //Process 
using System.Windows.Forms;

namespace 지니_64
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]

        static void Main()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            Console.WriteLine("프로그램 실행");

            Process[] processes = Process.GetProcessesByName("Excel");

            foreach (Process p in processes)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }

            if (IsExistProcess(Process.GetCurrentProcess().ProcessName))
            {
                MessageBox.Show(new Form { TopMost = true }, "'지니_64' 이미 실행 중입니다", "실행알람", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }

        static bool IsExistProcess(string processName)
        {
            Process[] process = Process.GetProcesses();
            int cnt = 0;

            //프로세스명으로 확인해서 동일한 프로세스 개수가 2개이상인지 확인합니다. 
            //현재실행하는 프로세스도 포함되기때문에 1보다커야합니다.
            foreach (var p in process)
            {
                if (p.ProcessName == processName)
                    cnt++;
                if (cnt > 1)
                    return true;
            }
            return false;
        }
    }
}
