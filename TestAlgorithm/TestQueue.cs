using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestQueue
    {

        [TestMethod]
        public void TestMyQueue()
        {
            NQueue<int> queue = new NQueue<int>();

            //enque
            for(int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }
            Assert.IsTrue(queue.Head() == 0);
            Assert.IsTrue(queue.Tail() == 9);

            for(int i = 0; i < 12; i++)
            {
                if (i < 5)
                {
                    int a = queue.Dequeue();
                    Assert.IsTrue(a == i);
                    Assert.IsTrue(queue.Head() == i + 1);
                    Assert.IsTrue(queue.Tail() == 9);
                }
                else
                {
                    if(queue.TryDequeue(out int b))
                    {
                        Assert.IsTrue(b == i);
                    }
                }
            }

            Assert.IsTrue(queue.Head() == 0);
            Assert.IsTrue(queue.Tail() == 0);
        }

        [TestMethod]
        public void TestDequeue()
        {
            DQueue<int> deque = new DQueue<int>();

            for(int i = 0; i < 12; i++)
            {
                deque.PushBack(i);
            }

            Assert.IsTrue(deque.Front() == 0);
            Assert.IsTrue(deque.Back() == 11);
            Assert.IsTrue(deque.Size == 12);
            for(int i = 12; i < 24; i++)
            {
                deque.PushFront(i);
            }
            Assert.IsTrue(deque.Front() == 23);
            Assert.IsTrue(deque.Back() == 11);
            Assert.IsTrue(deque.Size == 24);

            for(int i = 0; i < 10; i++)
            {
                int v = deque.PopBack();
                Assert.IsTrue(v == 11 - i);
                Assert.IsTrue(deque.Front() == 23);
                Assert.IsTrue(deque.Back() == 10 - i);
            }

            for(int i = 0; i < 10; i++)
            {
                int v = deque.PopFront();
                Assert.IsTrue(v == 23 - i);
                Assert.IsTrue(deque.Front() == 22 - i);
                Assert.IsTrue(deque.Back() == 1);
            }
        }

        [TestMethod]
        public void TestRingQueue()
        {
            RingQueue<int> queue = new RingQueue<int>(10);

            for(int i = 0; i < 12; i++)
            {
                queue.Enqueue(i);
            }

            Assert.IsTrue(queue.Head() == 0);
            Assert.IsTrue(queue.Tail() == 9);

            for(int i = 0; i < 5; i++)
            {
                int v = queue.Dequeue();
                Assert.IsTrue(v == i);
                queue.Enqueue(v+10);
            }

            Assert.IsTrue(queue.Head() == 5);
            Assert.IsTrue(queue.Tail() == 14);
        }

        [TestMethod]
        public void TestPriorityQueue()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();

            queue.Push(6);
            queue.Push(10);
            queue.Push(7);
            queue.Push(5);
            queue.Push(8);
            queue.Push(1);
            queue.Push(3);
            queue.Push(4);
            queue.Push(2);
            queue.Push(9);


            for(int i = 0; i < queue.Size; i++)
            {
                Assert.IsTrue(queue.Peek() == i+1);
                Assert.IsTrue(queue.Pop() == i + 1);
            }
        }
    }
}
