using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Algorithm
{
    /* 红黑树的性质：
     *  1）每个结点要么是红的，要么是黑的。
     *  2）根结点是黑的。
     *  3）每个叶结点，即空结点（NIL）是黑的。
     *  4）如果一个结点是红的，那么它的俩个儿子都是黑的。
     *  5）对每个结点，从该结点到其子孙结点的所有路径上包含相同数目的黑结点。
     *  
     *  也就是说，红黑树的黑色节点数量最多是红色节点的2倍；
     */

    public class RedBlackTree : IEnumerable
    {

        class RBNode
        {
            public RBNode Parent;
            public RBNode Left;
            public RBNode Right;
            public int Value;
            public int Color;//0红色 1黑色

            public RBNode(int v, int color)
            {
                Value = v;
                Color = color;
            }

            public override string ToString()
            {
                return $"{Value}|{(Color == 0 ? 'R' : 'B')}";
            }
        }

        RBNode mRoot;
        const int RED = 0;
        const int BLACK = 1;

        //I是当前节点，P是父节点，PP是祖父节点，S是叔叔节点
        public void Add(int val)
        {
            Console.WriteLine($"Add({val})");
            if (mRoot == null)//1.红黑树为空
            {
                mRoot = new RBNode(val, BLACK); //根节点黑色
            }
            else
            {
                var I = mRoot;
                var P = I; //p是最终的父节点
                while (I != null)
                {
                    if (I.Value == val) //2.key已存在
                    {
                        return;
                    }
                    if (val < I.Value)
                    {
                        P = I;
                        I = I.Left;
                    }
                    else
                    {
                        P = I;
                        I = I.Right;
                    }
                }

                var node = new RBNode(val, RED);//插入节点设为红色
                if (P.Value > node.Value)
                {
                    P.Left = node;
                    node.Parent = P;
                }
                else
                {
                    P.Right = node;
                    node.Parent = P;
                }
                AddFix(node);

            }
            Console.WriteLine(Print());

        }

        void AddFix(RBNode I)
        {
            RBNode P = I.Parent;
            if (P == null)//说明是根节点
            {
                I.Color = BLACK;
                return;
            }
            if (P.Color == BLACK)//3.父节点黑色不用处理
            {
                return;
            }
            else //4.父节点红色，必然存在祖父节点
            {
                RBNode PP = P.Parent;
                RBNode S = PP.Left;
                if (PP.Left == P)
                {
                    S = PP.Right;
                }

                if (S != null && S.Color == RED) //4.1叔叔节点是红色，节点数量平衡，只改颜色即可
                {
                    P.Color = BLACK;//父节点涂黑
                    S.Color = BLACK;//叔叔节点涂黑
                    PP.Color = RED;//祖父节点涂红
                    AddFix(PP);//祖父节点再平衡
                }
                else //4.2叔叔节点不存在或是黑色，此时左右不平衡，只改颜色不行，需要旋转
                {
                    if (P == PP.Left)//4.2.1 父节点是祖父节点的左节点
                    {
                        if (I == P.Left) //4.2.1.1 当前节点是父节点的左节点
                        {
                            P.Color = BLACK;//父节点涂黑
                            PP.Color = RED;//祖父节点涂红
                            RightRotate(PP);//祖父节点右旋
                        }
                        else //4.2.1.2 当前节点是父节点的右节点，左旋成4.2.1.1的情况
                        {
                            LeftRotate(P);
                            AddFix(P);
                        }
                    }
                    else //4.2.2 父节点是祖父节点的右节点
                    {
                        if (I == P.Right) //4.2.2.1 右右
                        {
                            P.Color = BLACK;
                            PP.Color = RED;
                            LeftRotate(PP);
                        }
                        else //4.2.2.2 右左
                        {
                            RightRotate(P);
                            AddFix(P);
                        }
                    }
                }
            }

        }

        void LeftRotate(RBNode root)
        {
            var p = root.Right;
            if (p == null)
            {
                return;
            }

            root.Right = p.Left;
            if (p.Left != null) p.Left.Parent = root;
            p.Parent = root.Parent;
            p.Left = root;

            if (root.Parent != null)
            {
                if (root == root.Parent.Left)
                {
                    root.Parent.Left = p;
                }
                else
                {
                    root.Parent.Right = p;
                }
            }
            else
            {
                mRoot = p;
            }
            root.Parent = p;

        }

        void RightRotate(RBNode root)
        {
            var p = root.Left;
            if (p == null)
            {
                return;
            }
            root.Left = p.Right;
            if (p.Right != null) p.Right.Parent = root;
            p.Parent = root.Parent;
            p.Right = root;

            if (root.Parent != null)
            {
                if (root == root.Parent.Left)
                {
                    root.Parent.Left = p;
                }
                else
                {
                    root.Parent.Right = p;
                }
            }
            else
            {
                mRoot = p;
            }
            root.Parent = p;

        }

        public void Remove(int val)
        {
            Console.WriteLine($"Remove({val})");
            RBNode I = FindNode(mRoot, val);
            if (I == null)
            {
                return;
            }

            Remove(I);
            Console.WriteLine(Print());
        }
        //I是要删除的节点，P是I的父节点，S是I的兄弟节点，SL是兄弟节点的左节点，SR是兄弟节点的右节点
        //删除I时如果I是叶节点就删除，否则用后继节点R替换，R节点总是叶节点；R替换I，删除的是R；
        //I移除后会导致所在的子树不平衡，需要调整
        void Remove(RBNode I)
        {
            RBNode P = I.Parent;
            //1.I没有子节点，直接删除
            if (I.Left == null && I.Right == null)
            {
                if (P.Left == I) { P.Left = null; }
                else { P.Right = null; }
                //1.1 I是红色，直接删除 I是黑色，需要平衡
                if (I.Color == BLACK)
                {
                    RemoveFix(I);
                }
                return;
            }
            //2.I有一个子节点C，C替换I，删除C
            if (I.Left == null && I.Right != null)
            {
                I.Value = I.Right.Value;
                Remove(I.Right);
                return;
            }
            if (I.Right == null && I.Left != null)
            {
                I.Value = I.Left.Value;
                Remove(I.Left);
                return;
            }
            //3.I有2个子节点，I替换为后继节点N，删除N
            RBNode next = FindNextNode(I);
            I.Value = next.Value;
            Remove(next);

        }

        void RemoveFix(RBNode I)
        {
            RBNode P = I.Parent;
            if (P.Left == I) //1.2.1 I是左节点
            {
                RBNode S = P.Right;
                RBNode SR = S.Right;
                RBNode SL = S.Left;

                if (S.Color == RED) //1.2.1.1 S是红色，则P和SL，SR一定都是黑色
                {
                    S.Color = BLACK;
                    P.Color = RED;
                    LeftRotate(P);
                    P.Left = null;
                }
                else //1.2.1.2 S是黑色
                {

                    if (SR != null && SR.Color == RED) //1.2.1.2.1 SR是红色
                    {
                        S.Color = P.Color;
                        P.Color = BLACK;
                        SR.Color = BLACK;
                        LeftRotate(P);
                        P.Left = null;
                    }
                    else if ((SR == null || SR.Color == BLACK) && (SL != null && SL.Color == RED)) // 1.2.1.2.2 SR黑色 SL红色
                    {
                        S.Color = RED;
                        SL.Color = BLACK;
                        RightRotate(S);
                        Remove(I);
                    }
                    else //1.2.1.2.3 SL黑色 SR黑色
                    {
                        S.Color = RED;
                        P.Left = null;
                        RemoveFix(P);
                    }
                }
            }
            else //1.2.2 I是右节点
            {
                //P.Right = null;
                RBNode S = P.Right;
                RBNode SR = S.Right;
                RBNode SL = S.Left;
                if (S.Color == RED)
                {
                    S.Color = BLACK;
                    P.Color = RED;
                    RightRotate(P);
                    P.Right = null;
                }
                else
                {
                    if (SL == null && SR == null)
                    {
                        P.Right = null;//直接删除
                        return;
                    }
                    if (SL.Color == RED)
                    {
                        S.Color = P.Color;
                        P.Color = BLACK;
                        SL.Color = BLACK;
                        RightRotate(P);
                        P.Right = null;
                    }
                    else if (SL.Color == BLACK && SR.Color == RED)
                    {
                        S.Color = RED;
                        SR.Color = BLACK;
                        LeftRotate(S);
                        Remove(I);
                    }
                    else
                    {
                        S.Color = RED;
                        Remove(P);
                    }
                }
            }
        }


        //寻找后继节点
        RBNode FindNextNode(RBNode node)
        {
            if (node.Right == null)
            {
                return null;
            }
            var p = node.Right;
            while (p.Left != null)
            {
                p = p.Left;
            }
            return p;
        }

        //寻找前驱节点
        RBNode FindPrevNode(RBNode node)
        {
            if (node.Left == null)
            {
                return null;
            }
            var p = node.Left;
            while (p.Right != null)
            {
                p = p.Right;
            }
            return p;
        }

        public bool Has(int val)
        {
            var p = FindNode(mRoot, val);
            return p != null;
        }

        RBNode FindNode(RBNode node, int val)
        {
            if (node == null) return null;
            if (node.Value == val)
            {
                return node;
            }
            if (node.Value < val)
            {
                return FindNode(node.Right, val);
            }
            else
            {
                return FindNode(node.Left, val);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var v in this)
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

        int _height(RBNode node)
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
                List<RBNode> nodes = new List<RBNode>();
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


        #region 迭代器
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        class Enumerator : IEnumerator
        {
            RBNode root;
            Stack<RBNode> stack = new Stack<RBNode>();
            public Enumerator(RedBlackTree tree)
            {
                root = tree.mRoot;
                push(root);
            }
            public RBNode Current { get; protected set; }

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (stack.Count == 0)
                {
                    return false;
                }
                Current = stack.Pop();
                if (Current.Right != null)
                {
                    push(Current.Right);
                }
                return true;
            }

            public void Reset()
            {
                stack.Clear();
                push(root);
            }

            void push(RBNode node)
            {
                stack.Push(node);
                while (node.Left != null)
                {
                    node = node.Left;
                    stack.Push(node);
                }
            }
        }
        #endregion
    }
}
