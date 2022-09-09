using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //哈希表，key支持string
    //支持添加、删除、查询、遍历操作
    //添加时可以自动扩容，删除时自动缩表
    //冲突处理：拉链法
    public class HashTable : IEnumerable<string>
    {
        static int[] primes = new[] { 7, 11, 17, 29, 47, 71, 107, 163, 251, 379, 569, 857, 1289, 1949, 2927, 4391, 6599, 9901, 14867, 22303, 33457, 50207, 75323, 112997, 169501 };
        List<List<string>> table = new List<List<string>>(7);
        int capacity = 7;
        int capacityIdx = 0;
        int size = 0;

        public int Capacity => capacity;
        public int Size => size;

        public HashTable()
        {
            for(int i = 0; i < capacity; i++)
            {
                table.Add(null);
            }
        }

        public void Add(string s)
        {
            int v = 0;
            foreach (char c in s)
            {
                v = v * 131 + c;
            }
            size++;
            if (size > capacity * 1.5)
            {
                rehash();
            }
            int index = v % capacity;
            
            List<string> list = table[index];
            if (list == null)
            {
                list = new List<string>();
                table[index] = list;
            }
            list.Add(s);
        }

        private void rehash(bool increase = true)
        {
            if (increase)
            {
                if (capacityIdx == primes.Length - 1)
                {
                    return;
                }
                ++capacityIdx;
            }
            else
            {
                if (capacityIdx == 0)
                {
                    return;
                }
                --capacityIdx;
            }
            //寻找下一个素数
            capacity = primes[capacityIdx];
            List<List<string>> newTable = new List<List<string>>();
            for(int i = 0; i < capacity; i++)
            {
                newTable.Add(null);
            }
            foreach (var s in this)
            {
                int v = 0;
                foreach (char c in s)
                {
                    v = v * 131 + c;
                }
                int index = v % capacity;
                List<string> list = newTable[index];
                if (list == null)
                {
                    list = new List<string>();
                    newTable[index] = list;
                }
                list.Add(s);

            }
            table = newTable;
        }

        public void Remove(string s)
        {
            int v = 0;
            foreach (char c in s)
            {
                v = v * 131 + c;
            }
            int index = v % capacity;
            List<string> list = table[index];
            list.Remove(s);

            size--;
            if (size * 1.5 < capacity)
            {
                rehash(false);
            }
        }

        public bool Has(string s)
        {
            int v = 0;
            foreach (char c in s)
            {
                v = v * 131 + c;
            }
            int index = v % capacity;
            List<string> list = table[index];
            if (list == null)
            {
                return false;
            }
            return list.Contains(s);
        }

        #region IEnumerator
        //隐式实现接口
        public IEnumerator<string> GetEnumerator()
        {
            return new HashTableEnumerator(this);
        }
        //显示实现接口
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        class HashTableEnumerator : IEnumerator<string>
        {
            public HashTableEnumerator(HashTable t)
            {
                mt = t;
            }
            int it1 = 0;
            int it2 = -1;
            HashTable mt;

            //隐式实现接口
            public string Current => mt.table[it1][it2];
            //显式实现接口
            object IEnumerator.Current => this.Current;

            public bool MoveNext()
            {
                it2++;
                if (it1 >= mt.table.Count)
                {
                    return false;
                }
                var list = mt.table[it1];
                if (list == null)
                {
                    it1++;
                    it2 = -1;
                    return MoveNext();
                }
                if (it2 >= list.Count)
                {
                    it1++;
                    it2 = -1;
                    return MoveNext();
                }
                return true;

            }

            public void Reset()
            {
                it1 = 0;
                it2 = 0;
            }

            public void Dispose()
            {
                
            }

        }
        #endregion
    }
}
