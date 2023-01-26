using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Algorithm
{
    /*
     * K维树，用于K维空间划分；树的每一层二分一个维度；以3维为例：x-y-z-x-y-z...
     * 用于查找距离目标最近的节点；
     */
    public class KDTree
    {
        public class KDNode
        {
            public int split;//切分维度
            public Vector2 data;//二维点
            public KDNode left;
            public KDNode right;
        }

        public KDNode Root;
        int K = 2;

        public void Build(List<Vector2> input)
        {
            Root = BuildTree(input, 0);
        }

        KDNode BuildTree(List<Vector2> input, int split)
        {
            if (input.Count == 0) return null;
            if (split == 0)
            {
                input.Sort((a, b) => (int)((a.X - b.X)*100));
            }
            else
            {
                input.Sort((a, b) => (int)((a.Y - b.Y)*100));
            }
            KDNode node = new KDNode();
            node.split = split;
            int mid = input.Count / 2;
            node.data = input[mid];
            input.RemoveAt(mid);

            split = (split + 1) % K;
            node.left = BuildTree(input.Take(mid).ToList(), split);
            node.right = BuildTree(input.Skip(mid).ToList(), split);
            return node;
        }

        //查找离目标点最近的点
        public Vector2 Search(Vector2 target)
        {
            if (Root == null) return Vector2.Zero;
            var p = SearchNode(Root, target, 0, out float d);
            return p.data;
        }

        KDNode SearchNode(KDNode node, Vector2 target, int split, out float distance)
        {
            int next = (split + 1) % K;
            distance = Vector2.Distance(node.data, target);
            float dd;
            KDNode nn;
            if (split == 0)
            {
                if (node.data.X < target.X)
                {
                    if (node.right == null)
                    {
                        return node;
                    }

                    nn = SearchNode(node.right, target, next, out dd);
                }
                else
                {
                    if (node.left == null) return node;
                    nn = SearchNode(node.left, target, next, out dd);
                }
            }
            else
            {
                if (node.data.Y < target.Y)
                {
                    if (node.right == null) return node;
                    nn = SearchNode(node.right, target, next, out dd);
                }
                else
                {
                    if (node.left == null) return node;
                    nn = SearchNode(node.left, target, next, out dd);
                }
            }

            return dd < distance ? nn : node;

        }


    }
}
