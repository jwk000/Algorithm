using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestEightQueen
    {
        [TestMethod]
        public void Test()
        {
            EightQueen eightQueen = new EightQueen();
            int ans = eightQueen.CalcEightQueen();
            //Console.WriteLine(ans);

            Assert.IsTrue(ans == 92);
        }
    }
}
