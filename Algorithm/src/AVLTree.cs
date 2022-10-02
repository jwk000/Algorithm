using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /* AVL树是最早被发明的自平衡二叉查找树。AVL 树得名于它的发明者 G. M. Adelson-Velsky 和 Evgenii Landis。
     * 在AVL树中，任一节点对应的两棵子树的最大高度差为1，因此它也被称为高度平衡树。
     * 查找、插入和删除在平均和最坏情况下的时间复杂度都是O(logn)
     * 参考 https://zhuanlan.zhihu.com/p/56066942
     */
    public class AVLTree
    {
        class AVLNode
        {
            public int Value;
            public AVLNode Left;
            public AVLNode Right;
            public AVLNode(int v)
            {
                Value = v;
            }
        }

        AVLNode mRoot;
        //添加节点
        public void Add(int v)
        {
            AVLNode node = new AVLNode(v);
            if (mRoot == null)
            {
                mRoot = node;
            }
            else
            {
                AddNode(mRoot, node);
            }
        }

        private void AddNode(AVLNode root, AVLNode node)
        {
            if (node.Value >= root.Value)
            {
                if (root.Right == null)
                {
                    root.Right = node;
                }
                else
                {
                    AddNode(root.Right, node);
                }
            }
            else
            {
                if (root.Left == null)
                {
                    root.Left = node;
                }
                else
                {
                    AddNode(root.Left, node);
                }
            }
        }

        //计算某个节点的平衡因子
        private int CalcBalancer(AVLNode root)
        {
             
        }

        //删除节点
        public void Remove(int v)
        {

        }

        //查找节点
        public bool Has(int v)
        {

        }
    }
}
