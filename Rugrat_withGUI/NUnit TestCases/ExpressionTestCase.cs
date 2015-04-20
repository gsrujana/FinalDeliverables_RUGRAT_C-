﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.uta.cse.proggen.nodes;
using NUnit.Framework;

namespace Rugrat_withGUI.NUnit_TestCases
{
    using Expression = edu.uta.cse.proggen.nodes.Expression;
    [TestFixture]
    public class ExpressionTestCase
    {

        [Test]
        public void ExpressionTest()
        {

            Expression ex = new Expression();

            Assert.AreEqual(null, ex.ToString());
        }
    }

}