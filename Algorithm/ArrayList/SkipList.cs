using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //跳表
    public class SkipList<T> : IEnumerable<T> where T : IComparable
    {

        class SkipListNode
        {
            public bool IsEmpty;
            public T Value;
            public SkipListNode Next;
            public SkipListNode Prev;
            public SkipListNode NextLevel;
            public SkipListNode()
            {
                IsEmpty = true;
            }

            public SkipListNode(T e)
            {
                Value = e;
                IsEmpty = false;
            }
            public override string ToString()
            {
                return Value.ToString();
            }
        }

        SkipListNode mHead;
        SkipListNode mHead0; //0层的起始点
        Random mRandor = new Random();
        public int Depth { get; private set; }
        public int Size { get; private set; }
        public SkipList()
        {
            mHead = new SkipListNode();
            mHead0 = mHead;
            Depth = 0;
        }

        public void Add(T e)
        {
            var p = mHead;
            List<SkipListNode> indexs = new List<SkipListNode>();
            while (p != null)
            {
                if (p.Next != null && p.Next.Value.CompareTo(e) <= 0)
                {
                    p = p.Next;
                    continue;
                }
                if (p.NextLevel == null)
                {
                    break;
                }

                indexs.Add(p);
                p = p.NextLevel;

            }

            StringBuilder sb = new StringBuilder();
            indexs.ForEach(n => sb.Append($"{n},"));
            if (sb.Length > 0) sb.Length--;
            Console.WriteLine($"Add({e}) indexs={indexs.Count}[{sb}] befor add depth={Depth}");

            SkipListNode node = new SkipListNode(e);
            node.Next = p.Next;
            node.Prev = p;
            if (p.Next != null) p.Next.Prev = node;
            p.Next = node;


            for (int i = 0; i < Depth; i++)
            {
                if (NeedIndex())
                {
                    SkipListNode nodeIdx = new SkipListNode(e);
                    nodeIdx.NextLevel = node;
                    var levelIdx = indexs[Depth - i - 1];
                    nodeIdx.Next = levelIdx.Next;
                    nodeIdx.Prev = levelIdx;
                    if (levelIdx.Next != null)
                    {
                        levelIdx.Next.Prev = nodeIdx;
                    }
                    levelIdx.Next = nodeIdx;
                    node = nodeIdx;
                }
                else
                {
                    break;
                }
            }

            Size++;
            int depth = CalcDepth();
            if (depth > Depth)
            {
                Depth = depth;
                AddDepth();
            }

            Console.WriteLine($"Add({e}) After Add Depth={depth}");
            Console.WriteLine(ShowAllNodes());

        }

        //计算新的深度（log2N）
        int CalcDepth()
        {
            int n = 0;
            for (int i = 2; i < 32; i++)
            {
                if ((Size & (1 << i)) != 0)
                {
                    n = i - 1;
                }
            }
            return n;

        }

        bool NeedIndex()
        {
            int r = mRandor.Next(10000);
            return r < 4000;
        }

        //新增一层
        void AddDepth()
        {
            SkipListNode head = new SkipListNode();
            head.NextLevel = mHead;
            var p = mHead.Next;
            mHead = head;
            var q = mHead;
            while (p != null)
            {
                if (NeedIndex())
                {
                    SkipListNode node = new SkipListNode(p.Value);
                    q.Next = node;
                    node.Prev = q;
                    node.NextLevel = p;
                    q = node;
                }
                p = p.Next;
            }
        }



        public void Remove(T e)
        {
            SkipListNode node = null;
            var p = mHead;
            while (p != null)
            {
                if (p.Next != null)
                {
                    if (p.Next.Value.CompareTo(e) == 0)
                    {
                        node = p.Next;
                        break;
                    }
                    if (p.Next.Value.CompareTo(e) < 0)
                    {
                        p = p.Next;
                        continue;
                    }
                }

                p = p.NextLevel;
            }

            while (node != null)
            {
                node.Prev.Next = node.Next;
                if (node.Next != null) node.Next.Prev = node.Prev;
                node = node.NextLevel;
            }
            Size--;
            int depth = CalcDepth();
            if (depth < Depth)
            {
                Depth = depth;
                RemoveDepth();
            }

            Console.WriteLine($"Remove({e}) After Remove Depth={depth}");
            Console.WriteLine(ShowAllNodes());

        }

        void RemoveDepth()
        {
            //移除第一层
            if (mHead == mHead0)
            {
                return;
            }

            mHead = mHead.NextLevel;

        }

        //查询e在跳表里
        public bool Has(T e)
        {
            var p = mHead;

            while (p != null)
            {
                if (p.Next != null)
                {
                    if (p.Next.Value.CompareTo(e) == 0)
                    {
                        return true;
                    }
                    if (p.Next.Value.CompareTo(e) < 0)
                    {
                        p = p.Next;
                        continue;
                    }
                }

                p = p.NextLevel;

            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var e in this)
            {
                sb.Append(e).Append(' ');
            }
            sb.Length--;
            return sb.ToString();
        }

        public string ShowAllNodes()
        {
            StringBuilder sb = new StringBuilder();
            var q = mHead;
            while (q != null)
            {
                var p = q;
                while (p != null)
                {
                    if (p.Prev != null)
                    {
                        sb.Append(p.Prev.Value).Append("<-");
                    }
                    sb.Append(p.Value);
                    if (p.Next != null)
                    {
                        sb.Append("->").Append(p.Next.Value);
                    }
                    if (p.NextLevel != null)
                    {
                        sb.Append('|').Append(p.NextLevel.Value);
                    }
                    sb.Append(' ');
                    p = p.Next;
                }
                sb.AppendLine();
                q = q.NextLevel;
            }
            return sb.ToString();
        }

        #region IEnumerable

        class SkipListEnumerator : IEnumerator<T>
        {
            public T Current => mNode.Value;

            object IEnumerator.Current => this.Current;

            SkipList<T> mSkipList;
            SkipListNode mNode;
            public SkipListEnumerator(SkipList<T> skiplist)
            {
                mSkipList = skiplist;
                mNode = mSkipList.mHead0;
            }
            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (mNode.Next == null)
                {
                    return false;
                }
                mNode = mNode.Next;
                return true;
            }

            public void Reset()
            {
                mNode = mSkipList.mHead0;

            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SkipListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
