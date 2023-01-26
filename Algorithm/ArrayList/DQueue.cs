using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //双端队列，两端都可以入队和出队

    public class DQueue<T>
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

        public DQueue()
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
}
