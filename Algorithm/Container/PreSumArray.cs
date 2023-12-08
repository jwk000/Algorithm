using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //前缀和数组：用于快速求解一维数组[i,j]区间内元素和
    public class PreSumArray
    {
        int[] mPreSum;
        public PreSumArray(int[] arr)
        {
            mPreSum = new int[arr.Length];
            mPreSum[0] = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                mPreSum[i] = mPreSum[i - 1] + arr[i];
            }
        }

        public int Sum(int i, int j)
        {
            if (i < 0 || j < 0 || i > mPreSum.Length - 1 || j > mPreSum.Length - 1) return -1;
            if (i == 0) return mPreSum[j];
            return mPreSum[j] - mPreSum[i - 1];
        }
    }

}
