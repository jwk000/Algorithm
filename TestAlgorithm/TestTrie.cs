using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestTrie
    {
        [TestMethod]
        public void Test()
        {
            Trie trie = new Trie();
            string text = "asdf this is a mao mao yu and that is not an orange goodbye deer girl";
            foreach(string s in text.Split(' '))
            {
                trie.Add(s);
            }

            Assert.IsTrue(trie.HasString("this"));
            Assert.IsTrue(trie.HasString("girl"));
            Assert.IsFalse(trie.HasString("ma"));
        }
    }
}
