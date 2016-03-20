using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public static class FieldsGen
    {
        public static void GenerateFields(string path, string outPath)
        {
            StringBuilder builder = new StringBuilder();

            string text = System.IO.File.ReadAllText(path);
            string[] stringSeparators = new string[] { "\r\n" };
            string[] core = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

            //string[] fields = core[0].Split(','); 

            foreach (var str in core)
            {
                if (str.Contains("Date"))
                {
                    builder.AppendLine("<fields>");
                    builder.AppendLine(string.Format("<fullName>{0}</fullName>", str));
                    builder.AppendLine("<externalId>false</externalId>");
                    builder.AppendLine(string.Format("<label>{0}</label>", str.Replace("__c", "")));
                    builder.AppendLine("<required>false</required>");
                    builder.AppendLine("<trackTrending>false</trackTrending>");
                    builder.AppendLine("<type>DateTime</type>");
                    builder.AppendLine("</fields>");
                }
                else
                {
                    builder.AppendLine("<fields>");
                    builder.AppendLine(string.Format("<fullName>{0}</fullName>", str));
                    builder.AppendLine("<externalId>false</externalId>");
                    builder.AppendLine(string.Format("<label>{0}</label>", str.Replace("__c", "")));
                    builder.AppendLine("<length>255</length>");
                    builder.AppendLine("<required>false</required>");
                    builder.AppendLine("<trackTrending>false</trackTrending>");
                    builder.AppendLine("<type>Text</type>");
                    builder.AppendLine("<unique>false</unique>");
                    builder.AppendLine("</fields>");
                }
            }

            //foreach (var str in fields)
            //{
            //    if (str.Contains("Date"))
            //    {
            //        builder.AppendLine("<fields>");
            //        builder.AppendLine(string.Format("<fullName>{0}__c</fullName>", str));
            //        builder.AppendLine("<externalId>false</externalId>");
            //        builder.AppendLine(string.Format("<label>{0}</label>", str.Replace("_", " ")));
            //        builder.AppendLine("<required>false</required>");
            //        builder.AppendLine("<trackTrending>false</trackTrending>");
            //        builder.AppendLine("<type>DateTime</type>");
            //        builder.AppendLine("</fields>");
            //    }
            //    else
            //    {
            //        builder.AppendLine("<fields>");
            //        builder.AppendLine(string.Format("<fullName>{0}__c</fullName>", str));
            //        builder.AppendLine("<externalId>false</externalId>");
            //        builder.AppendLine(string.Format("<label>{0}</label>", str.Replace("_", " ")));
            //        builder.AppendLine("<length>255</length>");
            //        builder.AppendLine("<required>false</required>");
            //        builder.AppendLine("<trackTrending>false</trackTrending>");
            //        builder.AppendLine("<type>Text</type>");
            //        builder.AppendLine("<unique>false</unique>");
            //        builder.AppendLine("</fields>");
            //    }
            //}

            //for (int i = 0; i < 10; i++)
            //{
            //    int fieldIncrement = i + 1;
            //    builder.AppendLine("<fields>");
            //    builder.AppendLine(string.Format("<fullName>{0}_{1}__c</fullName>", "Text_Field",fieldIncrement));
            //    builder.AppendLine("<externalId>false</externalId>");
            //    builder.AppendLine(string.Format("<label>{0} {1}</label>", "Text Field", fieldIncrement));
            //    builder.AppendLine("<length>255</length>");
            //    builder.AppendLine("<required>false</required>");
            //    builder.AppendLine("<trackTrending>false</trackTrending>");
            //    builder.AppendLine("<type>Text</type>");
            //    builder.AppendLine("<unique>false</unique>");
            //    builder.AppendLine("</fields>");
            //}

            System.IO.File.WriteAllText(outPath, builder.ToString());
        }
    }
}
