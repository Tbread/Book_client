using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Book.dto.response;

namespace Book
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            base.OnStartup(e);
            LoginWindow login = new LoginWindow();
            bool? result = login.ShowDialog();
            if (result == true)
            {
                //todo: jwt 파싱후 role에 따라 표시할 윈도우 수정
                string token = SingletonStorage.Instance.getAccessToken();
                string role = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "role")?.Value;
                //여기서부터 role에따른 분기윈도우 출력
                MainWindow main = new MainWindow();
                main.Closed += (s, args) => Application.Current.Shutdown();
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
