﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.uta.cse.proggen.operators;
using NUnit.Framework;

namespace ConsoleApplication4.NUnit_TestCases
{
    using Primitives = edu.uta.cse.proggen.classLevelElements.Type.Primitives;
    using BinaryOperator = edu.uta.cse.proggen.operators.BinaryOperator;

    [TestFixture]
    public class BinaryOperatorTestCase
    {

        [Test]
        public void Binaryoperator()
        {

            const Primitives p1 = Primitives.STRING;
            const Primitives p2 = Primitives.SHORT;

            BinaryOperator b1 = new BinaryOperator(p1);
            BinaryOperator b2 = new BinaryOperator(p2);

            Assert.AreEqual("+", b1.ToString());
            Assert.NotNull(b1.ToString());
            Assert.NotNull(b2.ToString());

        }
    }
}