using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 
     * 并查集（英文：Disjoint-set data structure，直译为不交集数据结构）是一种数据结构，用于处理一些不交集（Disjoint sets，一系列没有重复元素的集合）的合并及查询问题。并查集支持如下操作：
     * 1) 查询：查询某个元素属于哪个集合，通常是返回集合内的一个“代表元素”。这个操作是为了判断两个元素是否在同一个集合之中。
     * 2) 合并：将两个集合合并为一个。
     * 3) 添加：添加一个新集合，其中有一个新元素。添加操作不如查询和合并操作重要，常常被忽略。
     * 经过优化的不交集森林有线性的空间复杂度O(n)，以及接近常数的单次操作平均时间复杂度O(α(n))。
     * 并查集是用于计算最小生成树的克鲁斯克尔演算法中的关键。
     * 
     * 并查集构建的是个只会合并不会拆分的系统！！
     */
    public class DisjointedSet
    {
        int[] mPrev;//s[i]=j表示元素i的父亲是j
        int mCount;//集合数量

        public DisjointedSet(int num)
        {
            mCount = num;
            mPrev = new int[num];
            for (int i = 0; i < num; i++)
            {
                mPrev[i] = i;//初始父节点是自己
                //mHeight[i] = 0;//初始高度为0
            }
        }

        //合并
        //按秩合并：将秩小的子树的根指向秩大的子树的根。秩的定义：对每个结点，用秩表示结点高度的一个上界。
        //按秩合并和路径压缩是两种优化思路，不能混用！（有使用负数表示秩的实现方案）
        public void Union(int x, int y)
        {
            int px = Find(x);
            int py = Find(y);
            if (px == py) return;

            mPrev[px] = py;
            mCount--;
        }


        //找到x所在集合的根（带路径压缩功能）
        public int Find(int x)
        {
            return mPrev[x] == x ? x : mPrev[x] = Find(mPrev[x]);
        }

        //检查x和y是否连通
        public bool Query(int x, int y)
        {
            return Find(x) == Find(y);
        }

        //集合数量
        public int GetCount()
        {
            return mCount;
        }
    }
}
