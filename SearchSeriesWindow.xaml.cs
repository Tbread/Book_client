using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Book.dto.response;

namespace Book
{
    /// <summary>
    /// SearchSeriesWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchSeriesWindow : Window
    {

        public ObservableCollection<Series> seriesCollection { get; set; }


        public SearchSeriesWindow()
        {
            InitializeComponent();
            initializeDataGrid();
        }

        private async void initializeDataGrid()
        {
            Result uncompletedRes = await SimpleHttpRequest.Instance.GetRequest("/api/v1/book/series/search", null);
            if (uncompletedRes.success)
            {
                List<Series> seriesList = uncompletedRes.data.ToObject<List<Series>>();
                seriesCollection = new ObservableCollection<Series>(seriesList);
                SeriesDataGrid.ItemsSource = seriesCollection;
            }
        }
    }
}
