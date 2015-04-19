﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.uta.cse.proggen.nodes;
using NUnit.Framework;

namespace ConsoleApplication4.NUnit_TestCases
{
    using Operator = edu.uta.cse.proggen.nodes.Operator;

    [TestFixture]
    public class OperatorTestCase
    {

        [Test]
        public void OperatorTest()
        {
            Operator op = new Operator();

            Assert.AreEqual("Not extended", op.ToString());
        }
    }
}