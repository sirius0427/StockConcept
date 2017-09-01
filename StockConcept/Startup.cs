using Ay.Framework.WPF.MVC;
using StockConcept.Views;
using System;
using System.Diagnostics;
using System.Windows;

namespace StockConcept
{
    public class Startup
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Process thisProc = Process.GetCurrentProcess();
                // Check how many total processes have the same name as the current one
                if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
                {
                    // If ther is more than one, than it is already running.
                    MessageBox.Show("程序已经运行.");
                    Application.Current.Shutdown();
                    return;
                }
                //new AYUIApplication<_ViewStart>(new Global()).Run();
                new AYUIApplication<StartConcept>(new Global()).Run();
            }
            catch (Exception)
            {
                //MessageBox.Show("查询失败了！");
            }
        }

    }
}
