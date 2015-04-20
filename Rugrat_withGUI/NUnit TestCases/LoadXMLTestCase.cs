using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LoadXMLParser = edu.uta.cse.proggen.configurationParser.LoadXMLParser;
using ConfigurationXMLParser = edu.uta.cse.proggen.configurationParser.ConfigurationXMLParser;

namespace Rugrat_withGUI.NUnit_TestCases
{
    [TestFixture, Description("TestFixture for LoadXMLParsor.cs")]
    public class LoadXmlTestCase
    {

        public LoadXMLParser lxp;
        public Dictionary<string, string> properties, expectedProperties;
        public HashSet<string> typeList, expectedTypeList;
        public String filePath;

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [TestFixtureSetUp, Description("Exclusive SetUp for TestFixture of LoadXMLParsor.cs")]
        public void TestFixtureSetup()
        {
            lxp = new LoadXMLParser();
            filePath = "C:\\cse 6324\\rugrat\\config.xml";

        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [TestFixtureTearDown, Description("Exclusive TearDown for TestFixture of LoadXMLParsor.cs")]
        public void TestFixtureTearDown()
        {
            lxp = null;
            filePath = null;
            typeList = null;
            expectedTypeList = null;
            properties = null;
            expectedProperties = null;
        }

        [Test]
        public void LoadXMLTest()
        {
            properties = lxp.loadXMLFile(filePath);
            expectedProperties = ConfigurationXMLParser.Properties;

            Assert.IsNotNull(properties);

            Assert.AreEqual(expectedProperties, properties);
        }

        [Test]
        public void TypeListTest()
        {
            typeList = lxp.TypeList;
            expectedTypeList = ConfigurationXMLParser.TypeList;

            Assert.IsNotNull(typeList);

            Assert.AreEqual(expectedTypeList, typeList);
        }

    }
}
