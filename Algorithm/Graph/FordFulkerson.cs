using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 流量网络最大流问题：
     * 一个流量网络flow network可以视为一个有向图，可以有环，这个图上有一个源点source和一个汇点sink，流量从源点出发汇入汇点。
     * 流量网络上的边有容量上限capacity和当前流量flow，计算从源点到汇点的最大流量max flow，称为流量网络的最大流问题。最大流只有一个值，但流量分配方式有多种。
     * 
     * 福德福克森算法：
     * 1）把流量网络抽象成余量网络residual network，原来的每个边转换为一对正向余量边（可用余量）和反向余量边（消耗流量）。
     * 2）从源点到汇点遍历所有可达路径，每条path称为增益路径argumenting path，因为这条path一定会增加最大流量。
     * 3）对每条增益路径上的余量边计算余量，方向相反的余量可以抵消，称为流量抵消flow cancellation或容量返还return。
     * 4）统计汇点的输入边的消耗流量（反向余量边的值）就是最大流量。
     */
    public class FordFulkerson
    {
        class NetEdge
        {
            public int nValue;
            public bool bForward = true;
            public int nIn;
            public int nOut;
            public NetEdge pairEdge;
        }

        
        List<NetEdge>[] mNetEdges;

        
        //输入的流量网络图用邻接矩阵表示 c=net[i,j]表示i到j的容量c
        public void BuildNetwork(int[,] net)
        {
            mNetEdges = new List<NetEdge>[net.GetLength(0)];
            for(int i = 0; i < net.GetLength(0); i++)
            {
                for(int j = 0; j < net.GetLength(1); j++)
                {
                    int c = net[i, j];
                    if (c > 0)
                    {
                        NetEdge edge = new NetEdge();
                        edge.nValue = c;
                        edge.nIn = i;
                        edge.nOut = j;
                        NetEdge redge = new NetEdge();
                        redge.nValue = 0;
                        redge.nIn = j;
                        redge.nOut = i;
                        redge.bForward = false;
                        edge.pairEdge = redge;
                        redge.pairEdge = edge;
                        if (mNetEdges[i] == null) mNetEdges[i] = new List<NetEdge>();
                        mNetEdges[i].Add(edge);
                        if (mNetEdges[j] == null) mNetEdges[j] = new List<NetEdge>();
                        mNetEdges[j].Add(redge);
                    }
                }
            }
        }

        //源点，汇点
        public int CalcMaxFlow(int source, int sink)
        {
            List<NetEdge> path = new List<NetEdge>();
            bool[] visited = new bool[mNetEdges.Length];

            dfs(path,visited, source, sink);
            int maxFlow = 0;
            foreach(var edges in mNetEdges)
            {
                foreach(var edge in edges)
                {
                    if (edge.nOut == sink && edge.bForward)
                    {
                        maxFlow += edge.pairEdge.nValue;
                    }
                }
            }
            return maxFlow;
        }

        void dfs(List<NetEdge> path,bool[] visited, int source, int sink)
        {
            if (source == sink)
            {
                int flow = int.MaxValue;
                foreach (var edge in path)
                {
                    if (edge.nValue < flow) flow = edge.nValue;
                }
                //所有边value减去flow
                foreach (var edge in path)
                {
                    edge.nValue -= flow;
                    edge.pairEdge.nValue += flow;
                }
                
                return;
            }
            visited[source] = true;
            foreach (NetEdge edge in mNetEdges[source])
            {
                if (visited[edge.nOut]) continue;
                if (edge.nValue <= 0) continue;
                path.Add(edge);
                
                dfs(path,visited, edge.nOut, sink);
                path.Remove(edge);
            }
            visited[source] = false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var edges in mNetEdges)
            {
                foreach (var edge in edges)
                {
                    if (edge.bForward)
                    {
                        sb.AppendLine($"{edge.nIn} -> {edge.nOut} : {edge.pairEdge.nValue}");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
