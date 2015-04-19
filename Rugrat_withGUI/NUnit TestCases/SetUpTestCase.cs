using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.uta.cse.proggen.configurationParser;
using NUnit.Framework;
using Type = edu.uta.cse.proggen.classLevelElements.Type;
using ClassGenerator = edu.uta.cse.proggen.namespaceLevelElements.ClassGenerator;
using Start = edu.uta.cse.proggen.start.Start.Start;
using Variable = edu.uta.cse.proggen.classLevelElements.Variable;

namespace ConsoleApplication4.NUnit_TestCases
{

    [SetUpFixture, Description("SetUp for all the Test Cases of Rugrat")]
    public class SetUpTestCase
    {
        public static List<ClassGenerator> ClassGenList;

        public static List<Variable> ParamList = new List<Variable>();

        public static ClassGenerator MyClassGen;

        [SetUp]
        public void RunBeforeAllTests()
        {
            Start.Main1(new[] { "C:\\cse 6324\\rugrat\\", "NUnit" });

            ClassGenList = Start.classGenList;

            MyClassGen = new ClassGenerator("NU1", 33, null);

            for (int i = 0; i < 5; i++)
            {
                ParamList.Add(Variable.generateVariable("var" + i, ClassGenList));
            }
        }

        [TearDown]
        public void RunAfterAllTests()
        {
            ClassGenList = null;
            MyClassGen = null;
            ParamList = null;
        }

    }
}
