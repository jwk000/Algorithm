using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.src
{
    //快速数组，用移动元素代替插入和删除
    public class FastArray
    {
        int[] mArr;
        int mLen;
        public FastArray(int[] arr)
        {
            mArr = arr;
            mLen = arr.Length;
        }

        public int Get(int idx)
        {
            if (idx < 0 || idx >= mLen) return -1;
            return mArr[idx];
        }

        public void Set(int idx, int val)
        {
            if (idx < 0 || idx >= mLen) return;
            mArr[idx] = val;
        }

        public void Add(int val)
        {
            if (mLen < mArr.Length)
            {
                mArr[mLen++] = val;
            }
        }

        public void Del(int idx)
        {
            if (idx < 0 || idx >= mLen) return;
            mLen--;
            mArr[idx] = mArr[mLen];
        }

        public void Insert(int idx, int val)
        {
            if (idx < 0 || idx >= mLen) return;
            if (mLen < mArr.Length)
            {
                mArr[mLen++] = mArr[idx];
                mArr[idx] = val;
            }
        }


    }
}
