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

            foreach (var cm in consumers)
            {
                SACDict.TryGetValue(cm.TU_AIIS.Text, out var sacItem);
                cm.ObjectId = new CellDTO { Text = sacItem != null ? sacItem.Obj_Id : "Нет данных" };
            }
            return consumers;
        }
    }
}
