using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
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
}
