using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestBPlusTree
    {

        [TestMethod]
        public void Test1()
        {
            BPlusTree bptree = new BPlusTree(5);

            for (int i = 1; i <= 20; i++)
            {
                bptree.Add(i, i.ToString());
            }

            string? s = bptree.Search(10) as string;
            Assert.IsTrue(s == "10");
            Assert.IsTrue(null == bptree.Search(100));

            var list1 = bptree.SearchRange(-10, 10);
            string s1 = string.Join(' ', list1);
            Assert.IsTrue(s1 == "1 2 3 4 5 6 7 8 9 10");

            var list2 = bptree.SearchRange(10, 100);
            string s2 = string.Join(' ', list2);
            Assert.IsTrue(s2 == "10 11 12 13 14 15 16 17 18 19 20");

            for (int i = 2; i < 20; i++)
            {
                bptree.Remove(i);
            }

            Assert.IsTrue(bptree.ToString() == "1,20");
        }

        [TestMethod]
        public void Test2()
        {
            BPlusTree bptree = new BPlusTree(5);

            for (int i = 20; i >= 1; i--)
            {
                bptree.Add(i, i.ToString());
            }

            string? s = bptree.Search(10) as string;
            Assert.IsTrue(s == "10");
            Assert.IsTrue(null == bptree.Search(100));

            var list1 = bptree.SearchRange(-10, 10);
            string s1 = string.Join(' ', list1);
            Assert.IsTrue(s1 == "1 2 3 4 5 6 7 8 9 10");

            var list2 = bptree.SearchRange(10, 100);
            string s2 = string.Join(' ', list2);
            Assert.IsTrue(s2 == "10 11 12 13 14 15 16 17 18 19 20");

            for (int i = 19; i > 1; i--)
            {
                bptree.Remove(i);
            }

            Assert.IsTrue(bptree.ToString() == "1,20");
        }

        [TestMethod]
        public void Test3()
        {
            BPlusTree bptree = new BPlusTree(5);
            bptree.Add(3,"3");
            bptree.Add(7,"7");
            bptree.Add(19,"19");
            bptree.Add(1,"1");
            bptree.Add(16,"16");
            bptree.Add(4,"4");
            bptree.Add(10,"10");
            bptree.Add(18,"18");
            bptree.Add(2,"2");
            bptree.Add(15,"15");
            bptree.Add(8,"8");
            bptree.Add(12,"12");
            bptree.Add(9,"9");
            bptree.Add(13,"13");
            bptree.Add(11,"11");
            bptree.Add(20,"20");
            bptree.Add(5,"5");
            bptree.Add(14,"14");
            bptree.Add(6,"6");
            bptree.Add(17,"17");

            string? s = bptree.Search(10) as string;
            Assert.IsTrue(s == "10");
            Assert.IsTrue(null == bptree.Search(100));

            var list1 = bptree.SearchRange(-10, 10);
            string s1 = string.Join(' ', list1);
            Assert.IsTrue(s1 == "1 2 3 4 5 6 7 8 9 10");

            var list2 = bptree.SearchRange(10, 100);
            string s2 = string.Join(' ', list2);
            Assert.IsTrue(s2 == "10 11 12 13 14 15 16 17 18 19 20");


            bptree.Remove(3);
            bptree.Remove(7);
            bptree.Remove(19);
            bptree.Remove(1);
            bptree.Remove(16);
            bptree.Remove(4);
            bptree.Remove(10);
            bptree.Remove(18);
            bptree.Remove(2);
            bptree.Remove(15);
            bptree.Remove(8);
            bptree.Remove(12);
            bptree.Remove(9);
            bptree.Remove(13);
            bptree.Remove(11);
            bptree.Remove(20);
            bptree.Remove(5);
            bptree.Remove(14);
            //bptree.Remove(6);
            //bptree.Remove(17);

            Assert.IsTrue(bptree.ToString() == "6,17");

        }
    }
}
