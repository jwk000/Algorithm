using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static float Dot(Vector3 u, Vector3 v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        }

        public static Vector3 Cross(Vector3 u, Vector3 v)
        {
            return new Vector3(u.Y * v.Z - v.Y * u.Z, v.X * u.Z - u.X * v.Z, u.X * v.Y - v.X * u.Y);
        }

        public static Vector3 operator +(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }

        public static Vector3 operator -(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        }

        public static Vector3 operator *(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X * v.X, u.Y * v.Y, u.Z * v.Z);
        }
        public static Vector3 operator *(float f, Vector3 v)
        {
            return new Vector3(f * v.X, f * v.Y, f * v.Z);
        }
        public static Vector3 operator *(Vector3 u, float f)
        {
            return new Vector3(u.X * f, u.Y * f, u.Z * f);
        }
        public static Vector3 operator /(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X / v.X, u.Y / v.Y, u.Z / v.Z);
        }
        public static Vector3 operator /(Vector3 u, float f)
        {
            return new Vector3(u.X / f, u.Y / f, u.Z / f);
        }

        public static bool operator ==(Vector3 u, Vector3 v)
        {
            return u.X == v.X && u.Y == v.Y && u.Z == v.Z;
        }
        public static bool operator !=(Vector3 u, Vector3 v)
        {
            return u.X != v.X || u.Y != v.Y || u.Z != v.Z;
        }

        public Vector3 Normalize()
        {
            float len = Length();
            return new Vector3(X / len, Y / len, Z / len);
        }

        public float Length()
        {
            return MathF.Sqrt(X * X + Y * Y + Z * Z);
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
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

            return this == (Vector3)obj;
        }

        public override int GetHashCode()
        {
            return (int)(X * 100000000 + Y*10000+Z);
        }
    }
}
