using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //复数
    public struct Complex
    {
        public float Real, Imag;

        public Complex(float r, float i)
        {
            Real = r;
            Imag = i;
        }

        public static Complex One = new Complex(1, 0);
        public static Complex Zero = new Complex(0, 0);
        public static Complex NaN = new Complex(float.NaN, float.NaN);

        public float Magnitude { get { return MyMath.sqrt(Real * Real + Imag * Imag); } }

        public override string ToString()
        {
            return $"({Real},{Imag})";
        }
        public override bool Equals(object obj)
        {
            return this == (Complex)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        //相等
        public static bool operator==(Complex lhs,Complex rhs)
        {
            return lhs.Real == rhs.Real && lhs.Imag == rhs.Imag;
        }

        //不等
        public static bool operator !=(Complex lhs, Complex rhs)
        {
            return lhs.Real != rhs.Real || lhs.Imag != rhs.Imag;
        }

        //加法
        public static Complex operator +(Complex lhs, Complex rhs)
        {
            return new Complex(lhs.Real + rhs.Real, lhs.Imag + rhs.Imag);
        }

        //减法
        public static Complex operator -(Complex lhs, Complex rhs)
        {
            return new Complex(lhs.Real - rhs.Real, lhs.Imag - rhs.Imag);
        }

        //取负
        public static Complex operator -(Complex lhs)
        {
            return new Complex(-lhs.Real, -lhs.Imag);
        }

        //乘法（多项式乘法）
        public static Complex operator *(Complex lhs, Complex rhs)
        {
            return new Complex(lhs.Real * rhs.Real - lhs.Imag * rhs.Imag, lhs.Real * rhs.Imag + lhs.Imag * rhs.Real);
        }

        //除法（多项式除法，分母实数化）
        public static Complex operator /(Complex lhs, Complex rhs)
        {
            float r = lhs.Real * rhs.Real + lhs.Imag * rhs.Imag;
            float i = lhs.Imag * rhs.Real - lhs.Real * rhs.Imag;
            float z = rhs.Real * rhs.Real + rhs.Imag * rhs.Imag;
            if (z == 0)
            {
                return NaN;
            }
            return new Complex(r/z,i/z);
        }


    }
}
