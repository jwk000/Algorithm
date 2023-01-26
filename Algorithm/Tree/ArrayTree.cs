using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 树状数组。
     * 可以实现1.快速对一维数组arr区间[i,j]求和，2.修改某个元素arr[i]。
     * 修改和查询的复杂度都是O(logN)。前缀和数组不能实现2，差分数组不能实现1。
     * 主要思想是给数组分层求和，类似二叉树，这样求和和修改只需要logN的复杂度。
     * 
     * 实现：
     * 对原始数组下标i从1开始计数；
     * 更新：第i位的上层下标j正好是 j=i+lowbit(i)，这样可以修改所有上层节点。
     * 构建：树状数组初始为全0，用arr[i]去更新tree即可。
     * 求和：tree[i]表示长度为lowbit(i)的部分和，递归对i-lowbit(i)求和即可。
     * -----------------
     * |       8       |
     * -----------------
     * |   4   |   x   |
     * -----------------
     * | 2 | x | 6 | x |
     * -----------------
     * |1|x|3|x|5|x|7|x|
     * -----------------
     */
    public class ArrayTree
    {
        int[] mTree = null;
        public ArrayTree(int[] arr)
        {
            mTree = new int[arr.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                Change(i, arr[i]);
            }
        }

        //修改第i位的值+v
        public void Change(int i, int v)
        {
            if (i < 0) return;
            while (i < mTree.Length)
            {
                mTree[i] += v;
                i += lowbit(i+1);
            }
        }

        //数组区间和[i,j]
        public int SumRange(int i, int j)
        {
            return SumN(j + 1) - SumN(i);
        }

        //前n项和
        public int SumN(int n)
        {
            if (n <= 0) return 0;
            int sum = 0;
            while (n > 0)
            {
                sum += mTree[n-1];
                n -= lowbit(n);
            }
            return sum;
        }

        //从0开始计数，二进制数字i最后一个1的位置k，lowbit(i)=2^k；
        //x&(-x)，当x为0时结果为0，否则结果为保留最后一个1。
        //lowbit(12)==4
        int lowbit(int i)
        {
            return i & (-i);
        }
    }
}
