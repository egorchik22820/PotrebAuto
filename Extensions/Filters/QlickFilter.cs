using PotrebAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotrebAuto.Extensions.Filters
{
    internal static class QlickFilter
    {
        public static List<QlickDataObject> GetFiltered(this List<QlickDataObject> QlickData)
        {
            return QlickData.Where(x => x.BuildingId != null)
                                .ToList();
        }
    }
}
