using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestBM
    {
        [TestMethod]
        public void test()
        {
            BM bm = new BM("abcabcabc");
            int n = bm.Search("abcabcababcabcabcbcabcabcabc");
            Assert.IsTrue(n == 8);
            n = bm.Search("a");
            Assert.IsTrue(n == -1);

            n = bm.Search("asdfabcabcababcccccc");
            Assert.IsTrue(n == -1);
        }
    }
}
