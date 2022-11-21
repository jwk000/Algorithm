using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestTreap
    {
        [TestMethod]
        public void Test()
        {
            Treap treap = new Treap();

            for(int i = 1; i < 20; i++)
            {
                treap.Add(i, i * 100);
            }

            int v = treap.Search(10);
            Assert.IsTrue(v == 1000);

            for(int i = 0; i < 10; i++)
            {
                treap.Remove(i);
            }

            int w = treap.Search(10);
            Assert.IsTrue(w == 1000);

        }
    }
}
