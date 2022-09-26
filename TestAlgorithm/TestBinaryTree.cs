using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{



    [TestClass]
    public class TestBinaryTree
    {
        [TestMethod]
        public void Test()
        {
            BinaryTree bt = BinaryTree.Create(new int[3,3] {{1,2,3 },{2,4,5},{3,-1,6}});

            bt.MirrorFlip();
            Console.WriteLine(bt.Print());

            bt.MirrorFlip();
            Console.WriteLine(bt.Print());

            string print2 = bt.Print2();
            Console.WriteLine(print2);

            var pre = bt.Preorder();
            Assert.IsTrue(pre.ToString2() == "1 2 4 5 3 6");

            var inorder = bt.Inorder();
            Assert.IsTrue(inorder.ToString2() == "4 2 5 1 3 6");

            var post = bt.Postorder();
            Assert.IsTrue(post.ToString2() == "4 5 2 6 3 1");

            var dfs = bt.DeepFirst();
            Assert.IsTrue(dfs.ToString2() == "1 2 4 5 3 6");

            var bfs = bt.BroadFirst();
            Assert.IsTrue(bfs.ToString2() == "1 2 3 4 5 6");

            Assert.IsTrue(bt.Has(6));
            Assert.IsFalse(bt.Has(9));
            Assert.IsTrue(bt.Height() == 3);

        }

    }
}
