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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Book.dto;
using Book.dto.response;
using MahApps.Metro.Controls;

namespace Book
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow
    {

        public ObservableCollection<BookData> books { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            initializeDataGrid();
        }

        private bool onlySeries = false;
        private bool onlyDiscard = false;

        private async void initializeDataGrid()
        {
            Result uncompletedRes = await SimpleHttpRequest.Instance.GetRequest("/api/v1/book/search?condition=TITLE&onlySeries=false&onlyDiscard=false", null);
            if (uncompletedRes.success)
            {
                List<BookData> bookList = uncompletedRes.data.ToObject<List<BookData>>();
                books = new ObservableCollection<BookData>(bookList);
                BookDataGrid.ItemsSource = books;
            }
        }

        private void LaunchGitHub(object sender, RoutedEventArgs e)
        {
            App.LaunchGitHub();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            Result uncompletedRes = await SimpleHttpRequest.Instance.GetRequest("/api/v1/book/search?condition="
                + SearchCondition.SelectedValue as string
                + "&onlySeries="+onlySeries
                +"&onlyDiscard="+onlyDiscard
                +"&keyword="+keyword.Text,
                null);
            if (uncompletedRes.success)
            {
                List<BookData> bookList = uncompletedRes.data.ToObject<List<BookData>>();
                books.Clear();
                foreach (BookData book in bookList)
                {
                    books.Add(book);
                }
            }
        }

        private void NewBook_Click(object sender, RoutedEventArgs e)
        {
            NewBookWindow window = new NewBookWindow()
            {
                Title = "새 책 추가"
            };
            window.ShowDialog();
        }
    }
}
