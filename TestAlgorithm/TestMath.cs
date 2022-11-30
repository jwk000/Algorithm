using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestMath
    {
        [TestMethod]
        public void Test()
        {
            int v = MyMath.gcd(6, 12);
            Assert.IsTrue(v == 6);

            v = MyMath.gcd(5, 7);
            Assert.IsTrue(v == 1);

            v = MyMath.gcd(0, 100);
            Assert.IsTrue(v == 100);

            v = MyMath.lcm(5, 7);
            Assert.IsTrue(v == 35);

            v = MyMath.lcm(6, 12);
            Assert.IsTrue(v == 12);

            float x = MyMath.sqrt(4);
            Assert.IsTrue(x == 2);

            x = MyMath.sqrt(9);
            Assert.IsTrue(x == 3);

            x = MyMath.sqrt(121);
            Assert.IsTrue(x == 11);

            x = MyMath.sqrt(1.21f);
            Assert.IsTrue(x == 1.1f);
        }

        [TestMethod]
        public void TestCRT()
        {
            //有物不知其数，三三数之剩二，五五数之剩三，七七数之剩二。问物几何？
            int[] a = { 3, 5, 7 };
            int[] b = { 2, 3, 2 };
            int x = MyMath.crt(a, b);

            Assert.IsTrue(x == 23);
        }
    }
}
