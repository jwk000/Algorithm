using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 图。邻接矩阵表示。
     * 连通图：在无向图中，若任意两个顶点vi与vj都有路径相通，则称该无向图为连通图。
     * 强连通图：在有向图中，若任意两个顶点vi与vj都有路径相通，则称该有向图为强连通图。
     * 连通网：在连通图中，若图的边具有一定的意义，每一条边都对应着一个数，称为权；权代表着连接连个顶点的代价，称这种连通图叫做连通网。
     * 
     */
    public class GraphMatrix
    {
        int[,] G;
        int N;

        //具有Num个顶点的图
        public GraphMatrix(int vertexNum)
        {
            N = vertexNum;
            G = new int[N, N];
        }

        //加边，带权，无向
        public void AddEdge(int i, int j, int w)
        {
            if (i < 0 || j < 0 || i >= N || j >= N) return;
            G[i, j] = w;//无向图
            G[j, i] = w;
        }

        //顶点i到j的权值
        public int GetWeight(int i, int j)
        {
            if (i < 0 || j < 0 || i >= N || j >= N) return -1;
            return G[i, j];
        }

        //i和j是否连通
        public bool CanReach(int i, int j)
        {
            List<int> visited = new List<int>();
            return reach(i, j, visited);
        }

        bool reach(int i, int j, List<int> visited)
        {
            if (G[i, j] > 0) return true;
            visited.Add(i);
            for (int k = 0; k < N; k++)
            {
                if (!visited.Contains(k) && G[i, k] > 0)
                {
                    if (reach(k, j, visited))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                sb.Append('[');
                for (int j = 0; j < 10; j++)
                {
                    sb.Append(G[i, j]);
                    if (j < 9) sb.Append(',');
                }
                sb.Append(']').Append('\n');

            }
            return sb.ToString();
        }

        public int Sum()
        {
            int sum = 0;
            foreach (int v in G)
            {
                sum += v;
            }
            return sum / 2;
        }

        /*
         * Prim最小生成树算法：
         * V是所有顶点，U是加入到树中的顶点集合
         * （1）把图中的一个顶点a加入集合U
         * （2）寻找U到V-U的所有边中权值最小的边E，把E在V-U中的顶点加入U
         * （3）重复上一步直到V-U为空，就得到了最小生成树。
         * 
         */
        public GraphMatrix MiniSpanTreePrim()
        {
            List<int> V = new List<int>(N);//未选择的顶点
            List<int> U = new List<int>(N);//已选择的顶点

            GraphMatrix ret = new GraphMatrix(N);
            for (int i = 1; i < N; i++)
            {
                V.Add(i);
            }
            U.Add(0);

            while (V.Count > 0)
            {
                int from = -1, to = -1;
                int minw = int.MaxValue;
                foreach (int i in U)
                {
                    foreach (int j in V)
                    {
                        int w = G[i, j];
                        if (w > 0 && w < minw)
                        {
                            minw = w;
                            from = i;
                            to = j;
                        }
                    }
                }
                U.Add(to);
                V.Remove(to);
                ret.AddEdge(from, to, minw);
            }

            return ret;
        }

        /*
         * Kruskal最小生成树算法：
         * 
         * （1）将图中的所有边都去掉。
         * （2）将边按权值从小到大的顺序添加到图中，保证添加的过程中不会形成环
         * （3）重复上一步直到连接所有顶点，此时就生成了最小生成树。这是一种贪心策略。
         * 
         */

        class Edge
        {
            public int A, B, W;
            public Edge(int a, int b, int w)
            {
                A = a;
                B = b;
                W = w;
            }
        }
        public GraphMatrix MiniSpanTreeKruskal()
        {
            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (G[i, j] > 0)
                    {
                        edges.Add(new Edge(i, j, G[i, j]));
                    }
                }
            }
            edges.Sort((a, b) => a.W - b.W);
            GraphMatrix ret = new GraphMatrix(N);

            while (edges.Count > 0)
            {
                Edge e = edges[0];
                edges.RemoveAt(0);
                if (ret.CanReach(e.A, e.B))
                {
                    continue;
                }
                ret.AddEdge(e.A, e.B, e.W);

            }

            return ret;
        }
    }
}
