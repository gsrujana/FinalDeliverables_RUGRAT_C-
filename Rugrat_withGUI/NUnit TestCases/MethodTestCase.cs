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
using Method = edu.uta.cse.proggen.classLevelElements.Method;
using SetUp = Rugrat_withGUI.NUnit_TestCases.SetUpTestCase;

namespace Rugrat_withGUI.NUnit_TestCases
{
    [TestFixture, Description("TestFixture for Method.cs")]
    public class MethodTestCase
    {
        public MethodSignature MethodSig1, MethodSig2;
        public Method Method1, Method2, Method3;

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [TestFixtureSetUp, Description("Exclusive SetUp for TestFixture of Method.cs")]
        public void TestFixtureSetup()
        {
            MethodSig1 = new MethodSignature(true, Primitives.INT, "MethSig1", SetUp.ParamList);
            MethodSig2 = new MethodSignature(false, Primitives.FLOAT, "MethSig2", SetUp.ParamList);

        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [TestFixtureTearDown, Description("Exclusive TearDown for TestFixture of MethodSignature.cs")]
        public void TestFixtureTearDown()
        {
            MethodSig1 = null;
            MethodSig2 = null;
            Method1 = null;
            Method2 = null;
        }

        [Test]
        public void M_GetMethod()
        {
            Method1 = Method.getMethod(MethodSig1, SetUp.MyClassGen, SetUp.ClassGenList, 50, 4, 5);

            Assert.IsNotNull(Method1);
            Assert.AreEqual(Method1.ClassList, SetUp.ClassGenList);
        }

        [Test]
        public void M_GenerateMethod()
        {
            Method2 = Method.generateMethod(SetUp.MyClassGen, 5, SetUp.ClassGenList, "Method2", 50, 4, 5, true);

            Assert.IsNotNull(Method2);
            Assert.AreEqual(Method2.ClassList, SetUp.ClassGenList);
        }

        [Test]
        public void M_ReturnType()
        {
            Primitives expected1 = Method1.ReturnType;

            Assert.IsNotNull(expected1);
            Assert.AreEqual(expected1, Primitives.INT);
        }

        [Test]
        public void M_ToString()
        {
            String expected2 = Method1.ToString();

            /*String actual2 = "public virtual static int MethSig1(int var0, int var1, int var2, int var3, int var4, int recursioncounter1){\n if(! (recursionCounter > 0 && recursionCounter < 1) )" +
                             "{return (int)recursionCounter1;} else{recursionCounter--; } if(((recursionCounter1+(int)(var2))<=(var1-(int)(var2)))){var2 = (int)((var1-(int)(var2))-(var1-(int)(var2)));}else{var2 = (int)((var1-(int)(var2))-(var1-(int)(var2)));}" +
                              "switch((var1-(int)(10))){" +
                               " case 0:var2 = (int)((var1-(int)(var2))-(var1-(int)(var2)));break;" +
                                "case 1:var2 = (int)((var1-(int)(var2))-(var1-(int)(var2)));break;" +
                                "default :var2 = (int)((var1-(int)(var2))-(var1-(int)(var2)));}" +
                                "if(((var1-(int)(var2))==(var3-(int)(var4)))){var3 = (int)((var3-(int)(var4))-(var3-(int)(var4)));}" +
                                "else{var3 = (int)((var3-(int)(var4))-(var3-(int)(var4)));}" +
                                "if(((var3-(int)(var4))>(var2+(int)(var3)))){Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 29\");}" +
                                "else{Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 32\");}"+
                                "if( ((var2+(int)(var3))>=(var4+(int)(var5)))){Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 37\");}"+
                                "switch((var4+(int)(var5))){"+
                                "case 0:Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 43\");break;"+
                                "case 1:Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 49\");break;"+
                                "case 2:Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 55\");break;"+
                                "case 3:Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 61\");break;"+
                                "case 4:Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 67\");break;"+
                                "case 5:Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 73\");break;"+
                                "case 6:Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 79\");break;"+
                                "case 7:Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 85\");break;"+
                                "case 8:Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 91\");break;"+
                                "default :Console.WriteLine(\"NU1 - MethSig1- LineInMethod: 97\");}" + "return (int)var4;}";*/

            Assert.IsNotNull(expected2);

        }
    }
}
