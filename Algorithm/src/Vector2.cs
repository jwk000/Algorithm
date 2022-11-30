using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class MyVector2
    {
        public float X;
        public float Y;

        public MyVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static float Dot(MyVector2 u, MyVector2 v)
        {
            return u.X * v.X + u.Y * v.Y;
        }

        public static MyVector2 operator +(MyVector2 u, MyVector2 v)
        {
            return new MyVector2(u.X + v.X, u.Y + v.Y);
        }

        public static MyVector2 operator -(MyVector2 u, MyVector2 v)
        {
            return new MyVector2(u.X - v.X, u.Y - v.Y);
        }

        public static MyVector2 operator *(MyVector2 u, MyVector2 v)
        {
            return new MyVector2(u.X * v.X, u.Y * v.Y);
        }
        public static MyVector2 operator *(float f, MyVector2 v)
        {
            return new MyVector2(f * v.X, f * v.Y);
        }
        public static MyVector2 operator *(MyVector2 u, float f)
        {
            return new MyVector2(u.X * f, u.Y * f);
        }
        public static MyVector2 operator /(MyVector2 u, MyVector2 v)
        {
            return new MyVector2(u.X / v.X, u.Y / v.Y);
        }
        public static MyVector2 operator /(MyVector2 u, float f)
        {
            return new MyVector2(u.X / f, u.Y / f);
        }

        public static bool operator ==(MyVector2 u, MyVector2 v)
        {
            return u.X == v.X && u.Y == v.Y;
        }
        public static bool operator !=(MyVector2 u, MyVector2 v)
        {
            return u.X != v.X || u.Y != v.Y;
        }

        public MyVector2 Normalize()
        {
            float len = Length();
            return new MyVector2(X / len, Y / len);
        }

        public float Length()
        {
            return MathF.Sqrt(X * X + Y * Y);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return this == (MyVector2)obj;
        }

        public override int GetHashCode()
        {
            return (int)(X * 1000000 + Y);
        }
    }
}
