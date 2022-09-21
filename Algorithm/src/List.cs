using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Algorithm
{

    //单向链表
    public class SingleList<T> where T : IComparable
    {
        SingleListNode<T> mHead;
        SingleListNode<T> mTail;

        public int Size { get; private set; }

        //后插入
        public SingleListNode<T> PushBack(T e)
        {
            SingleListNode<T> node = new SingleListNode<T>(e);
            if (mHead == null)
            {
                mHead = node;
                mTail = node;
            }
            else
            {
                mTail.Next = node;
                mTail = node;
            }
            Size++;
            return node;
        }

        //前插入
        public SingleListNode<T> PushFront(T e)
        {
            SingleListNode<T> node = new SingleListNode<T>(e);
            if (mHead == null)
            {
                mHead = node;
                mTail = node;
            }
            else
            {
                node.Next = mHead;
                mHead = node;
            }
            Size++;
            return node;

        }

        //指定位置插入
        public SingleListNode<T> Insert(int pos, T e)
        {
            if (pos > Size || pos < 0)
            {
                return null;
            }
            SingleListNode<T> p = mHead;
            if (pos == 0)
            {
                return PushFront(e);
            }
            if (pos == Size)
            {
                return PushBack(e);
            }
            for (int i = 0; i < pos - 1; i++)
            {
                p = p.Next;
            }
            SingleListNode<T> node = new SingleListNode<T>(e);
            node.Next = p.Next;
            p.Next = node;
            return node;
        }

        //移除元素e
        public void Remove(T e)
        {

            var p = mHead;
            var q = mHead;
            while (p != null)
            {
                if (p.Value.CompareTo(e) == 0)
                {
                    if (p == mHead)
                    {
                        mHead = p.Next;
                    }
                    if (p == mTail)
                    {
                        mTail = q == p ? null : q;
                    }
                    if (p == q)
                    {
                        p = p.Next;
                        q = p;
                    }
                    else
                    {
                        q.Next = p.Next;
                        p = p.Next;
                    }
                    break;
                }
                q = p;
                p = p.Next;
            }
        }

        //移除所有e
        public void RemoveAll(T e)
        {

            var p = mHead;
            var q = mHead;
            while (p != null)
            {
                if (p.Value.CompareTo(e) == 0)
                {
                    if (p == mHead)
                    {
                        mHead = p.Next;
                    }
                    if (p == mTail)
                    {
                        mTail = q == p ? null : q;
                    }
                    if (p == q)
                    {
                        p = p.Next;
                        q = p;
                    }
                    else
                    {
                        q.Next = p.Next;
                        p = p.Next;
                    }
                    continue;
                }
                q = p;
                p = p.Next;
            }
        }

        //移除pos处元素
        public void RemoveAt(int pos)
        {
            if (pos < 0 || pos >= Size)
            {
                return;
            }

            var p = mHead;
            var q = mHead;
            for (int i = 0; i < pos; i++)
            {
                q = p;
                p = p.Next;
            }
            if (p == mHead)
            {
                mHead = mHead.Next;
            }
            else if (p == mTail)
            {
                q.Next = null;
                mTail = q;
            }
            else
            {
                q.Next = p.Next;
            }
        }

        //查找e的下标，找不到返回-1
        public int FindIndex(T e)
        {
            int idx = 0;
            var p = mHead;
            while (p != null)
            {
                if (p.Value.CompareTo(e) == 0)
                {
                    return idx;
                }
                idx++;
                p = p.Next;
            }
            return -1;
        }

        //默认排序，从小到大
        public void Sort()
        {
            Sort((a, b) => a.CompareTo(b));
        }

        //排序，指定比较规则
        public void Sort(Comparison<T> cmp)
        {
            if (mHead == null)
            {
                return;
            }
            //插入排序
            var head = mHead;
            var tail = head;
            var p = mHead.Next;
            head.Next = null;

            while (p != null)
            {
                if (cmp(tail.Value, p.Value) < 0)
                {
                    tail.Next = p;
                    tail = p;
                    p = p.Next;
                    tail.Next = null;
                    continue;
                }

                if (cmp(head.Value, p.Value) > 0)
                {
                    var q = p;
                    p = p.Next;
                    q.Next = head;
                    head = q;
                    continue;
                }

                {
                    var q = head;
                    var r = q;
                    while (cmp(q.Value, p.Value) <= 0)
                    {
                        r = q;
                        q = q.Next;
                    }
                    r.Next = p;
                    p = p.Next;
                    r.Next.Next = q;
                }
            }
            mHead = head;
            mTail = tail;

        }

        //翻转链表
        public void Reverse()
        {
            if (mHead == null)
            {
                return;
            }
            var head = mHead;
            var p = mHead.Next;
            mHead.Next = null;
            while (p != null)
            {
                var q = p;
                p = p.Next;
                q.Next = mHead;
                mHead = q;
            }
            mTail = head;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (var p = mHead; p != null; p = p.Next)
            {
                sb.Append(p.Value).Append(' ');
            }
            sb.Length--;
            return sb.ToString();
        }
    }

    public class SingleListNode<T>
    {
        public T Value { get; set; }
        public SingleListNode<T> Next { get; set; }

        public SingleListNode(T e)
        {
            Value = e;
        }
    }


    //双向链表
    public class DoubleList<T> where T : IComparable
    {
        DoubleListNode<T> mHead;
        DoubleListNode<T> mTail;

        public int Size { get; private set; }

        //后插入
        public DoubleListNode<T> PushBack(T e)
        {
            DoubleListNode<T> node = new DoubleListNode<T>(e);
            if (mTail == null)
            {
                mHead = node;
                mTail = node;
            }
            else
            {
                mTail.Next = node;
                node.Previous = mTail;
                mTail = node;
            }
            Size++;
            return node;
        }

        //前插入
        public DoubleListNode<T> PushFront(T e)
        {
            DoubleListNode<T> node = new DoubleListNode<T>(e);
            if (mTail == null)
            {
                mHead = node;
                mTail = node;
            }
            else
            {
                node.Next = mHead;
                mHead.Previous = node;
                mHead = node;
            }
            Size++;
            return node;
        }

        //指定位置插入
        public DoubleListNode<T> Insert(int pos, T e)
        {
            if (pos < 0 || pos > Size)
            {
                return null;
            }

            if (pos == 0)
            {
                return PushFront(e);
            }
            if (pos == Size)
            {
                return PushBack(e);
            }

            DoubleListNode<T> node = new DoubleListNode<T>(e);
            var p = mHead;
            for (int i = 0; i < pos; i++)
            {
                p = p.Next;
            }
            p.Previous.Next = node;
            node.Previous = p.Previous;
            node.Next = p;
            p.Previous = node;

            return node;
        }

        //移除元素e
        public void Remove(T e)
        {

            var p = mHead;
            while (p != null)
            {
                if (p.Value.CompareTo(e) == 0)
                {
                    if (p.Previous != null)
                    {
                        p.Previous.Next = p.Next;
                    }
                    else
                    {
                        mHead = mHead.Next;
                        if (mHead != null) mHead.Previous = null;
                    }
                    if (p.Next != null)
                    {
                        p.Next.Previous = p.Previous;
                    }
                    else
                    {
                        mTail = p.Previous;
                        if (mTail != null) mTail.Next = null;
                    }

                    break;
                }
                p = p.Next;
            }
        }

        //移除所有e
        public void RemoveAll(T e)
        {
            var p = mHead;
            while (p != null)
            {
                if (p.Value.CompareTo(e) == 0)
                {
                    if (p.Previous != null)
                    {
                        p.Previous.Next = p.Next;
                    }
                    else
                    {
                        mHead = mHead.Next;
                        if (mHead != null) mHead.Previous = null;
                    }
                    if (p.Next != null)
                    {
                        p.Next.Previous = p.Previous;
                    }
                    else
                    {
                        mTail = p.Previous;
                        if (mTail != null) mTail.Next = null;
                    }
                }
                p = p.Next;
            }
        }

        //移除pos处元素
        public void RemoveAt(int pos)
        {
            if (pos < 0 || pos >= Size)
            {
                return;
            }

            var p = mHead;

            for (int i = 0; i < pos; i++)
            {
                p = p.Next;
            }
            if (p == mHead)
            {
                mHead = mHead.Next;
                mHead.Previous = null;
            }
            else if (p == mTail)
            {
                mTail = mTail.Previous;
                mTail.Next = null;
            }
            else
            {
                p.Previous.Next = p.Next;
                p.Next.Previous = p.Previous;
            }
        }

        //查找e的下标，找不到返回-1
        public int FindIndex(T e)
        {
            int idx = 0;
            var p = mHead;
            while (p != null)
            {
                if (p.Value.CompareTo(e) == 0)
                {
                    return idx;
                }
                idx++;
                p = p.Next;
            }
            return -1;

        }

        //默认排序，从小到大
        public void Sort()
        {
            Sort((a, b) => a.CompareTo(b));
        }
        //排序，指定比较规则
        public void Sort(Comparison<T> cmp)
        {
            if (mHead == null)
            {
                return;
            }
            //插入排序
            var head = mHead;
            var tail = head;
            var p = mHead.Next;
            head.Next = null;

            while (p != null)
            {
                if (cmp(tail.Value, p.Value) < 0)
                {
                    tail.Next = p;
                    p.Previous = tail;
                    tail = p;
                    p = p.Next;
                    tail.Next = null;

                    continue;
                }

                if (cmp(p.Value, head.Value) < 0)
                {
                    var q = p;
                    p = p.Next;
                    q.Next = head;
                    q.Previous = null;
                    head.Previous = q;
                    head = q;
                    continue;
                }

                {
                    var q = head;
                    while (cmp(q.Value, p.Value) <= 0)
                    {
                        q = q.Next;
                    }
                    //p插入q和q.previous之间
                    q.Previous.Next = p;
                    p.Previous = q.Previous;
                    q.Previous = p;
                    p = p.Next;
                    q.Previous.Next = q;
                }
            }
            mHead = head;
            mTail = tail;

        }

        //翻转链表
        public void Reverse()
        {
            if (mHead == null)
            {
                return;
            }
            var head = mHead;
            var p = mHead.Next;
            mHead.Next = null;
            while (p != null)
            {
                var q = p;
                p = p.Next;
                q.Next = mHead;
                q.Previous = null;
                mHead.Previous = q;
                mHead = q;
            }
            mTail = head;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (var p = mHead; p != null; p = p.Next)
            {
                sb.Append(p.Value).Append(' ');
            }
            sb.Length--;
            return sb.ToString();
        }

    }

    public class DoubleListNode<T>
    {
        public T Value { get; set; }
        public DoubleListNode<T> Previous;
        public DoubleListNode<T> Next;

        public DoubleListNode(T e)
        {
            Value = e;
        }
    }

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
            while (p!=null)
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
            if(sb.Length>0) sb.Length--;
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
                if(node.Next!=null) node.Next.Prev = node.Prev;
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
