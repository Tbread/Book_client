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
using MahApps.Metro.Controls;

namespace Book
{
    /// <summary>
    /// NewBookWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NewBookWindow
    {
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
                    searchSeries.ShowDialog();
                    seriesIdBox.Text = searchSeries.seriesId.ToString();
                }
                else
                {
                    seriesIdBox.Visibility = Visibility.Hidden;
                    seriesIdLabel.Visibility = Visibility.Hidden;
                    seriesIdBox.Text = null;
                }
            }
        }


        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            long? seriesId = (seriesIdBox.Visibility == Visibility.Visible) ? (long?)long.Parse(seriesIdBox.Text) : null;
        }
    }
}
