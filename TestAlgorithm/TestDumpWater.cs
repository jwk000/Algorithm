using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestDumpWater
    {
        [TestMethod]
        public void Test()
        {
            DumpWater dw = new DumpWater();
            var ans = dw.Dump();

            Assert.IsNotNull(ans);

            foreach(string s in ans)
            {
                Console.Write(s);
            }
            Console.WriteLine();


        }
    }
}
