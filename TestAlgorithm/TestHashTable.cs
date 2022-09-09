using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestHashTable
    {
        HashTable htable = new HashTable();
        [TestMethod]
        [Timeout(100)]
        public void TestAdd() 
        {
            htable.Add("jwk");
            htable.Add("123");
            htable.Add("ÖÐ¹úÈË");

            Assert.IsTrue(htable.Size == 3);
            Assert.IsTrue(htable.Capacity == 7);


            for (int i = 0; i < 10; i++)
            {
                htable.Add(i.ToString());
            }
            Assert.IsTrue(htable.Size == 13);
            Assert.IsTrue(htable.Capacity == 11);
        }

        [TestMethod]
        public void TestEnumerator()
        {
            TestAdd();
            foreach(var  s in htable)
            {
                Assert.IsTrue(htable.Has(s));
                Console.WriteLine(s);
            }
        }

        [TestMethod]
        public void TestRemove()
        {
            TestAdd();
            for (int i = 0; i < 10; i++)
            {
                htable.Remove(i.ToString());
            }

            Assert.IsTrue(htable.Size == 3);
            Assert.IsTrue(htable.Capacity == 7);
        }


    }
}