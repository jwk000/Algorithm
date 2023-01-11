using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestDiffArray
    {
        [TestMethod]
        public void TestDiffArray1()
        {
            DiffArray arr = new DiffArray(new int[] { 0, 2, 5, 4, 9, 7, 10, 0 });
            arr.ChangeValue(1, 4, 3);
            int v = arr.GetValue(2);
            Assert.IsTrue(v == 8);
            int[] o = arr.GetArray();
            Assert.IsTrue(string.Join(',', o) == "0,5,8,7,12,7,10,0");
        }

        [TestMethod]
        public void TestDiffMatrix()
        {
            int[,] m = new int[3, 3]{{ 1,3,6 },{5,12,21 },{12,27,25 }};
            DiffMatrix mat = new DiffMatrix(m);
            mat.ChangeRange(0, 1, 1, 2, -3);
            int v = mat.GetValue(1, 2);
            Assert.IsTrue(v == 18);
            int[,] o = mat.GetMatrix();
            string s="";
            foreach (int i in o) s += i.ToString() + ",";
            Assert.IsTrue(s == "1,0,3,5,9,18,12,27,25,");
        }
    }
}
