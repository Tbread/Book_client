using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            try
            {
                Result uncompletedRes = await SimpleHttpRequest.Instance.PostRequest("/api/v1/user/signin", null, request);
                if (!uncompletedRes.success)
                {
                    await this.ShowMessageAsync("알림", "로그인에 실패했습니다.");
                }
                if (uncompletedRes.success)
                {
                    this.DialogResult = true;
                    this.Close();
                }
            }
            catch (HttpRequestException ex)
            {
                await this.ShowMessageAsync("알림", "서버와의 연결에 실패했습니다.");
            }
        }

        private void SignUp_Click(object sender, MouseButtonEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow()
            {
                Title = "회원가입",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            signUpWindow.ShowDialog();
        }
    }
 }
