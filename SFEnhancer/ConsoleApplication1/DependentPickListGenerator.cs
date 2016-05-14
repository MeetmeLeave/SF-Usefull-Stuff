using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public static class DependentPickListGenerator
    {
        public static void PickDependentPicklists(string path, string outPath)
        {
            string text = System.IO.File.ReadAllText(path);
            string[] stringSeparators = new string[] { "\r\n" };
            string[] core = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> pickListDependencies = new Dictionary<string, string>();

            foreach (var str in core)
            {
                stringSeparators[0] = ("\t");
                string[] picklistCombo = str.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                string key = picklistCombo[1].Replace("\"", "");
                string controling = picklistCombo[0].Replace("\"", "");

                if (!pickListDependencies.ContainsKey(key))
                {
                    string temp = "<picklistValues>";
                    temp += "<fullName>" + key + "</fullName>";
                    temp += "<controllingFieldValues>" + controling + "</controllingFieldValues>";
                    pickListDependencies.Add(key, temp);
                }
                else {
                    pickListDependencies[key] += "<controllingFieldValues>" + controling + "</controllingFieldValues>";
                }
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<fields>");
            builder.AppendLine("<fullName>Infustry_Type__c</fullName>");
            builder.AppendLine("<externalId>false</externalId>");
            builder.AppendLine("<label>Infustry Type</label>");
            builder.AppendLine("<picklist>");
            builder.AppendLine("<controllingField>Industry</controllingField>");

            foreach (var key in pickListDependencies.Keys)
            {
                builder.AppendLine(pickListDependencies[key]);
                builder.AppendLine("<default>false</default>");
                builder.AppendLine("</picklistValues>");
            }

            builder.AppendLine("<sorted>false</sorted>");
            builder.AppendLine("</picklist>");
            builder.AppendLine("<trackFeedHistory>false</trackFeedHistory>");
            builder.AppendLine("<type>Picklist</type>");
            builder.AppendLine("</fields>");

            System.IO.File.WriteAllText(outPath, builder.ToString());
        }
    }
}
