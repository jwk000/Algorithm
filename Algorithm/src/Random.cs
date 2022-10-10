using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 线性同余法计算随机数
     * 设m是一个给定的正整数，如果两个整数a，b用m除，所得的余数相同，则称a，b对模m同余。
     * 所谓线性同余法（又叫混合同余法），就是这样的一个公式：X[i+1]=(A*X[i]+C) mod M；
     */
    public class MyRandom
    {
        ulong mSeed;
        ulong A;
        ulong C;
        ulong M;

        public MyRandom(long seed)
        {
            mSeed = (ulong)seed;
            M = ulong.MaxValue / 3;
            A = (ulong)Math.Sqrt(M) + 1;
            C = 3;
        }

        public MyRandom()
        {
            mSeed = (ulong)DateTime.Now.Ticks;
            M = ulong.MaxValue / 3;
            A = (ulong)Math.Sqrt(M) + 1;
            C = 3;

        }

        public double NextDouble()
        {
            mSeed = (A * mSeed + C) % M;
            return mSeed * 1.0 / M;
        }


        public int Next(int min, int max)
        {
            if (min >= max)
            {
                throw new Exception("invalid range");
            }
            return min + (int)(NextDouble() * (max - min));
        }

        public int Next(int max)
        {
            return Next(0, max);
        }

        public float NextSingle()
        {
            return (float)(NextDouble());
        }
    }
}
