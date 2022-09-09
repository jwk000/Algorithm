namespace Algorithm;
class PokerTypeNum
{
    //一副扑克牌（去掉大小王牌，共52张），均匀发给4个人，每个人13张。  如果不考虑花色，只考虑点数，也不考虑自己得到的牌的先后顺序，自己手里能拿到的初始牌型组合一共有多少种呢？

    //从剩余的m种牌型里取出n张（m和n的减小速度不同，所以递归结束的情况就不同，除非能找出所有情况，不然结果就会漏一部分）
    int PokerNum(int m, int n)
    {
        if (n < 0 || m * 4 < n) return 0; //包含n==0
        if (m == 1) return 1;

        int ret = 0;
        for (int i = 0; i < 5; i++)
        {
            ret += PokerNum(m - 1, n - i);
        }
        return ret;
    }

    void TestPokerNum()
    {
        int v = PokerNum(13, 13);
        Console.WriteLine(v);
    }

    int Poker_DFS()
    {
        //按牌型t取n张牌，遍历所有牌型t和所有牌数n
        int ans = 0;
        void dfs(int t, int n)
        {
            if (t > 13 || n > 13) return;
            if (t == 13 && n == 13) { ans++; return; }
            for (int i = 0; i < 5; i++)
            {
                dfs(t + 1, n + i);
            }
        }
        dfs(0, 0);
        return ans;
    }

    void TestDfs()
    {

        int ans = Poker_DFS();
        Console.WriteLine(ans);
    }

    //动态规划思路：dp[i][j]表示i种牌型取j张的牌型数量，dp[i][j]=dp[i-1][j]+dp[i-1][j-1]+dp[i-1][j-2]+dp[i-1][j-3]+dp[i-1][j-4]
    int Poker_DP()
    {
        int[,] dp = new int[14, 14];
        for (int i = 0; i < 5; i++)
        {
            dp[1, i] = 1;
        }
        int idx = 0;
        for (int i = 2; i <= 13; i++)
        {
            idx = 1 - idx;
            for (int j = 0; j <= 13; j++) //包含取0张
            {
                for (int k = 0; k < 5; k++)
                {
                    if (j - k >= 0)
                    {
                        dp[i, j] += dp[i - 1, j - k];
                    }
                }
            }
        }
        return dp[13, 13];
    }

    void TestPoker()
    {

        int ret = Poker_DP();
        Console.WriteLine(ret);
    }
}
