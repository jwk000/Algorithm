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
        public void Test1()
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

        [TestMethod]
        public void Test2()
        {
            Trie trie = new Trie();
            trie.Add("his");
            trie.Add("he");
            trie.Add("her");
            trie.Add("she");

            Assert.IsTrue(trie.Match("ahisher"));

        }

    }
}
