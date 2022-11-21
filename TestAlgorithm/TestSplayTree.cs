using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestSplayTree
    {

        [TestMethod]
        public void Test()
        {
            SplayTree sptree = new SplayTree();
            for(int i = 1; i < 20; i++)
            {
                sptree.Add(i, i*100);
            }

            for(int i = 1; i < 20; i++)
            {
                int v = sptree.Search(i);
                Assert.IsTrue(v == i * 100);
            }

            for(int i = 1; i < 10; i++)
            {
                sptree.Remove(i);
            }

            int w = sptree.Search(10);
            Assert.IsTrue(w == 1000);


        }
    }
}
