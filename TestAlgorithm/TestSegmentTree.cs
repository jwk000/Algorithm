using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestSegmentTree
    {
        [TestMethod]
        public void Test()
        {
            SegmentTree tree = new SegmentTree(new int[] { 3, 7, 5, 4, 8, 2, 1, 9, 6, 0 });
            int min = tree.RangeMin(3, 7);
            Assert.IsTrue(min == 1);
            tree.Update(4, -1);
            min = tree.RangeMin(3, 8);
            Assert.IsTrue(min == -1);

        }
    }
}
