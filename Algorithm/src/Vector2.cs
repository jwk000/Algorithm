using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static float Dot(Vector2 u, Vector2 v)
        {
            return u.X * v.X + u.Y * v.Y;
        }

        public static Vector2 operator +(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X + v.X, u.Y + v.Y);
        }

        public static Vector2 operator -(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X - v.X, u.Y - v.Y);
        }

        public static Vector2 operator *(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X * v.X, u.Y * v.Y);
        }
        public static Vector2 operator *(float f, Vector2 v)
        {
            return new Vector2(f * v.X, f * v.Y);
        }
        public static Vector2 operator *(Vector2 u, float f)
        {
            return new Vector2(u.X * f, u.Y * f);
        }
        public static Vector2 operator /(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X / v.X, u.Y / v.Y);
        }
        public static Vector2 operator /(Vector2 u, float f)
        {
            return new Vector2(u.X / f, u.Y / f);
        }

        public static bool operator ==(Vector2 u, Vector2 v)
        {
            return u.X == v.X && u.Y == v.Y;
        }
        public static bool operator !=(Vector2 u, Vector2 v)
        {
            return u.X != v.X || u.Y != v.Y;
        }

        public Vector2 Normalize()
        {
            float len = Length();
            return new Vector2(X / len, Y / len);
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

            return this == (Vector2)obj;
        }

        public override int GetHashCode()
        {
            return (int)(X * 1000000 + Y);
        }
    }
}
