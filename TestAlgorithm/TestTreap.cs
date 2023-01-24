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
        public void TestTreap1()
        {
            Treap treap = new Treap();
            int[] pr = new[] { 72, 118, 934, 663, 374, 323, 941, 861, 7 };
            for(int i = 1; i < 10; i++)
            {
                treap.Add(i, i * 100,pr[i-1]);
            }

            int v = treap.Search(8);
            Assert.IsTrue(v == 800);

            for(int i = 0; i < 9; i++)
            {
                treap.Remove(i);
                int x = treap.Search(i);
                Assert.IsTrue(x == -1);
            }

            int w = treap.Search(9);
            Assert.IsTrue(w == 900);

        }

        [TestMethod]
        public void TestTreap2()
        {
            Treap treap = new Treap();
            for (int i = 1; i < 10; i++)
            {
                treap.Add(i, i * 100);
            }

            int v = treap.Search(8);
            Assert.IsTrue(v == 800);

            for (int i = 0; i < 9; i++)
            {
                treap.Remove(i);
                int x = treap.Search(i);
                Assert.IsTrue(x == -1);
            }

            int w = treap.Search(9);
            Assert.IsTrue(w == 900);

        }

    }
}
