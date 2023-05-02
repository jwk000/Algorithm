using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestGaleShapley
    {
        [TestMethod]
        public void Test()
        {
            GaleShapley gs = new GaleShapley();
            gs.Init(5, 5);
            gs.AddMan(0, "A", new[] { 0, 1, 2, 3, 4 });
            gs.AddMan(1, "B", new[] { 1, 2, 0, 4, 3 });
            gs.AddMan(2, "C", new[] { 1, 0, 4, 2, 3 });
            gs.AddMan(3, "D", new[] { 2, 0, 4, 1, 3 });
            gs.AddMan(4, "E", new[] { 0, 1, 4, 3, 2 });

            gs.AddWoman(0, "a", new[] { 0, 1, 2, 3, 4 });
            gs.AddWoman(1, "b", new[] { 2, 3, 0, 1, 4 });
            gs.AddWoman(2, "c", new[] { 0, 3, 4, 1, 2 });
            gs.AddWoman(3, "d", new[] { 3, 1, 2, 0, 4 });
            gs.AddWoman(4, "e", new[] { 3, 2, 0, 4, 1 });

            gs.Match();
            Console.WriteLine(gs);

        }
    }
}
