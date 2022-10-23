using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestMajiang
    {
        [TestMethod]
        public void Test()
        {
            Majiang majiang = new Majiang();
            bool ret = majiang.CheckHu(new List<string> { "1T", "2W", "5S", "Fa", "6W", "1T", "1T", "2W", "5S", "5S", "Fa", "Fa", "6W", "6W" });
            Assert.IsTrue(ret);

            ret = majiang.CheckHu(new List<string> { "1T", "2W", "5S", "Fa", "6W", "1T", "1T", "2W", "5S", "5S", "Fa", "Fa", "Fa", "6W" });
            Assert.IsFalse(ret);
        }
    }
}
