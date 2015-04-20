using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ConfigurationXMLParser = edu.uta.cse.proggen.configurationParser.ConfigurationXMLParser;

namespace Rugrat_withGUI.NUnit_TestCases
{
    [TestFixture, Description("TestFixture for ConfigurationXMLParser.cs")]
    public class ConfigXmlTestCase
    {

        public Dictionary<string, string> properties, incorrectProperty;
        public HashSet<string> typeList, expectedTypeList, incorrectTypeList;
        public String property;
        public int propertyValue;

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [TestFixtureSetUp, Description("Exclusive SetUp for TestFixture of ConfigurationXMLParser.cs")]
        public void TestFixtureSetup()
        {
            incorrectProperty = new Dictionary<string, string>();
            incorrectProperty["noOfClasses"] = "55";
            incorrectProperty["maxAllowedMethodCalls"] = "15";

            expectedTypeList = new HashSet<string>();
            expectedTypeList.Add("int");

            incorrectTypeList = new HashSet<string>();
            incorrectTypeList.Add("int");
            incorrectTypeList.Add("char");
            incorrectTypeList.Add("double");

        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [TestFixtureTearDown, Description("Exclusive TearDown for TestFixture of ConfigurationXMLParser.cs")]
        public void TestFixtureTearDown()
        {
            typeList = null;
            expectedTypeList = null;
            incorrectTypeList = null;
            properties = null;
            incorrectProperty = null;
            property = null;
            propertyValue = 0;
        }

        [Test]
        public void PropertiesTest()
        {
            properties = ConfigurationXMLParser.Properties;

            Assert.IsNotNull(properties);

            Assert.AreNotEqual(incorrectProperty, properties);
        }

        [Test]
        public void GetPropertyTest()
        {
            property = ConfigurationXMLParser.getProperty("classNamePrefix");
            Assert.IsNotNull(property);
            Assert.AreEqual(property, "TP");

        }

        [Test]
        public void GetPropertyAsIntTest()
        {
            propertyValue = ConfigurationXMLParser.getPropertyAsInt("noOfClasses");
            Assert.IsNotNull(propertyValue);
            Assert.AreEqual(propertyValue, 3);

            propertyValue = ConfigurationXMLParser.getPropertyAsInt("maxAllowedMethodCalls");
            Assert.IsNotNull(propertyValue);
            Assert.AreEqual(propertyValue, 2);

            propertyValue = ConfigurationXMLParser.getPropertyAsInt("intMaxValue");
            Assert.IsNotNull(propertyValue);
            Assert.AreEqual(propertyValue, 50);

            propertyValue = ConfigurationXMLParser.getPropertyAsInt("totalLOC");
            Assert.IsNotNull(propertyValue);
            Assert.AreEqual(propertyValue, 100);

        }

        [Test]
        public void TypeListTest()
        {
            typeList = ConfigurationXMLParser.TypeList;

            Assert.IsNotNull(typeList);

            Assert.AreEqual(expectedTypeList, typeList);

            Assert.AreNotEqual(incorrectTypeList, typeList);
        }

    }
}
