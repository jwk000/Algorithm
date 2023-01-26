using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * Dijkstra算法是解决单源最短路径问题的贪心算法
     * 它先求出长度最短的一条路径，再参照该最短路径求出长度次短的一条路径，直到求出从源点到其他各个顶点的最短路径。
     * 本算法采用邻接矩阵map[n,n]表示图，map[i,j]=d表示顶点i到j的距离为d，算法步骤为：
     * 1. 初始化。
     *          S为已探索集合，V为剩余集合，S初始只有原点u；
     *          dist[i]记录u到i的最短距离，初始化为dist[i]=map[u,i]；
     *          prev[i]记录i的前驱顶点，初始化为-1，如果map[u,i]可达则prev[i]=u；
     * 2. 贪心法找最小顶点。从V中找到dist[i]最小的顶点t加入S集合。
     * 3. 探索t。遍历t到V中顶点的距离d=map[t,i]+dist[t]，如果d小于dist[i]则更新dist[i]=d和prev[i]=t。
     * 4. 循环2-3直到V集合为空算法停止。遍历prev[i]即可找到u到i的最短路径。
     * ref：https://blog.csdn.net/qq_45776662/article/details/107177424
     */
    public class Djkstra
    {
        int[,] map;//图
        int start;//起点
        int count;//顶点数量
        bool[] S;//S[i]=true表示i加入S集合，否则i在V集合
        int[] dist;//最短距离
        int[] prev;//前驱顶点
        int INF = 1000;//不用int.MaxValue，防止加法溢出！

        public void LoadMap(int[,]map,int start)
        {
            count = map.GetLength(0);//第0维长度
            this.map = map;
            this.start = start;
            S = new bool[count];
            dist = new int[count];
            prev = new int[count];
            init();
            search();
        }

        void init()
        {
            S[start] = true;
            for(int i = 0; i < count; i++)
            {
                prev[i] = -1;
                dist[i] = map[start, i];
                if (dist[i] < INF)
                {
                    prev[i] = start;
                }
            }

        }

        void search()
        {
            int t = nextVertex();
            while (t != -1)
            {
                for (int i = 0; i < count; i++)
                {
                    int d = dist[t] + map[t, i];
                    if (d < dist[i])
                    {
                        dist[i] = d;
                        prev[i] = t;
                    }
                }
                t = nextVertex();
            }
        }

        int nextVertex()
        {
            int m = INF;
            int k = -1;
            for (int i = 0; i < count; i++)
            {
                if (!S[i] && dist[i] < m)
                {
                    m = dist[i];
                    k = i;
                }
            }
            if (k > -1)
            {
                S[k] = true;
            }
            return k;
        }

        public List<int> FindPath(int end)
        {
            List<int> path = new List<int>();
            path.Add(end);
            int p = prev[end];
            path.Add(p);
            while (p != start)
            {
                p = prev[p];
                path.Add(p);
            }
            path.Reverse();
            return path;
        }
    }
}
