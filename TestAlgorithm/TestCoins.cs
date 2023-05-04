using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;


namespace TestAlgorithm
{
    [TestClass]
    public class TestCoins
    {
        [TestMethod]
        public void Test()
        {
            int[] coins = new int[] { 2, 3, 5 };
            int amount = 21;

            Coins CC = new Coins();
            int ans = CC.MakeCoinAStar(coins, amount);
            Assert.IsTrue(ans == 5);

            ans = CC.MakeCoinBFS(coins, amount);
            Assert.IsTrue(ans == 5);

            ans = CC.MakeCoinDFS(coins, amount);
            Assert.IsTrue(ans == 5);

            ans = CC.MakeCoinDjkstra(coins, amount);
            Assert.IsTrue(ans == 5);

            ans = CC.MakeCoinDP(coins, amount);
            Assert.IsTrue(ans == 5);

            ans = CC.MakeCoinDP2(coins, amount);
            Assert.IsTrue(ans == 5);

            ans = CC.MakeCoinDPRecursion(coins, amount);
            Assert.IsTrue(ans == 5);

            ans = CC.MakeCoinDPRecursion2(coins, amount);
            Assert.IsTrue(ans == 5);

            ans = CC.MakeCoinEnumerate(coins, amount);
            Assert.IsTrue(ans == 5);

        }
    }
}
