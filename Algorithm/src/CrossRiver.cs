using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 和尚过河
     * 三个和尚带着三个妖怪过河，只有一条容纳两人的小船，任何情况下妖怪数量大于和尚数量时，妖怪会吃掉和尚。问如何过河？
     * 思路：
     * 从初始状态派生下一步n个子状态，每个子状态进一步派生子状态，形成一颗搜索树；BFS遍历这棵树达到终结状态；
     * 还可以优化一下，如果子状态是出现过的则不再继续派生；
     */
    public class CrossRiver
    {
        class Node
        {
            int[] Left = new int[] { 3, 3 };
            int[] Right = new int[] { 0, 0 };
            public int State = 0;
            public Node Parent = null;
            bool boatAtLeft = true;

            public Node(int lh, int lm, int rh, int rm,bool boat)
            {
                Left[0] = lh;
                Left[1] = lm;
                Right[0] = rh;
                Right[1] = rm;
                State = lh * 1000 + lm * 100 + rh * 10 + rm;
                boatAtLeft = boat;
            }
            public override string ToString()
            {
                return $"{State}|{boatAtLeft}";
            }
            public bool Finish()
            {
                return State == 33;
            }
            bool check(int[] f, int[] t, int h, int m)
            {
                return f[0] >= h && f[1] >= m && (f[0] - h==0||f[1] - m <= f[0] - h) &&(t[0]+h==0|| t[1] + m <= t[0] + h);
            }
            public void Cross(List<Node> q, int[] from, int[] to, int h, int m)
            {
                if (check(from, to, h, m))
                {
                    Node n = null;
                    if (boatAtLeft)
                    {
                     n = new Node(from[0] - h, from[1] - m, to[0] + h, to[1] + m,!boatAtLeft);
                    }
                    else
                    {
                        n = new Node( to[0] + h, to[1] + m, from[0] - h, from[1] - m, !boatAtLeft);
                    }
                    n.Parent = this;
                    q.Add(n);
                }
            }
            public void L2R(List<Node> queue)
            {
                Cross(queue, Left, Right, 1, 0);
                Cross(queue, Left, Right, 2, 0);
                Cross(queue, Left, Right, 0, 1);
                Cross(queue, Left, Right, 0, 2);
                Cross(queue, Left, Right, 1, 1);
            }

            public void R2L(List<Node> queue)
            {
                Cross(queue, Right, Left, 1, 0);
                Cross(queue, Right, Left, 2, 0);
                Cross(queue, Right, Left, 0, 1);
                Cross(queue, Right, Left, 0, 2);
                Cross(queue, Right, Left, 1, 1);
            }

            public void Run(List<Node> queue)
            {
                if (boatAtLeft)
                {
                    L2R(queue);
                }
                else
                {
                    R2L(queue);
                }
            }
        }

        List<Node> queue = new List<Node>();
        public List<int> CrossingTheRiver()
        {
            queue.Add(new Node(3, 3, 0, 0,true));
            while (queue.Count > 0)
            {
                var node = queue[0];
                if (node.Finish())
                {
                    List<int> path = new List<int>();
                    while (node.Parent != null)
                    {
                        path.Add(node.State);
                        node = node.Parent;
                    }
                    path.Add(node.State);
                    path.Reverse();
                    return path;
                }
                queue.RemoveAt(0);
                node.Run(queue);
            }

            return null;
        }

    }
}
