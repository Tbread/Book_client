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
    public partial class PasswordResetWindow
    {
        public PasswordResetWindow()
        {
            InitializeComponent();
        }

        private bool PasswordReset_ConfirmButtonHandler()
        {
            string password = passwordBox.Password;
            string passwordConfirm = passwordConfirmBox.Password;
            if (String.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            if (password.Equals(passwordConfirm))
            {
                return true;
            }
            return false;
        }

        private void PasswordChangeHandler(object sender, RoutedEventArgs e)
        {
            PasswordResetConfirmButton.IsEnabled = PasswordReset_ConfirmButtonHandler();
        }

        private async void PasswordReset_Confirm(object sender, RoutedEventArgs e)
        {
            PasswordResetRequest req = new PasswordResetRequest(usernameBox.Text,passwordBox.Password,"temporaryCi");
            try
            {
                Result res = await SimpleHttpRequest.Instance.PostRequest("/api/v1/user/reset-password", null, req);
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
