using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //9大数组排序算法
    //ref:https://zhuanlan.zhihu.com/p/42586566
    public class ArraySort
    {
        //冒泡排序
        public static void BubbleSort(int[] arr)
        {
            for (int i = arr.Length - 1; i > 0; i--)
            {
                bool swap = false;
                for (int j = 0; j < i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int t = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = t;
                        swap = true;
                    }
                }
                if (!swap) //优化：当某一轮比较没有发生交换时说明已经有序
                {
                    break;
                }
            }
        }
        //选择排序
        public static void SelectSort(int[] arr)
        {
            for (int i = arr.Length - 1; i > 0; i--)
            {
                int max = i;
                for (int j = 0; j < i; j++)
                {
                    if (arr[j] > arr[max])
                    {
                        max = j;
                    }
                }
                (arr[i], arr[max]) = (arr[max], arr[i]);
            }
        }
        //插入排序
        public static void InsertSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                //从后往前遍历更快
                int t = arr[i];
                int j = i;
                while (j > 0 && arr[j - 1] > t)
                {
                    arr[j] = arr[j - 1];
                    j--;
                }
                arr[j] = t;
            }
        }
        //希尔排序。
        //希尔排序是第一个突破O(n2)的排序算法，它是简单插入排序的改进版。希尔排序的提出，主要基于以下两点：
        //1. 插入排序算法在数组基本有序的情况下，可以近似达到O(n)复杂度，效率极高。
        //2. 但插入排序每次只能将数据移动一位，在数组较大且基本无序的情况下性能会迅速恶化。
        //实现思路：执行k趟排序，每趟按步长t插入排序，t=1的时候序列有序；
        //1. 选择一个增量序列t1，t2，…，tk，其中ti>tj，tk=1；
        //2. 按增量序列个数k，对序列进行 k 趟排序；
        //3. 每趟排序，根据对应的增量ti，将待排序列分割成若干长度为m 的子序列，分别对各子表进行直接插入排序。仅增量因子为1 时，整个序列作为一个表来处理，表长度即为整个序列的长度。
        //下面是一些常见的增量序列:
        //第一种增量是最初Donald Shell提出的增量，即折半降低直到1。据研究，使用希尔增量，其时间复杂度还是O(n2)。
        //第二种增量Hibbard：{1, 3, ..., 2k-1}。该增量序列的时间复杂度大约是O(n1.5)。
        //第三种增量Sedgewick增量：(1, 5, 19, 41, 109,...)，其生成序列或者是94i - 92i + 1或者是4i - 3*2i + 1。
        public static void ShellSort(int[] arr)
        {
            int t = 1;
            while (2 * t < arr.Length)
            {
                t = 2 * t + 1;
            }
            while (t >= 1)
            {
                //t个子数组
                for (int i = 0; i < t; i++)
                {
                    //步长为t
                    for (int j = i + t; j < arr.Length; j += t)
                    {
                        int k = j, v = arr[j];
                        while (k - t >= 0 && arr[k - t] > v)//从后往前寻找
                        {
                            arr[k] = arr[k - t];
                            k = k - t;
                        }
                        arr[k] = v;
                    }
                }
                t = (t - 1) / 2;
            }
        }


        //快速排序，分治法
        //划分：先找一个轴心p，把<=p的值放在左边，>p的值放在右边
        //对左边和右边继续递归划分，直到不能分
        public static void QuickSort(int[] arr)
        {
            quick(arr, 0, arr.Length - 1);
        }
        private static void quick(int[] arr, int left, int right)
        {
            if (left >= right) return;
            //p取单一值可能取到最大值或最小值导致划分无效
            int p = (arr[left] + arr[right]) / 2;
            int L = left, R = right;
            while (left < right)
            {
                while (left < right && arr[left] <= p) { left++; }
                while (left < right && arr[right] > p) { right--; }
                if (left < right)
                {
                    int t = arr[left];
                    arr[left] = arr[right];
                    arr[right] = t;
                }
            }
            //left和right最终会相等，而且left会超过左边部分的边界
            quick(arr, L, left - 1);
            quick(arr, left, R);
        }

        //归并排序，分治法
        //二路归并排序：先把数组递归二分到不能分，然后借助临时数组合并排序，结果写回原数组
        public static void MergeSort(int[] arr)
        {
            int[] temp = new int[arr.Length];
            merge(arr, temp, 0, arr.Length - 1);
        }
        private static void merge(int[] arr, int[] temp, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                merge(arr, temp, left, mid);
                merge(arr, temp, mid + 1, right);
                //此时从[left~mid]和[mid+1~right]都是有序的
                int p = left;
                int q = mid + 1;
                int i = left;
                while (p <= mid || q <= right)
                {
                    if ((p <= mid && q <= right && arr[p] <= arr[q]) || q > right)
                    {
                        temp[i++] = arr[p++];
                    }
                    else
                    {
                        temp[i++] = arr[q++];
                    }
                }
                for (i = left; i <= right; i++)
                {
                    arr[i] = temp[i];
                }
            }
        }
        //计数排序。
        //构建一个和数组值相关的桶对所有元素计数，适用于数值范围较小而且是整数的情况，比如考试分数。
        //1. 找出待排序的数组中最大和最小的元素构建len=max-min的数组C；
        //2. 统计数组中每个值为i的元素出现的次数，存入数组C的第i项；
        //3. 对所有的计数累加（从C中的第一个元素开始，每一项和前一项相加）；
        //4. 反向填充目标数组：将每个元素i放在新数组的第C(i)项，每放一个元素就将C(i)减去1。
        public static void CountSort(int[] arr)
        {
            int max = arr[0], min = arr[0];
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max) max = arr[i];
                if (arr[i] < min) min = arr[i];
            }
            int[] C = new int[max - min + 1];
            for (int i = 0; i < arr.Length; i++)
            {
                C[arr[i] - min]++;
            }

            int c = 0;
            for (int i = 0; i < C.Length; i++)
            {
                while (C[i] > 0)
                {
                    arr[c++] = i + min;
                    C[i]--;
                }
            }
        }

        //堆排序，需要构建堆结构，适用于大量数据取前n个最大最小值的问题
        public static void HeapSort(int[] arr)
        {
            //这里直接借助之前实现的优先队列
            PriorityQueue<int> heap = new PriorityQueue<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                heap.Push(arr[i]);
            }
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = heap.Pop();
            }
        }

        //基数排序(桶排序的扩展)
        //它的基本思想是：将整数按位数切割成不同的数字，然后按每个位数分别比较。
        //排序过程：
        //1. 将所有待比较数值（正整数）统一为同样的数位长度，数位较短的数前面补零。
        //2. 从最低位到最高位依次进行一次排序，数列就变成一个有序序列。
        public static void RadixSort(int[] arr)
        {
            //先找最大数字
            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max) max = arr[i];
            }
            int width = GetWidth(max);
            int[,] radix = new int[19, arr.Length];//19个桶(-9,9)
            int[] count = new int[19];//每个桶计数
            //切割比较
            for (int k = 0; k <= width; k++)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    int r = GetRadix(arr[i], k);
                    radix[r, count[r]++] = arr[i];
                }

                int c = 0;
                for (int i = 0; i < 19; i++)
                {
                    for (int j = 0; j < count[i]; j++)
                    {
                        arr[c++] = radix[i, j];
                    }
                    count[i] = 0;
                }
            }

        }

        //计算x的第k位数字
        private static int GetRadix(int x, int k)
        {
            for (int i = 0; i < k; i++) x = x / 10;
            return x % 10 + 9;
        }


        //计算x有多少位
        private static int GetWidth(int x)
        {
            if (x == 0) return 1;
            if (x < 0) x = -x;
            int ret = 0;
            while (x > 0)
            {
                x = x / 10;
                ret++;
            }
            return ret;
        }
    }
}
