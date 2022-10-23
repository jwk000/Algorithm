using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestPoker101
    {

        [TestMethod]
        public void Test1()
        {
            Poker101 poker = new Poker101();
            var solution = poker.BestHandCard(new List<int> { 103,303,403,204,304,404,307,308,309,408,409,410,110,210,310,311,411,211,204,101,102});
            Assert.IsTrue(solution.value == 135);
        }
    }
}
