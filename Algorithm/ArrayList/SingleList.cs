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

}
