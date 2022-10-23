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
                return $"{Value}|{(Color==0?'R':'B')}";
            }
        }

        RBNode mRoot;


        public void Add(int val)
        {
            if (mRoot == null)
            {
                mRoot = new RBNode(val, 1); //根节点黑色
            }
            else
            {
                var p = mRoot;
                var pp = p; //pp是最终的父节点
                while (p != null)
                {
                    if (p.Value == val)
                    {
                        return;
                    }
                    if (val < p.Value)
                    {
                        pp = p;
                        p = p.Left;
                    }
                    else
                    {
                        pp = p;
                        p = p.Right;
                    }
                }

                var node = new RBNode(val, 0);//插入节点红色
                if (pp.Value > node.Value)
                {
                    pp.Left = node;
                    node.Parent = pp;
                    if (pp.Color == 1)//父节点黑色不用处理
                    {
                        return;
                    }
                    else
                    {
                        //
                    }
                }
                else
                {
                    pp.Right = node;
                    node.Parent = pp;
                }
            }
        }



        public void Remove(int val)
        {
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
