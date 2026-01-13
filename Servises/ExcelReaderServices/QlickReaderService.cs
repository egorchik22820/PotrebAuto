using PotrebAuto.Configuration;
using PotrebAuto.Extensions;
using PotrebAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotrebAuto.Servises.ExcelReaderServices
{
    public class QlickReaderService
    {
        public static List<QlickDataObject> ReadExcelFile(string filePath)
        {
            var result = new List<QlickDataObject>();

            using (var package = ExcelReaderExtensions.GetExcelPackage(filePath))
            {
                // Получаем первый лист из книги
                var worksheet = package.GetWorksheet(1);

                // Кэшируем конфигурацию перед циклом   // поменять
                var config = ConfigModel.GiTConf;
                var constConfig = ConfigModel.ConstantsConf;
                int startRow = constConfig.GiTDataRowStart;
                int buildingId = config.BuildingId;
                int buildingType = config.BuildingType;
                int city = config.City;

                // Определяем количество строк с данными
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 3; row <= rowCount; row++)
                {
                    try
                    {
                        if (worksheet.IsEmptyRow(row))/////////
                            break;

                        var data = new QlickDataObject
                        {

                            BuildingGUID = worksheet.SafeGetCellValue(row, 2),
                            BuildingId = worksheet.SafeGetCellValue(row, 3)

                        };

                        result.Add(data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка чтения строки {row}: {ex.Message}");
                    }
                }
            }

            return result;
        }
    }
}
