﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

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

        public static Point2 operator+(Point2 lhs, Vector2 rhs)
        {
            return new Point2(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }
    }
}
