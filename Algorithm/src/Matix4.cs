using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //矩阵
    public class Matrix4
    {
        public float m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44;

        public Matrix4()
        {

        }

        public Matrix4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m24 = m24;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
            this.m34 = m34;
            this.m41 = m41;
            this.m42 = m42;
            this.m43 = m43;
            this.m44 = m44;

        }

        //转置
        public void Transpose()
        {
            (m12, m21, m13, m31, m14, m41, m23, m32, m24, m42, m34, m43) = (m21, m12, m31, m13, m41, m14, m32, m23, m42, m24, m43, m34);
        }


        //加法
        public static Matrix4 operator +(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4(
                lhs.m11 + rhs.m11,
                lhs.m12 + rhs.m12,
                lhs.m13 + rhs.m13,
                lhs.m14 + rhs.m14,
                lhs.m21 + rhs.m21,
                lhs.m22 + rhs.m22,
                lhs.m23 + rhs.m23,
                lhs.m24 + rhs.m24,
                lhs.m31 + rhs.m31,
                lhs.m32 + rhs.m32,
                lhs.m33 + rhs.m33,
                lhs.m34 + rhs.m34,
                lhs.m41 + rhs.m41,
                lhs.m42 + rhs.m42,
                lhs.m43 + rhs.m43,
                lhs.m44 + rhs.m44
                );
        }
        //减法
        public static Matrix4 operator -(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4(
                lhs.m11 - rhs.m11,
                lhs.m12 - rhs.m12,
                lhs.m13 - rhs.m13,
                lhs.m14 - rhs.m14,
                lhs.m21 - rhs.m21,
                lhs.m22 - rhs.m22,
                lhs.m23 - rhs.m23,
                lhs.m24 - rhs.m24,
                lhs.m31 - rhs.m31,
                lhs.m32 - rhs.m32,
                lhs.m33 - rhs.m33,
                lhs.m34 - rhs.m34,
                lhs.m41 - rhs.m41,
                lhs.m42 - rhs.m42,
                lhs.m43 - rhs.m43,
                lhs.m44 - rhs.m44
                );
        }
        //负号
        public static Matrix4 operator -(Matrix4 lhs)
        {
            return new Matrix4(
                -lhs.m11,
                -lhs.m12,
                -lhs.m13,
                -lhs.m14,
                -lhs.m21,
                -lhs.m22,
                -lhs.m23,
                -lhs.m24,
                -lhs.m31,
                -lhs.m32,
                -lhs.m33,
                -lhs.m34,
                -lhs.m41,
                -lhs.m42,
                -lhs.m43,
                -lhs.m44
                );
        }
        //乘法
        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4(
                lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31 + lhs.m14 * rhs.m41,
                lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32 + lhs.m14 * rhs.m42,
                lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33 + lhs.m14 * rhs.m43,
                lhs.m11 * rhs.m14 + lhs.m12 * rhs.m24 + lhs.m13 * rhs.m34 + lhs.m14 * rhs.m44,
                lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31 + lhs.m24 * rhs.m41,
                lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32 + lhs.m24 * rhs.m42,
                lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33 + lhs.m24 * rhs.m43,
                lhs.m21 * rhs.m14 + lhs.m22 * rhs.m24 + lhs.m23 * rhs.m34 + lhs.m24 * rhs.m44,
                lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31 + lhs.m34 * rhs.m41,
                lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32 + lhs.m34 * rhs.m42,
                lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33 + lhs.m34 * rhs.m43,
                lhs.m31 * rhs.m14 + lhs.m32 * rhs.m24 + lhs.m33 * rhs.m34 + lhs.m34 * rhs.m44,
                lhs.m41 * rhs.m11 + lhs.m42 * rhs.m21 + lhs.m43 * rhs.m31 + lhs.m44 * rhs.m41,
                lhs.m41 * rhs.m12 + lhs.m42 * rhs.m22 + lhs.m43 * rhs.m32 + lhs.m44 * rhs.m42,
                lhs.m41 * rhs.m13 + lhs.m42 * rhs.m23 + lhs.m43 * rhs.m33 + lhs.m44 * rhs.m43,
                lhs.m41 * rhs.m14 + lhs.m42 * rhs.m24 + lhs.m43 * rhs.m34 + lhs.m44 * rhs.m44

                );
        }

        //乘以向量
        public static MyVector4 operator *(Matrix4 lhs, MyVector4 rhs)
        {
            return new MyVector4(
                lhs.m11 * rhs.X + lhs.m12 * rhs.Y + lhs.m13 * rhs.Z + lhs.m14 * rhs.W,
                lhs.m21 * rhs.X + lhs.m22 * rhs.Y + lhs.m23 * rhs.Z + lhs.m24 * rhs.W,
                lhs.m31 * rhs.X + lhs.m32 * rhs.Y + lhs.m33 * rhs.Z + lhs.m34 * rhs.W,
                lhs.m41 * rhs.X + lhs.m42 * rhs.Y + lhs.m43 * rhs.Z + lhs.m44 * rhs.W);
        }

        //乘以标量
        public static Matrix4 operator *(Matrix4 lhs, float f)
        {
            return new Matrix4(lhs.m11 * f, lhs.m12 * f, lhs.m13 * f, lhs.m14 * f, lhs.m21 * f, lhs.m22 * f, lhs.m23 * f, lhs.m24 * f, lhs.m31 * f, lhs.m32 * f, lhs.m33 * f, lhs.m34 * f, lhs.m41 * f, lhs.m42 * f, lhs.m43 * f, lhs.m44 * f);
        }

        public static bool operator ==(Matrix4 lhs, Matrix4 rhs)
        {
            return
                lhs.m11 == rhs.m11 &&
                lhs.m12 == rhs.m12 &&
                lhs.m13 == rhs.m13 &&
                lhs.m14 == rhs.m14 &&
                lhs.m21 == rhs.m21 &&
                lhs.m22 == rhs.m22 &&
                lhs.m23 == rhs.m23 &&
                lhs.m24 == rhs.m24 &&
                lhs.m31 == rhs.m31 &&
                lhs.m32 == rhs.m32 &&
                lhs.m33 == rhs.m33 &&
                lhs.m34 == rhs.m34 &&
                lhs.m41 == rhs.m41 &&
                lhs.m42 == rhs.m42 &&
                lhs.m43 == rhs.m43 &&
                lhs.m44 == rhs.m44;

        }

        public static bool operator !=(Matrix4 lhs, Matrix4 rhs)
        {
            return !(lhs == rhs);
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
            var rhs = obj as Matrix4;
            if (rhs == null) return false;
            return this == rhs;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
