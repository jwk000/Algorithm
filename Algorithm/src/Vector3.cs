using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class MyVector3
    {
        public float X;
        public float Y;
        public float Z;

        public MyVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static float Dot(MyVector3 u, MyVector3 v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        }

        public static MyVector3 Cross(MyVector3 u, MyVector3 v)
        {
            return new MyVector3(u.Y * v.Z - v.Y * u.Z, v.X * u.Z - u.X * v.Z, u.X * v.Y - v.X * u.Y);
        }

        public static MyVector3 operator +(MyVector3 u, MyVector3 v)
        {
            return new MyVector3(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }

        public static MyVector3 operator -(MyVector3 u, MyVector3 v)
        {
            return new MyVector3(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        }

        public static MyVector3 operator *(MyVector3 u, MyVector3 v)
        {
            return new MyVector3(u.X * v.X, u.Y * v.Y, u.Z * v.Z);
        }
        public static MyVector3 operator *(float f, MyVector3 v)
        {
            return new MyVector3(f * v.X, f * v.Y, f * v.Z);
        }
        public static MyVector3 operator *(MyVector3 u, float f)
        {
            return new MyVector3(u.X * f, u.Y * f, u.Z * f);
        }
        public static MyVector3 operator /(MyVector3 u, MyVector3 v)
        {
            return new MyVector3(u.X / v.X, u.Y / v.Y, u.Z / v.Z);
        }
        public static MyVector3 operator /(MyVector3 u, float f)
        {
            return new MyVector3(u.X / f, u.Y / f, u.Z / f);
        }

        public static bool operator ==(MyVector3 u, MyVector3 v)
        {
            return u.X == v.X && u.Y == v.Y && u.Z == v.Z;
        }
        public static bool operator !=(MyVector3 u, MyVector3 v)
        {
            return u.X != v.X || u.Y != v.Y || u.Z != v.Z;
        }

        public MyVector3 Normalize()
        {
            float len = Length();
            return new MyVector3(X / len, Y / len, Z / len);
        }

        public float Length()
        {
            return MathF.Sqrt(X * X + Y * Y + Z * Z);
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }
    }
}
