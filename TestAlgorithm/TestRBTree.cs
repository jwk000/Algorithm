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

            Assert.IsTrue(tree.ToString() == "1B 2B 3B 4R 5B 6B 7B 8B 9B 10B 11B 12R 13B 14B 15B 16R 17R 18B 19R");

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

            Assert.IsTrue(tree.ToString() == "1R 2B 3R 4R 5B 6B 7B 8R 9B 10B 11B 12B 13B 14B 15B 16R 17B 18B 19B");

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
            
            Assert.IsTrue(tree.ToString() == "1R 2B 3R 4B 5R 6B 7R 8B 9B 10R 11B 12R 13B 14B");

            //随机删除节点
            tree.Remove(2);
            tree.Remove(14);
            tree.Remove(9);
            tree.Remove(4);
            tree.Remove(11);
            tree.Remove(6);
            Assert.IsTrue(tree.Has(5));
            tree.Remove(13);
            tree.Remove(8);
            tree.Remove(1);
            tree.Remove(10);
            tree.Remove(3);
            tree.Remove(7);
            tree.Remove(12);
            tree.Remove(5);
            Assert.IsFalse(tree.Has(5));
        }
    }

}

