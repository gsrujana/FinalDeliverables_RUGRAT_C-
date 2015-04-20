using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml;
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
            saveConfigToXML();

            MessageBox.Show("XML File is saved in the default location : \n" + Start.Start.PathToDir);
        }
        //This is Generate C# code button
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            String[] args = { };

            saveConfigToXML();

            Start.Start.Main1(args);
            //Application.Exit();

            MessageBox.Show("C# Files are created successfully based on the given parameters.! Check the path: \n " + Start.Start.PathToDir);
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

        public void saveConfigToXML()
        {
            string filename = (Start.Start.PathToDir + "config.xml");
            XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("RUGRAT");
            Dictionary<string, string> returned_Dictionary = retrieveFormFields();

           
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
        
    }
}
