using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestFordFulkerson
    {
        [TestMethod]
        public void Test()
        {
            int[,] g = new int[6, 6]
            {
                {0,16,0,13,0,0},
                {0,0,12,0,0,0},
                {0,0,0,9,0,20},
                {0,4,0,0,14,0},
                {0,0,7,0,0,4},
                {0,0,0,0,0,0}
            };

            FordFulkerson ff = new FordFulkerson();
            ff.BuildNetwork(g);
            int v = ff.CalcMaxFlow(0, 5);
            Console.WriteLine(ff);
            Assert.IsTrue(v == 23);
        }
    }
}
