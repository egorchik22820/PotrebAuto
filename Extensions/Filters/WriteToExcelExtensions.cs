using OfficeOpenXml;
using PotrebAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotrebAuto.Extensions.Filters
{
    public static class WriteToExcelExtensions
    {
        ///////////////////////////НАСТРОЙКА СТОЛБЦОВ В ШАБЛОНЕ///////////////////////////////////


        public static void InsertData_With_ODPU(this List<ConsumersDataObject> GVSData, ExcelPackage package, string sheetName)
        {
            var worksheet = package.Workbook.Worksheets[sheetName];

            for (int i = 0; i < GVSData.Count; i++)
            {
                var item = GVSData[i];
                int row = i + 2;

                worksheet.Cells[row, 1].Value = item.Base;
                worksheet.Cells[row, 2].Value = item.BuildingId;
                worksheet.Cells[row, 3].Value = item.TU_AIIS_Id;
                worksheet.Cells[row, 4].Value = item.SysPU_Id;
                worksheet.Cells[row, 5].Value = item.City;
                worksheet.Cells[row, 6].Value = item.HeatSupplyZone;
                worksheet.Cells[row, 7].Value = item.Address;
                worksheet.Cells[row, 8].Value = item.Rashod_ODPU_GVS_Gcal;
                worksheet.Cells[row, 9].Value = item.Rashod_ODPU_GVS_m3;
                worksheet.Cells[row, 10].Value = item.h_CoeffHeatContent_With_ODPU;
                worksheet.Cells[row, 11].Value = item.PO_1C_Gcal;
                worksheet.Cells[row, 12].Value = item.PO_1C_m3;
                worksheet.Cells[row, 13].Value = item.PO_1C_WithOutRecalc_Gcal;
                worksheet.Cells[row, 14].Value = item.PO_1C_WithOutRecalc_m3;
                worksheet.Cells[row, 15].Value = item.LossGVS_With_ODPU;
                worksheet.Cells[row, 16].Value = item.FormulaValue;
                worksheet.Cells[row, 17].Value = item.ZTP;
                worksheet.Cells[row, 18].Value = item.BuildingType;
                worksheet.Cells[row, 19].Value = item.NegativeODN_Gcal;
                worksheet.Cells[row, 20].Value = item.NegativeODN_m3;
            }
        }
    }
}
