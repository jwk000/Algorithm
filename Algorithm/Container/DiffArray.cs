using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 差分数组。
     * 如果数组a的前缀和数组是矩阵b，则a是b的差分数组。
     * 用于处理一维数组arr任意区间[i,j]所有元素同时+val的问题，修改不用遍历，但获取arr[i]需要遍历。
     */
    public class DiffArray
    {
        int[] mDiffArray;
        public DiffArray(int[] arr)
        {
            mDiffArray = new int[arr.Length];
            mDiffArray[0] = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                mDiffArray[i] = arr[i] - arr[i - 1];
            }
        }

        public void ChangeValue(int i, int j, int val)
        {
            mDiffArray[i] += val;
            if (j + 1 < mDiffArray.Length) mDiffArray[j + 1] -= val;
        }

        public int GetValue(int index)
        {
            int ret = 0;
            for (int i = 0; i <= index; i++)
            {
                ret += mDiffArray[i];
            }
            return ret;
        }

        public int[] GetArray()
        {
            int[] arr = new int[mDiffArray.Length];
            arr[0] = mDiffArray[0];
            for(int i = 1; i < mDiffArray.Length; i++)
            {
                arr[i] = arr[i - 1] + mDiffArray[i];
            }
            return arr;
        }
    }

}
