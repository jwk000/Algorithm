using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestRBTree
    {

        [TestMethod]
        [Timeout(1000)]
        public void Test1()
        {
            RedBlackTree tree = new RedBlackTree();
            //顺序添加节点
            for (int i = 1; i < 20; i++)
            {
                tree.Add(i);
                Assert.IsTrue(tree.Has(i));
            }

            Assert.IsFalse(tree.Has(20));

            Assert.IsTrue(tree.ToString() == "1|B 2|B 3|B 4|R 5|B 6|B 7|B 8|B 9|B 10|B 11|B 12|R 13|B 14|B 15|B 16|R 17|R 18|B 19|R");

            //顺序删除节点
            for (int i = 1; i < 20; i++)
            {
                tree.Remove(i);
                Assert.IsFalse(tree.Has(i));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void Test2()
        {
            RedBlackTree tree = new RedBlackTree();
            //倒序添加节点
            for (int i = 19; i > 0; i--)
            {
                tree.Add(i);
                Assert.IsTrue(tree.Has(i));
            }

            Assert.IsFalse(tree.Has(20));

            Assert.IsTrue(tree.ToString() == "1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19");

            //倒序删除节点
            for (int i = 19; i > 0; i--)
            {
                tree.Remove(i);
                Assert.IsFalse(tree.Has(i));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void Test3()
        {
            RedBlackTree tree = new RedBlackTree();
            //随机添加节点
            tree.Add(2);
            tree.Add(14);
            tree.Add(9);
            tree.Add(4);
            tree.Add(11);
            tree.Add(6);
            tree.Add(13);
            tree.Add(8);
            tree.Add(1);
            tree.Add(10);
            tree.Add(3);
            tree.Add(7);
            tree.Add(12);
            tree.Add(5);


            Assert.IsFalse(tree.Has(20));

            Assert.IsTrue(tree.ToString() == "1 2 3 4 5 6 7 8 9 10 11 12 13 14");

            //随机删除节点
            tree.Remove(2);
            tree.Remove(14);
            tree.Remove(9);
            tree.Remove(4);
            tree.Remove(11);
            tree.Remove(6);
            tree.Remove(13);
            tree.Remove(8);
            tree.Remove(1);
            tree.Remove(10);
            tree.Remove(3);
            tree.Remove(7);
            tree.Remove(12);
            tree.Remove(5);

        }
    }

}

