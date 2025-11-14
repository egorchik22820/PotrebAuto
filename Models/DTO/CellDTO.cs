using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace PotrebAuto.Models.DTO
{
    public class CellDTO
    {
        public CellDTO(ExcelRange cell)
        {
            Text = cell.Text ?? string.Empty;
            Hyperlink = cell.Hyperlink;
            BackGroundColor = cell.Style.Fill.BackgroundColor;
        }

        public CellDTO()
        {

        }

        public string Text { get; set; }
        public Uri Hyperlink { get; set; }
        public ExcelColor BackGroundColor { get; set; }
    }
}
