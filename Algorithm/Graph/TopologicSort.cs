using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * AOV网拓扑排序
     * 对一个有向无环图(Directed Acyclic Graph简称DAG)G进行拓扑排序，是将G中所有顶点排成一个线性序列，使得图中任意一对顶点u和v，若边(u,v)∈E(G)，则u在线性序列中出现在v之前。
     * 我们把顶点表示活动、边表示活动间先后关系的有向图称做顶点活动网(Activity On Vertex network)，简称AOV网。
     * 一个AOV网应该是一个有向无环图，所有活动可排列成一个线性序列，使得每个活动的所有前驱活动都排在该活动的前面，我们把此序列叫做拓扑序列(Topological order)，由AOV网构造拓扑序列的过程叫做拓扑排序(Topological sort)。
     * AOV网的拓扑序列不是唯一的，满足上述定义的任一线性序列都称作它的拓扑序列。
     * 
     * 实现步骤：
     * 1. 在有向图中选一个没有前驱的顶点并且输出
     * 2. 从图中删除该顶点和所有以它为尾的弧（白话就是：删除所有和它有关的边）
     * 3. 重复上述两步，直至所有顶点输出，或者当前图中不存在无前驱的顶点为止，后者代表我们的有向图是有环的，因此，也可以通过拓扑排序来判断一个图是否有环。
     */
    public partial class GraphAdjList
    {
        //实现为Graph类的方法
        public List<int> TopologicalSort()
        {
            //前驱顶点数量
            int[] PrevNum = new int[G.Length];
            for (int i = 0; i < G.Length; i++)
            {
                Vertex v = G[i];
                if (v.Edges != null)
                {
                    foreach (Edge e in v.Edges)
                    {
                        PrevNum[e.ToID]++;
                    }
                }
            }

            //找前驱顶点数量为0的顶点
            List<int> ret = new List<int>();
            bool finish = false;
            while (!finish)
            {
                finish = true;
                for (int i = 0; i < PrevNum.Length; i++)
                {
                    if (PrevNum[i] == 0)
                    {
                        finish = false;
                        PrevNum[i] = -1;
                        ret.Add(i);
                        if (G[i].Edges != null)
                        {
                            foreach (Edge e in G[i].Edges)
                            {
                                PrevNum[e.ToID]--;
                            }
                        }
                    }
                }
            }

            return ret;
        }

        public bool CheckTopological(List<int> order)
        {
            //后面的顶点dfs遍历不可能找到前面的顶点

            for (int i = 0; i < order.Count; i++)
            {
                int id = G[order[i]].ID;
                for (int j = i + 1; j < order.Count; j++)
                {
                    Vertex v = G[order[j]];
                    if (CanReachToID(v, id))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        bool CanReachToID(Vertex v, int id)
        {
            if (v.Edges != null)
            {
                foreach (Edge e in v.Edges)
                {
                    if (e.ToID == id)
                    {
                        return true;
                    }
                    if (CanReachToID(G[e.ToID], id))
                    {
                        return true;
                    }
                }
            }
            return false;

        }
    }
}
