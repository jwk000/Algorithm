using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestDynamicProgramming
    {


        [TestMethod]
        public void TestMakeChange()
        {
            int[] coins = new[] { 2, 3, 5 };
            int amount = 12;
            int ans = DynamicPrograming.MakeChange(coins, amount);
            Assert.IsTrue(ans == 3);
            amount = 11;
            ans = DynamicPrograming.MakeChange(coins, amount);
            Assert.IsTrue(ans == 3);
            amount = 1;
            ans = DynamicPrograming.MakeChange(coins, amount);
            Assert.IsTrue(ans == -1);
        }

        [TestMethod]
        public void TestMaxContiguousSubsequence()
        {
            int[] arr = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            int ans = DynamicPrograming.MaxContiguousSubsequence(arr);
            Assert.IsTrue(ans == 6);
        }

        [TestMethod]
        public void TestLongestIncreasingSubsequence()
        {
            int[] arr = new[] { 10, 9, 2, 5, 3, 7, 101, 18 };
            int ans = DynamicPrograming.LongestIncreasingSubsequence(arr);
            Assert.IsTrue(ans == 4);

            arr = new[] { 10, 1, 8, 3, 5, 6, 2, 7, 3 };
            ans = DynamicPrograming.LongestIncreasingSubsequence(arr);
            Assert.IsTrue(ans == 5);

            arr = new[] { 1, 1, 1, 1, 1, 1 };
            ans = DynamicPrograming.LongestIncreasingSubsequence(arr);
            Assert.IsTrue(ans == 1);
        }

        [TestMethod]
        public void TestLongestCommonSubsequence()
        {
            int[] a = new[] { 1, 3, 5, 4, 2, 6, 8, 7 };
            int[] b = new[] { 1, 4, 8, 6, 7, 5 };
            int ans = DynamicPrograming.LongestCommonSubsequnce(a, b);
            Assert.IsTrue(ans == 4);

        }

        [TestMethod]
        public void TestBackPackI()
        {
            int[] A = new[] { 1, 3, 5, 7, 2 };
            int[] V = new[] { 4, 8, 6, 5, 3 };
            int M = 10;
            int ans = DynamicPrograming.BackPack01(A, V, M);
            Assert.IsTrue(ans == 18);

            int ans2 = DynamicPrograming.DP01(A, V, M, A.Length);
            Assert.IsTrue(ans2 == 18);

        }

        [TestMethod]
        public void TestEditDistance()
        {
            var word1 = "intention".ToList();
            var word2 = "execution".ToList();
            int ans = DynamicPrograming.EditDistance(word1, word2);
            Assert.IsTrue(ans == 5);
        }
    }
}
