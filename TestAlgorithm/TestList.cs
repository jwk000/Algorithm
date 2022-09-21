using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestList
    {
        [TestMethod]
        public void TestSingleList()
        {
            SingleList<int> slist = new SingleList<int>();

            for(int i = 0; i < 5; i++)
            {
                slist.PushBack(i);
            }
            Assert.IsTrue(slist.ToString() == "0 1 2 3 4");
            slist.Remove(3);
            Assert.IsTrue(slist.ToString() == "0 1 2 4");
            slist.PushFront(4);
            Assert.IsTrue(slist.ToString() == "4 0 1 2 4");
            slist.RemoveAll(4);
            Assert.IsTrue(slist.ToString() == "0 1 2");

            for (int i = 5; i < 10; i++)
            {
                slist.PushFront(i);
            }
            Assert.IsTrue(slist.ToString() == "9 8 7 6 5 0 1 2");
            slist.RemoveAt(5);
            slist.RemoveAt(0);
            Assert.IsTrue(slist.ToString() == "8 7 6 5 1 2");
            slist.Insert(2, 9);
            Assert.IsTrue(slist.ToString() == "8 7 9 6 5 1 2");
            
            slist.Sort();
            Assert.IsTrue(slist.ToString() == "1 2 5 6 7 8 9");
            slist.Reverse();
            Assert.IsTrue(slist.ToString() == "9 8 7 6 5 2 1");

        }

        [TestMethod]
        public void TestDoubleList()
        {
            DoubleList<int> slist = new DoubleList<int>();

            for (int i = 0; i < 5; i++)
            {
                slist.PushBack(i);
            }
            Assert.IsTrue(slist.ToString() == "0 1 2 3 4");
            slist.Remove(3);
            Assert.IsTrue(slist.ToString() == "0 1 2 4");
            slist.PushFront(4);
            Assert.IsTrue(slist.ToString() == "4 0 1 2 4");
            slist.RemoveAll(4);
            Assert.IsTrue(slist.ToString() == "0 1 2");

            for (int i = 5; i < 10; i++)
            {
                slist.PushFront(i);
            }
            Assert.IsTrue(slist.ToString() == "9 8 7 6 5 0 1 2");
            slist.RemoveAt(5);
            slist.RemoveAt(0);
            Assert.IsTrue(slist.ToString() == "8 7 6 5 1 2");
            slist.Insert(2, 9);
            Assert.IsTrue(slist.ToString() == "8 7 9 6 5 1 2");

            slist.Sort();
            Assert.IsTrue(slist.ToString() == "1 2 5 6 7 8 9");
            slist.Reverse();
            Assert.IsTrue(slist.ToString() == "9 8 7 6 5 2 1");

        }

        [TestMethod]
        public void TestSkipList()
        {
            SkipList<int> slist = new SkipList<int>();

            slist.Add(9);
            slist.Add(6);
            slist.Add(10);
            Assert.IsTrue(slist.Depth == 0);

            slist.Add(17);
            Assert.IsTrue(slist.Depth == 1);
            slist.Add(15);
            slist.Add(8);
            Assert.IsTrue(slist.ToString() == "6 8 9 10 15 17");

            slist.Add(11);
            slist.Add(7);
            Assert.IsTrue(slist.Depth == 2);

            slist.Add(1);
            slist.Add(20);
            slist.Add(5);
            slist.Add(12);
            slist.Add(19);
            slist.Add(4);
            slist.Add(13);
            Assert.IsTrue(slist.Depth == 2);

            slist.Add(3);
            Assert.IsTrue(slist.Depth == 3);

            slist.Add(14);
            slist.Add(16);
            slist.Add(2);
            slist.Add(18);
            Assert.IsTrue(slist.ToString() == "1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20");

            Assert.IsTrue(slist.Has(18));
            Assert.IsFalse(slist.Has(22));

            Assert.IsTrue(slist.Size == 20);
            Assert.IsTrue(slist.Depth == 3);

            slist.Remove(9);
            Assert.IsTrue(slist.ToString() == "1 2 3 4 5 6 7 8 10 11 12 13 14 15 16 17 18 19 20");

            slist.Remove(6);
            slist.Remove(10);
            slist.Remove(17);
            Assert.IsTrue(slist.Size == 16);
            Assert.IsTrue(slist.Depth == 3);

            slist.Remove(15);
            Assert.IsTrue(slist.Size == 15);
            Assert.IsTrue(slist.Depth == 2);

            Assert.IsTrue(slist.Has(18));

            slist.Remove(8);
            slist.Remove(11);
            slist.Remove(7);
            slist.Remove(1);
            slist.Remove(20);
            slist.Remove(5);
            slist.Remove(12);
            Assert.IsTrue(slist.Size == 8);
            Assert.IsTrue(slist.Depth == 2);

            Assert.IsFalse(slist.Has(20));

            slist.Remove(19);
            Assert.IsTrue(slist.Size == 7);
            Assert.IsTrue(slist.Depth == 1);

            slist.Remove(4);
            slist.Remove(13);
            slist.Remove(3);
            Assert.IsTrue(slist.Size == 4);
            Assert.IsTrue(slist.Depth == 1);

            slist.Remove(14);
            Assert.IsTrue(slist.Size == 3);
            Assert.IsTrue(slist.Depth == 0);

            slist.Remove(16);
            slist.Remove(2);
            slist.Remove(18);

            Assert.IsTrue(slist.Size == 0);
            Assert.IsTrue(slist.Depth == 0);

            Assert.IsFalse(slist.Has(18));

        }
    }
}
