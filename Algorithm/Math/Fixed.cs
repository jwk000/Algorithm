using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //64位定点数 
    public class Fixed64
    {
        const int TotalBitCnt = 64; //总共64位
        const int FractBitCnt = 16; //小数部分16位，精度为1/65536 == 0.000015
        const int IntBitCnt = TotalBitCnt - FractBitCnt; //整数部分48位
        const long FractMask = (long)(ulong.MaxValue >> IntBitCnt); //64位的低16位==1
        const long IntMask = (long)(-1L & ~FractMask); //64位的高48位==1
        const long FractRange = 1 << FractBitCnt;//小数部分的取值范围

        public const long MinVal = long.MinValue >> FractBitCnt;
        public const long MaxVal = long.MaxValue >> FractBitCnt;

        long mRaw; //raw视为原值*65536之后的结果
        public long Raw => mRaw;

        public Fixed64(int intVal) { mRaw = (long)intVal << FractBitCnt; }
        public Fixed64(long longVal) { mRaw = longVal << FractBitCnt; }
        public Fixed64(float fVal) { mRaw = (long)(fVal * (1 << FractBitCnt)); }
        public Fixed64(double dVal) { mRaw = (long)(dVal * (1 << FractBitCnt)); }
        public Fixed64(long raw, int f) { mRaw = raw; }

        public static Fixed64 operator +(Fixed64 a, Fixed64 b)
        {
            return new Fixed64(a.mRaw + b.mRaw, 0);
        }

        public static Fixed64 operator -(Fixed64 a, Fixed64 b)
        {
            return new Fixed64(a.mRaw - b.mRaw, 0);
        }

        public static Fixed64 operator *(Fixed64 a, Fixed64 b)
        {
            return new Fixed64((a.mRaw * b.mRaw) >> FractBitCnt, 0);
        }

        public static Fixed64 operator /(Fixed64 a, Fixed64 b)
        {
            return new Fixed64((a.mRaw << FractBitCnt) / b.mRaw, 0);
        }

        public static explicit operator double(Fixed64 fixVal)
        {
            return (double)(fixVal.mRaw >> FractBitCnt) + (fixVal.mRaw & FractMask) / (double)FractRange;
        }
    }
}
