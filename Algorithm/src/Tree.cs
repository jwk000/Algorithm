using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //通用的树结构，仅用来构建树
    public class Tree<T>
    {
        public class Node
        {
            public int ID;
            public T Data;
            public Node Parent;
            public List<Node> Nexts;
        }

        Node Root;

        public void AddNode(int parentID, int nodeID, T data)
        {
            if (Root == null)
            {
                Root = new Node() { ID = parentID };
            }
            Node p = FindNode(Root, parentID);
            if (p != null)
            {
                Node q = FindNode(Root, nodeID);
                if (q == null)
                {
                    q = new Node() { ID = nodeID, Data = data };
                    if (p.Nexts == null)
                    {
                        p.Nexts = new List<Node>();
                    }
                    p.Nexts.Add(q);
                    q.Parent = p;
                }
            }
        }

        public void RemoveNode(int id)
        {
            Node p = FindNode(Root, id);
            if (p != null)
            {
                p.Parent.Nexts.Remove(p);
            }
        }


        public Node FindNode(Node p, int id)
        {
            if (p == null) return null;
            if (p.ID == id)
            {
                return p;
            }

            if (p.Nexts == null) return null;
            foreach (Node q in p.Nexts)
            {
                var r = FindNode(q, id);
                if (r != null)
                {
                    return r;
                }
            }

            return null;
        }

        public void DFS(Action<T> action)
        {
            dfs(Root, action);
        }

        void dfs(Node node, Action<T> action)
        {
            if (node == null) return;
            action(node.Data);
            if (node.Nexts == null) return;
            foreach (var next in node.Nexts)
            {
                dfs(next, action);
            }
        }
    }
}
