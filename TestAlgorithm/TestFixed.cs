using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestFixed
    {
        [TestMethod]
        public void TestAdd()
        {
            Fixed64 a = new Fixed64(123.45);
            Fixed64 b = new Fixed64(678.90);
            double c = (double)(a + b);
            double d = 123.45 + 678.90;
            Console.WriteLine("123.45 + 678.90={0} real={1}", c,d);
            //Assert.IsTrue(c == d);
        }
        [TestMethod]
        public void TestMinus()
        {
            Fixed64 a = new Fixed64(123.45);
            Fixed64 b = new Fixed64(678.90);
            double c = (double)(a - b);
            double d = 123.45 - 678.90;
            Console.WriteLine("123.45 - 678.90={0} real={1}", c, d);
            //Assert.IsTrue(c == d);
        }
        [TestMethod]
        public void TestMul()
        {
            Fixed64 a = new Fixed64(123.45);
            Fixed64 b = new Fixed64(678.90);
            double c = (double)(a * b);
            double d = 123.45 * 678.90;
            Console.WriteLine("123.45*678.90={0} real={1}", c, d);
            //Assert.IsTrue(c == d);
        }
        [TestMethod]
        public void TestDiv()
        {
            Fixed64 a = new Fixed64(123.45);
            Fixed64 b = new Fixed64(678.90);
            double c = (double)(a / b);
            double d = 123.45 / 678.90;
            Console.WriteLine("123.45/678.90={0} real={1}", c, d);
            //Assert.IsTrue(c == d);
            
        }

    }
}
