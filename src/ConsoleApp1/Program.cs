using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = new XmlDocument();
            doc.Load("XMLFile1.xml");
            var descriptions = doc.LastChild;
            StringBuilder code = new StringBuilder();
            code.AppendLine("public enum FontAwesomeIcons")
                .AppendLine("{");

            foreach (XmlNode desc in descriptions.ChildNodes)
            {
                var elements = desc.Children().ToArray();
                var name = elements[1].Value.Trim().Substring(3);
                if (name == "500px")
                    name = "FiveHundredPx";
                name = name.Substring(0, 1).ToUpper() + name.Substring(1);
                int index = 0;
                while ((index = name.IndexOf('-')) >= 0)
                {
                    string first = name.Substring(0, index);
                    string last = name.Substring(index + 1);
                    last = last.Substring(0, 1).ToUpper() + last.Substring(1);
                    name = first + last;
                }
                string value = elements[2].InnerText;
                if (value == "(alias)")
                    value = elements[3].InnerText;
                code.AppendLine($"{name} = {value},");
            }
            code.AppendLine("}");
            File.WriteAllText("out.cs", code.ToString());
        }
    }

    public static class e
    {
        public static IEnumerable<XmlNode> Children(this XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
                yield return child;
        }
    }
}
