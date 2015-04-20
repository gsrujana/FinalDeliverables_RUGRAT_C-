using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using edu.uta.cse.proggen.configurationParser;

namespace Rugrat_withGUI
{
    using Start = edu.uta.cse.proggen.start.Start;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtArraySize.Text = "";
            txtIntMaxValue.Text = "";
            txtMaxMethod.Text = "";
            txtMaxParam.Text = "";
            txtMethodCalls.Text = "";
            txtMinParam.Text = "";
            txtNamePrefix.Text = "";
            txtNestedIF.Text = "";
            txtNoOfClasses.Text = "";
            txtTotalLOC.Text = "";
        }


        private void btnSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            var status = saveConfigToXML();

            if (status)
            {
                MessageBox.Show("XML File is saved in the default location : \n" + Start.Start.PathToDir);
            }

        }
        //This is Generate C# code button
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            String[] args = { };

            var status = saveConfigToXML();

            if (status)
            {
                Start.Start.Main1(args);

                MessageBox.Show(
                    "C# Files are created successfully based on the given parameters.! Check the path: \n " +
                    Start.Start.PathToDir);
            }
        }
        public Dictionary<string, string> retrieveFormFields()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("NoOfClasses", txtNoOfClasses.Text.ToString());
            dictionary.Add("MethodCalls", txtMethodCalls.Text.ToString());
            dictionary.Add("ArraySize", txtArraySize.Text.ToString());
            dictionary.Add("NestedIF", txtNestedIF.Text.ToString());
            dictionary.Add("MaxParam", txtMaxParam.Text.ToString());
            dictionary.Add("TotalLOC", txtTotalLOC.Text.ToString());
            dictionary.Add("NamePrefix", txtNamePrefix.Text.ToString());
            dictionary.Add("MaxMethod", txtMaxMethod.Text.ToString());
            dictionary.Add("MinParam", txtMinParam.Text.ToString());
            dictionary.Add("IntMaxValue", txtIntMaxValue.Text.ToString());
            return dictionary;
        }

        public Boolean saveConfigToXML()
        {
            Dictionary<string, string> returned_Dictionary = retrieveFormFields();
            Boolean validData = validateForm(returned_Dictionary);

            if (validData)
            {

                string filename = (Start.Start.PathToDir + "config.xml");
                XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("RUGRAT");


                writer.WriteStartElement("useQueries");
                writer.WriteString("false");
                writer.WriteEndElement();

                writer.WriteStartElement("injectFilename");
                writer.WriteEndElement();

                writer.WriteStartElement("password");
                writer.WriteEndElement();

                writer.WriteStartElement("allowIndirectRecursion");
                writer.WriteString("no");
                writer.WriteEndElement();

                writer.WriteStartElement("doReachabilityMatrix");
                writer.WriteString("no");
                writer.WriteEndElement();

                writer.WriteStartElement("noOfInheritanceChains");
                writer.WriteString("0");
                writer.WriteEndElement();

                writer.WriteStartElement("probability");
                writer.WriteString("0");
                writer.WriteEndElement();

                writer.WriteStartElement("minNoOfClassFields");
                writer.WriteString("0");
                writer.WriteEndElement();

                writer.WriteStartElement("allowArray");
                writer.WriteString("no");
                writer.WriteEndElement();

                writer.WriteStartElement("maxRecursionDepth");
                writer.WriteString("1");
                writer.WriteEndElement();

                writer.WriteStartElement("maxNoOfClassFields");
                writer.WriteString("0");
                writer.WriteEndElement();

                writer.WriteStartElement("recursionProbability");
                writer.WriteString("0");
                writer.WriteEndElement();

                writer.WriteStartElement("callType");
                writer.WriteString("MCO1_2");
                writer.WriteEndElement();

                writer.WriteStartElement("noOfClasses");
                writer.WriteString(returned_Dictionary["NoOfClasses"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("maxAllowedMethodCalls");
                writer.WriteString(returned_Dictionary["MethodCalls"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("maximumArraySize");
                writer.WriteString(returned_Dictionary["ArraySize"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("maxNestedIfs");
                writer.WriteString(returned_Dictionary["NestedIF"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("maxNoOfParametersPerMethod");
                writer.WriteString(returned_Dictionary["MaxParam"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("totalLOC");
                writer.WriteString(returned_Dictionary["TotalLOC"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("classNamePrefix");
                writer.WriteString(returned_Dictionary["NamePrefix"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("maxNoOfMethodsPerClass");
                writer.WriteString(returned_Dictionary["MaxMethod"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("minNoOfParametersPerMethod");
                writer.WriteString(returned_Dictionary["MinParam"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("intMaxValue");
                writer.WriteString(returned_Dictionary["IntMaxValue"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("allowedTypes");
                writer.WriteStartElement("type");
                writer.WriteString("int");
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();

                return true;

            }
            else
            {
                return false;
            }

        }

        private void btnUseConfig_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            LoadXMLParser lxp = new LoadXMLParser();
            Dictionary<string, string> properties;
            HashSet<string> typeList;
            String filePath, fileName;

            if (ofd.ShowDialog() == true)
            {
                filePath = ofd.FileName;
                fileName = ofd.SafeFileName;
                if (!filePath.Equals(""))
                {
                    properties = lxp.loadXMLFile(filePath);
                    typeList = lxp.TypeList;

                    txtNoOfClasses.Text = properties["noOfClasses"];
                    txtMethodCalls.Text = properties["maxAllowedMethodCalls"];
                    txtArraySize.Text = properties["maximumArraySize"];
                    txtNestedIF.Text = properties["maxNestedIfs"];
                    txtMaxParam.Text = properties["maxNoOfParametersPerMethod"];
                    txtTotalLOC.Text = properties["totalLOC"];
                    txtNamePrefix.Text = properties["classNamePrefix"];
                    txtMaxMethod.Text = properties["maxNoOfMethodsPerClass"];
                    txtMinParam.Text = properties["minNoOfParametersPerMethod"];
                    txtIntMaxValue.Text = properties["intMaxValue"];

                    validateAllowedTypes(typeList);

                    MessageBox.Show(fileName + " is loaded to the GUI successfully ! ");
                }
                else
                {
                    MessageBox.Show("Invalid File Path ! ");
                }
            }
        }

        public void validateAllowedTypes(HashSet<string> typeList)
        {
            foreach (String type in typeList)
            {
                if (!type.Equals("int"))
                {
                    MessageBox.Show(type + " datatype is not allowed in this version..! ");
                }
            }
        }

        public Boolean isEmpty(String formValue)
        {
            if (formValue.Trim().Length == 0)
                return true;
            return false;
        }

        public Boolean isNumber(String formValue)
        {
            return formValue.All(Char.IsDigit);
        }

        public Boolean isvalidateClassName(String input)
        {
            String[] keywords = new[]{"abstract","as","base","bool","break","byte","case","catch","char",
                "checked","class","const","continue","decimal","default","delegate","do","double","else","enum","event",
                "explicit","extern","false","finally","fixed","float","for","foreach","goto","if","implicit","in","in (generic modifier)",
                "int","interface","internal","is","lock","long","namespace","new","null","object","operator","out","out (generic modifier)",
                "override","params","private","protected","public","readonly","ref","return","sbyte","sealed","short","sizeof","stackalloc",
                "static","string","struct","switch","this","throw","true","try","typeof","uint","ulong","unchecked","unsafe","ushort","using",
                "virtual","void","volatile","while"
                };

            HashSet<String> cSharpKeywords = new HashSet<String>();
            Regex regex = new Regex(@"([a-zA-Z_$][a-zA-Z\d_$]*\.)*[a-zA-Z_$][a-zA-Z\d_$]*");

            foreach (String s in keywords)
            {
                cSharpKeywords.Add(s);
            }

            foreach (String part in input.Split('.'))
            {
                Match match = regex.Match(part);
                if (cSharpKeywords.Contains(part) || !match.Success)
                {
                    return false;
                }
            }
            return keywords.Length > 0;



        }



        public Boolean validateForm(Dictionary<string, string> returned_Dictionary)
        {
            foreach (String eachDictionaryKey in returned_Dictionary.Keys)
            {
                if (returned_Dictionary.ContainsKey(eachDictionaryKey))
                {
                    String currentDictionaryValue = returned_Dictionary[eachDictionaryKey];
                    Boolean test = isNumber(currentDictionaryValue);

                    if (isEmpty(currentDictionaryValue))
                    {
                        MessageBox.Show(eachDictionaryKey + " is Empty !", "Important Message");
                        return false;
                    }
                    else if (eachDictionaryKey.Equals("NamePrefix"))
                    {
                        Boolean isValidClassName = isvalidateClassName(currentDictionaryValue);
                        if (!isValidClassName)
                        {
                            MessageBox.Show("Class Name is Invalid. Follow c# class Name convention !", "Important Message");
                            return false;
                        }

                    }
                    else
                    {
                        if (!isNumber(currentDictionaryValue))
                        {
                            MessageBox.Show(eachDictionaryKey + " is Invalid !", "Important Message");
                            return false;
                        }
                    }
                }

            }
            /* Check LOC < #Method/perClass*/
            if (int.Parse(returned_Dictionary["TotalLOC"]) < int.Parse(returned_Dictionary["MaxMethod"]))
            {
                MessageBox.Show("Maximum #methods cant be > TotalLOC", "Important Message");
                return false;

            }
            return true;
        }

    }
}
