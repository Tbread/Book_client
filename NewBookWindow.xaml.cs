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
using Book.enums;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Book
{
    /// <summary>
    /// NewBookWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NewBookWindow
    {

        public bool result { get; set; }

        public NewBookWindow()
        {
            InitializeComponent();
        }

        private void ToggleSeries(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = sender as ToggleSwitch;
            if (toggle != null)
            {
                if (toggle.IsOn)
                {
                    seriesIdBox.Visibility = Visibility.Visible;
                    seriesIdLabel.Visibility = Visibility.Visible;
                    SearchSeriesWindow searchSeries = new SearchSeriesWindow();
                    bool? searchSeriesResult = searchSeries.ShowDialog();
                    if (searchSeriesResult == true)
                    {
                        seriesIdBox.Text = searchSeries.seriesId.ToString();
                    }
                    else
                    {
                        toggle.IsOn = false;
                    }
                }
                else
                {
                    seriesIdBox.Visibility = Visibility.Hidden;
                    seriesIdLabel.Visibility = Visibility.Hidden;
                    seriesIdBox.Text = null;
                }
            }
        }


        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long? seriesId = (seriesIdBox.Visibility == Visibility.Visible) ? (long?)long.Parse(seriesIdBox.Text) : null;
                string title = titleBox.Text;
                string author = authorBox.Text;
                string publisher = publisherBox.Text;
                string isbn = String.IsNullOrWhiteSpace(isbnBox.Text) ? null : isbnBox.Text;
                string isni = String.IsNullOrWhiteSpace(isniBox.Text) ? null : isniBox.Text;
                int? ea = int.Parse(eaBox.Text);
                KDC kdc = (KDC)Enum.Parse(typeof(KDC), kdcBox.SelectedValue.ToString());
                NewBookRequest req = new NewBookRequest(title, author, publisher, isbn, isni, kdc, (seriesId != null), seriesId.Value, ea);
                Result res = await SimpleHttpRequest.Instance.PostRequest("/api/v1/book/new", null, req);
                if (res.success)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                await this.ShowMessageAsync("알림", res.message);
            }
            catch (FormatException)
            {
                await this.ShowMessageAsync("알림", "권수는 정수만 입력가능합니다.");
            }
            catch (HttpRequestException)
            {
                await this.ShowMessageAsync("알림", "서버와의 연결에 실패했습니다.");
            }
        }
    }
}
