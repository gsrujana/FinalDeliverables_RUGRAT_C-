using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace edu.uta.cse.proggen.configurationParser
{
    using XMLException = System.Xml.XmlException;
    using IOException = System.IO.IOException;

    public class LoadXMLParser
    {
        private XmlDocument document = null;
        private Dictionary<string, string> properties = new Dictionary<string, string>();
        private HashSet<string> typeList = new HashSet<string>();

        public Dictionary<string, string> loadXMLFile(String filePath)
        {
            try
            {
                document = new XmlDocument();
                document.Load(filePath);
                try
                {
                    parseProperties();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error parsing properties from XML!");
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                    Environment.Exit(1);
                }
            }
            catch (XMLException e)
            {
                Console.WriteLine("error parsing XML configuration!");
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                Environment.Exit(1);
            }
            catch (IOException e)
            {
                Console.WriteLine("error processing XML configuration file!");
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                Environment.Exit(1);
            }
            return properties;
        }

        public void parseProperties()
        {
            XmlNode root = document.DocumentElement;
            XmlNodeList propertyNodes = root.ChildNodes;

            int numberOfPropertyNodes = propertyNodes.Count;

            for (int i = 0; i < numberOfPropertyNodes; i++)
            {
                XmlNode node = propertyNodes.Item(i);
                string name = node.Name;
                if (name.Equals("allowedTypes"))
                {
                    parseAllowedTypes(node);
                    continue;
                }
                string value = node.InnerText;
                properties[name] = value;
            }
        }

        private void parseAllowedTypes(XmlNode node)
        {
            if (!node.Name.Equals("allowedTypes"))
            {
                throw new Exception("Invalid node allowedTypes");
            }

            XmlNodeList typeNodes = node.ChildNodes;
            int noOfTypes = typeNodes.Count;

            for (int i = 0; i < noOfTypes; i++)
            {
                string str = typeNodes.Item(i).InnerText.Trim();
                if (str.Equals(""))
                {
                    continue;
                }
                typeList.Add(str);
            }
        }

        public HashSet<string> TypeList
        {
            get
            {
                return typeList;
            }
        }

    }
}
