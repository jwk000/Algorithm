using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //2d大小
    public struct Size2D
    {
        public float Width,Height;

        public Size2D(float w,float h)
        {
            Width = w;Height = h;
        } 

        public static Size2D operator+(Size2D lhs,Size2D rhs)
        {
            return new Size2D(lhs.Width + rhs.Width, lhs.Height + rhs.Height);
        }

        public static Size2D operator -(Size2D lhs, Size2D rhs)
        {
            return new Size2D(lhs.Width - rhs.Width, lhs.Height - rhs.Height);
        }
    }
}
