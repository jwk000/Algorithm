using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestGraphMatrix
    {
        [TestMethod]
        public void TestMiniSpanTree()
        {
            GraphMatrix G = new GraphMatrix(10);
            G.AddEdge(0, 1, 9);
            G.AddEdge(0, 2, 7);
            G.AddEdge(0, 3, 3);
            G.AddEdge(6, 0, 9);
            G.AddEdge(6, 9, 5);
            G.AddEdge(6, 5, 7);
            G.AddEdge(2, 1, 2);
            G.AddEdge(2, 9, 6);
            G.AddEdge(3, 1, 1);
            G.AddEdge(9, 7, 2);
            G.AddEdge(3, 4, 4);
            G.AddEdge(4, 5, 8);
            G.AddEdge(5, 7, 6);
            G.AddEdge(4, 8, 3);
            G.AddEdge(7, 8, 5);

            var t1 = G.MiniSpanTreeKruskal();
            var t2 = G.MiniSpanTreePrim();

            Console.WriteLine("Kruskal tree:");
            Console.WriteLine(t1);

            Console.WriteLine("Prim tree:");
            Console.WriteLine(t2);


            Assert.IsTrue(t1.Sum()==t2.Sum());
        }

        
    }
}
