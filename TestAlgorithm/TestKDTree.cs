using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestKDTree
    {
        [TestMethod]
        public void Test()
        {
            List<Vector2> input = new List<Vector2>();
            input.Add(new Vector2(3.5f, 5f));
            input.Add(new Vector2(1.5f, 6.5f));
            input.Add(new Vector2(2f, 4f));
            input.Add(new Vector2(5f, 3f));
            input.Add(new Vector2(3f, 2f));
            input.Add(new Vector2(6f, 1f));
            input.Add(new Vector2(1f, 2.2f));
            input.Add(new Vector2(7f, 4.5f));

            KDTree kdtree = new KDTree();
            kdtree.Build(input);

            var v = kdtree.Search(new Vector2(4, 3.5f));
            Assert.IsTrue(v == new Vector2(5, 3));


        }
    }
}
