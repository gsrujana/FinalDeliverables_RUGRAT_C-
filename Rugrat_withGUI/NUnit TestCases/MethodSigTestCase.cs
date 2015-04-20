using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.uta.cse.proggen.configurationParser;
using NUnit.Framework;
using Type = edu.uta.cse.proggen.classLevelElements.Type;
using ClassGenerator = edu.uta.cse.proggen.namespaceLevelElements.ClassGenerator;
using Variable = edu.uta.cse.proggen.classLevelElements.Variable;
using Primitives = edu.uta.cse.proggen.classLevelElements.Type.Primitives;
using MethodSignature = edu.uta.cse.proggen.classLevelElements.MethodSignature;
using SetUp = Rugrat_withGUI.NUnit_TestCases.SetUpTestCase;

namespace Rugrat_withGUI.NUnit_TestCases
{
    [TestFixture, Description("TestFixture for MethodSignature.cs")]
    public class MethodSigTestCase
    {

        public static List<Variable> ParamList2 = null;
        public MethodSignature MethodSig1, MethodSig2, MethodSig3, MethodSig4;

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [TestFixtureSetUp, Description("Exclusive SetUp for TestFixture of MethodSignature.cs")]
        public void TestFixtureSetup()
        {
            ParamList2 = SetUp.ParamList;
            ParamList2.Add(Variable.generateVariable("recursionCounter1", SetUp.ClassGenList));

            MethodSig1 = new MethodSignature(true, Primitives.INT, "MethSig1", ParamList2);
            MethodSig2 = new MethodSignature(false, Primitives.FLOAT, "MethSig2", SetUp.ParamList);
            MethodSig3 = new MethodSignature(true, Primitives.INT, "MethSig1", SetUp.ParamList);
            MethodSig4 = null;
        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [TestFixtureTearDown, Description("Exclusive TearDown for TestFixture of MethodSignature.cs")]
        public void TestFixtureTearDown()
        {
            ParamList2 = null;
            MethodSig1 = null;
            MethodSig2 = null;
            MethodSig3 = null;
        }

        [Test]
        public void MS_ToString()
        {
            String expected1 = MethodSig1.ToString();
            String actual1 =
                "public virtual static int MethSig1(int var0, int var1, int var2, int var3, int var4, int recursioncounter1)";

            Assert.IsNotNull(expected1);
            Assert.AreEqual(expected1, actual1);

            expected1 = MethodSig2.ToString();
            actual1 =
                "public virtual float MethSig2(int var0, int var1, int var2, int var3, int var4, int recursioncounter1)";

            Assert.IsNotNull(expected1);
            Assert.AreEqual(expected1, actual1);

        }

        [Test]
        public void MS_Equals()
        {
            bool expected2 = MethodSig1.Equals(MethodSig2);

            Assert.IsFalse(expected2);

            expected2 = MethodSig1.Equals(MethodSig4);

            Assert.IsFalse(expected2);

            expected2 = MethodSig1.Equals(ParamList2);

            Assert.IsFalse(expected2);

            expected2 = MethodSig1.Equals(MethodSig3);

            Assert.IsTrue(expected2);
        }
    }
}
