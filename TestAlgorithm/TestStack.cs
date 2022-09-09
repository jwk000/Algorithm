
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestStack
    {
        MyStack<int> mStack = new MyStack<int>();

        [TestMethod]
        public void TestPush()
        {
            mStack.Push(1);
            mStack.Push(2);
            mStack.Push(3);
            Assert.IsTrue(mStack.Size == 3);
        }

        [TestMethod]
        public void TestPop()
        {
            for(int i = 0; i < 10; i++)
            {
                mStack.Push(i);
            }
            int x = mStack.Pop();
            Assert.IsTrue(x == 9);
            int y = mStack.Top();
            Assert.IsTrue(y == 8);
        }

        [TestMethod]
        public void TestIndex()
        {
            for (int i = 0; i < 10; i++)
            {
                mStack.Push(i);
            }

            int a = mStack[1];
            int b = mStack[10];
            int c = mStack[-1];
            int d = mStack[-10];
            Assert.IsTrue(a == 0 && b == 9 && c == 9 && d == 0);
        }

        [TestMethod]
        public void TestInsert()
        {
            for (int i = 0; i < 10; i++)
            {
                mStack.Push(i);
            }

            mStack.Insert(-3, 999);
            mStack.Insert(3, 888);

            Assert.IsTrue(mStack.ToString() == "0,1,888,2,3,4,5,6,999,7,8,9");
        }

        [TestMethod]
        public void TestRemove()
        {
            for (int i = 0; i < 10; i++)
            {
                mStack.Push(i);
            }

            mStack.Remove(3);
            mStack.Remove(-3);
            Assert.IsTrue(mStack.ToString() == "0,1,3,4,5,6,8,9");
        }

        [TestMethod]
        public void TestRotate()
        {
            for (int i = 0; i < 10; i++)
            {
                mStack.Push(i);
            }

            mStack.Rotate(1);
            Assert.IsTrue(mStack.ToString() == "9,0,1,2,3,4,5,6,7,8");
            mStack.Rotate(10);
            Assert.IsTrue(mStack.ToString() == "9,0,1,2,3,4,5,6,7,8");
            mStack.Rotate(-1);
            Assert.IsTrue(mStack.ToString() == "0,1,2,3,4,5,6,7,8,9");
            mStack.Rotate(-10);
            Assert.IsTrue(mStack.ToString() == "0,1,2,3,4,5,6,7,8,9");

        }

        [TestMethod]
        public void TestMove()
        {
            for (int i = 0; i < 10; i++)
            {
                mStack.Push(i);
            }

            mStack.Move(3, 2);
            Assert.IsTrue(mStack.ToString() == "0,1,3,4,2,5,6,7,8,9");

            mStack.Move(-3, -2);
            Assert.IsTrue(mStack.ToString() == "0,1,3,4,2,7,5,6,8,9");
        }
    }
}
