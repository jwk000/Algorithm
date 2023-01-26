using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //二叉树
    public class BinaryTree
    {
        class Node
        {
            public int Value;
            public Node Left;
            public Node Right;
            public Node(int v)
            {
                Value = v;
            }
            public override string ToString()
            {
                return Value.ToString();
            }
        }

        Node mRoot;

        //根据输入节点数组生成二叉树
        // 1,2,3
        // 2,4,5
        // 3,-1,6
        //对应的树如下：
        //    1
        //   / \
        //  2   3
        // / \   \
        //4   5   6
        public static BinaryTree Create(int[,] arr)
        {
            BinaryTree bt = new BinaryTree();
            for (int i = 0; i < arr.GetLength(0); i++)
            {

                var node = bt.FindNode(arr[i, 0]);

                if (node == null)
                {
                    node = new Node(arr[i, 0]);

                    bt.mRoot = node;
                }
                if (arr[i, 1] > -1) node.Left = new Node(arr[i, 1]);
                if (arr[i, 2] > -1) node.Right = new Node(arr[i, 2]);
            }
            return bt;
        }



        //翻转二叉树，左右镜像翻转
        public void MirrorFlip()
        {
            flip(mRoot);
        }

        void flip(Node node)
        {
            if (node == null) return;
            (node.Left, node.Right) = (node.Right, node.Left);
            flip(node.Left);
            flip(node.Right);
        }



        //前序遍历二叉树 根-左-右（非递归）
        public int[] Preorder()
        {
            List<int> ret = new List<int>();
            Stack<Node> st = new Stack<Node>();
            var p = mRoot;
            while (p != null || st.Count > 0)
            {
                if (p != null)
                {
                    ret.Add(p.Value);
                    st.Push(p);
                    p = p.Left;
                }
                else
                {
                    var q = st.Pop();
                    if (q.Right != null)
                    {
                        p = q.Right;
                    }
                }
            }
            return ret.ToArray();
        }


        //中序遍历二叉树 左-根-右
        public int[] Inorder()
        {
            List<int> ret = new List<int>();
            Stack<Node> st = new Stack<Node>();
            var p = mRoot;
            while (p != null || st.Count > 0)
            {
                if (p != null)
                {
                    st.Push(p);
                    p = p.Left;
                }
                else
                {
                    var q = st.Pop();
                    ret.Add(q.Value);

                    if (q.Right != null)
                    {
                        p = q.Right;
                    }
                }
            }
            return ret.ToArray();
        }

        //后序遍历二叉树 左-右-根
        public int[] Postorder()
        {
            List<int> ret = new List<int>();
            Stack<Node> st = new Stack<Node>();
            var p = mRoot;
            while (p != null || st.Count > 0)
            {
                if (p != null)
                {
                    st.Push(p);
                    p = p.Left;
                }
                else
                {
                    var q = st.Peek();
                    if (q.Right != null && q.Right.Value!=ret.Last())
                    {
                        p = q.Right;
                    }
                    else
                    {
                        q = st.Pop();
                        ret.Add(q.Value);
                    }
                }
            }
            return ret.ToArray();
        }

        //深度优先遍历二叉树
        public int[] DeepFirst()
        {
            List<int> ret = new List<int>();
            dfs(mRoot, ret);
            return ret.ToArray();
        }

        void dfs(Node node, List<int> ret)
        {
            if (node == null) return;
            ret.Add(node.Value);
            dfs(node.Left, ret);
            dfs(node.Right, ret);
        }

        //广度优先遍历二叉树
        public int[] BroadFirst()
        {
            List<Node> nodes = new List<Node>();
            List<int> ret = new List<int>();

            if (mRoot == null) return ret.ToArray();
            nodes.Add(mRoot);
            while (nodes.Count > 0)
            {
                Node node = nodes[0];
                nodes.RemoveAt(0);
                ret.Add(node.Value);

                if (node.Left != null)
                {
                    nodes.Add(node.Left);
                }
                if (node.Right != null)
                {
                    nodes.Add(node.Right);
                }
            }
            return ret.ToArray();
        }



        //根据值查找节点
        private Node FindNode(int v)
        {
            return _FindNode(mRoot, v);
        }

        private Node _FindNode(Node node, int v)
        {
            if (node == null)
            {
                return null;
            }
            if (node.Value == v)
            {
                return node;
            }
            var left = _FindNode(node.Left, v);
            if (left != null) return left;

            var right = _FindNode(node.Right, v);
            if (right != null) return right;

            return null;
        }

        //是否存在节点
        public bool Has(int v)
        {
            return _Has(mRoot, v);
        }
        bool _Has(Node node, int v)
        {
            if (node == null)
            {
                return false;
            }
            if (node.Value == v) return true;
            return _Has(node.Left, v) || _Has(node.Right, v);
        }

        public int Height()
        {
            return _height(mRoot);
        }

        int _height(Node node)
        {
            if (node == null) return 0;
            int hl = _height(node.Left) + 1;
            int hr = _height(node.Right) + 1;
            return Math.Max(hl, hr);
        }


        //控制台打印二叉树，输出如下：
        //    1
        //   / \
        //  2   3
        // / \   \
        //4   5   6

        public string Print()
        {
            StringBuilder sb = new StringBuilder();

            if (mRoot != null)
            {
                int h = Height();
                //int w = 1 << h;
                List<Node> nodes = new List<Node>();
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
                            sb.Append(' ');
                        }
                        if (nodes[m] != null)
                        {
                            if (st == 0)
                            {

                                sb.Append(nodes[m].Value);
                            }
                            else
                            {
                                sb.Append(m % 2 > 0 ? '/' : '\\');
                            }
                        }
                        else
                        {
                            sb.Append(' ');
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


        //控制台打印树，输出如下：
        //  1
        //  |- 2
        //  |  |- 4
        //  |  |- 5
        //  |- 3
        //     |- 6
        public string Print2()
        {
            StringBuilder sb = new StringBuilder();
            _print2(mRoot, 0, sb);
            return sb.ToString();
        }

        void _print2(Node node, int depth, StringBuilder sb)
        {
            if (node == null) return;
            for (int i = 0; i < depth; i++)
            {
                if (i < depth - 1) sb.Append("|  ");
                else sb.Append("|- ");
            }
            sb.Append(node.Value);
            sb.AppendLine();
            _print2(node.Left, depth + 1, sb);
            _print2(node.Right, depth + 1, sb);
        }


    }
}
