using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //树堆：平衡二叉树的一种实现；BST和heap的结合；新增节点时会随机分配一个优先级priority，旋转节点层级，使其priority满足堆的性质；
    public class Treap
    {
        class TPNode
        {
            public int Key;
            public int Value;
            public int Priority;
            public TPNode Parent;
            public TPNode Left;
            public TPNode Right;

            public TPNode(int key,int value)
            {
                Key = key;
                Value = value;
            }

            public override string ToString()
            {
                return $"{Key}|{Priority}|{Parent?.Key}";
            }
        }

        Random mRander = new Random();
        TPNode mRoot;

        public void Add(int key, int val, int pr=0)
        {
            var node = new TPNode(key, val);
            node.Priority =pr>0?pr: mRander.Next(0,1000);
            Console.WriteLine("Add {0}", node);
            if (mRoot == null)
            {
                mRoot = node;
            }
            else
            {
                var p = mRoot;
                while (p != null)
                {
                    if (p.Key == key)
                    {
                        return;
                    }
                    if (p.Key < key)
                    {
                        if (p.Right == null)
                        {
                            p.Right = node;
                            node.Parent = p;
                            break;
                        }
                        p = p.Right;
                    }
                    else
                    {
                        if (p.Left == null)
                        {
                            p.Left = node;
                            node.Parent = p;
                            break;
                        }
                        p = p.Left;
                    }
                }

                FixHeap(node);

            }
            Console.WriteLine(Print());
        }

        void FixHeap(TPNode node)
        {
            while (node.Parent != null)
            {
                if (node.Priority > node.Parent.Priority)
                {
                    return;
                }
                if (node == node.Parent.Left)
                {
                    RightRotate(node.Parent);
                }
                else
                {
                    LeftRotate(node.Parent);
                }
            }
            mRoot = node;
        }

        //删除的永远是叶子节点，不影响heap性质
        public void Remove(int key)
        {
            Console.Write("remove key {0} ", key);
            var p = mRoot;
            while (p != null)
            {
                if (p.Key == key)
                {
                    var next = FindNext(p);
                    if (next == null)
                    {
                        RemoveNode(p);
                    }
                    else
                    {
                        p.Key = next.Key;
                        p.Value = next.Value;
                        RemoveNode(next);
                    }
                    return;
                }
                if (p.Key < key)
                {
                    p = p.Right;
                }
                else
                {
                    p = p.Left;
                }
            }
        }
        void RemoveNode(TPNode node)
        {
            Console.WriteLine("Remove Node {0}", node);

            if (node.Parent != null)
            {
                if (node == node.Parent.Left)
                {
                    node.Parent.Left = node.Left;
                    if(node.Left!=null) node.Left.Parent = node.Parent;
                }
                else
                {
                    node.Parent.Right = node.Right;
                    if(node.Right!=null) node.Right.Parent = node.Parent;
                }
            }
            else
            {
                mRoot = null;
            }
            Console.WriteLine(Print());

        }

        TPNode FindNext(TPNode node)
        {
            var p = node.Right;
            if (p == null)
            {
                p = node.Left;
                if (p == null)
                {
                    return null;
                }
                else
                {
                    while (p.Right != null)
                    {
                        p = p.Right;
                    }
                }
            }
            else
            {
                while (p.Left != null)
                {
                    p = p.Left;
                }
            }
            return p;
        }

        void LeftRotate(TPNode node)
        {
            var P = node.Parent;
            var R = node.Right;
            if (P != null)
            {
                if (P.Left == node) P.Left = R;
                else P.Right = R;
            }
            node.Parent = R;
            node.Right = R.Left;
            if (R.Left != null) R.Left.Parent = node;
            R.Parent = P;
            R.Left = node;
        }

        void RightRotate(TPNode node)
        {
            var P = node.Parent;
            var L = node.Left;
            if (P != null)
            {
                if (P.Left == node) P.Left = L;
                else P.Right = L;
            }
            node.Parent = L;
            node.Left = L.Right;
            if (L.Right != null) L.Right.Parent = node;
            L.Parent = P;
            L.Right = node;
        }

        public int Search(int key)
        {
            var p = mRoot;
            while (p != null)
            {
                if (p.Key == key)
                {
                    return p.Value;
                }
                else if (p.Key < key)
                {
                    p = p.Right;
                }
                else
                {
                    p = p.Left;
                }
            }

            return -1;
        }

        public int Height()
        {
            return _height(mRoot);
        }

        int _height(TPNode node)
        {
            if (node == null) return 0;
            int hl = _height(node.Left) + 1;
            int hr = _height(node.Right) + 1;
            return Math.Max(hl, hr);
        }
        public string Print()
        {
            StringBuilder sb = new StringBuilder();

            if (mRoot != null)
            {
                int h = Height();
                List<TPNode> nodes = new List<TPNode>();
                nodes.Add(mRoot);
                for (int i = 0; i < (1 << h - 1) - 1; i++)
                {
                    var node = nodes[i];
                    if (node != null)
                    {
                        nodes.Add(node.Left);
                        nodes.Add(node.Right);
                    }
                    else
                    {
                        nodes.Add(null);
                        nodes.Add(null);
                    }
                }

                int st = 0;//0输出数字 1输出连线
                for (int i = 0; i < h; i++)//行数
                {
                    int j = (1 << (h - i - 1)) - 1;//每行开头空格数
                    for (int k = 0; k < (1 << i); k++)//每行节点数
                    {
                        int m = k + (1 << i) - 1;//下标=k+起始下标
                        int z = k == 0 ? j : j * 2 + 1;//间隔空格数
                        for (int n = 0; n < z; n++)
                        {
                            sb.Append("  ");
                        }
                        if (nodes[m] != null)
                        {
                            if (st == 0)
                            {

                                sb.Append(nodes[m]);
                            }
                            else
                            {
                                sb.Append(m % 2 > 0 ? '/' : '\\').Append("  ");
                            }
                        }
                        else
                        {
                            sb.Append("  ");
                        }
                    }
                    sb.AppendLine();

                    if (st == 0)
                    {
                        st = 1;
                    }
                    else
                    {
                        st = 0;
                        i--;
                    }
                }
            }


            return sb.ToString();
        }


    }
}
