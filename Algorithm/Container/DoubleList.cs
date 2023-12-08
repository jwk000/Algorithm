using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
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
}
