using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestHuffmanTree
    {

        [TestMethod]
        public void Test()
        {
            HuffmanTree htree = new HuffmanTree();
        
            byte[] buff = htree.Encode("aaa bb cccc dd e", out int bitlen);
            Assert.IsTrue(buff.Length == 6);
            Assert.IsTrue(bitlen == 40);

            string output = htree.Decode(buff, bitlen);
            Assert.IsTrue(output == "aaa bb cccc dd e");
        }
    }
}
