using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;


namespace TestAlgorithm
{

    [TestClass]
    public class TestDjkstra
    {
        [TestMethod]
        public void test()
        {
            int INF = 1000;
            int[,] map = new int[5, 5]
            {
                {INF,2,5,INF,INF},
                {INF,INF,2,6,INF},
                {INF,INF,INF,7,1},
                {INF,INF,2,INF,4},
                {INF,INF,INF,INF,INF}
            };

            Djkstra dj = new Djkstra();
            dj.LoadMap(map, 0);
            var path = dj.FindPath(3);
            string ret = string.Join(',', path);
            Assert.IsTrue(ret == "0,1,3");

            path = dj.FindPath(4);
            ret = string.Join(',', path);
            Assert.IsTrue(ret == "0,1,2,4");

        }
    }
}
