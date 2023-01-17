using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static float Dot(Vector4 u, Vector4 v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z + u.W * v.W;
        }

        public static Vector4 operator +(Vector4 u, Vector4 v)
        {
            return new Vector4(u.X + v.X, u.Y + v.Y, u.Z + v.Z, u.W + v.W);
        }

        public static Vector4 operator -(Vector4 u, Vector4 v)
        {
            return new Vector4(u.X - v.X, u.Y - v.Y, u.Z - v.Z, u.W - v.W);
        }

        public static Vector4 operator *(Vector4 u, Vector4 v)
        {
            return new Vector4(u.X * v.X, u.Y * v.Y, u.Z * v.Z, u.W * v.W);
        }
        public static Vector4 operator *(float f, Vector4 v)
        {
            return new Vector4(f * v.X, f * v.Y, f * v.Z, f * v.W);
        }
        public static Vector4 operator *(Vector4 u, float f)
        {
            return new Vector4(u.X * f, u.Y * f, u.Z * f, u.W * f);
        }
        public static Vector4 operator /(Vector4 u, Vector4 v)
        {
            return new Vector4(u.X / v.X, u.Y / v.Y, u.Z / v.Z, u.W / v.W);
        }
        public static Vector4 operator /(Vector4 u, float f)
        {
            return new Vector4(u.X / f, u.Y / f, u.Z / f, u.W / f);
        }

        public static bool operator ==(Vector4 u, Vector4 v)
        {
            return u.X == v.X && u.Y == v.Y && u.Z == v.Z && u.W == v.W;
        }
        public static bool operator !=(Vector4 u, Vector4 v)
        {
            return u.X != v.X || u.Y != v.Y || u.Z != v.Z || u.W != v.W;
        }

        public Vector4 Normalize()
        {
            float len = Length();
            return new Vector4(X / len, Y / len, Z / len, W / len);
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

            return this == (Vector4)obj;
        }

        public override int GetHashCode()
        {
            return (int)(X * 100000000 + Y * 10000 + Z * 100 + W);
        }
    }
}
