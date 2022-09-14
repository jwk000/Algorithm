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
            MyQueue<int> queue = new MyQueue<int>();

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
            Deque<int> deque = new Deque<int>();

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
    }
}
