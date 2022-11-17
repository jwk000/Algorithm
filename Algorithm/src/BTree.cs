using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * B树是一种专用的M阶树，可广泛用于磁盘访问。 M阶树顺序的B树最多可以有m-1个键和M个子树。 使用B树的主要原因之一是它能够在单个节点中存储大量键，并且通过保持树的高度相对较小来存储大键值。
     * 排序M的B树包含M阶树的所有属性。 它还包含以下属性。
     * 1) B树中的每个节点最多包含m个子节点。
     * 2) 除根节点和叶节点外，B树中的每个节点至少包含m/2个子节点。根节点必须至少有2个节点。
     * 3) 所有叶节点必须处于同一级别。
     * 
     */
    public class BTree
    {
        class BNode
        {
            public bool isleaf = true;
            public List<int> keys = new List<int>();
            public List<BNode> children = new List<BNode>();
            public BNode parent = null;

            public override string ToString()
            {
                return String.Join(",", keys.ToArray());
            }
        }

        public readonly int M;
        private BNode Root;

        public BTree(int m)
        {
            M = m;
        }

        //插入始终发生在叶节点
        //插入满的节点会导致节点分裂，中值提升到父节点
        public void Add(int val)
        {
            BNode node = new BNode();
            node.keys.Add(val);
            if (Root == null)
            {
                Root = node;
            }
            else
            {
                var p = Root;
                while (p.isleaf == false)
                {
                    //去下一层插入
                    for (int i = 0; i < p.keys.Count; i++)
                    {
                        if (p.keys[i] == val)
                        {
                            return;//什么也不做
                        }
                        if (p.keys[i] < val)
                        {
                            if (i + 1 == p.keys.Count)
                            {
                                p = p.children[i + 1];
                                break;
                            }
                        }
                        else
                        {
                            p = p.children[i];
                            break;
                        }
                    }
                }
                AddNode(p, val, null, null);
            }
        }

        void AddNode(BNode p, int val, BNode l, BNode r)
        {
            //p是要插入的节点
            int pos = p.keys.Count;
            for (int i = 0; i < p.keys.Count; i++)
            {
                if (val < p.keys[i])
                {
                    pos = i;
                    break;
                }
            }
            p.keys.Insert(pos, val);
            if (p.isleaf == false)
            {
                if (p.children.Count==0)
                {
                    p.children.Add(l);
                    p.children.Add(r);
                }
                else
                {
                    p.children[pos] = l;
                    p.children.Insert(pos + 1, r);
                }
            }

            //满了就分裂
            if (p.keys.Count == M)
            {
                var pp = p.parent;
                if (pp == null)
                {
                    pp = new BNode();
                    Root = pp;
                    pp.isleaf = false;
                }

                var left = new BNode();
                left.parent = pp;
                left.keys.AddRange(p.keys.Take(M / 2));
                if (p.isleaf == false)
                {
                    left.isleaf = false;
                    left.children.AddRange(p.children.Take(M / 2 + 1));
                    foreach(var c in left.children)
                    {
                        c.parent = left;
                    }
                }

                int mid = p.keys[M / 2];

                var right = new BNode();
                right.keys.AddRange(p.keys.Skip(M / 2 + 1));
                right.parent = pp;
                if (p.isleaf == false)
                {
                    right.isleaf = false;
                    right.children.AddRange(p.children.Skip(M / 2 + 1));
                    foreach(var c in right.children)
                    {
                        c.parent = right;
                    }
                }


                AddNode(pp, mid, left, right);
            }
        }

        //删除总是发生在叶节点
        //删除非叶节点时替换成前驱或后继节点
        //删除后如果节点key数量<m/2则合并节点
        public void Remove(int val)
        {
            var p = FindNode(Root, val, out int index);
            if (p == null)
            {
                return;
            }

            RemoveNode(p, index);
        }

        void RemoveNode(BNode node, int index)
        {
            if (node.isleaf)
            {
                node.keys.RemoveAt(index);
                MergeNode(node);
            }
            else
            {
                var next = FindNextNode(node, index);
                node.keys[index] = next.keys[0];
                RemoveNode(next, 0);
            }
        }

        void MergeNode(BNode node)
        {
            if (node.keys.Count < M / 2)
            {
                var parent = node.parent;
                if (parent == null)//根节点
                {
                    return;
                }
                int idx = parent.children.IndexOf(node);
                //先借，借不着才合并
                if (idx > 0)
                {
                    var left = parent.children[idx - 1];//左兄弟
                    if (left.keys.Count > M / 2)
                    {
                        node.keys.Insert(0, parent.keys[idx - 1]);
                        parent.keys[idx - 1] = left.keys.Last();
                        left.keys.RemoveAt(left.keys.Count - 1);
                        if (node.isleaf == false)
                        {
                            node.children.Insert(0, left.children.Last());
                            left.children.RemoveAt(left.children.Count - 1);
                            foreach(var c in node.children)
                            {
                                c.parent = node;
                            }
                        }
                        return;
                    }
                }
                else// idx==0
                {
                    var right = parent.children[idx + 1];//右兄弟
                    if (right.keys.Count > M / 2)
                    {
                        node.keys.Add(parent.keys[idx]);
                        parent.keys[idx] = right.keys[0];
                        right.keys.RemoveAt(0);
                        if (node.isleaf == false)
                        {
                            node.children.Add(right.children[0]);
                            right.children.RemoveAt(0);
                            foreach(var c in node.children)
                            {
                                c.parent = node;
                            }
                        }
                        return;
                    }
                }

                if (idx > 0)
                {
                    //和左兄弟合并
                    var left = parent.children[idx - 1];//左兄弟
                    left.keys.Add(parent.keys[idx - 1]);
                    left.keys.AddRange(node.keys);
                    if (node.isleaf == false)
                    {
                        left.children.AddRange(node.children);
                        foreach(var c in left.children)
                        {
                            c.parent = left;
                        }
                    }
                    parent.keys.RemoveAt(idx - 1);
                    parent.children.RemoveAt(idx);

                    if (parent.keys.Count == 0)//删除空的父节点
                    {
                        left.parent = parent.parent;
                        if (left.parent == null)
                        {
                            Root = left;
                        }
                    }
                    else
                    {
                        MergeNode(parent);
                    }
                }
                else //和右兄弟合并
                {
                    var right = parent.children[idx + 1];//右兄弟
                    node.keys.Add(parent.keys[idx]);
                    node.keys.AddRange(right.keys);
                    if (node.isleaf == false)
                    {
                        node.children.AddRange(right.children);
                        foreach(var c in node.children)
                        {
                            c.parent = node;
                        }
                    }
                    parent.keys.RemoveAt(idx);
                    parent.children.RemoveAt(idx + 1);
                    if (parent.keys.Count == 0)
                    {
                        node.parent = parent.parent;
                        if (node.parent == null)
                        {
                            Root = node;
                        }
                    }
                    else
                    {
                        MergeNode(parent);
                    }
                }
            }
        }

        BNode FindNextNode(BNode node, int index)
        {
            var p = node.children[index + 1];
            while (p.isleaf == false)
            {
                p = p.children[0];
            }
            return p;
        }

        BNode FindPrevNode(BNode node, int index)
        {
            var p = node.children[index];
            while (p.isleaf == false)
            {
                p = p.children.Last();
            }
            return p;
        }



        BNode FindNode(BNode node, int val, out int index)
        {
            index = -1;
            if (node == null)
            {
                return null;
            }
            for (int i = 0; i < node.keys.Count; i++)
            {
                if (node.keys[i] == val)
                {
                    index = i;
                    return node;
                }

                if (node.keys[i] < val)
                {
                    if (i + 1 == node.keys.Count)
                    {
                        if (node.isleaf)
                        {
                            return null;
                        }
                        return FindNode(node.children[i + 1], val, out index);
                    }
                }
                else
                {
                    if (node.isleaf)
                    {
                        return null;
                    }
                    return FindNode(node.children[i], val, out index);
                }
            }
            return null;
        }

        public bool Has(int val)
        {
            return FindNode(Root, val, out int i) != null;
        }

        public void Visit(Action<int> action)
        {
            visit(Root, action);
        }

        void visit(BNode node, Action<int> action)
        {
            if (node == null)
            {
                return;
            }
            for (int i = 0; i < node.keys.Count; i++)
            {
                if (!node.isleaf) visit(node.children[i], action);
                action(node.keys[i]);
            }
            if (!node.isleaf) visit(node.children.Last(), action);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            visit(Root, v => sb.Append(v).Append(' '));
            sb.Length--;
            return sb.ToString();
        }

        public int Count()
        {
            int sum = 0;
            visit(Root, v => sum++);
            return sum;
        }
    }
}
