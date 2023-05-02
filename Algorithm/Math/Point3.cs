using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public struct Point3
    {
        public float X, Y, Z;
        public Point3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 operator -(Point3 lhs, Point3 rhs)
        {
            return new Vector3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static Point3 operator +(Point3 lhs, Vector3 rhs)
        {
            return new Point3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static float Distance(Point3 lhs,Point3 rhs)
        {
            return (lhs - rhs).Length();
        }
    }
}
