using System.Linq;
using System.Windows;
using System.Data.Entity;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new L5Entities())
            {
                // Завантаження даних для Books
                context.Books.Load();
                BooksDataGrid.ItemsSource = context.Books.Local;

                // Завантаження даних для Publishers
                context.Publishers.Load();
                PublishersDataGrid.ItemsSource = context.Publishers.Local;
            }
        }

        private void LoadBooksByPublisher(object sender, RoutedEventArgs e)
        {
            int publisherId;
            if (int.TryParse(PublisherIdTextBox.Text, out publisherId))
            {
                using (var context = new L5Entities())
                {
                    var booksByPublisher = context.Books
                        .Where(b => b.IDPublisher == publisherId)
                        .ToList();
                    BooksByPublisherDataGrid.ItemsSource = booksByPublisher;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Publisher ID.");
            }
        }
        private void LoadBooksByYear(object sender, RoutedEventArgs e)
        {
            string year = YearTextBox.Text;
            using (var context = new L5Entities())
            {
                var booksByYear = context.Books
                    .Where(b => b.Year == year)
                    .ToList();
                BooksByYearDataGrid.ItemsSource = booksByYear;
            }
        }

    }
}
