using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 线段树。(下面实现的是最小值线段树）
     * 用来处理数组相应的区间查询（range query）和元素更新（update）操作。与树状数组不同的是，线段树不止可以适用于区间求和的查询，也可以进行区间最大值，区间最小值（Range Minimum/Maximum Query problem）或者区间异或值的查询。
     * 线段树进行更新（update）的操作为O(logn)，进行区间查询（range query）的操作也为O(logn)。
     * 线段树是用一个完全二叉树来存储对应于其每一个区间（segment）的数据。该二叉树的每一个结点中保存着相对应于这一个区间的信息。
     * 给定一个长度为N的数组arr，其所对应的线段树T各个结点的含义如下：
     *(1) T的根结点代表整个数组所在的区间对应的信息，及arr[0:N]（不含N)所对应的信息。
     *(2) T的每一个叶结点存储对应于输入数组的每一个单个元素构成的区间arr[i]所对应的信息，此处0≤i<N。
     *(3) T的每一个中间结点存储对应于输入数组某一区间arr[i:j]对应的信息，此处0≤i<j<N。
     * 例如给定一个输入数组[1,5,3,7,3,2,5,7]，其所对应的最小值线段树应如下图所示：
     * -----------------
     * |       1       |
     * -----------------
     * |   1   |   2   |
     * -----------------
     * | 1 | 3 | 2 | 5 |
     * -----------------
     * |1|5|3|7|3|2|5|7|
     * -----------------
     * 
     */
    public class SegmentTree
    {
        int[] mSegMin;
        int N;

        public SegmentTree(int[] arr)
        {
            N = arr.Length;
            mSegMin = new int[2 * N];
            for (int i = 0; i < N; i++)
            {
                mSegMin[i] = 0;
                mSegMin[N + i] = arr[i];
            }
            //i=0没有意义，因为2*i==0
            for (int i = N - 1; i > 0; i--)
            {
                mSegMin[i] = Math.Min(mSegMin[2 * i], mSegMin[2 * i + 1]);
            }
        }

        //更新arr[i]=v
        public void Update(int i, int v)
        {
            i = N + i;
            mSegMin[i] = v;
            while (i > 1)
            {
                i = i / 2;
                mSegMin[i] = Math.Min(mSegMin[2 * i], mSegMin[2 * i + 1]);
            }
        }

        //区间[left,right)内的最小值
        //从下往上查找，所有下标为奇数的在右边，偶数在左边
        public int RangeMin(int left, int right)
        {
            left += N;
            right += N;
            int min = mSegMin[left];

            while (left < right)
            {
                if ((left & 1) == 1)
                {
                    min = Math.Min(min, mSegMin[left]);
                    left++;
                }
                if ((right & 1)== 1){
                    right--;
                    min = Math.Min(min, mSegMin[right]);
                }
                left >>= 1;
                right >>= 1;//注意，每层比较永远不包含right
            }
            return min;
        }


    }
}
