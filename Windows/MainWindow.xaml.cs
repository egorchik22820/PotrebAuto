using Microsoft.Win32;
using PotrebAuto.Extensions;
using PotrebAuto.Models;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _selectedConsumersPath;
        private string _selectedConsumersAndSourcesPath;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void OriginalFileBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls",
                Title = "Выберите файл потребителей",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _selectedConsumersPath = openFileDialog.FileName;
                var fileName = $"{Path.GetFileName(_selectedConsumersPath)}";
                OriginalFileText.Text = fileName.Length <= 20 ? fileName : fileName.Substring(0, 20) + "...";
                OriginalFileText.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void ConsumersAndSourcesFileBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls",
                Title = "Выберите файл с исочниками",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _selectedConsumersAndSourcesPath = openFileDialog.FileName;
                var fileName = $"{Path.GetFileName(_selectedConsumersAndSourcesPath)}";
                ConsumersAndSourcesFileText.Text = fileName.Length <= 20 ? fileName : fileName.Substring(0, 20) + "...";
                ConsumersAndSourcesFileText.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            List<ConsumersDataObject> consumers = ConsumersFileReaderService.ReadExcelFile(_selectedConsumersPath);

            List<SourcesAndConsumersObject> sources = SourcesAndConsumersFileReaderService.ReadExcelFile(_selectedConsumersAndSourcesPath);

            List<ConsumersDataObject> result = consumers.GetUnionData(sources);
        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void instructionBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
