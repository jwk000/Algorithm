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
        ulong X;
        ulong A;
        ulong C;
        ulong M;

        public MyRandom(long seed)
        {
            X = (ulong)seed;
            M = ulong.MaxValue / 7;
            A = (ulong)Math.Sqrt(M) + 1;
            C = 3;
        }

        public MyRandom()
        {
            X = (ulong)DateTime.Now.Ticks;
            M = ulong.MaxValue / 3;
            A = (ulong)Math.Sqrt(M) + 1;
            C = 3;

        }

        public double NextDouble()
        {
            X = (A * X + C) % M;
            return X * 1.0 / M;
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


    /*
     * 平方取中法是由冯·诺依曼在1946年提出的，其基本思想为：将数列中的第a(i)项（假设其有m位）平方，取得到的2m位数（若不足2m位，在最高位前补0）中间部分的m位数字，作为a(i)的下一项a(i+1)，由此产生一个伪随机数数列。即：
     * x(i+1)=(10^(-m/2)*x(i)*x(i))mod(10^m)
     * 平方取中法计算较快，但在实际应用时会发现该方法容易产生周期性明显的数列，而且在某些情况下计算到一定步骤后始终产生相同的数甚至是零，或者产生的数字位数越来越小直至始终产生零。
     * 所以用平方取中法产生伪随机数数列时不能单纯使用公式，应该在计算过程中认为加入更多变化因素，比如根据前一个数的奇偶性进行不同的运算，如果产生的数字位数减少时通过另一种运算使其恢复成m位。
     */
    public class Random_MidPower
    {
        ulong seed = 0;
        ulong second = 2143967;
        public Random_MidPower()
        {
            seed = (ulong)DateTime.Now.Ticks;
        }
        ulong intlen(ulong x)        //整数x的长度
        {
            ulong count = 0;
            while (x != 0)
            {
                x /= 10;
                count++;
            }
            return count;
        }
        ulong power_10(ulong x)      //10的x次幂
        {
            ulong i, y = 1;
            for (i = 0; i < x; i++)
                y *= 10;
            return y;
        }
        public ulong Rand(int max)      //平方取中
        {
            ulong len;
            while (seed < 10000)            //保持数位一致
                seed = seed * 13 + seed / 10 + second / 3;
            len = intlen(seed);
            ulong temp = ((seed * seed / power_10(len >> 1)) % power_10(len));
            if (temp % 2 == 0) temp += second / 3 + 7854;       //增加改变因素，
            else temp += second * second / 2317;            //以延长计算周期
            seed = temp;
            return (seed % 10000 * (ulong)max) / 10000;
        }

        public int Next(int min, int max)
        {
            return (int)Rand(max - min) + min;
        }

        public double NextDouble()
        {
            return Rand(int.MaxValue) * 1.0 / int.MaxValue;
        }
    }

    /*
     * 异或移位法
     */

    public class Random_XorShift
    {
        ulong seed;

        public Random_XorShift()
        {
            seed = (ulong)DateTime.Now.Ticks;
        }
        ulong Rand()
        {
            ulong x = seed;  /* The state must be seeded with a nonzero value. */
            x ^= x >> 12; // a
            x ^= x << 25; // b
            x ^= x >> 27; // c
            seed = x;
            return x * 0x2545F4914F6CDD1D;
        }



        public double NextDouble()
        {
            return Rand() * 1.0 / ulong.MaxValue;
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

    /*
     * 梅森旋转算法
     * 常见的两种为基于32位的 MT19937和基于64位的 MT19937-64。由于
     * 梅森旋转算法是利用线性反馈移位寄存器（LFSR）产生随机数的，对于LFRS有结论：一个 k 位的移位寄存器，选取合适的特征多项式（即加1为本原多项式）时，取得最大周期 2k−1.
     * 而MT19937梅森旋转算法的周期为 2^19937−1（正如算法名，这是一个梅森素数），说明它是一个19937级的线性反馈移位寄存器，梅森旋转算法是利用线性反馈寄存器一直进行移位旋转，
     * 因此实际上 MT19937-32只需要用32位就能做到。它能做到在 1≤k≤623 都可以均匀分布。使用MT19937算法可以生成范围在 [232−1] 的均匀分布的32位整数。
     * 梅森素数：如果形如 2n−1 的数是一个素数，则称之为梅森素数。
     */

    public class Random_MersenneTwister
    {

        uint seed;
        uint[] MT = new uint[624];//624 * 32 - 31 = 19937
        int index = 0;

        public Random_MersenneTwister()
        {
            seed = (uint)DateTime.Now.Ticks;
            MT[0] = seed;
            for(int i = 1; i < 624; i++)
            {
                long t = 1812433253 * (MT[i - 1] ^ (MT[i - 1] >> 30)) + i;
                MT[i] = (uint)(t & 0xffffffff);
            }


        }

        void generate()
        {
            for (int i = 0; i < 624; i++)
            {
                // 2^31 = 0x80000000
                // 2^31-1 = 0x7fffffff
                uint y = (MT[i] & 0x80000000) + (MT[(i + 1) % 624] & 0x7fffffff);
                MT[i] = MT[(i + 397) % 624] ^ (y >> 1);
                if ((y & 1)!=0)
                    MT[i] ^= 2567483615;
            }
        }
        uint Rand()
        {
            if (index == 0)
                generate();
            uint y = MT[index];
            y = y ^ (y >> 11);                 //y右移11个bit
            y = y ^ ((y << 7) & 2636928640);   //y左移7个bit与2636928640相与，再与y进行异或
            y = y ^ ((y << 15) & 4022730752);  //y左移15个bit与4022730752相与，再与y进行异或
            y = y ^ (y >> 18);                 //y右移18个bit再与y进行异或
            index = (index + 1) % 624;
            return y;
        }

        public double NextDouble()
        {
            return Rand() * 1.0 / uint.MaxValue;
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
