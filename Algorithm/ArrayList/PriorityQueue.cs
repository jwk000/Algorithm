using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //优先队列（堆）
    //元素进入队列后放入堆中，出队的永远是最小元素
    public class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> mQueue = new List<T>();

        public int Size => mQueue.Count;
        public void Push(T e)
        {
            mQueue.Add(e);
            if (mQueue.Count > 1)
            {
                int idx = mQueue.Count - 1;
                int parentIdx = (idx - 1) / 2;
                while (parentIdx < idx)
                {
                    //比父节点小
                    if (mQueue[idx].CompareTo(mQueue[parentIdx]) < 0)
                    {
                        T t = mQueue[parentIdx];
                        mQueue[parentIdx] = mQueue[idx];
                        mQueue[idx] = t;
                        idx = parentIdx;
                        parentIdx = (idx - 1) / 2;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void Update(T e)
        {
            int idx = mQueue.IndexOf(e);
            if (idx < 0) return;
            int parentIdx = (idx - 1) / 2;
            while (parentIdx < idx)
            {
                //比父节点小
                if (mQueue[idx].CompareTo(mQueue[parentIdx]) < 0)
                {
                    T t = mQueue[parentIdx];
                    mQueue[parentIdx] = mQueue[idx];
                    mQueue[idx] = t;
                    idx = parentIdx;
                    parentIdx = (idx - 1) / 2;
                }
                else
                {
                    break;
                }
            }
        }

        public T Peek()
        {
            if (mQueue.Count == 0)
            {
                return default(T);
            }
            return mQueue[0];
        }

        public T Pop()
        {
            if (mQueue.Count == 0)
            {
                return default(T);
            }
            T head = mQueue[0];
            //尾节点放入头节点
            T tail = mQueue.Last();
            mQueue.RemoveAt(mQueue.Count - 1);
            if (mQueue.Count == 0)
            {
                return head;
            }

            mQueue[0] = tail;
            int idx = 0;
            while (idx < mQueue.Count)
            {
                int left = idx * 2 + 1;
                if (left < mQueue.Count)
                {
                    int select = left;
                    int right = left + 1;
                    if (right < mQueue.Count)
                    {
                        if (mQueue[left].CompareTo(mQueue[right]) > 0)
                        {
                            select = right;
                        }
                    }
                    if (mQueue[idx].CompareTo(mQueue[select]) > 0)
                    {
                        T t = mQueue[idx];
                        mQueue[idx] = mQueue[select];
                        mQueue[select] = t;
                        idx = select;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return head;
        }
    }
}
