using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //xy轴对齐的矩形
    public class Rect
    {
        public float X, Y, Width, Height;

        public Rect(float x, float y, float w, float h)
        {
            X = x; Y = y; Width = w; Height = h;
        }

        public Rect(Point2 p, Size2D s)
        {
            X = p.X; Y = p.Y; Width = s.Width; Height = s.Height;
        }

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        //点在矩形内
        public bool Contains(Point2 p)
        {
            return (p.X - X > 0 && p.X - X < Width && p.Y - Y > 0 && p.Y - Y < Height);
        }

        //矩形在矩形内
        public bool Contains(Rect r)
        {
            return (r.X < X && r.Y < Y && Right > r.Right && Bottom > r.Bottom);
        }

        //矩形相交
        public static bool IsIntersect(Rect a,Rect b)
        {
            return false;
        }
        //矩形交集
        public static Rect Intersect(Rect a,Rect b)
        {
            return null;
        }
        //矩形并集
        public static Rect Union(Rect a,Rect b)
        {
            return null;
        }

        
    }
}
