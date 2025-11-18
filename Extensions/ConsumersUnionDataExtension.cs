using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotrebAuto.Models;
using PotrebAuto.Models.DTO;
using PotrebAuto.Servises;

namespace PotrebAuto.Extensions
{
    public static class ConsumersUnionDataExtension
    {
        public static List<ConsumersDataObject> GetUnionData(this List<ConsumersDataObject> consumers,
                                                                    List<SourcesAndConsumersObject> sourcesAndConsumers)
        {
            var SACDict = sourcesAndConsumers.GroupBy(x => x.TU_Id).ToDictionary(g => g.Key, g => g.First());// поменять архитектуру, не нужно создавать новый объект, разделить логику

            var cons = consumers.Where(x => x.TU_AIIS.Value != null)
                                .ToList();

            foreach (var cm in cons)
            {
                SACDict.TryGetValue(cm.TU_AIIS.Value.ToString(), out var sacItem);

                cm.ObjectId = new CellDTO { Value = sacItem != null ? sacItem.Obj_Id : "Нет данных" };
                cm.PO_AIIS_Total = new CellDTO { Value = cm.PU_GcalTotal.Digit + cm.ZM_GcalTotal.Digit };
                cm.ColorDaysCount = new CellDTO { Value = cm.DaysValue.GetColorDaysCount() };
            }
            return consumers;
        }
    }
}
