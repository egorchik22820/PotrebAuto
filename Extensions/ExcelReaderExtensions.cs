using OfficeOpenXml;
using PotrebAuto.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotrebAuto.Extensions
{
    public static class ExcelReaderExtensions
    {
        //////////////
        public static ExcelPackage GetExcelPackage(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл не найден: {filePath}");
            }

            return new ExcelPackage(new FileInfo(filePath));
        }

        public static decimal GetDecimalValue(this ExcelRange cell)
        {
            if (cell == null || string.IsNullOrEmpty(cell.Text))
                return 0m;

            // Обработка чисел с пробелами (например: "1 687 351.86659")
            string cleanedText = cell.Text.Replace(" ", "").Replace(",", ".");

            if (decimal.TryParse(cell.Text, out decimal result))
                return result;

            return 0m;
        }

        public static CellDTO GetCellDTO(this ExcelRange cell)
        {
            var cellDTO = new CellDTO(cell);
            return cellDTO;
        }

        public static List<CellDTO> GetMonthIndicationsList(this ExcelWorksheet worksheet, int row, int startColNumber)
        {
            var daysList = new List<CellDTO>(31);

            for (int col = startColNumber; col < startColNumber + 31; col++)
            {
                try
                {
                    var cell = worksheet.Cells[row, col];
                    var cellDto = cell.GetCellDTO();

                    // Можно добавить дополнительную информацию
                    cellDto.Text = cell.Text;
                    cellDto.Value = decimal.TryParse(cell.Value,out var res);

                    daysList.Add(cellDto);
                }
                catch (Exception ex)
                {
                    daysList.Add(new CellDTO
                    {
                        Text = string.Empty,
                        BackgroundColor = Color.LightGray,
                        DayNumber = col - startColNumber + 1
                    });
                }
            }

            return daysList;
        }

        public static decimal GetDecimaPositivelValue(this ExcelRange cell)
        {
            if (cell == null || string.IsNullOrEmpty(cell.Text))
                return 0m;

            // Обработка чисел с пробелами (например: "1 687 351.86659")
            string cleanedText = cell.Text.Replace(" ", "").Replace(",", ".");


            if (decimal.TryParse(cell.Text, out decimal result) && result > 0)
                return result;

            return 0m;
        }

        public static string GetStringYear(this ExcelRange cell)
        {
            if (cell == null || string.IsNullOrEmpty(cell.Text))
                return string.Empty;

            return string.Join("", cell.Text.Split()); // Split() разделяет по всем пробельным символам
        }

        public static bool IsEmptyRow(this ExcelWorksheet worksheet, int row)
        {
            if (worksheet.Dimension == null) return true;

            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                if (!string.IsNullOrEmpty(worksheet.Cells[row, col]?.Text?.Trim()))
                    return false;
            }
            return true;
        }

        public static string SafeGetCellValue(this ExcelWorksheet worksheet, int row, int col)
        {
            try
            {
                return worksheet.Cells[row, col]?.Text?.Trim() ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static ExcelWorksheet GetWorksheet(this ExcelPackage package, int worksheetIndex = 1)
        {
            try
            {
                if (worksheetIndex < 1)
                    throw new ArgumentOutOfRangeException(nameof(worksheetIndex), "Индекс листа должен быть больше 0");

                var worksheets = package.Workbook.Worksheets;

                if (worksheets.Count == 0)
                    throw new Exception("Файл не содержит листов");

                // Создаем массив вместо List для большей производительности
                var indexes = new int[worksheets.Count];

                for (int i = 0; i < worksheets.Count; i++)
                {
                    indexes[i] = worksheets[i + 1].Index; // Worksheets индексируется с 1
                }

                if (worksheetIndex > indexes.Length)
                    throw new Exception($"Лист с индексом {worksheetIndex} не найден. Всего листов: {indexes.Length}");

                var worksheet = package.Workbook.Worksheets[indexes[worksheetIndex - 1]];

                if (worksheet.Dimension == null)
                    throw new Exception($"Лист '{worksheet.Name}' не содержит данных");

                return worksheet;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении листа с индексом {worksheetIndex}: {ex.Message}", ex);
            }
        }
    }
}
