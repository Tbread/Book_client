using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Book
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoginWindow login = new LoginWindow();
            bool? result = login.ShowDialog();
            if (result == true)
            {
                MainWindow main = new MainWindow();
                main.Show();
            }
            else
            {
                Shutdown();
            }
        }

        public static void LaunchGitHub()
        {
            String url = "https://github.com/tbread";
            var processInfo = new System.Diagnostics.ProcessStartInfo(url);
            System.Diagnostics.Process.Start(processInfo);
        }
    }
}
