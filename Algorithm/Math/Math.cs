using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public static class MyMath
    {
        //最大公约数 Greatest Common Divisor(GCD)
        public static int gcd(int a, int b)
        {
            int c;
            while (b != 0)
            {
                c = a;
                a = b;
                b = c % b;
            }
            return a;
        }

        //最小公倍数（Least Common Multiple，LCM）
        //最小公倍数=两数的乘积/最大公约数
        public static int lcm(int a, int b)
        {
            return a * b / gcd(a, b);
        }

        //牛顿迭代法求平方根 Xn+1 = 1/2*(Xn+n/Xn)
        public static float sqrt(float c)
        {
            float x = c, y = 0;
            while (MathF.Abs(x - y) > float.Epsilon)
            {
                y = x;
                x = 0.5f * (x + c / x);
            }
            return x;
        }

        //扩展欧几里得算法 求 ax+by=gcd(a,b) 的一组解，返回值是gcd(a,b)
        //【裴蜀定理】 设 a,b 为正整数，则关于 x,y 的方程 ax+by=c 有整数解当且仅当c是gcd(a,b) 的倍数。
        public static int exgcd(int a, int b, out int x, out int y)
        {
            if (b == 0) { x = 1; y = 0; return a; }
            else
            {
                int r = exgcd(b, a % b, out y, out x);
                y -= (a / b) * x;
                return r;
            }
        }

        //a模b的逆元，a和b互质，则a*inv(a)=1(mod b)，求inv(a)
        public static int inv(int a, int b)
        {
            int g = exgcd(a, b, out int x, out int y);
            if (g != 1) return -1;
            return (x % b + b) % b;
        }


        //中国剩余定理求解同余方程组 
        // x = bi(mod ai) a是模数 b是余数 x是通解
        public static int crt(int[] a, int[] b)
        {
            int p = 1;
            int x = 0;
            foreach (int i in a) p *= i;
            for (int i = 0; i < a.Length; i++)
            {
                int r = p / a[i];
                x += (b[i] * r * inv(r, a[i])) % p;
            }
            return x % p;
        }

        public static float lerp(float a, float b, float w)
        {
            return a * (1 - w) + b * w;
        }

        public static int clamp(int v, int min, int max)
        {
            return v < min ? min : v > max ? max : v;
        }

        //快速幂，求a的n次方，对n二进制分解，最多32个因子
        public static float qpow(float a, int n)
        {
            float ans = 1;
            while (n > 0)
            {
                if ((n & 1) > 0)
                {
                    ans *= a;
                }
                a *= a;
                n >>= 1;
            }
            return ans;
        }

        //龟速乘，用加法实现乘法，为了防止大数乘法溢出，每次加法之后取模
        public static long qmul(long a,long b,long M)
        {
            long ans = 0;
            while (b > 0)
            {
                if ((b & 1) == 1) ans = (ans + a) % M;
                a = (a + a) % M;
                b >>= 1;
            }
            return ans;
        }

        static MyRandom _randor = new MyRandom();
        //打乱数组
        public static void shuffle(int[] arr)
        {
            for(int i = arr.Length - 1; i > 0; i--)
            {
                int r = _randor.Next(0, i);
                int t = arr[i];
                arr[i] = arr[r];
                arr[r] = t;
            }
        }
    }
}
