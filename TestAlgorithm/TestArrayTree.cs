using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestArrayTree
    {
        [TestMethod]
        public void Test()
        {
            ArrayTree tree = new ArrayTree(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            int sum5 = tree.SumN(5);
            Assert.IsTrue(sum5 == 15);

            tree.Change(4, 5);//5->10

            int sum = tree.SumRange(3, 5);
            Assert.IsTrue(sum == 20);

        }
    }
}
