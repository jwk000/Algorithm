using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /* AVL树是最早被发明的自平衡二叉查找树。
     * 在AVL树中，任一节点对应的两棵子树的最大高度差为1，因此它也被称为高度平衡树。
     * 查找、插入和删除在平均和最坏情况下的时间复杂度都是O(logn)
     */
    public class AVLTree : IEnumerable
    {
        class AVLNode
        {
            public int Value;
            public AVLNode Left;
            public AVLNode Right;
            public AVLNode Parent;
            public int Factor;//平衡因子
            public AVLNode(int v)
            {
                Value = v;
                Factor = 0;
            }
            public override string ToString()
            {
                return $"{Value}|{Factor}";
            }
        }

        AVLNode mRoot;
        //添加节点
        public void Add(int v)
        {
            Console.WriteLine($"Add({v}):----------------");
            AVLNode node = new AVLNode(v);
            if (mRoot == null)
            {
                mRoot = node;
            }
            else
            {
                AddNode(mRoot, node);
            }
            Console.WriteLine(Print());

        }

        void AddNode(AVLNode cur, AVLNode node)
        {
            if (node.Value == cur.Value)
            {
                return;
            }
            if (node.Value > cur.Value)
            {
                if (cur.Right == null)
                {
                    cur.Right = node;
                    node.Parent = cur;
                    cur.Factor++;
                }
                else
                {
                    int before = cur.Right.Factor;
                    AddNode(cur.Right, node);
                    if (before == 0 && Math.Abs(cur.Right.Factor) == 1) cur.Factor++;
                }
            }
            else
            {
                if (cur.Left == null)
                {
                    cur.Left = node;
                    node.Parent = cur;
                    cur.Factor--;
                }
                else
                {
                    int before = cur.Left.Factor;
                    AddNode(cur.Left, node);
                    if (before == 0 && Math.Abs(cur.Left.Factor) == 1) cur.Factor--;
                }
            }
            Balance(cur);
        }

        void Balance(AVLNode root)
        {
            if (root == null)
            {
                return;
            }

            if (root.Factor == 2)
            {
                if (root.Right.Factor == 1)
                {
                    Console.WriteLine($"balance({root}):RotateRR()");
                    RotateRR(root);
                }
                else if (root.Right.Factor == -1)
                {
                    Console.WriteLine($"balance({root}):RotateRL()");
                    RotateRL(root);
                }
                else
                {
                    Console.WriteLine($"balance({root}):RotateRE()");
                    RotateRE(root);
                }
            }
            else if (root.Factor == -2)
            {
                if (root.Left.Factor == -1)
                {
                    Console.WriteLine($"balance({root}):RotateLL()");
                    RotateLL(root);
                }
                else if (root.Left.Factor == 1)
                {
                    Console.WriteLine($"balance({root}):RotateLR()");
                    RotateLR(root);
                }
                else
                {
                    Console.WriteLine($"balance({root}):RotateLE()");
                    RotateLE(root);
                }
            }
        }


        //LL需要右旋，此时root.bf=-2 root.left.bf=-1
        //旋转后root变成root.left的右节点，root.left变成root.parent
        //此时 root.bf=0 root.parent.bf=0
        void RotateLL(AVLNode root)
        {
            root.Left.Parent = root.Parent;
            if (root.Parent != null)
            {
                if (root.Parent.Left == root)
                {
                    root.Parent.Left = root.Left;
                }
                else
                {
                    root.Parent.Right = root.Left;
                }
            }
            if (root == mRoot)
            {
                mRoot = root.Left;
            }
            var t = root.Left.Right;
            root.Left.Right = root;
            root.Parent = root.Left;
            root.Left = t;
            if (t != null) t.Parent = root;
            root.Factor = 0;
            root.Parent.Factor = 0;
        }

        //RR需要左旋，此时root.bf=2 root.right.bf=1
        //旋转后，root变成root.right左节点，root.right变成root.parent
        //此时，root.bf=0 root.right.bf=0
        void RotateRR(AVLNode root)
        {
            root.Right.Parent = root.Parent;
            if (root.Parent != null)
            {
                if (root.Parent.Left == root)
                {
                    root.Parent.Left = root.Right;
                }
                else
                {
                    root.Parent.Right = root.Right;
                }
            }
            if (root == mRoot)
            {
                mRoot = root.Right;
            }
            var t = root.Right.Left;
            root.Right.Left = root;
            root.Parent = root.Right;
            root.Right = t;
            if (t != null) t.Parent = root;
            root.Factor = 0;
            root.Parent.Factor = 0;
        }

        //LR需要先把左子树视为RR左旋变成LL再右旋
        //旋转前，root.bf=-2 root.left.bf=1
        //旋转后，root.bf=0 root.left.bf=0
        void RotateLR(AVLNode root)
        {
            RotateRR(root.Left);
            RotateLL(root);
        }

        //RL需要先把右子树视为LL右旋变成RR再左旋
        void RotateRL(AVLNode root)
        {
            RotateLL(root.Right);
            RotateRR(root);
        }

        //删除节点会出现RE的情况，旋转和RR相同，但平衡因子不同
        void RotateRE(AVLNode root)
        {
            RotateRR(root);
            root.Factor = 1;
            root.Parent.Factor = -1;
        }

        //删除节点会出现LE的情况，旋转和LL相同，但平衡因子不同
        void RotateLE(AVLNode root)
        {
            RotateLL(root);
            root.Factor = -1;
            root.Parent.Factor = 1;
        }


        //删除节点
        //（1）被删除节点是叶子节点，直接删除
        //（2）被删除节点有右子树，将后继节点上提，再递归删除后继节点
        //（3）被删除节点只有左子树，将前驱节点上提，在递归删除前驱节点
        public void Remove(int v)
        {
            Console.WriteLine($"Remove({v}):===========");
            RemoveNode(mRoot, v);
            Console.WriteLine(Print());
        }

        //删除操作必须递归进行，上层判断平衡因子调整平衡
        void RemoveNode(AVLNode node, int v)
        {
            if (node == null)
            {
                return;
            }
            if (node.Value == v)
            {
                //删除node
                RemoveLeafNode(node);
            }
            else if (node.Value < v)
            {
                if (node.Right != null)
                {
                    int bf = node.Right.Factor;
                    RemoveNode(node.Right, v);
                    if (node.Right == null || (Math.Abs(bf) == 1 && node.Right.Factor == 0))
                    {
                        node.Factor--;
                        Balance(node);
                    }
                }
            }
            else
            {
                if (node.Left != null)
                {
                    int bf = node.Left.Factor;
                    RemoveNode(node.Left, v);
                    if (node.Left == null || (Math.Abs(bf) == 1 && node.Left.Factor == 0))
                    {
                        node.Factor++;
                        Balance(node);
                    }
                }
            }
        }

        void RemoveLeafNode(AVLNode node)
        {
            if (node.Right != null)
            {
                int bf = node.Right.Factor;
                RemoveNextNode(node, node.Right);
                if (node.Right == null || (Math.Abs(bf) == 1 && node.Right.Factor == 0))
                {
                    node.Factor--;
                    Balance(node);
                }
            }
            else if (node.Left != null)
            {
                int bf = node.Left.Factor;
                RemovePrevNode(node, node.Left);
                if (node.Left == null || (Math.Abs(bf) == 1 && node.Left.Factor == 0))
                {
                    node.Factor++;
                    Balance(node);
                }
            }
            else
            {
                var p = node.Parent;
                node.Parent = null;
                if (p != null)
                {
                    if (p.Left == node)
                    {
                        p.Left = null;
                    }
                    else
                    {
                        p.Right = null;
                    }
                }
                else //没有父节点是根节点
                {
                    mRoot = null;
                }
            }
        }

        void RemoveNextNode(AVLNode node, AVLNode p)
        {
            if (p.Left != null)
            {
                int bf = p.Left.Factor;
                RemoveNextNode(node, p.Left);
                if (p.Left == null || (Math.Abs(bf) == 1 && p.Left.Factor == 0))
                {
                    p.Factor++;
                    Balance(p);
                }
            }
            else
            {
                node.Value = p.Value;
                RemoveLeafNode(p);
            }
        }

        void RemovePrevNode(AVLNode node, AVLNode p)
        {
            if (p.Right != null)
            {
                int bf = p.Right.Factor;
                RemovePrevNode(node, p.Right);
                if (p.Right == null || (Math.Abs(bf) == 1 && p.Right.Factor == 0))
                {
                    p.Factor--;
                    Balance(p);
                }
            }
            else
            {
                node.Value = p.Value;
                RemoveLeafNode(p);
            }
        }



        //查找节点
        public bool Has(int v)
        {
            return FindNode(v) != null;
        }

        private AVLNode FindNode(int v)
        {
            var root = mRoot;
            while (root != null)
            {
                if (root.Value == v)
                {
                    return root;
                }
                if (v > root.Value)
                {
                    root = root.Right;
                }
                else
                {
                    root = root.Left;
                }
            }
            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int v in this)
            {
                sb.Append(v).Append(' ');
            }
            sb.Length--;
            Console.WriteLine(sb);
            return sb.ToString();
        }
        public int Height()
        {
            return _height(mRoot);
        }

        int _height(AVLNode node)
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
                //int w = 1 << h;
                List<AVLNode> nodes = new List<AVLNode>();
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
                            sb.Append(" ");
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
                            sb.Append(" ");
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

        public IEnumerator<int> GetEnumerator()
        {
            return new AVLTreeEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        class AVLTreeEnumerator : IEnumerator<int>
        {
            AVLTree mTree;
            AVLNode mCurr;
            Stack<AVLNode> mStack = new Stack<AVLNode>();
            public AVLTreeEnumerator(AVLTree tree)
            {
                mTree = tree;
                Reset();
            }

            public int Current => mCurr.Value;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                mTree = null;
                mCurr = null;
                mStack.Clear();
                mStack = null;
            }

            public bool MoveNext()
            {
                if (mStack.Count == 0)
                {
                    return false;
                }
                var p = mStack.Pop();
                mCurr = p;
                if (p.Right != null)
                {
                    p = p.Right;
                    while (p != null)
                    {
                        mStack.Push(p);
                        p = p.Left;
                    }
                }
                return true;
            }

            public void Reset()
            {
                var p = mTree.mRoot;
                while (p != null)
                {
                    mStack.Push(p);
                    p = p.Left;
                }
            }
        }
    }
}
