using OfficeOpenXml;
using OfficeOpenXml.Style;
using PotrebAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotrebAuto.Servises
{
    public static class DataServices
    {
        public static void ApplyRgbColorToCell(ExcelRange cell, string rgbColor)
        {
            if (string.IsNullOrEmpty(rgbColor)) return;

            try
            {
                // Прямое преобразование RGB в Color
                var color = System.Drawing.Color.FromArgb(
                    int.Parse(rgbColor, System.Globalization.NumberStyles.HexNumber));

                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(color);
            }
            catch
            {
                // Игнорируем ошибки преобразования цвета
            }
        }

        public static string TryGetIdFromHyperlink(this Uri hyperlink)
        {
            if (hyperlink == null)
                return "Нет данных";

            string url = hyperlink.ToString();

            if (string.IsNullOrWhiteSpace(url))
                return "Нет данных";

            url = url.TrimEnd('/');

            int lastSlashIndex = url.LastIndexOf('/');

            if (lastSlashIndex >= 0 && lastSlashIndex < url.Length - 1)
                return url.Substring(lastSlashIndex + 1);

            return "Нет данных";

        }

        
    }
}
