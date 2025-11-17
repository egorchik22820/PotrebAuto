using OfficeOpenXml;
using PotrebAuto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotrebAuto.Extensions;

namespace PotrebAuto.Servises
{
    public class ExcelInsertService
    {
        public static void ExcelDataInsert(string templatePath, string newFilePath,
                                            List<ConsumersDataObject> consumers)
        {

            CopyTemplate(templatePath, newFilePath);

            using (var package = new ExcelPackage(new FileInfo(newFilePath)))
            {
                consumers.InsertData(package, "Page 1");

                package.Save();
            }
        }

        private static void CopyTemplate(string templatePath, string newFilePath)
        {
            File.Copy(templatePath, newFilePath, true);
        }
    }
}
