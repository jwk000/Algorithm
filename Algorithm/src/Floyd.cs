using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //Floyd 算法是一个基于「贪心」、「动态规划」求一个图中 所有点到所有点 最短路径的算法，时间复杂度 O(n3)
    public class Floyd
    {
        int[,] G; //下标是顶点编号，值是距离；-1表示不可达，0是i->i的距离，>0是i->j的距离
        int[,] dist;//记录i->j最短距离
        int[,] path;//记录i->j最短路径经过的中转点k，递归查找i->k,k->j的中转点就得到了path
        int N;
        public Floyd(int[,] map, int n)
        {
            N = n;
            G = new int[n, n];
            dist = new int[n, n];
            path = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    G[i, j] = map[i, j];
                    dist[i, j] = G[i, j];
                    if (G[i, j] > 0)
                    {
                        path[i, j] = j;
                    }
                    else
                    {
                        path[i, j] = -1;
                    }
                }
            }

            //遍历所有通过k中转的路径，检查是否有更短路径
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (dist[i, k] > 0 && dist[k, j] > 0 && (dist[i, j] < 0 || dist[i, k] + dist[k, j] < dist[i, j]))
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                            path[i, j] = k;
                        }
                    }
                }
            }
        }

        //i->j最短路径
        public List<int> GetPath(int i, int j)
        {
            List<int> ret = new List<int>();
            if (i < 0 || i >= N || j < 0 || j >= N)
            {
                return ret;
            }
            if (path[i, j] < 0)
            {
                return ret;
            }
            ret.Add(i);
            search(i, j, ret);
            return ret;
        }

        void search(int i, int j, List<int> ret)
        {
            int k = path[i, j];
            if (k != j)
            {
                search(i, k, ret);
                search(k, j, ret);
            }
            else
            {
                ret.Add(j);
            }
        }
    }
}
