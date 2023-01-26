using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 差分数组。
     * 如果数组a的前缀和数组是矩阵b，则a是b的差分数组。
     * 用于处理一维数组arr任意区间[i,j]所有元素同时+val的问题，修改不用遍历，但获取arr[i]需要遍历。
     */
    public class DiffArray
    {
        int[] mDiffArray;
        public DiffArray(int[] arr)
        {
            mDiffArray = new int[arr.Length];
            mDiffArray[0] = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                mDiffArray[i] = arr[i] - arr[i - 1];
            }
        }

        public void ChangeValue(int i, int j, int val)
        {
            mDiffArray[i] += val;
            if (j + 1 < mDiffArray.Length) mDiffArray[j + 1] -= val;
        }

        public int GetValue(int index)
        {
            int ret = 0;
            for (int i = 0; i <= index; i++)
            {
                ret += mDiffArray[i];
            }
            return ret;
        }

        public int[] GetArray()
        {
            int[] arr = new int[mDiffArray.Length];
            arr[0] = mDiffArray[0];
            for(int i = 1; i < mDiffArray.Length; i++)
            {
                arr[i] = arr[i - 1] + mDiffArray[i];
            }
            return arr;
        }
    }

    /*
     * 差分矩阵。
     * 如果矩阵a的前缀和矩阵是b，则a是b的差分矩阵。
     * 用于处理二维数组mat任意矩形范围(x1,y1)到(x2,y2)所有元素同时+val的问题，修改只影响四个值，但获取mat[i,j]需要遍历。
     */
    public class DiffMatrix
    {
        int[,] mDiffMatrix;
        int mRowNum, mColNum;
        public DiffMatrix(int[,] mat)
        {
            mRowNum = mat.GetLength(0);
            mColNum = mat.GetLength(1);
            mDiffMatrix = new int[mRowNum, mColNum];
            for (int x = 0; x < mRowNum; x++)
            {
                for (int y = 0; y < mColNum; y++)
                {
                    int L = 0, U = 0, LU = 0;
                    if (x > 0) L = mat[x - 1, y];
                    if (y > 0) U = mat[x, y - 1];
                    if (x > 0 && y > 0) LU = mat[x - 1, y - 1];
                    mDiffMatrix[x, y] = mat[x, y] - L - U + LU;
                }
            }
        }

        public void ChangeRange(int x1, int y1, int x2, int y2, int val)
        {
            if (x1 < 0 || x2 < 0 || y1 < 0 || y2 < 0) return;
            if (x1 + 1 > mRowNum || x2 + 1 > mRowNum || y1 + 1 > mColNum || y2 + 1 > mColNum) return;
            int minX = x1 < x2 ? x1 : x2;
            int maxX = x1 + x2 - minX;
            int minY = y1 < y2 ? y1 : y2;
            int maxY = y1 + y2 - minY;


            mDiffMatrix[minX, minY] += val;
            if (maxX + 1 < mRowNum) mDiffMatrix[maxX + 1, minY] -= val;
            if (maxY + 1 < mColNum) mDiffMatrix[minX, maxY + 1] -= val;
            if (maxX + 1 < mRowNum && maxY + 1 < mColNum) mDiffMatrix[maxX + 1, maxY + 1] += val;
        }

        public int GetValue(int x, int y)
        {
            int ret = 0;
            for (int i = 0; i <= x; i++)
            {
                for (int j = 0; j <= y; j++)
                {
                    ret += mDiffMatrix[i, j];
                }
            }
            return ret;
        }

        public int[,] GetMatrix()
        {
            int[,] matrix = new int[mRowNum, mColNum];
            for (int x = 0; x < mRowNum; x++)
            {
                for (int y = 0; y < mColNum; y++)
                {
                    int L = 0, U = 0, LU = 0;
                    if (x > 0) L = matrix[x - 1, y];
                    if (y > 0) U = matrix[x, y - 1];
                    if (x > 0 && y > 0) LU = matrix[x - 1, y - 1];
                    matrix[x, y] = L + U - LU + mDiffMatrix[x, y];
                }
            }
            return matrix;
        }
    }
}
