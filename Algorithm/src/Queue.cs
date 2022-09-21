using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //普通队列：FIFO
    public class MyQueue<T>
    {

        LinkedList<T> mBuffer;

        public MyQueue()
        {
            mBuffer = new LinkedList<T>();
        }

        public int Size => mBuffer.Count();

        public T Head()
        {
            if (Size > 0)
            {
                return mBuffer.First();
            }
            return default(T);
        }

        public T Tail()
        {
            if (Size > 0)
            {
                return mBuffer.Last();
            }
            return default(T);
        }

        public void Enqueue(T e)
        {
            mBuffer.AddLast(e);
        }

        public T Dequeue()
        {
            if (Size > 0)
            {
                T e = mBuffer.First();
                mBuffer.RemoveFirst();
                return e;
            }
            return default(T);
        }

        public bool TryDequeue(out T e)
        {
            if (Size > 0)
            {
                e = mBuffer.First();
                mBuffer.RemoveFirst();
                return true;
            }
            else
            {
                e = default(T);
                return false;
            }
        }

    }

    //双端队列，两端都可以入队和出队

    public class Deque<T>
    {
        class Block
        {
            T[] mBuffer;

            public Block(int size)
            {
                mBuffer = new T[size];
            }

            public T[] Buffer => mBuffer;
        }

        LinkedList<Block> mBlocks = new LinkedList<Block>();
        LinkedListNode<Block> mFirstBlock;
        LinkedListNode<Block> mLastBlock;
        int mFrontIndex = -1;
        int mBackIndex = -1;
        const int mBlockSize = 10;

        public Deque()
        {
            mFirstBlock = mBlocks.AddFirst(new Block(mBlockSize));
            mLastBlock = mFirstBlock;
            Size = 0;
        }

        public int Size { get; private set; }

        public T Front()
        {
            if (mFrontIndex == -1)
            {
                return default(T);
            }
            return mFirstBlock.Value.Buffer[mFrontIndex];
        }

        public T Back()
        {
            if (mBackIndex == -1)
            {
                return default(T);
            }
            return mLastBlock.Value.Buffer[mBackIndex];

        }

        public void PushFront(T e)
        {
            if (mFrontIndex == -1)
            {
                mFrontIndex = mBlockSize / 2;
                mBackIndex = mFrontIndex;
            }
            else if (mFrontIndex == 0)
            {
                mFirstBlock = mBlocks.AddFirst(new Block(mBlockSize));
                mFrontIndex = mBlockSize - 1;
            }
            else
            {
                mFrontIndex--;
            }
            mFirstBlock.Value.Buffer[mFrontIndex] = e;
            Size++;
        }

        public void PushBack(T e)
        {
            if (mBackIndex == -1)
            {
                mFrontIndex = mBlockSize / 2;
                mBackIndex = mFrontIndex;
            }
            else if (mBackIndex == mBlockSize - 1)
            {
                mLastBlock = mBlocks.AddLast(new Block(mBlockSize));
                mBackIndex = 0;
            }
            else
            {
                mBackIndex++;
            }
            mLastBlock.Value.Buffer[mBackIndex] = e;
            Size++;
        }

        public T PopFront()
        {
            if (mFrontIndex == -1)
            {
                return default(T);
            }
            T e = mFirstBlock.Value.Buffer[mFrontIndex++];
            Size--;
            if (mFirstBlock == mLastBlock && mFrontIndex > mBackIndex)
            {
                mFrontIndex = -1;
                mBackIndex = -1;
                return e;
            }
            if (mFirstBlock != mLastBlock && mFrontIndex == mBlockSize)
            {
                mFirstBlock = mFirstBlock.Next;
                mFrontIndex = 0;
                mBlocks.RemoveFirst();
            }
            return e;
        }

        public T PopBack()
        {
            if (mBackIndex == -1)
            {
                return default(T);
            }
            T e = mLastBlock.Value.Buffer[mBackIndex--];
            Size--;
            if (mFirstBlock == mLastBlock && mFrontIndex > mBackIndex)
            {
                mFrontIndex = -1;
                mBackIndex = -1;
                return e;
            }
            if (mFirstBlock != mLastBlock && mBackIndex == -1)
            {
                mLastBlock = mLastBlock.Previous;
                mBackIndex = mBlockSize - 1;
                mBlocks.RemoveLast();
            }
            return e;
        }

    }


    //循环队列，大小固定，循环复用空间
    public class RingQueue<T>
    {

        T[] mBuffer;
        int mHeadIndex = -1;
        int mTailIndex = -1;
        public RingQueue(int size)
        {
            mBuffer = new T[size];
        }

        public int Size { get; private set; }


        public T Head()
        {
            if (mHeadIndex == -1)
            {
                return default(T);
            }

            return mBuffer[mHeadIndex];
        }

        public T Tail()
        {
            if (mTailIndex == -1)
            {
                return default(T);
            }
            return mBuffer[mTailIndex];
        }

        public bool Enqueue(T e)
        {
            int index = (mTailIndex + 1) % mBuffer.Length;
            if (index == mHeadIndex)
            {
                return false;
            }
            if (mHeadIndex == -1)
            {
                mHeadIndex = index;
            }
            mTailIndex = index;
            mBuffer[mTailIndex] = e;
            Size++;
            return true;
        }

        public T Dequeue()
        {

            if (mHeadIndex == mTailIndex)
            {
                return default(T);
            }
            int index = (mHeadIndex + 1) % mBuffer.Length;
            T e = mBuffer[mHeadIndex];
            mHeadIndex = index;
            Size--;
            return e;
        }


    }

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
                int parentIdx = (idx-1) / 2;
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
