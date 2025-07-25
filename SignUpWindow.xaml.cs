using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using MahApps.Metro.Controls.Dialogs;

namespace Book
{
    /// <summary>
    /// SignUpWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignUpWindow
    {
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private bool usernameConfirm = false;

        private async void Username_Confirm(object sender, RoutedEventArgs e)
        {
            try
            {
                Result res = await SimpleHttpRequest.Instance.GetRequest("/api/v1/user/username-check?username=" + usernameBox.Text, null);
                if (res.success)
                {
                    this.usernameConfirm = true;
                    usernameConfirmButton.IsEnabled = false;
                    signUpConfirmButton.IsEnabled = SignUpConfirmButtonHandler();
                }
                else
                {
                    this.usernameConfirm = false;
                    usernameConfirmButton.IsEnabled = true;
                }
                await this.ShowMessageAsync("알림", res.message);
            }
            catch (HttpRequestException ex)
            {
                await this.ShowMessageAsync("알림", "서버와의 연결에 실패했습니다.");
                this.Close();
            }
        }

        private void UsernameChangeHandler(object sender,TextChangedEventArgs args)
        {
            this.usernameConfirm = false;
            usernameConfirmButton.IsEnabled = true;
            signUpConfirmButton.IsEnabled = false;
        }

        private void PasswordChangeHandler(object sender, RoutedEventArgs e)
        {
            signUpConfirmButton.IsEnabled = SignUpConfirmButtonHandler();
        }

        private bool SignUpConfirmButtonHandler()
        {
            string password = passwordBox.Password;
            string passwordConfirm = passwordConfirmBox.Password;
            if (String.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            if (password.Equals(passwordConfirm) && usernameConfirm)
            {
                return true;
            }
            return false;
        }

        private async void SignUp_Confirm(object sender, RoutedEventArgs e)
        {
            SignUpRequest req = new SignUpRequest(usernameBox.Text,passwordBox.Password);
            try
            {
                Result res = await SimpleHttpRequest.Instance.PostRequest("/api/v1/user/signup", null, req);
                if (res.success)
                {
                    await this.ShowMessageAsync("알림", res.message);
                    this.Close();
                }
                else
                {
                    await this.ShowMessageAsync("알림", res.message);
                }
            }
            catch (HttpRequestException ex)
            {
                await this.ShowMessageAsync("알림", "서버와의 연결에 실패했습니다.");
                this.Close();
            }
        }
    }
}
