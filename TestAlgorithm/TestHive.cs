namespace TestAlgorithm;
using Algorithm;

[TestClass]
public class TestHive
{
    [TestMethod]
    public void TestAdd()
    {
        Hive<int> hive = new Hive<int>();
        for (int i = 0; i < 100; i++)
        {
            hive.Add(i);
        }
        int index = 0;
        foreach (var item in hive)
        {
            Assert.AreEqual(index, item);
            index++;
        }

        //É¾³ý
        for (int i = 0; i < 50; i++)
        {
            hive.Del(i);
        }

        index = 50;
        foreach (var item in hive)
        {
            Assert.AreEqual(index, item);
            index++;
        }
    }
}
