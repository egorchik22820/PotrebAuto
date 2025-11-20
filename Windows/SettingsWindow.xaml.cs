using PotrebAuto.Configuration;
using PotrebAuto.Configuration.jsonModels;
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
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace PotrebAuto.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Loaded += SettingsWindow_Loaded; // Подписываемся на событие загрузки окна
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Проверяем существование файлов перед загрузкой
            CheckConfigFiles();
            LoadAllConfigurations();
        }

        // Метод для проверки валидности всех TextBox'ов
        private bool AreAllTextboxesValid()
        {
            // Проверяем все TextBox'ы в окне
            foreach (var textBox in FindVisualChildren<TextBox>(this))
            {
                // Пропускаем TextBox для года (особое поле)
                if (textBox.Name == "NakladCalcYearValueTextBox")
                    continue;

                if (Validation.GetHasError(textBox))
                    return false;

                if (string.IsNullOrEmpty(textBox.Text) || !int.TryParse(textBox.Text, out int value) || value < 0 || value >= 100)
                    return false;
            }
            return true;
        }

        // Вспомогательный метод для поиска всех дочерних элементов определенного типа
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void CheckConfigFiles()
        {
            string[] configPaths = {
                                        ConfigModel._Constants_ConfigPath,
                                        ConfigModel._Consumers_ConfigPath,
                                        ConfigModel._SAC_ConfigPath
                                    };

            foreach (string path in configPaths)
            {
                if (!File.Exists(path))
                {
                    System.Diagnostics.Debug.WriteLine($"Файл не найден: {path}");
                    // Создаем директорию если её нет
                    string directory = Path.GetDirectoryName(path);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Файл найден: {path}");
                }
            }
        }



        private void LoadAllConfigurations()
        {
            try
            {
                // Загружаем все конфигурации
                ConfigModel.LoadAllConfigurations();

                // Заполняем поля для ConsumersData вкладки
                NumberTextBox.Text = ConfigModel.ConsumersConf.Number.ToString();
                AddressTextBox.Text = ConfigModel.ConsumersConf.Address.ToString();
                IdTextBox.Text = ConfigModel.ConsumersConf.Id.ToString();
                PU_GcalTotalTextBox.Text = ConfigModel.ConsumersConf.PU_GcalTotal.ToString();
                PU_WithVNR_GcalTextBox.Text = ConfigModel.ConsumersConf.PU_WithVNR_Gcal.ToString();
                ZM_GcalTotalTextBox.Text = ConfigModel.ConsumersConf.ZM_GcalTotal.ToString();
                ZM_WithAverage_GcalTextBox.Text = ConfigModel.ConsumersConf.ZM_WithAverage_Gcal.ToString();
                WithBS_GcalTextBox.Text = ConfigModel.ConsumersConf.WithBS_Gcal.ToString();
                WithRealLoadBS_GcalTextBox.Text = ConfigModel.ConsumersConf.WithRealLoadBS_Gcal.ToString();
                WithRealLoadTU_GcalTextBox.Text = ConfigModel.ConsumersConf.WithRealLoadTU_Gcal.ToString();
                WithNP_GcalTextBox.Text = ConfigModel.ConsumersConf.WithNP_Gcal.ToString();
                WithCab_GcalTextBox.Text = ConfigModel.ConsumersConf.WithCab_Gcal.ToString();
                WithKSN_GcalTextBox.Text = ConfigModel.ConsumersConf.WithKSN_Gcal.ToString();
                WithRealLoad_FN_GcalTextBox.Text = ConfigModel.ConsumersConf.WithRealLoad_FN_Gcal.ToString();
                WithDocAndKSN_GcalTextBox.Text = ConfigModel.ConsumersConf.WithDocAndKSN_Gcal.ToString();
                WithDoc_GcalTextBox.Text = ConfigModel.ConsumersConf.WithDoc_Gcal.ToString();
                CorrectNotesCountTextBox.Text = ConfigModel.ConsumersConf.CorrectNotesCount.ToString();
                Q_T_M_IsNullTextBox.Text = ConfigModel. ConsumersConf.Q_T_M_IsNull.ToString();
                ActsBSTextBox.Text = ConfigModel.ConsumersConf.ActsBS.ToString();
                Max_Min_Q_MTextBox.Text = ConfigModel.ConsumersConf.Max_Min_Q_M.ToString();
                IsValid_VNRTextBox.Text = ConfigModel.ConsumersConf.IsValid_VNR.ToString();
                IsValid_M1_M2TextBox.Text = ConfigModel.ConsumersConf.IsValid_M1_M2.ToString();
                IsValid_M1_M2_2TextBox.Text = ConfigModel.ConsumersConf.IsValid_M1_M2_2.ToString();
                Q_engTextBox.Text = ConfigModel.ConsumersConf.Q_eng.ToString();
                IsValid_TTextBox.Text = ConfigModel.ConsumersConf.IsValid_T.ToString();

                // Заполняем поля для SourcesAndConsumers вкладки
                TU_IdTextBox.Text = ConfigModel.SACConf.TU_Id.ToString();
                Obj_IdTextBox.Text = ConfigModel.SACConf.Obj_Id.ToString();

                // Заполняем поля для Table Settings вкладки
                ConsumersTableRowStartTextBox.Text = ConfigModel.ConstantsConf.ConsumersTableRowStart.ToString();
                ConsumersDataRowStartTextBox.Text = ConfigModel.ConstantsConf.ConsumersDataRowStart.ToString();
                SACTableRowStartTextBox.Text = ConfigModel.ConstantsConf.SACTableRowStart.ToString();
                SACDataRowStartTextBox.Text = ConfigModel.ConstantsConf.SACDataRowStart.ToString();
                DatesColStartTextBox.Text = ConfigModel.ConstantsConf.DatesColStart.ToString();
                DaysInMonthTextBox.Text = ConfigModel.ConstantsConf.DaysInMonth.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке конфигураций: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void SourcesAndConsumersSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AreAllTextboxesValid())
            {
                MessageBox.Show("Пожалуйста, исправьте ошибки в полях ввода. Все числа должны быть в диапазоне от 0 до 99.", "Ошибка валидации",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var sacConf = new SACConfig
                {
                    TU_Id = int.Parse(TU_IdTextBox.Text),
                    Obj_Id = int.Parse(Obj_IdTextBox.Text)
                };

                ConfigModel.SaveConfig(ConfigModel._SAC_ConfigPath, sacConf);
                MessageBox.Show("Настройки МП сохранены!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении настроек источников и потребителей: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConsumersDataSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AreAllTextboxesValid())
            {
                MessageBox.Show("Пожалуйста, исправьте ошибки в полях ввода. Все числа должны быть в диапазоне от 0 до 99.", "Ошибка валидации",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var consumersConf = new ConsumersConfig
                {
                    TU_Id = int.Parse(TU_IdTextBox.Text),
                    Obj_Id = int.Parse(Obj_IdTextBox.Text)
                };

                ConfigModel.SaveConfig(ConfigModel._Consumers_ConfigPath, consumersConf);
                MessageBox.Show("Настройки МП сохранены!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении настроек источников и потребителей: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TableSettingsSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
