using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace PotrebAuto.Models.DTO
{
    public class CellDTO
    {
        public string Text { get; set; }
        public decimal Value { get; set; }
        public string Hyperlink { get; set; }
        public Color? BackGroundColor { get; set; }
    }
}
