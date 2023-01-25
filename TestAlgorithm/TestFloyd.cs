using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestFloyd
    {
        [TestMethod]
        public void Test1()
        {
            //0-1只有唯一路径可达
            int[,] map = new int[5, 5]
            {
                { 0,-1,1,-1,-1 },
                {-1,0,-1,1,-1 },
                { 1,-1,0,-1,1 },
                { -1,1,-1,0,1 },
                {-1,-1,1,1,0 } };

            Floyd floyd = new Floyd(map,5);

            var path = floyd.GetPath(0, 1);
            Assert.IsTrue(string.Join(',', path) == "0,2,4,3,1");
        }

        [TestMethod]
        public void Test2()
        {
            //0-1有多条路径可达
            int[,] map = new int[5, 5]
            {
                { 0,-1,1,3,-1 },
                {-1,0,8,-1,2 },
                { 1,8,0,1,-1 },
                { 3,-1,1,0,2 },
                {-1,2,-1,2,0 } };

            Floyd floyd = new Floyd(map, 5);

            var path = floyd.GetPath(0, 1);
            Assert.IsTrue(string.Join(',', path) == "0,2,3,4,1");

        }
    }
}
