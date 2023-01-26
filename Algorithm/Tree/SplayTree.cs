using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //伸展树：一种平衡二叉树，假设查找具有局部性，会连续查找同一个元素时使用；
    //每次查找到一个元素，就把它旋转到根节点（旋转不会影响BST的性质）
    public class SplayTree
    {

        class SPNode
        {
            public int Key;
            public int Value;
            public SPNode Parent;
            public SPNode Left;
            public SPNode Right;

            public SPNode(int key,int val)
            {
                Key = key;
                Value = val;
            }
            public override string ToString()
            {
                return Key.ToString();
            }
        }

        SPNode mRoot;

        public void Add(int key, int val)
        {
            var node = new SPNode(key, val);
            if (mRoot == null)
            {
                mRoot = node;
                return;
            }
            var p = mRoot;
            while (p != null)
            {
                if(p.Key == key)
                {
                    return;
                }
                if (p.Key < key)
                {
                    if (p.Right == null)
                    {
                        p.Right = node;
                        node.Parent = p;
                        return;
                    }
                    p = p.Right;
                }
                else
                {
                    if (p.Left == null)
                    {
                        p.Left = node;
                        node.Parent = p;
                        return;
                    }
                    p = p.Left;
                }
            }

        }

        public void Remove(int key)
        {
            var p = mRoot;
            while (p != null)
            {
                if(p.Key == key)
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

        void RemoveNode(SPNode p)
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

        SPNode FindNext(SPNode node)
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

        void LeftRotate(SPNode node)
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

        void RightRotate(SPNode node)
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
            if(R!=null) R.Parent = P;
            P.Parent = node;
            
        }

        void MoveToRoot(SPNode node)
        {
            while (node.Parent != null)
            {
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

        public int Search(int key)
        {
            var p = mRoot;
            while (p != null)
            {
                if(p.Key == key)
                {
                    MoveToRoot(p);
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
