using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApp1;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            void canExecute_Save(object sender, CanExecuteRoutedEventArgs e)
            {
                if (inputTextBox.Text.Trim().Length > 0) e.CanExecute = true; else e.CanExecute = false;
            }
            void execute_Save(object sender, ExecutedRoutedEventArgs e)
            {
                System.IO.File.WriteAllText("C:\\Users\\user\\Desktop\\myFile.txt.txt", inputTextBox.Text);
                MessageBox.Show("The file was saved!");
            }

            void canExecute_Copy(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = true;
            }
            void execute_Copy(object sender, ExecutedRoutedEventArgs e)
            {
                Clipboard.SetText(inputTextBox.SelectedText); // Копіювати виділений текст у буфер обміну
                MessageBox.Show("Text copied to clipboard!");
            }


            
            void execute_Open(object sender, ExecutedRoutedEventArgs e)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        string fileContent = File.ReadAllText(filePath); // Зчитуємо вміст файлу

                        // Додаємо зчитаний вміст файлу до текстового поля
                        inputTextBox.Text = fileContent;

                        MessageBox.Show("File opened successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error opening file: {ex.Message}");
                    }
                }
            }


            void execute_Clear(object sender, ExecutedRoutedEventArgs e)
            {
                inputTextBox.Text = string.Empty; // Очистити текстове поле
                MessageBox.Show("Text cleared!");
            }

            void execute_Paste(object sender, ExecutedRoutedEventArgs e)
            {
                if (Clipboard.ContainsText())
                {
                    string clipboardText = Clipboard.GetText(); // Отримуємо текст з буфера обміну
                    inputTextBox.SelectedText = clipboardText; // Вставляємо текст з буфера обміну у поточну позицію курсора
                    MessageBox.Show("Text pasted!");
                }
                else
                {
                    MessageBox.Show("Clipboard does not contain text!");
                }
            }

            //Створення прив'язки та приєднання обробників
            CommandBinding saveCommand = new CommandBinding(ApplicationCommands.Save, execute_Save, canExecute_Save);
            //Реєстрація прив'язки
            CommandBindings.Add(saveCommand);

            //Створення прив'язки та приєднання обробників
            CommandBinding copyCommand = new CommandBinding(ApplicationCommands.Copy, execute_Copy, canExecute_Copy);
            //Реєстрація прив'язки
            CommandBindings.Add(copyCommand);

            //Створення прив'язки та приєднання обробників
            CommandBinding openCommand = new CommandBinding(ApplicationCommands.Open, execute_Open);
            //Реєстрація прив'язки
            CommandBindings.Add(openCommand);

            //Створення прив'язки та приєднання обробників
            CommandBinding clearCommand = new CommandBinding(ApplicationCommands.Delete, execute_Clear);
            //Реєстрація прив'язки
            CommandBindings.Add(clearCommand);

            //Створення прив'язки та приєднання обробників
            CommandBinding pasteCommand = new CommandBinding(ApplicationCommands.Paste, execute_Paste);
            //Реєстрація прив'язки
            CommandBindings.Add(pasteCommand);

        }
    }
}
