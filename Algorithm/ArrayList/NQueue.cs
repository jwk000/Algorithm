using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //普通队列：FIFO
    public class NQueue<T>
    {

        LinkedList<T> mBuffer;

        public NQueue()
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




}
