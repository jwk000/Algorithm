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
                return Key.ToString();
            }
        }

        Random mRander = new Random();
        TPNode mRoot;

        public void Add(int key, int val)
        {
            var node = new TPNode(key, val);
            node.Priority = mRander.Next(0,1000);
            if (mRoot == null)
            {
                mRoot = node;
                return;
            }
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
                    RightRotate(node);
                }
                else
                {
                    LeftRotate(node);
                }
            }
            mRoot = node;
        }

        //删除的永远是叶子节点，不影响heap性质
        public void Remove(int key)
        {
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
        void RemoveNode(TPNode p)
        {
            if (p.Parent != null)
            {
                if (p == p.Parent.Left)
                {
                    p.Parent.Left = null;
                }
                else
                {
                    p.Parent.Right = null;
                }
            }
            else
            {
                mRoot = null;
            }

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
            var L = node.Left;
            var PP = P.Parent;
            if (PP != null)
            {
                if (PP.Left == P) PP.Left = node;
                else PP.Right = node;
            }
            node.Parent = P.Parent;
            node.Left = P;
            P.Right = L;
            if (L != null) L.Parent = P;
            P.Parent = node;
        }

        void RightRotate(TPNode node)
        {
            var P = node.Parent;
            var R = node.Right;
            var PP = P.Parent;
            if (PP != null)
            {
                if (PP.Left == P) PP.Left = node;
                else PP.Right = node;
            }
            node.Parent = P.Parent;
            node.Right = P;
            P.Left = R;
            if (R != null) R.Parent = P;
            P.Parent = node;

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



    }
}
