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
    /// NewSeriesWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NewSeriesWindow
    {
        public NewSeriesWindow()
        {
            InitializeComponent();
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string seriesName = seriesNameBox.Text;
            NewSeriesRequest request = new NewSeriesRequest(seriesName);
            try
            {
                Result res = await SimpleHttpRequest.Instance.PostRequest("/api/v1/book/series/new", null, request);
                await this.ShowMessageAsync("알림", res.message);
                if (res.success)
                {
                    this.Close();
                }
            } catch (HttpRequestException)
            {
                await this.ShowMessageAsync("알림", "서버와의 연결에 실패했습니다.");
            }
        }
    }
}
