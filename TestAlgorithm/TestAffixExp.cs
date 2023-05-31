using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestAffixExp
    {
        [TestMethod]
        public void Test1()
        {
            string exp = "1+(2+3)*4-5";
            string affix = AffixExp.Convert(exp);
            Console.WriteLine(affix);
            int ans = AffixExp.Calculate(affix);
            Console.WriteLine(ans);
            Assert.IsTrue(ans == 16);
        }

        [TestMethod]
        public void Test2()
        {
            string exp = "1+3-2+(2+3*4-7+1)*4-5+9/3";
            string affix = AffixExp.Convert(exp);
            Console.WriteLine(affix);
            int ans = AffixExp.Calculate(affix);
            Console.WriteLine(ans);
            Assert.IsTrue(ans == 32);
        }
    }
}
