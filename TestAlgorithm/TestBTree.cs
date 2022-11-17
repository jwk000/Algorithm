using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestBTree
    {
        [TestMethod]
        public void Test()
        {
            BTree btree = new BTree(5);
            for (int i = 1; i < 20; i++)
            {
                btree.Add(i);
            }

            Assert.IsTrue(btree.Count() == 19);
            Assert.IsTrue(btree.Has(15));
            Assert.IsFalse(btree.Has(200));

            for (int i = 0; i < 20; i++)
            {
                btree.Remove(i);
            }

            Assert.IsTrue(btree.Count() == 0);


        }

        [TestMethod]
        public void Test2()
        {
            BTree btree = new BTree(5);
            for (int i = 20; i > 0; i--)
            {
                btree.Add(i);
            }

            Assert.IsTrue(btree.Count() == 20);
            Assert.IsTrue(btree.Has(15));
            Assert.IsFalse(btree.Has(200));

            for (int i = 20; i > 0; i--)
            {
                btree.Remove(i);
            }

            Assert.IsTrue(btree.Count() == 0);

        }

        [TestMethod]
        public void Test3()
        {
            BTree btree = new BTree(5);
            btree.Add(3);
            btree.Add(7);
            btree.Add(19);
            btree.Add(1);
            btree.Add(16);
            btree.Add(4);
            btree.Add(10);
            btree.Add(18);
            btree.Add(2);
            btree.Add(15);
            btree.Add(8);
            btree.Add(12);
            btree.Add(9);
            btree.Add(13);
            btree.Add(11);
            btree.Add(20);
            btree.Add(5);
            btree.Add(14);
            btree.Add(6);
            btree.Add(17);

            Assert.IsTrue(btree.Count() == 20);
            Assert.IsTrue(btree.Has(15));
            Assert.IsFalse(btree.Has(200));


            btree.Remove(3);
            btree.Remove(7);
            btree.Remove(19);
            btree.Remove(1);
            btree.Remove(16);
            btree.Remove(4);
            btree.Remove(10);
            btree.Remove(18);
            btree.Remove(2);
            btree.Remove(15);
            btree.Remove(8);
            btree.Remove(12);
            btree.Remove(9);
            btree.Remove(13);
            btree.Remove(11);
            btree.Remove(20);
            btree.Remove(5);
            btree.Remove(14);
            btree.Remove(6);
            btree.Remove(17);

            Assert.IsTrue(btree.Count() == 0);
        }
    }
}
