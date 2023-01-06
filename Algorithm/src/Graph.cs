using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 图，用邻接链表表示的图
     * 
     */
    public partial class Graph
    {
        //顶点
        class Vertex
        {
            public int ID;
            public List<Edge> Edges;
        }
        //边
        class Edge
        {
            public int ToID;//指向顶点的ID
            public int Weight;//边权值

        }

        Vertex[] G;//图

        //构造一个Num个顶点的图
        public Graph(int vertexNum)
        {
            G = new Vertex[vertexNum];
            for (int i = 0; i < vertexNum; i++)
            {
                G[i] = new Vertex() { ID = i };
            }
        }

        public void AddEdge(int from, int to, int weight)
        {
            if (from < 0 || from > G.Length - 1) return;
            if (to < 0 || to > G.Length - 1) return;
            Vertex vfrom = G[from];
            if (vfrom.Edges == null)
            {
                vfrom.Edges = new List<Edge>();
            }
            if (vfrom.Edges.Any(e => e.ToID == to)) return;
            vfrom.Edges.Add(new Edge() { ToID = to, Weight = weight });
        }

        //整个图是不能BFS的，只能从某个顶点出发去BFS
        public List<int> BFS(int id)
        {
            List<int> ret = new List<int>();
            List<int> queue = new List<int>();
            queue.Add(id);
            while (queue.Count > 0)
            {
                id = queue.First();
                queue.RemoveAt(0);
                ret.Add(id);
                Vertex v = G[id];
                if (v.Edges != null)
                {
                    foreach (Edge e in v.Edges)
                    {
                        queue.Add(e.ToID);
                    }
                }
            }
            return ret;
        }

        //整个图是不能DFS的，只能从某个顶点出发去DFS
        public void DFS(int id, List<int> ret)
        {
            ret.Add(id);
            Vertex v = G[id];
            if (v.Edges != null)
            {
                foreach (Edge e in v.Edges)
                {
                    DFS(e.ToID, ret);
                }
            }
        }


    }
}
