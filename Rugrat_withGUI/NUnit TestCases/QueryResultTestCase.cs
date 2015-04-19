using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.uta.cse.proggen.configurationParser;
using NUnit.Framework;
using Type = edu.uta.cse.proggen.classLevelElements.Type;

namespace ConsoleApplication4.NUnit_TestCases
{
    [TestFixture, Description("TestFixture for QueryResult.cs")]
    public class QueryResultTestCase
    {
        QueryResult qs1 = new QueryResult("XML", 1245, Type.Primitives.INT);
        QueryResult qs2 = new QueryResult("HTML", 1598, Type.Primitives.FLOAT);

        [Test]
        public void QS_getName()
        {
            Assert.AreEqual(qs1.Name, "XML");
            Assert.AreEqual(qs2.Name, "HTML");
        }

        [Test]
        public void QS_getseqNumber()
        {
            Assert.AreEqual(qs1.SeqNumber, 1245);
            Assert.AreEqual(qs2.SeqNumber, 1598);
        }

        [Test]
        public void QS_getType()
        {
            Assert.AreEqual(qs1.Type, Type.Primitives.INT);
            Assert.AreEqual(qs2.Type, Type.Primitives.FLOAT);
        }
    }
}
