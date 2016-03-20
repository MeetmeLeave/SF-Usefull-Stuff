using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ConsoleApplication1
{
    public static class CodeGen
    {
        public static void GenerateCode(string path, string objectTo, string objectFrom)
        {
            string text = System.IO.File.ReadAllText(path);
            string[] stringSeparators = new string[] { "\n" };
            string[] fields = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder builder = new StringBuilder();

            foreach (var str in fields)
            {
                string[] fieldMap = str.Split('=');

                builder.AppendLine(string.Format("{0}.{1} = {2}.{3}__c;", objectTo, fieldMap[1].Trim(), objectFrom, fieldMap[0].Trim()));
            }

            System.IO.File.WriteAllText(@"D:\result3.txt", builder.ToString());
        }

        public static void GenerateAnonymousCode(string path, string objectTo)
        {
            string text = System.IO.File.ReadAllText(path);
            string[] stringSeparators = new string[] { "\n" };
            string[] core = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

            string[] fields = core[0].Split(',');
            string[] values = core[1].Split(',');
            StringBuilder builder = new StringBuilder();

            int size = fields.Count();

            if (size == values.Count())
            {
                for (int i = 0; i < size; i++)
                {
                    string value = values[i];
                    if (!String.IsNullOrEmpty(value))
                    {
                        builder.AppendLine(string.Format("{0}.{1}__c = '{2}';", objectTo, fields[i].Trim(), values[i]));
                    }
                }
            }

            System.IO.File.WriteAllText(@"D:\result4.txt", builder.ToString());
        }

        public static void ReverseCode(string path)
        {
            string text = System.IO.File.ReadAllText(path);

            string[] stringSeparators = new string[] { "\r" };
            string[] core = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder builder = new StringBuilder();

            int size = core.Count();

            for (int i = 0; i < size; i++)
            {
                string value = core[i].Replace(";","");
                string[] stringSeparators2 = new string[] { "=" };
                string[] values = value.Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);

                builder.AppendLine(string.Format("{0} = {1};", values[1].Trim(), values[0].Trim()));
            }

            System.IO.File.WriteAllText(@"D:\result5.txt", builder.ToString());
        }

        public static void GenerateInsertStatementForSWAPI(string path)
        {
            /*string text = System.IO.File.ReadAllText(path);

            string[] stringSeparators = new string[] { "\r" };
            string[] core = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder builder = new StringBuilder();

            int size = core.Count();

            for (int i = 0; i < size; i++)
            {
                string value = core[i].Replace(";", "");
                string[] stringSeparators2 = new string[] { "=" };
                string[] values = value.Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);

                builder.AppendLine(string.Format("{0} = {1};", values[1].Trim(), values[0].Trim()));
            }

            System.IO.File.WriteAllText(@"D:\result5.txt", builder.ToString());*/

            XmlDocument xml = new XmlDocument();
            xml.Load(path);

           // String insert = "<params>INTO accounts (acct_name, acct_type, full_name, date_time, zip) VALUES \'*GUESTS*  1\', \'0\', \'*GUESTS*1\', \'2014-06-10T15:31:07\', \'\'</params>";

            Dictionary<String, String> attrs = new Dictionary<string, string>();

            XmlNode node = xml.FirstChild;
            foreach (XmlElement n in node.ChildNodes)
            {
                if (n.LastChild.LastChild == null)
                {
                    XmlAttributeCollection atributos = n.LastChild.Attributes;
                    foreach (XmlAttribute at in atributos)
                    {
                        attrs.Add(at.Name, at.Value);
                    }
                }
            }

            string result = "<>" + "";
        }
    }
}
