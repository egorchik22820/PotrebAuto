using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotrebAuto.Models;
using PotrebAuto.Models.DTO;
using PotrebAuto.Configuration;
using PotrebAuto.Servises;

namespace PotrebAuto.Extensions
{
    public static class ConsumersUnionDataExtension
    {
        private readonly static string _noData = ConfigModel.NoData;
        public static List<ConsumersDataObject> GetUnionData(this List<ConsumersDataObject> consumers,
                                                     Dictionary<string, SourcesAndConsumersObject> SACDict)
        {
            foreach (var cm in consumers)
            {
                SACDict.TryGetValue(cm.TU_AIIS.Value?.ToString(), out var sacItem);

                cm.ObjectId = new CellDTO { Value = sacItem?.Obj_Id ?? _noData };

            }
            return consumers;
        }

        public static List<ConsumersDataObject> GetUnionDataExtra(this List<ConsumersDataObject> consumers, Dictionary<string, ConsumersDataObject> consumersSecond,
                                                                     Dictionary<string, SourcesAndConsumersObject> SACDict,
                                                                     Dictionary<string, GiTDataObject> GiTData,
                                                                     Dictionary<string, QlickDataObject> qlickData)
        {

            foreach (var cm in consumers)
            {

                consumersSecond.TryGetValue(cm.TU_AIIS.Value?.ToString(), out var secondItem);

                cm.PO_AIIS_Total_2 = new CellDTO { Value = secondItem?.PO_AIIS_Total.Value ?? _noData };
                cm.ColorDaysCount_2 = new CellDTO { Value = secondItem?.ColorDaysCount.Value ?? _noData };
                //cm.ColorDaysCount_2 = new CellDTO { Value = secondItem?.ColorDaysCount.Value ?? _noData };
                cm.PU_GcalTotal_2 = new CellDTO { Value = secondItem?.PU_GcalTotal.Value ?? _noData };
                cm.ZM_GcalTotal_2 = new CellDTO { Value = secondItem?.ZM_GcalTotal.Value ?? _noData };


                SACDict.TryGetValue(cm.TU_AIIS.Value?.ToString(), out var sacItem);

                cm.ObjectId = new CellDTO { Value = sacItem?.Obj_Id ?? _noData };



                qlickData.TryGetValue(cm.ObjectId.Value?.ToString(), out var qlickItem);

                cm.BuildingId = new CellDTO { Value = qlickItem?.BuildingId.Value ?? _noData };


                GiTData.TryGetValue(cm.BuildingId.Value?.ToString(), out var GiTItem);
                
                cm.BuildingType = new CellDTO { Value = GiTItem?.BuildingType.Value ?? _noData };
                cm.CityGiT = new CellDTO { Value = GiTItem?.City.Value ?? _noData };


                
            }
            return consumers;
        }
    }
}
