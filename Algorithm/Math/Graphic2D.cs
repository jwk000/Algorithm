using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public struct Point2
    {
        public float X, Y;
        public Point2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator -(Point2 lhs, Point2 rhs)
        {
            return new Vector2(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static Point2 operator +(Point2 lhs, Vector2 rhs)
        {
            return new Point2(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }
    }

    //2d大小
    public struct Size2
    {
        public float Width, Height;

        public Size2(float w, float h)
        {
            Width = w; Height = h;
        }

        public static Size2 operator +(Size2 lhs, Size2 rhs)
        {
            return new Size2(lhs.Width + rhs.Width, lhs.Height + rhs.Height);
        }

        public static Size2 operator -(Size2 lhs, Size2 rhs)
        {
            return new Size2(lhs.Width - rhs.Width, lhs.Height - rhs.Height);
        }
    }

    public class Line2
    {
        public Point2 From;
        public Point2 To;

        public Line2(Point2 from, Point2 to)
        {
            From = from;
            To = to;
        }
    }

    public class Circle
    {
        public Point2 Center;
        public int Radius;

        public Circle(Point2 center, int radius)
        {
            Center = center;
            Radius = radius;
        }
    }

    //xy轴对齐的矩形
    public class Rect
    {
        public float X, Y, Width, Height;

        public Rect(float x, float y, float w, float h)
        {
            X = x; Y = y; Width = w; Height = h;
        }

        public Rect(Point2 p, Size2 s)
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
        public static bool IsIntersect(Rect a, Rect b)
        {
            return false;
        }
        //矩形交集
        public static Rect Intersect(Rect a, Rect b)
        {
            return null;
        }
        //矩形并集
        public static Rect Union(Rect a, Rect b)
        {
            return null;
        }


    }



}
