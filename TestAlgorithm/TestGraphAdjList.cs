using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestGraphAdjList
    {
        [TestMethod]
        public void TestToplogic()
        {
            GraphAdjList G = new GraphAdjList(10);
            G.AddEdge(6, 7, 1);
            G.AddEdge(6, 3, 1);
            G.AddEdge(3, 4, 1);
            G.AddEdge(7, 8, 1);
            G.AddEdge(7, 4, 1);
            G.AddEdge(8, 9, 1);
            G.AddEdge(8, 4, 1);
            G.AddEdge(4, 5, 1);
            G.AddEdge(4, 9, 1);
            G.AddEdge(5, 1, 1);
            G.AddEdge(5, 2, 1);
            G.AddEdge(9, 0, 1);
            G.AddEdge(0, 2, 1);

            var ret = G.TopologicalSort();
            Console.WriteLine(string.Join("->", ret)); //6->7->8->3->4->5->9->0->1->2
            Assert.IsTrue(G.CheckTopological(ret));

        }
    }
}
