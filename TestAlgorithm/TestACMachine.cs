using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestACMachine
    {
        [TestMethod]
        public void Test()
        {
            ACMachine acm = new ACMachine();

            acm.Init(new List<string> { "his", "he", "her", "she" });

            var ans = acm.Match("ahisher");
            Assert.IsTrue(ans.Contains("his"));
            Assert.IsTrue(ans.Contains("he"));
            Assert.IsTrue(ans.Contains("her"));
            Assert.IsTrue(ans.Contains("she"));
        }
    }
}
