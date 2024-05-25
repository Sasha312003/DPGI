using System;
using System.Data;
using System.Windows;
using System.Data.SqlClient;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        // Рядок з'єднання з базою даних
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;
        // Метод для виконання SQL-запиту
        private void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка виконання SQL-запиту: " + ex.Message);
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadBooks();
        }

        // Метод для завантаження списку книг
        private void LoadBooks()
        {
            // Виконання SQL-запиту для отримання списку книг
            string query = "SELECT Title, ISBN, Author, Publisher, Year FROM Books";
            DataTable booksTable = ExecuteQuery(query);

            // Встановлення джерела даних для списку книг та табличної частини
            BooksListBox.ItemsSource = booksTable.DefaultView;
            DetailsDataGrid.ItemsSource = booksTable.DefaultView;
        }

        // Метод для виконання SQL-запиту
        private DataTable ExecuteQuery(string query)
        {
            DataTable result = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                try
                {
                    connection.Open();
                    adapter.Fill(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка виконання SQL-запиту: " + ex.Message);
                }
            }
            return result;
        }

        // Обробник події Click для кнопки Create
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Отримання даних з інтерфейсу користувача (приклад)
            string title = TitleTextBox.Text;
            string isbn = ISBNTexBox.Text;
            string author = AuthorTextBox.Text;
            string publisher = PublisherTextBox.Text;
            int year = Convert.ToInt32(YearTextBox.Text);

            // Створення SQL-запиту для додавання запису в базу даних
            string query = $"INSERT INTO Books (Title, ISBN, Author, Publisher, Year) " +
                           $"VALUES ('{title}', '{isbn}', '{author}', '{publisher}', {year})";

            // Виклик методу для виконання SQL-запиту
            ExecuteNonQuery(query);

            // Оновлення списку книг
            LoadBooks();
        }

        // Обробник події Click для кнопки Update
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Отримання даних з інтерфейсу користувача та вибраного рядка в DataGrid 
            DataRowView selectedRow = (DataRowView)DetailsDataGrid.SelectedItem;
            if (selectedRow != null)
            {
                int id = Convert.ToInt32(selectedRow["ISBN"]);
                string title = TitleTextBox.Text;
                string isbn = ISBNTexBox.Text;
                string author = AuthorTextBox.Text;
                string publisher = PublisherTextBox.Text;
                int year = Convert.ToInt32(YearTextBox.Text);

                // Створення SQL-запиту для оновлення запису в базі даних
                string query = $"UPDATE Books SET Title = '{title}', ISBN = '{isbn}', " +
                               $"Author = '{author}', Publisher = '{publisher}', Year = {year} " +
                               $"WHERE ID = {id}";

                // Виклик методу для виконання SQL-запиту
                ExecuteNonQuery(query);

                // Оновлення списку книг
                LoadBooks();
            }
        }

        // Обробник події Click для кнопки Delete
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Отримання ID вибраного рядка в DataGrid (приклад)
            DataRowView selectedRow = (DataRowView)DetailsDataGrid.SelectedItem;
            if (selectedRow != null)
            {
                int id = Convert.ToInt32(selectedRow["ISBN"]);

                // Створення SQL-запиту для видалення запису з бази даних
                string query = $"DELETE FROM Books WHERE ISBN = {id}";

                // Виклик методу для виконання SQL-запиту
                ExecuteNonQuery(query);

                // Оновлення списку книг
                LoadBooks();
            }
        }
    }
}
