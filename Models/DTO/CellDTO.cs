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
        public CellDTO() { }

        public object Value { get; set; }

        public decimal? Digit
        {
            get
            {
                if (Value == null) return null;

                if (Value is decimal decimalValue)
                    return decimalValue;

                if (Value is int intValue)
                    return (decimal)intValue;

                if (Value is double doubleValue)
                    return (decimal)doubleValue;

                if (Value is float floatValue)
                    return (decimal)floatValue;

                if (decimal.TryParse(Value.ToString(), out decimal result))
                    return result;

                return null;
            }
        }

        public string Text
        {
            get
            {
                if (Value == null) return string.Empty;

                if (Value is string stringValue)
                    return stringValue;

                return Value.ToString();
            }
        }

        public Uri Hyperlink { get; set; }
        public string BackGroundColorRgb { get; set; }
    }
}
