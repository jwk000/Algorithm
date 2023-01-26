namespace Algorithm;
class BinarySearch
{
    //arr是有序数组，有重复元素，找第一个n的下标，找不到返回-1
    int BinSearch(int[] arr, int left, int right, int n)
    {
        if (left > right) return -1;
        int mid = (left + right) / 2;

        if (arr[mid] == n)
        {
            if (left == right)
            {
                return mid;
            }
            else
            {
                int ret = BinSearch(arr, left, mid, n);
                if (ret == -1)
                {
                    return mid;
                }
                else
                {
                    return ret;
                }
            }
        }
        else if (arr[mid] < n)
        {
            return BinSearch(arr, mid + 1, right, n);
        }
        else
        {
            return BinSearch(arr, left, mid - 1, n);
        }
    }

    //非递归
    int BinSearch2(int[] arr, int n)
    {
        int left = 0;
        int right = arr.Length - 1;
        int ret = -1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (arr[mid] < n)
            {
                left = mid + 1;
            }
            else if (arr[mid] > n)
            {
                right = mid - 1;
            }
            else
            {
                ret = mid;
                right = mid - 1;
            }
        }
        return ret;
    }

    public void Test()
    {

        int[] arr = new int[] { 1, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 6, 6, 7 };
        int idx = BinSearch(arr, 0, arr.Length - 1, 4);
        Console.WriteLine(idx);

        idx = BinSearch2(arr, 4);
        Console.WriteLine(idx);
    }

}
