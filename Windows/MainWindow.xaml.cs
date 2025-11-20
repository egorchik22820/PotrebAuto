using Microsoft.Win32;
using PotrebAuto.Extensions;
using PotrebAuto.Models;
using PotrebAuto.Servises;
using PotrebAuto.Servises.ExcelReaderServices;
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
using Path = System.IO.Path;

namespace PotrebAuto.Windows
{
    /// <summary>
    /// Р›РѕРіРёРєР° РІР·Р°РёРјРѕРґРµР№СЃС‚РІРёСЏ РґР»СЏ MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _selectedConsumersPath;
        private string _selectedConsumersAndSourcesPath;
        private readonly int _maxPathLength = 20;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void OriginalFileBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls",
                Title = "Выберете исходный файл",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _selectedConsumersPath = openFileDialog.FileName;
                var fileName = $"{Path.GetFileName(_selectedConsumersPath)}";
                OriginalFileText.Text = fileName.Length <= _maxPathLength ? fileName : fileName.Substring(0, _maxPathLength) + "...";
                OriginalFileText.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void ConsumersAndSourcesFileBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls",
                Title = "Выберете файл Источники - потребители",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _selectedConsumersAndSourcesPath = openFileDialog.FileName;
                var fileName = $"{Path.GetFileName(_selectedConsumersAndSourcesPath)}";
                ConsumersAndSourcesFileText.Text = fileName.Length <= _maxPathLength ? fileName : fileName.Substring(0, _maxPathLength) + "...";
                ConsumersAndSourcesFileText.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            // Путь до шаблона и итоговой формы
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExcelTemplates", "ConsumersTemplate.xlsx");
            // Автоматическое сохранение в папку на рабочем столе
            string reportsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Потребители");

            // Создаем папку если ее нет
            if (!Directory.Exists(reportsFolder))
                Directory.CreateDirectory(reportsFolder);

            string newFilePath = Path.Combine(reportsFolder, "Потребители_ДАТА.xlsx");



            List<ConsumersDataObject> consumers = ConsumersFileReaderService.ReadExcelFile(_selectedConsumersPath);

            List<SourcesAndConsumersObject> sources = SourcesAndConsumersFileReaderService.ReadExcelFile(_selectedConsumersAndSourcesPath);

            List<ConsumersDataObject> result = consumers.GetUnionData(sources);



            // вставка в эксель
            ExcelInsertService.ExcelDataInsert(templatePath, newFilePath,
                                                result);

            ReadyText.Text = "ГОТОВО";
            ReadyText.Margin = new Thickness(10);

            // Открываем папку с файлом
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{newFilePath}\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть папку: {ex.Message}",
                              "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Show();
        }

        private void instructionBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
