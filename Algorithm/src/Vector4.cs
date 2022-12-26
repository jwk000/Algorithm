using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class MyVector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public MyVector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static float Dot(MyVector4 u, MyVector4 v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z + u.W * v.W;
        }

        public static MyVector4 operator +(MyVector4 u, MyVector4 v)
        {
            return new MyVector4(u.X + v.X, u.Y + v.Y, u.Z + v.Z, u.W + v.W);
        }

        public static MyVector4 operator -(MyVector4 u, MyVector4 v)
        {
            return new MyVector4(u.X - v.X, u.Y - v.Y, u.Z - v.Z, u.W - v.W);
        }

        public static MyVector4 operator *(MyVector4 u, MyVector4 v)
        {
            return new MyVector4(u.X * v.X, u.Y * v.Y, u.Z * v.Z, u.W * v.W);
        }
        public static MyVector4 operator *(float f, MyVector4 v)
        {
            return new MyVector4(f * v.X, f * v.Y, f * v.Z, f * v.W);
        }
        public static MyVector4 operator *(MyVector4 u, float f)
        {
            return new MyVector4(u.X * f, u.Y * f, u.Z * f, u.W * f);
        }
        public static MyVector4 operator /(MyVector4 u, MyVector4 v)
        {
            return new MyVector4(u.X / v.X, u.Y / v.Y, u.Z / v.Z, u.W / v.W);
        }
        public static MyVector4 operator /(MyVector4 u, float f)
        {
            return new MyVector4(u.X / f, u.Y / f, u.Z / f, u.W / f);
        }

        public static bool operator ==(MyVector4 u, MyVector4 v)
        {
            return u.X == v.X && u.Y == v.Y && u.Z == v.Z && u.W == v.W;
        }
        public static bool operator !=(MyVector4 u, MyVector4 v)
        {
            return u.X != v.X || u.Y != v.Y || u.Z != v.Z || u.W != v.W;
        }

        public MyVector4 Normalize()
        {
            float len = Length();
            return new MyVector4(X / len, Y / len, Z / len, W / len);
        }

        public float Length()
        {
            return MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z},{W}";
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

            return this == (MyVector4)obj;
        }

        public override int GetHashCode()
        {
            return (int)(X * 100000000 + Y * 10000 + Z * 100 + W);
        }
    }
}
