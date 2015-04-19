﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.uta.cse.proggen.nodes;
using NUnit.Framework;

namespace ConsoleApplication4.NUnit_TestCases
{
    using Operand = edu.uta.cse.proggen.nodes.Operand;

    [TestFixture]
    public class OperandTestCase
    {

        [Test]
        public void OperandTest()
        {

            Operand o = new Operand();
            const string literal = "";
            Assert.AreEqual(literal, o.ToString());
        }
    }
}