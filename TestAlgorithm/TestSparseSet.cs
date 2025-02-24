namespace TestAlgorithm;
using Algorithm;

[TestClass]
public class TestSparseSet
{

    //test case for sparseset
    [TestMethod]
    public void TestAddDel()
    {
        SparseSet set = new SparseSet();
        set.Add(1);
        set.Add(2);
        set.Add(3);
        set.Add(4);
        set.Add(5);
        set.Add(6);
        set.Add(7);
        set.Add(8);
        set.Add(9);
        set.Add(10);
        Assert.IsTrue(set.Contains(1));
        Assert.IsTrue(set.Contains(2));
        Assert.IsTrue(set.Contains(3));
        Assert.IsTrue(set.Contains(4));
        Assert.IsTrue(set.Contains(5));
        Assert.IsTrue(set.Contains(6));
        Assert.IsTrue(set.Contains(7));
        Assert.IsTrue(set.Contains(8));
        Assert.IsTrue(set.Contains(9));
        Assert.IsTrue(set.Contains(10));
        Assert.IsFalse(set.Contains(11));
        set.Del(1);
        Assert.IsFalse(set.Contains(1));
        set.Del(2);
        Assert.IsFalse(set.Contains(2));
        set.Del(3);
        Assert.IsFalse(set.Contains(3));
        set.Del(4);
        Assert.IsFalse(set.Contains(4));
        set.Del(5);
        Assert.IsFalse(set.Contains(5));
        set.Del(6);
        Assert.IsFalse(set.Contains(6));
        set.Del(7);
        Assert.IsFalse(set.Contains(7));
        set.Del(8);
        Assert.IsFalse(set.Contains(8));
        set.Del(9);
        Assert.IsFalse(set.Contains(9));
        set.Del(10);
        Assert.IsFalse(set.Contains(10));
    }

    [TestMethod]
    public void TestEnumerator()
    {
        SparseSet set = new SparseSet();
        set.Add(1);
        set.Add(100);
        set.Add(66);
        set.Add(32);
        set.Add(99);
        set.Add(88);
        set.Add(15);
        set.Add(7);

        int[] arr = new int[] { 1, 100, 66, 32, 99, 88, 15, 7 };
        int idx = 0;
        foreach (int i in set)
        {
            Assert.IsTrue(i == arr[idx++]);
        }


    }

}
