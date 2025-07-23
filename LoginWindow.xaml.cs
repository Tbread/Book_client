using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Book.dto;
using Book.dto.request;
using Book.dto.response;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;

namespace Book
{
    /// <summary>
    /// LoginWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LaunchGitHub(object sender, RoutedEventArgs e)
        {
            App.LaunchGitHub();
        }

        private async void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Password;
            UsernameAndPassword request  = new UsernameAndPassword();
            request.username = username;
            request.password = password;

            Result uncompletedRes = await SimpleHttpRequest.PostRequest("/api/v1/user/signin", null, request, null);
            if (!uncompletedRes.success)
            {
                await this.ShowMessageAsync("알림", "로그인에 실패했습니다.");
            }
            //추후 컨테이너에 jwt 저장 로직 작성

            if (uncompletedRes.success)
            {
                TokenPackage tokenPackage = uncompletedRes.data.ToObject<TokenPackage>();
                await this.ShowMessageAsync("디버그", "accessToken: " + tokenPackage.accessToken + "\nrefreshToken: " + tokenPackage.refreshToken);
                this.DialogResult = true;
                this.Close();
            }   
        }
    }
}
