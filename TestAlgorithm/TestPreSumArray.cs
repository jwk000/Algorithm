using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestPreSumArray
    {
        [TestMethod]
        public void TestPreSumArray1()
        {
            PreSumArray arr = new PreSumArray(new int[] { 1,2,3,4,5,6,7,8,9,10});
            int sum = arr.Sum(3, 8);
            Assert.IsTrue(sum == 39);
        }

        [TestMethod]
        public void TestPresumMatrix()
        {
            int[,] mat = new int[6, 9];
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    mat[i, j] = i * 10 + j;
                }
            }
            PreSumMatrix m = new PreSumMatrix(mat);
            int sum = m.Sum(2, 3, 5, 7);
            Assert.IsTrue(sum == 800);
        }
    }
}
