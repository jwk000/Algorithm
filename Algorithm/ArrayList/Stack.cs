using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //栈，FILO，不考虑容量限制
    //支持push pop top 操作
    //支持1~N索引栈底到栈顶元素
    //支持-1~-N索引栈顶到栈底元素
    //支持旋转上移（123->231）、下移(123->312)操作
    //支持单元素移动 move
    //支持insert remove 
    public class MyStack<T>
    {
        List<T> mElements = new List<T>();
        public int Size => mElements.Count;

        //压栈
        public void Push(T e)
        {
            mElements.Add(e);
        }

        //出栈
        public T Pop()
        {
            if (mElements.Count == 0)
            {

                return default(T);
            }
            else
            {
                T e = mElements.Last();
                mElements.RemoveAt(mElements.Count - 1);
                return e;
            }
        }

        //栈顶
        public T Top()
        {
            return mElements.LastOrDefault();
        }

        //索引器
        public T this[int i]
        {
            get
            {
                if (i > 0 && i <= mElements.Count)
                {
                    return mElements[i - 1];
                }
                if (i < 0 && (-i) <= mElements.Count)
                {
                    return mElements[mElements.Count + i];
                }
                return default(T);
            }
        }

        //n>0旋转上移n次，n<0旋转下移n次
        public void Rotate(int n)
        {
            n = n % Size;
            if (n == 0) return;
            if (n > 0)
            {
                while (n-- > 0)
                {
                    T e = mElements.Last();
                    for (int i = mElements.Count - 1; i > 0; i--)
                    {
                        mElements[i] = mElements[i - 1];
                    }
                    mElements[0] = e;
                }
            }
            else
            {
                while (n++ < 0)
                {
                    T e = mElements.First();
                    for (int i = 0; i < mElements.Count - 1; i++)
                    {
                        mElements[i] = mElements[i + 1];
                    }
                    mElements[mElements.Count - 1] = e;

                }
            }
        }

        //pos处的元素向上移动n个位置，n<0向下移动n个位置
        public void Move(int pos, int n)
        {
            if (pos < 0)
            {
                pos = (Size + pos) % Size + 1;
            }
            if (pos < 1 || pos > Size || n == 0)
            {
                return;
            }
            if (n > 0)
            {
                int pos2 = pos + n;
                if (pos2 > Size)
                {
                    pos2 = Size;
                }
                T e = mElements[pos - 1];
                for (int i = pos - 1; i < pos2 - 1; i++)
                {
                    mElements[i] = mElements[i + 1];
                }
                mElements[pos2 - 1] = e;
            }
            else
            {
                int pos2 = pos + n;
                if (pos2 < 1) pos2 = 1;
                T e = mElements[pos - 1];
                for (int i = pos - 1; i > pos2 - 1; i--)
                {
                    mElements[i] = mElements[i - 1];
                }
                mElements[pos2 - 1] = e;
            }
        }

        //在pos出插入e
        public void Insert(int pos, T e)
        {
            if (pos < 0)
            {
                pos = (Size + pos) % Size+1;
            }
            if (pos < 1 || pos > Size)
            {
                return;
            }
            mElements.Add(e);
            for (int i = Size - 1; i > pos - 1; i--)
            {
                mElements[i] = mElements[i - 1];
            }
            mElements[pos - 1] = e;
        }

        //移除pos处的元素
        public void Remove(int pos)
        {
            if (pos < 0)
            {
                pos = (Size + pos) % Size + 1;
            }
            if (pos < 1 || pos > Size)
            {
                return;
            }
            for (int i = pos - 1; i < Size - 1; i++)
            {
                mElements[i] = mElements[i + 1];
            }
            mElements.RemoveAt(Size - 1);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(T e in mElements)
            {
                sb.Append(e).Append(',');
            }
            sb.Length--;
            return sb.ToString();
        }

    }
}
