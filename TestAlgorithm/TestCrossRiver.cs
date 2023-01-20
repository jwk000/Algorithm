using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestCrossRiver
    {
        [TestMethod]
        public void test()
        {
            CrossRiver s = new CrossRiver();
            var path = s.CrossingTheRiver();
            Assert.IsNotNull(path);
            Console.WriteLine(string.Join(',', path));

        }
    }
}
