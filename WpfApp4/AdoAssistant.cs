using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp4
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows;

    public class AdoAssistant
    {
        // Отримуємо рядок з'єднання з файлу App.config
        String connectionString = System.Configuration.
            ConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString;

        // Метод для завантаження даних з таблиці у DataTable
        public DataTable TableLoad()
        {
            DataTable dt = new DataTable(); // Посилання на об'єкт DataTable
                                            // Заповнюємо об'єкт таблиці даними з БД
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand(); // Створюємо об'єкт команди
                SqlDataAdapter adapter = new SqlDataAdapter(command); // Створюємо об'єкт читання
                                                                      // Завантажуємо дані 
                command.CommandText = "Select ID, (Surname + Name) AS FullName, Address, Place, PostalCode From Table_Address";
                try
                {
                    // Метод сам відкриває БД і сам її закриває
                    adapter.Fill(dt);
                }
                catch (Exception)
                {
                    MessageBox.Show("Помилка підключення до БД");
                }
            }
            return dt;
        }

        // Метод для виконання SQL-запиту типу NonQuery (додавання, оновлення, видалення)
        public bool ExecuteNonQuery(string query)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка виконання SQL-запиту: " + ex.Message);
                }
            }
            return success;
        }
    }
}
