using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConsoleApplication4.NUnit_TestCases
{
    using Primitives = edu.uta.cse.proggen.classLevelElements.Type.Primitives;
    using Type = edu.uta.cse.proggen.classLevelElements.Type;

    [TestFixture, Description("TestFixture for Type.cs")]
    public class TypeTestCase
    {
        [Test]
        public void Type_reverseLookup()
        {
            Primitives p1 = Type.reverseLookup("char");
            Assert.AreEqual(p1, Primitives.CHAR);

            Primitives p2 = Type.reverseLookup("byte");
            Assert.AreEqual(p2, Primitives.BYTE);

            Primitives p3 = Type.reverseLookup("short");
            Assert.AreEqual(p3, Primitives.SHORT);

            Primitives p4 = Type.reverseLookup("int");
            Assert.AreEqual(p4, Primitives.INT);

            Primitives p5 = Type.reverseLookup("long");
            Assert.AreEqual(p5, Primitives.LONG);

            Primitives p6 = Type.reverseLookup("float");
            Assert.AreEqual(p6, Primitives.FLOAT);

            Primitives p7 = Type.reverseLookup("double");
            Assert.AreEqual(p7, Primitives.DOUBLE);

            Primitives p8 = Type.reverseLookup("String");
            Assert.AreEqual(p8, Primitives.STRING);

            Primitives p9 = Type.reverseLookup("boolean");
            Assert.AreEqual(p9, Primitives.OBJECT);

            Primitives p10 = Type.reverseLookup("object");
            Assert.AreEqual(p10, Primitives.OBJECT);

            Primitives p11 = Type.reverseLookup("dummy");
            Assert.AreEqual(p11, Primitives.OBJECT);

        }

        [Test]
        public void Type_getType()
        {
            Primitives primitive = Type.reverseLookup("int");
            Type type = new Type(primitive, "Integer");

            Primitives result = type.getType();
            Assert.AreEqual(primitive, result);
        }

        [Test]
        public void Type_Equal()
        {
            Primitives primitive1 = Type.reverseLookup("float");
            Type type1 = new Type(primitive1, "Float");

            // This one passes null object
            bool result1 = type1.Equals(null);
            Assert.IsFalse(result1);

            // This one returns false eventhough both Primitives are same, since the passes object is not Type instance (it is primitive)
            bool result2 = type1.Equals(primitive1);
            Assert.IsFalse(result2);

            Primitives primitive2 = Type.reverseLookup("Object");
            Type type2 = new Type(primitive2, "Object");

            bool result3 = type1.Equals(type2);
            Assert.IsFalse(result3);

            Type typeFloat = new Type(Primitives.FLOAT, "Float");

            bool result4 = type1.Equals(typeFloat);
            Assert.IsTrue(result4);

            // Result5 and Result6 will test the Name isntead of primitive since both are Objects
            Primitives primitive3 = Type.reverseLookup("bool");
            Type type3 = new Type(primitive2, "Boolean");

            bool result5 = type2.Equals(type3);
            Assert.IsFalse(result5);

            Type typeObject = new Type(Primitives.OBJECT, "Object");

            bool result6 = type2.Equals(typeObject);
            Assert.IsTrue(result6);


        }

        [Test]
        public void Type_Prim_ToString()
        {
            //Test Data 1
            Primitives primitive1 = Type.reverseLookup("float");
            Type type1 = new Type(primitive1, "Float");

            String result1 = type1.getType().ToString();
            Assert.AreEqual(result1.ToLower(), "float");

            //Test Data 2
            Primitives primitive2 = Type.reverseLookup("int");
            Type type2 = new Type(primitive2, "Integer");

            String result2 = type2.getType().ToString();
            Assert.AreEqual(result2.ToLower(), "int");

            //Test Data 3
            Primitives primitive3 = Type.reverseLookup("byte");
            Type type3 = new Type(primitive3, "Byte");

            String result3 = type3.getType().ToString();
            Assert.AreEqual(result3.ToLower(), "byte");

            //Test Data 4
            Primitives primitive4 = Type.reverseLookup("long");
            Type type4 = new Type(primitive4, "Long");

            String result4 = type4.getType().ToString();
            Assert.AreEqual(result4.ToLower(), "long");

            //Test Data 5
            Primitives primitive5 = Type.reverseLookup("double");
            Type type5 = new Type(primitive5, "Double");

            String result5 = type5.getType().ToString();
            Assert.AreEqual(result5.ToLower(), "double");

            //Test Data 6
            Primitives primitive6 = Type.reverseLookup("char");
            Type type6 = new Type(primitive6, "Char");

            String result6 = type6.getType().ToString();
            Assert.AreEqual(result6.ToLower(), "char");

            //Test Data 7
            Primitives primitive7 = Type.reverseLookup("short");
            Type type7 = new Type(primitive7, "Short");

            String result7 = type7.getType().ToString();
            Assert.AreEqual(result7.ToLower(), "short");

            //Test Data 8
            Primitives primitive8 = Type.reverseLookup("String");
            Type type8 = new Type(primitive8, "String");

            String result8 = type8.getType().ToString();
            Assert.AreEqual(result8.ToLower(), "string");

            //Test Data 9
            Primitives primitive9 = Type.reverseLookup("Object");
            Type type9 = new Type(primitive9, "Object");

            String result9 = type9.getType().ToString();
            Assert.AreEqual(result9.ToLower(), "object");

            //Test Data 10
            Type type10 = new Type(Primitives.OTHER, "Other");

            String result10 = type10.getType().ToString();
            Assert.AreEqual(result10.ToLower(), "other");

        }
    }
}
