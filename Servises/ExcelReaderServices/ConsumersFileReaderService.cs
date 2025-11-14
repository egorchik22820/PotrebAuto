using PotrebAuto.Extensions;
using PotrebAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotrebAuto.Servises.ExcelReaderServices
{
    public class ConsumersFileReaderService
    {
        public static List<ConsumersDataObject> ReadExcelFile(string filePath)
        {
            var result = new List<ConsumersDataObject>();

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

                for (int row = 4; row <= rowCount; row++)
                {
                    try
                    {
                        if (worksheet.IsEmptyRow(row))
                            break;

                        var data = new ConsumersDataObject
                        {
                            Number = worksheet.Cells[row, 1].GetCellDTO(),
                            Address = worksheet.Cells[row, 2].GetCellDTO(),
                            Id = worksheet.Cells[row, 3].GetCellDTO(),
                            PU_GcalTotal = worksheet.Cells[row, 4].GetCellDTO(),
                            PU_WithVNR_Gcal = worksheet.Cells[row, 5].GetCellDTO(),
                            ZM_GcalTotal = worksheet.Cells[row, 6].GetCellDTO(),
                            ZM_WithAverage_Gcal = worksheet.Cells[row, 7].GetCellDTO(),
                            WithBS_Gcal = worksheet.Cells[row, 8].GetCellDTO(),
                            WithRealLoadBS_Gcal = worksheet.Cells[row, 9].GetCellDTO(),
                            WithRealLoadTU_Gcal = worksheet.Cells[row, 10].GetCellDTO(),
                            WithNP_Gcal = worksheet.Cells[row, 11].GetCellDTO(),
                            WithCab_Gcal = worksheet.Cells[row, 12].GetCellDTO(),
                            WithKSN_Gcal = worksheet.Cells[row, 13].GetCellDTO(),
                            WithRealLoad_FN_Gcal = worksheet.Cells[row, 14].GetCellDTO(),
                            WithDocAndKSN_Gcal = worksheet.Cells[row, 15].GetCellDTO(),
                            WithDoc_Gcal = worksheet.Cells[row, 16].GetCellDTO(),
                            CorrectNotesCount = worksheet.Cells[row, 17].GetCellDTO(),
                            Q_T_M_IsNull = worksheet.Cells[row, 18].GetCellDTO(),
                            ActsBS = worksheet.Cells[row, 19].GetCellDTO(),
                            Max_Min_Q_M = worksheet.Cells[row, 20].GetCellDTO(),
                            IsValid_VNR = worksheet.Cells[row, 21].GetCellDTO(),
                            IsValid_M1_M2 = worksheet.Cells[row, 22].GetCellDTO(),
                            IsValid_M1_M2_2 = worksheet.Cells[row, 23].GetCellDTO(),
                            Q_eng = worksheet.Cells[row, 24].GetCellDTO(),
                            IsValid_T = worksheet.Cells[row, 25].GetCellDTO(),
                            DaysValue = worksheet.GetMonthIndicationsList(row,26)

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
