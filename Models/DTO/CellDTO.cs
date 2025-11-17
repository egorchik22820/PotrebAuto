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

        public CellDTO()
        {

        }

        public object Value
        {
            get
            {
                if (Digit == null)
                {
                    return Text;
                }
                return Digit;
            }
            set { }
        }
        public decimal? Digit { get; set; }
        public string Text {  get; set; }
        public Uri Hyperlink { get; set; }
        public string BackGroundColorRgb { get; set; }
    }
}
