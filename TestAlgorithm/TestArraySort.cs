using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestArraySort
    {
        bool isSorted(int[] arr)
        {
            for(int i = 0; i < arr.Length-1; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }

        [TestMethod]
        public void TestBubbleSort()
        {
            int[] arr = new int[] { 34, 76, 89, 45, 63, 24, 17, 30, 57, 33 };
            ArraySort.BubbleSort(arr);
            Assert.IsTrue(isSorted(arr));

        }

        [TestMethod]
        public void TestSelectSort()
        {
            int[] arr = new int[] { 34, 76, 89, 45, 63, 24, 17, 30, 57, 33 };
            ArraySort.SelectSort(arr);
            Assert.IsTrue(isSorted(arr));

        }

        [TestMethod]
        public void TestInsertSort()
        {
            int[] arr = new int[] { 34, 76, 89, 45, 63, 24, 17, 30, 57, 33 };
            ArraySort.InsertSort(arr);
            Assert.IsTrue(isSorted(arr));

        }

        [TestMethod]
        public void TestMergeSort()
        {
            int[] arr = new int[] { 34, 76, 89, 45, 63, 24, 17, 30, 57, 33 };
            ArraySort.MergeSort(arr);
            Assert.IsTrue(isSorted(arr));

        }

        [TestMethod]
        public void TestQuickSort()
        {
            int[] arr = new int[] { 34, 76, 89, 45, 63, 24, 17, 30, 57, 33 };
            ArraySort.QuickSort(arr);
            Assert.IsTrue(isSorted(arr));
        }

        [TestMethod]
        public void TestHeapSort()
        {
            int[] arr = new int[] { 34, 76, 89, 45, 63, 24, 17, 30, 57, 33 };
            ArraySort.HeapSort(arr);
            Assert.IsTrue(isSorted(arr));

        }

        [TestMethod]
        public void TestShellSort()
        {
            int[] arr = new int[] { 34, 76, 89, 45, 63, 24, 17, 30, 57, 33 };
            ArraySort.ShellSort(arr);
            Assert.IsTrue(isSorted(arr));
        }

        [TestMethod]
        public void TestCountSort()
        {
            int[] arr = new int[] { 34, 76, 89, 45, 63, 24, 17, 30, 57, 33 };
            ArraySort.CountSort(arr);
            Assert.IsTrue(isSorted(arr));
        }

        [TestMethod]
        public void TestRadixSort()
        {
            int[] arr = new int[] { 34, 76, 89, 45, 63, 24, 17, 30, 57, 33 };
            ArraySort.RadixSort(arr);
            Assert.IsTrue(isSorted(arr));

            arr = new int[] { 134, -76, 989, -45, 603, 224, -17, -30, 537, 533 };
            ArraySort.RadixSort(arr);
            Assert.IsTrue(isSorted(arr));

        }
    }
}
