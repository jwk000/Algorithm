using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //前缀和矩阵：用于快速求解二维数组(x1,y1)到(x2,y2)矩形区间内的元素和
    public class PreSumMatrix
    {
        int[,] mPreSum;
        int mColNum, mRowNum;
        public PreSumMatrix(int[,] matrix)
        {
            mRowNum = matrix.GetLength(0);
            mColNum = matrix.GetLength(1);
            mPreSum = new int[mRowNum, mColNum];
            for (int x = 0; x < mRowNum; x++)
            {
                for (int y = 0; y < mColNum; y++)
                {
                    int LeftSum = 0;
                    int UpSum = 0;
                    int LeftUpSum = 0;
                    if (x > 0) LeftSum = mPreSum[x - 1, y];
                    if (y > 0) UpSum = mPreSum[x, y - 1];
                    if (x > 0 && y > 0) LeftUpSum = mPreSum[x - 1, y - 1];
                    mPreSum[x, y] = LeftSum + UpSum - LeftUpSum + matrix[x, y];
                }
            }
        }

        public int Sum(int x1, int y1, int x2, int y2)
        {
            if (x1 < 0 || x2 < 0 || y1 < 0 || y2 < 0) return -1;
            if (x1 + 1 > mRowNum || x2 + 1 > mRowNum || y1 + 1 > mColNum || y2 + 1 > mColNum) return -1;
            int minX = x1 < x2 ? x1 : x2;
            int maxX = x1 + x2 - minX;
            int minY = y1 < y2 ? y1 : y2;
            int maxY = y1 + y2 - minY;

            int Left = 0;
            int Up = 0;
            int LeftUp = 0;
            if (minX > 0) Left = mPreSum[minX - 1, maxY];
            if (minY > 0) Up = mPreSum[maxX, minY - 1];
            if (minX > 0 && minY > 0) LeftUp = mPreSum[minX - 1, minY - 1];
            int ret = mPreSum[maxX, maxY] - Left - Up + LeftUp;
            return ret;
        }
    }
}
