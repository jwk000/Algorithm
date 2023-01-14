using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestDisjontedSet
    {
        [TestMethod]
        public void Test()
        {
            DisjointedSet dj = new DisjointedSet(10);
            dj.Union(1, 3);
            dj.Union(1, 2);
            dj.Union(1, 4);
            dj.Union(4, 5);
            dj.Union(4, 6);
            dj.Union(7, 8);
            dj.Union(9, 8);
            dj.Union(9, 0);

            Assert.IsFalse(dj.Query(0, 1));
            Assert.IsTrue(dj.Query(3, 5));
        }
    }
}
