using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 凑硬币：给面值2，3，5的三种硬币，凑21，求最少用几个硬币。如果无解返回-1。
     * 9种算法：穷举法；递推动态规划；递归动态规划；递归实现递推；递推实现递归；DFS；BFS；DJkstra；A*；
     */
    public class Coins
    {
        //穷举法
        public int MakeCoinEnumerate(int[] coins, int amount)
        {
            int n = -1;
            for (int c2 = 0; c2 <= amount / 2; c2++)
            {
                for (int c3 = 0; c3 <= amount / 3; c3++)
                {
                    for (int c5 = 0; c5 <= amount / 5; c5++)
                    {
                        if (c2 * 2 + c3 * 3 + c5 * 5 == amount)
                        {
                            if (n == -1 || (c2 + c3 + c5 < n))
                            {
                                n = c2 + c3 + c5;
                            }
                        }
                    }
                }
            }
            return n;
        }

        //递推动态规划
        public int MakeCoinDP(int[] coins, int amount)
        {
            //凑1-amount需要的最少硬币数
            int[] dp = new int[amount + 1];
            for (int i = 0; i < amount + 1; i++) dp[i] = -1;
            //硬币面值需要的值是1
            foreach (int c in coins)
            {
                dp[c] = 1;
            }
            //递推计算每个面值加一个硬币后的值
            for (int i = 1; i < amount; i++)
            {
                if (dp[i] == -1) continue;//跳过无意义的值
                foreach (int c in coins)
                {
                    int t = i + c;//新的面值
                    if (t <= amount && (dp[t] == -1 || dp[i] + 1 < dp[t]))
                    {
                        dp[t] = dp[i] + 1;//新的硬币数
                    }

                }
            }
            return dp[amount];
        }

        //递归实现递推
        public int MakeCoinDP2(int[] coins, int amount)
        {
            int[] dp = new int[amount + 1];
            for (int i = 0; i < amount + 1; i++) dp[i] = -1;
            DP2(coins, amount, dp, 0, 0);
            return dp[amount];
        }

        void DP2(int[] coins, int amount, int[] dp, int v, int n)
        {
            if (dp[v] == -1 || dp[v] > n) dp[v] = n;
            foreach (int c in coins)
            {
                if (v + c <= amount)
                {
                    DP2(coins, amount, dp, v + c, n + 1);
                }
            }
        }

        //递归动态规划
        public int MakeCoinDPRecursion(int[] coins, int amount)
        {
            if (amount == 0) return 0;
            int ans = -1;
            int[] cache = new int[amount + 1];
            foreach (int c in coins)
            {
                if (amount - c >= 0)
                {
                    if (cache[amount - c] != 0) return cache[amount - c];
                    int v = MakeCoinDPRecursion(coins, amount - c);
                    cache[amount - c] = v;
                    if (v >= 0 && (ans == -1 || v + 1 < ans)) ans = v + 1;//子问题的最优解+1
                }
            }
            return ans;
        }

        //递推实现递归
        public int MakeCoinDPRecursion2(int[] coins, int amount)
        {
            int[] index = new int[amount + 1];//记录凑n时遍历coins的下标
            int[] cache = new int[amount + 1];
            Array.Fill(cache, -1);
            
            Stack<int> stack = new Stack<int>();
            stack.Push(amount);//凑amount
            
            while (stack.Count > 0)
            {
                int n = stack.Peek();
                if (index[n] < coins.Length)//尝试所有子问题
                {
                    int c = coins[index[n]];
                    index[n]++;

                    if (n == c)
                    {
                        cache[n] = 1;
                        index[n] = coins.Length;//结束尝试
                    }
                    else if (n - c > 0)
                    {
                        if (cache[n - c] > 0) //有缓存
                        {
                            if (cache[n] == -1 || cache[n] > cache[n - c] + 1) cache[n] = cache[n - c] + 1;
                        }
                        else //未缓存
                        {
                            stack.Push(n - c);
                        }
                    }
                }
                else //尝试结束回到上一层
                {
                    stack.Pop(); 
                    if (stack.Count == 0) break;

                    if (cache[n] > 0)
                    {
                        int m = stack.Peek();
                        if (cache[m] == -1 || cache[m] > cache[n] + 1) cache[m] = cache[n] + 1;
                    }
                }
            }

            return cache[amount];
        }

        //dfs
        public int MakeCoinDFS(int[] coins, int amount)
        {
            return dfs(coins, amount, 0, 0);
        }
        //dfs通常不带cache
        //v是当前累加值，n是累加次数
        int dfs(int[] coins, int amount, int v, int n)
        {
            if (amount == v) return n;
            int min = -1;
            foreach (int c in coins)
            {
                if (c + v <= amount)
                {
                    int x = dfs(coins, amount, c + v, n + 1);
                    if (x > 0 && (min == -1 || x < min))
                    {
                        min = x;
                    }
                }
            }
            return min;
        }


        //bfs
        public int MakeCoinBFS(int[] coins, int amount)
        {
            //key是累加值 value是累加次数
            Queue<KeyValuePair<int, int>> q = new Queue<KeyValuePair<int, int>>();
            q.Enqueue(new KeyValuePair<int, int>(0, 0));
            while (q.Count > 0)
            {
                var kv = q.Dequeue();
                foreach (int c in coins)
                {
                    if (kv.Key + c == amount)
                    {
                        return kv.Value + 1;
                    }
                    if (kv.Key + c < amount)
                    {
                        q.Enqueue(new KeyValuePair<int, int>(kv.Key + c, kv.Value + 1));
                    }
                }
            }
            return -1;
        }

        //djkstra
        public int MakeCoinDjkstra(int[] coins, int amount)
        {
            int[] optimal = new int[amount + 1];
            for (int i = 0; i < amount + 1; i++) optimal[i] = -1;
            //节点0放入集合，然后寻找未放入集合的边，得到新的节点
            optimal[0] = 0;
            //抽象思路：i是节点 i+c是连通的节点
            for (int i = 0; i < amount; i++)
            {
                if (optimal[i] == -1) continue;//无效的节点
                foreach (int c in coins)
                {
                    if (i + c <= amount)
                    {
                        if (optimal[i + c] == -1 || optimal[i + c] > optimal[i] + 1)
                        {
                            optimal[i + c] = optimal[i] + 1;
                        }
                    }
                }
            }
            return optimal[amount];
        }


        class MinCount : IComparable<MinCount>
        {
            public int Count;
            public int Value;
            public MinCount(int c, int v)
            {
                Count = c;
                Value = v;
            }
            //数量越少越优先，面值越大越优先
            public int CompareTo(MinCount other)
            {
                if (Count == other.Count)
                {
                    return other.Value - Value;
                }
                return Count - other.Count;
            }
        }
        //a*
        public int MakeCoinAStar(int[] coins, int amount)
        {
            PriorityQueue<MinCount> pq = new PriorityQueue<MinCount>();
            pq.Push(new MinCount(0, 0));
            while (pq.Size > 0)
            {
                var mc = pq.Pop();
                if (mc.Value == amount)
                {
                    return mc.Count;
                }
                foreach (int c in coins)
                {
                    if (mc.Value + c > amount) continue;
                    pq.Push(new MinCount(mc.Count + 1, mc.Value + c));
                }
            }
            return -1;
        }
    }
}
