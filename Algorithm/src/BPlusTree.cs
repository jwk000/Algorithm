using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * B+树每个节点m阶，节点key数量 m/2-m个，除根节点外非叶子节点的key数量和子节点数量一致；
     * 数据只存放在叶子节点上，叶子节点之间单链表连接，方便范围查询；
     */
    public class BPlusTree
    {
        class BPNode
        {
            public List<int> keys = new List<int>();
            public List<BPNode> children = new List<BPNode>();
            public BPNode parent = null;
            public bool isleaf = true;
            public List<object> values = new List<object>();//只有叶子节点有值
            public BPNode prev;//只有叶子节点有
            public BPNode next;//只有叶子节点有

            public override string ToString()
            {
                return String.Join(",", keys);
            }
        }


        public readonly int M;//阶
        private BPNode Root;//根

        public BPlusTree(int m)
        {
            M = m;
        }

        //总是从叶节点插入，但需要修改非叶节点的key范围
        public void Add(int key, object value)
        {

            if (Root == null)
            {
                Root = new BPNode();
                Root.keys.Add(key);
                Root.values.Add(value);
            }
            else
            {
                var p = Root;
                while (p.isleaf == false)
                {
                    for (int i = 0; i < p.keys.Count; i++)
                    {
                        if (key <= p.keys[i])
                        {
                            p = p.children[i];
                            break;
                        }
                        if (i == p.keys.Count - 1)
                        {
                            p.keys[i] = key;
                            p = p.children[i];
                            break;
                        }
                    }
                }
                //p是叶节点
                int pos = p.keys.Count;
                for (int i = 0; i < p.keys.Count; i++)
                {
                    if (p.keys[i] == key)
                    {
                        throw new Exception("B+ tree has key " + key);
                    }
                    else if (p.keys[i] > key)
                    {
                        pos = i;
                        break;
                    }
                }
                p.keys.Insert(pos, key);
                p.values.Insert(pos, value);

                //分裂
                SpliteNode(p);
            }
        }

        void SpliteNode(BPNode p)
        {
            if (p.keys.Count > M)
            {
                var pp = p.parent;
                if (pp == null)
                {
                    pp = new BPNode();
                    pp.isleaf = false;
                    Root = pp;
                }
                BPNode left = new BPNode();
                BPNode right = new BPNode();

                left.parent = pp;
                left.isleaf = p.isleaf;
                left.keys.AddRange(p.keys.Take(M / 2));
                if (p.isleaf)
                {
                    left.values.AddRange(p.values.Take(M / 2));
                    left.prev = p.prev;
                    left.next = right;
                    if(p.prev!=null)p.prev.next = left;
                }
                else
                {
                    left.children.AddRange(p.children.Take(M / 2));
                    foreach (var c in left.children)
                    {
                        c.parent = left;
                    }
                }

                right.parent = pp;
                right.isleaf = p.isleaf;
                right.keys.AddRange(p.keys.Skip(M / 2));
                if (p.isleaf)
                {
                    right.values.AddRange(p.values.Skip(M / 2));
                    right.prev = left;
                    right.next = p.next;
                    if(p.next!=null)p.next.prev = right;
                }
                else
                {
                    right.children.AddRange(p.children.Skip(M / 2));
                    foreach (var c in right.children)
                    {
                        c.parent = right;
                    }
                }

                if (pp.keys.Count==0)
                {
                    pp.keys.Add(left.keys.Last());
                    pp.keys.Add(right.keys.Last());
                    pp.children.Add(left);
                    pp.children.Add(right);
                }
                else
                {
                    int pos = pp.keys.IndexOf(p.keys.Last());
                    pp.keys.Insert(pos, left.keys.Last());
                    pp.children[pos] = right;
                    pp.children.Insert(pos, left);

                    SpliteNode(pp);
                }
            }
        }


        //移除一个key，总是在叶子节点移除，如果移除的是边界值需要更新父节点边界值
        //移除key会导致节点key数量<m/2，此时需要借兄弟节点的边界值，或者和兄弟节点合并
        public void Remove(int key)
        {
            if (Root == null)
            {
                return;
            }

            BPNode node = FindNode(key);
            int index = node.keys.IndexOf(key);
            if (index >= 0)
            {
                int last = node.keys.Count - 1;
                node.keys.RemoveAt(index);
                node.values.RemoveAt(index);
                if (index == last)
                {
                    int newkey = node.keys.Last();
                    var pp = node.parent;
                    while (pp != null)
                    {
                        int idx = pp.keys.IndexOf(key);
                        if (idx == -1)
                        {
                            break;
                        }
                        pp.keys[idx] = newkey;
                        pp = pp.parent;
                    }
                }

                MergeNode(node);
            }
        }

        void MergeNode(BPNode node)
        {
            if (node.keys.Count < M / 2)
            {
                var pp = node.parent;
                if (pp == null)
                {
                    return;//node是根节点
                }
                //根节点至少包含2个子节点，否则不能作为根
                if(pp == Root && pp.keys.Count<2)
                {
                    Root = node;
                    node.parent = null;
                    return;
                }
                int pos = pp.children.IndexOf(node);
                if (pos == 0)
                {

                    //右兄弟
                    BPNode right = pp.children[pos + 1];
                    if (right.keys.Count > M / 2)
                    {
                        int newkey = right.keys[0];
                        node.keys.Add(newkey);
                        right.keys.RemoveAt(0);

                        if (node.isleaf)
                        {
                            node.values.Add(right.values[0]);
                            right.values.RemoveAt(0);
                        }
                        else
                        {
                            var newnode = right.children[0];
                            node.children.Add(newnode);
                            right.children.RemoveAt(0);
                            newnode.parent = node;
                        }

                        int oldkey = pp.keys[pos];
                        pp.keys[pos] = newkey;
                        while (pp.parent != null)
                        {
                            pp = pp.parent;
                            pos = pp.keys.IndexOf(oldkey);
                            if (pos == -1)
                            {
                                break;
                            }
                            pp.keys[pos] = newkey;
                        }
                        return;//借成功了
                    }

                }
                else //pos>0
                {
                    //左兄弟
                    BPNode left = pp.children[pos - 1];
                    if (left.keys.Count > M / 2)
                    {
                        int oldkey = left.keys.Last();
                        node.keys.Insert(0, oldkey);
                        left.keys.RemoveAt(left.keys.Count - 1);

                        if (node.isleaf)
                        {
                            node.values.Insert(0, left.values.Last());
                            left.values.RemoveAt(left.values.Count - 1);
                        }
                        else
                        {
                            var oldnode = left.children.Last();
                            node.children.Insert(0, oldnode);
                            left.children.RemoveAt(left.children.Count - 1);
                            oldnode.parent = node;
                        }


                        int newkey = left.keys.Last();
                        pp.keys[pos - 1] = newkey;
                        while (pp.parent != null)
                        {
                            pp = pp.parent;
                            pos = pp.keys.IndexOf(oldkey);
                            if (pos == -1)
                            {
                                break;
                            }
                            pp.keys[pos] = newkey;
                        }
                        return;//借成功了
                    }
                }

                //借失败了，合并兄弟节点
                if (pos == 0)
                {
                    //右兄弟
                    BPNode right = pp.children[pos + 1];
                    node.keys.AddRange(right.keys);
                    pp.keys[pos] = right.keys.Last();
                    pp.keys.RemoveAt(pos + 1);

                    if (node.isleaf)
                    {
                        node.values.AddRange(right.values);
                        node.next = right.next;
                        if (right.next != null) right.next.prev = node;
                    }
                    else
                    {
                        node.children.AddRange(right.children);
                        foreach(var c in right.children)
                        {
                            c.parent = node;
                        }
                    }
                    pp.children.RemoveAt(pos + 1);

                    //pp的key减少了
                    MergeNode(pp);
                }
                else
                {
                    //左兄弟
                    BPNode left = pp.children[pos - 1];
                    left.keys.AddRange(node.keys);
                    pp.keys[pos - 1] = node.keys.Last();
                    pp.keys.RemoveAt(pos);

                    if (node.isleaf)
                    {
                        left.values.AddRange(node.values);
                        left.next = node.next;
                        if (node.next != null) node.next.prev = left;
                    }
                    else
                    {
                        left.children.AddRange(node.children);
                        foreach(var c in node.children)
                        {
                            c.parent = left;
                        }
                    }
                    pp.children.RemoveAt(pos);

                    MergeNode(pp);
                }
            }
        }

        

        //查找范围包含key的叶子节点
        private BPNode FindNode(int key)
        {
            if (Root == null)
            {
                return null;
            }
            var p = Root;
            if (key > Root.keys.Last())
            {
                while (p.isleaf == false)
                {
                    p = p.children.Last();
                }
            }
            else
            {
                while (p.isleaf == false)
                {
                    for (int i = 0; i < p.keys.Count; i++)
                    {
                        if (key <= p.keys[i])
                        {
                            p = p.children[i];
                            break;
                        }
                    }
                }
            }

            return p;
        }




        //单值查询
        public object Search(int key)
        {
            BPNode node = FindNode(key);
            if (node != null)
            {
                for (int i = 0; i < node.keys.Count; i++)
                {
                    if (key == node.keys[i])
                    {
                        return node.values[i];
                    }
                    if (key < node.keys[i])
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        //范围查询[min,max]
        public List<object> SearchRange(int min, int max)
        {
            List<object> ret = new List<object>();
            BPNode minNode = FindNode(min);
            BPNode maxNode = FindNode(max);
            if (minNode != null && maxNode != null)
            {
                for (int i = 0; i < minNode.keys.Count; i++)
                {
                    if (min <= minNode.keys[i])
                    {
                        ret.AddRange(minNode.values.GetRange(i, minNode.keys.Count - i));
                        var p = minNode.next;
                        while (p != maxNode)
                        {
                            ret.AddRange(p.values);
                            p = p.next;
                        }
                        for (int j = 0; j < maxNode.keys.Count; j++)
                        {
                            if (maxNode.keys[j] <= max)
                            {
                                ret.Add(maxNode.keys[j]);
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return ret;
        }

        public override string ToString()
        {
            if (Root == null)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            var p = Root;
            while (p.isleaf == false)
            {
                p = p.children[0];
            }

            sb.Append(p.ToString()).Append(' ');
            while (p.next != null)
            {
                p = p.next;
                sb.Append(p.ToString()).Append(' ');
            }
            sb.Length--;
            return sb.ToString();
        }
    }
}
