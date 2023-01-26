using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 三个水桶等分8升水
     * 水桶容量3，5，8升，初始8是满的，其他两个桶是空的，问如何利用三个桶等分8升水，即两个4升。
     * 
     * 思路：
     * 穷举法，从初始状态开始搜索所有倒水可能，状态去重，直到得到两个4升的情况。
     * 分支限界法，生成所有解空间分支，BFS搜索最短路径。内存占用较多，本题未使用。
     */
    public class DumpWater
    {


        int[] T = new int[] { 3, 5, 8 };
        int[] W = new int[] { 0, 0, 8 };

        List<int> visit = new List<int>();
        List<string> ans = new List<string>();

        public List<string> Dump()
        {
            int st = W[0] * 100 + W[1] * 10 + W[2];
            visit.Add(st);
            bool finish = false;
            while (!finish)
            {
                finish = true;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        st = dump(i, j);
                        if (st == 44)
                        {
                            return ans;
                        }
                        if (st > 0)
                        {
                            finish = false;
                        }
                    }
                }
            }
            return null;
        }


        //从i桶向j桶倒水
        int dump(int i,int j)
        {
            if (i==j||W[i] == 0 ) return -1;
            int empty = T[j]-W[j];
            int water = Math.Min(W[i], empty);
            W[i] -= water;
            W[j] += water;
            int st = W[0] * 100 + W[1] * 10 + W[2];
            if (visit.Contains(st))
            {
                W[i] += water;
                W[j] -= water;
                st = -1;
            }
            else
            {
                visit.Add(st);
                ans.Add($"{i}->{j}|{st} ");
            }
            return st;
        }
    }
}
