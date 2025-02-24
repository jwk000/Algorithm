using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * std::hive 是一个通用对象池，它拥有以下一些重点功能和特性：
        std::hive 是一个同质容器，其中存放的对象的类型是固定的，由 std::hive 的模板参数给出。
        程序可以动态地向 std::hive 中增加对象，可以动态地从 std::hive 中删除对象。
        std::hive 是一个带所有权的容器，在其析构时会同时析构其中包含的所有对象。增加或删除单个对象的时间复杂度均为 O(1) 。
        std::hive 支持遍历操作。std::hive 的迭代器是双向迭代器，不支持随机访问。
        std::hive 中的对象是无序的，也就是遍历对象的顺序是不确定的，和插入对象的顺序无关。迭代器前进或后退一步的时间复杂度均为 O(1) 。
        std::hive 中包含的所有对象的地址都是固定的。也就是说：
        在向 std::hive 中插入新对象时，保证所有指向已有对象的指针和迭代器都不会失效。
        在从 std::hive 中移除对象时，除了指向被移除的对象的指针和迭代器外，保证所有指向其他对象的指针和迭代器都不会失效。
     * 
     */
    public class Hive<T>:IEnumerable<T>
    {
        class Block
        {
            public T[] data;
            public int size;
            public int capacity;
            public Block next;
            public int[] skipField;
            public Block(int capacity)
            {
                data = new T[capacity];
                skipField = new int[capacity];
                skipField[0] = capacity;
                this.capacity = capacity;
                size = 0;
                next = null;
            }

        }

        class Bubble
        {
            public int index;
            public Block block;
            public Bubble next;

            public Bubble(Block b, int i)
            {
                index = i;
                block = b;
                next = null;
            }
        }

        Block m_head = null;
        Block m_tail = null;
        Bubble m_bubbleList = null;

        public Hive()
        {
            m_head = new Block(4);
            m_tail = m_head;
            m_bubbleList = new Bubble(m_head, 0);
        }

        public void Add(T t)
        {
            //有空闲
            if (m_bubbleList.block.size < m_bubbleList.block.capacity)
            {
                Block block = m_bubbleList.block;
                block.data[m_bubbleList.index] = t;
                block.size++;
                int skipsize = block.skipField[m_bubbleList.index];
                if (skipsize > 0)
                {
                    block.skipField[m_bubbleList.index + 1] = skipsize;
                    m_bubbleList.index++;
                }
                block.skipField[m_bubbleList.index] = 0;
            }
            else //没有空闲
            {
                int capacity = m_tail.capacity * 2;
                Block block = new Block(capacity);
                block.data[0] = t;
                block.size = 1;
                m_tail.next = block;
                m_tail = block;
                m_bubbleList = new Bubble(block, 1);

            }
        }

        public void Del(T t)
        {
            Block block = m_head;
            while (block != null)
            {
                int i = 0;
                while (i < block.capacity)
                {
                    if (block.skipField[i] == 0)
                    {
                        if (block.data[i].Equals(t))
                        {

                            block.size--;
                            block.skipField[i] = i + 1 < block.capacity ? 1 + block.skipField[i + 1] : 1;

                            return;
                        }
                        i++;
                    }
                    else
                    {
                        i += block.skipField[i];
                    }
                }
                block = block.next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        class HiveEnumerator:IEnumerator<T>
        {
            Hive<T> m_hive;
            Block m_block;
            int m_index;
            public HiveEnumerator(Hive<T> hive)
            {
                m_hive = hive;
                m_block = m_hive.m_head;
                m_index = 0;
            }
            public T Current => m_block.data[m_index];
            object IEnumerator.Current => m_block.data[m_index];
            public void Dispose()
            {
                m_hive = null;
                m_block = null;
            }
            public bool MoveNext()
            {
                if(m_block == null)
                {
                    return false;
                }
                if (m_block.size == m_block.capacity)
                {
                    return false;
                }
                if (m_index+1 < m_block.capacity)
                {
                    m_index = m_index + m_block.skipField[m_index + 1];
                    if(m_index >= m_block.capacity)
                    {
                        return MoveNext();
                    }
                    return true;
                }
                else
                {
                    m_block = m_block.next;
                    if (m_block == null)
                    {
                        return false;
                    }
                    m_index = 0;
                    return true;
                }
            }
            public void Reset()
            {
                m_block = m_hive.m_head;
                m_index = 0;
            }
        }
    }
}
