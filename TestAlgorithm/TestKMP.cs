using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestKMP
    {
        [TestMethod]
        public void test()
        {
            KMP kmp = new KMP("aaabc");
            int r = kmp.Search("aaabbabcaaabccc");
            Assert.IsTrue(r == 8);
            r = kmp.Search("asdf");
            Assert.IsTrue(r == -1);
        }

        [TestMethod]
        public void test2()
        {
            KMP kmp = new KMP("abababcabcd");
            int r = kmp.Search("abcabcaaabcaabbccabababcabcd");
            Assert.IsTrue(r == 17);
            r = kmp.Search("ababababaababb");
            Assert.IsTrue(r == -1);
        }
    }
}
