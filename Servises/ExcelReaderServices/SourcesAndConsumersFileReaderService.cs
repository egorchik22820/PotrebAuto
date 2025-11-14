using PotrebAuto.Extensions;
using PotrebAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotrebAuto.Servises.ExcelReaderServices
{
    public class SourcesAndConsumersFileReaderService
    {
        public static List<SourcesAndConsumersObject> ReadExcelFile(string filePath)
        {
            var result = new List<SourcesAndConsumersObject>();

            using (var package = ExcelReaderExtensions.GetExcelPackage(filePath))
            {
                // Получаем первый лист из книги
                var worksheet = package.GetWorksheet(1);

                //// Кэшируем конфигурацию перед циклом
                //var config = ConfigModel.MP;
                //int startRow = config.StartRow;
                //int isODPUCol = config.IsODPU;
                //int sysPU_IdCol = config.SysPU_Id;
                //int addressCol = config.Address;
                //int q_CalcCol = config.Q_Calc;
                //int dV_CalcCol = config.dV_Calc;
                //int vNR_CalcCol = config.VNR_Calc;
                //int loadTypeCol = config.LoadType;
                //int buildingIdCol = config.BuildingId;
                //int tu_AIIS_IdCol = config.TU_AIIS_Id;

                // Определяем количество строк с данными
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        if (worksheet.IsEmptyRow(row))
                            break;

                        var data = new SourcesAndConsumersObject
                        {

                            TU_Id = worksheet.SafeGetCellValue(row, 27),
                            Obj_Id = worksheet.SafeGetCellValue(row, 28)

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
