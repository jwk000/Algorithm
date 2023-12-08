using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public static class Vector2Utils
    {
        //两个线段求交点 ab cd
        public static bool line2CrossPoint(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 p)
        {
            p = Vector2.Zero;

            //ab的法线(xy镜像)
            Vector2 m = new Vector2(a.Y - b.Y, b.X - a.X);
            //a c d 在m上投影距离
            float dist_a_m = Vector2.Dot(a, m);
            float dist_c_m = Vector2.Dot(c, m);
            float dist_d_m = Vector2.Dot(d, m);

            //cd的法线(xy镜像)
            Vector2 n = new Vector2(d.Y - c.Y, c.X - d.X);
            //a b c 在n上投影距离
            float dist_a_n = Vector2.Dot(a, n);
            float dist_b_n = Vector2.Dot(b, n);
            float dist_c_n = Vector2.Dot(c, n);

            //判断相交
            if ((dist_a_m - dist_c_m) * (dist_a_m - dist_d_m) > 0 ||
                (dist_a_n - dist_c_n) * (dist_b_n - dist_c_n) > 0)
            {
                return false;
            }

            //求交点
            float t = (dist_a_n - dist_c_n) / (dist_a_n - dist_b_n);
            p = a + t * (b - a);
            return true;
        }

    }
}
