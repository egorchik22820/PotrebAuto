using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotrebAuto.Models;

namespace PotrebAuto.Servises
{
    public static class DataServices
    {
        public static string TryGetIdFromHyperlink(this Uri hyperlink)
        {
            if (hyperlink == null)
                return "Нет данных";

            string url = hyperlink.ToString();

            if (string.IsNullOrWhiteSpace(url))
                return "Нет данных";

            url = url.TrimEnd('/');

            int lastSlashIndex = url.LastIndexOf('/');

            if (lastSlashIndex >= 0 && lastSlashIndex < url.Length - 1)
                return url.Substring(lastSlashIndex + 1);

            return "Нет данных";

        }

        
    }
}
