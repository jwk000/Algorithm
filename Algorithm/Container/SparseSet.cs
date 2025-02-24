using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * sparse set是一个数据结构，用于极快速地对正整数进行增删改查并能够较好地利用CPU Cache。
     * sparse set由两部分组成：
        packed：一个线性表，用于紧密存储所有正整数。也是真正保存所有数据的地方。
        sparse：一个线性表，比较稀疏，用于存储packed中整数在packed数组中的下标，主要是为了建立值和下标的映射以加快查找。
     需要注意的是，packed和sparse必须是内存连续的线性表，这样才能发挥它易命中Cache的优势。
     */
    public class SparseSet : IEnumerable<int>
    {

        int[] m_packed = new int[m_capacity];
        int[] m_sparse = new int[m_capacity];
        int m_size = 0;
        const int m_capacity = 4096;
        public void Add(int x)
        {
            if (x < m_capacity && m_size < m_capacity)
            {
                m_packed[m_size++] = x;
                m_sparse[x] = m_size - 1;

            }
        }

        public bool Contains(int x)
        {
            return x < m_size && m_sparse[x] < m_size && m_packed[m_sparse[x]] == x;
        }

        public void Del(int x)
        {
            if (Contains(x))
            {
                int last = m_packed[--m_size];
                int idx = m_sparse[x];
                m_packed[idx] = last;
                m_sparse[last] = idx;
            }
        }

        public int Size()
        {
            return m_size;
        }

        public int[] Data()
        {
            return m_packed;
        }

        public void Clear()
        {
            m_size = 0;
        }

        public int this[int idx]
        {
            get
            {
                return m_packed[idx];
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new SparseSetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SparseSetEnumerator(this);
        }

        public class SparseSetEnumerator : IEnumerator<int>
        {
            SparseSet m_set;
            int m_idx;
            public SparseSetEnumerator(SparseSet set)
            {
                m_set = set;
                m_idx = -1;
            }
            public int Current
            {
                get
                {
                    return m_set[m_idx];
                }
            }
            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return m_set[m_idx];
                }
            }
            public void Dispose()
            {
            }
            public bool MoveNext()
            {
                return ++m_idx < m_set.Size();
            }
            public void Reset()
            {
                m_idx = -1;
            }



        }

    }
}
