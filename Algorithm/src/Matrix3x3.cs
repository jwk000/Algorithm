using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //矩阵
    public class Matrix3x3
    {
        public float m11, m12, m13, m21, m22, m23, m31, m32, m33;

        public Matrix3x3()
        {

        }

        public Matrix3x3(float m11, float m12, float m13,  float m21, float m22, float m23, float m31, float m32, float m33)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
        }

        //转置
        public void Transpose()
        {
            (m12, m21, m13, m31, m23, m32) = (m21, m12, m31, m13,  m32, m23);
        }


        //加法
        public static Matrix3x3 operator +(Matrix3x3 lhs, Matrix3x3 rhs)
        {
            return new Matrix3x3(
                lhs.m11 + rhs.m11,
                lhs.m12 + rhs.m12,
                lhs.m13 + rhs.m13,
                lhs.m21 + rhs.m21,
                lhs.m22 + rhs.m22,
                lhs.m23 + rhs.m23,

                lhs.m31 + rhs.m31,
                lhs.m32 + rhs.m32,
                lhs.m33 + rhs.m33

                );
        }
        //减法
        public static Matrix3x3 operator -(Matrix3x3 lhs, Matrix3x3 rhs)
        {
            return new Matrix3x3(
                lhs.m11 - rhs.m11,
                lhs.m12 - rhs.m12,
                lhs.m13 - rhs.m13,
                lhs.m21 - rhs.m21,
                lhs.m22 - rhs.m22,
                lhs.m23 - rhs.m23,
                lhs.m31 - rhs.m31,
                lhs.m32 - rhs.m32,
                lhs.m33 - rhs.m33
                );
        }
        //负号
        public static Matrix3x3 operator -(Matrix3x3 lhs)
        {
            return new Matrix3x3(
                -lhs.m11,
                -lhs.m12,
                -lhs.m13,
                -lhs.m21,
                -lhs.m22,
                -lhs.m23,
                -lhs.m31,
                -lhs.m32,
                -lhs.m33
                );
        }
        //乘法
        public static Matrix3x3 operator *(Matrix3x3 lhs, Matrix3x3 rhs)
        {
            return new Matrix3x3(
                lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31,
                lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32,
                lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33,
                lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31,
                lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32,
                lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33,
                lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31,
                lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32,
                lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33
                );
        }

        //乘以向量
        public static Vector3 operator *(Matrix3x3 lhs, Vector4 rhs)
        {
            return new Vector3(
                lhs.m11 * rhs.X + lhs.m12 * rhs.Y + lhs.m13 * rhs.Z,
                lhs.m21 * rhs.X + lhs.m22 * rhs.Y + lhs.m23 * rhs.Z ,
                lhs.m31 * rhs.X + lhs.m32 * rhs.Y + lhs.m33 * rhs.Z );
        }

        //乘以标量
        public static Matrix3x3 operator *(Matrix3x3 lhs, float f)
        {
            return new Matrix3x3(lhs.m11 * f, lhs.m12 * f, lhs.m13 * f,  lhs.m21 * f, lhs.m22 * f, lhs.m23 * f, lhs.m31 * f, lhs.m32 * f, lhs.m33 * f);
        }

        //相等
        public static bool operator ==(Matrix3x3 lhs, Matrix3x3 rhs)
        {
            return
                lhs.m11 == rhs.m11 &&
                lhs.m12 == rhs.m12 &&
                lhs.m13 == rhs.m13 &&
                lhs.m21 == rhs.m21 &&
                lhs.m22 == rhs.m22 &&
                lhs.m23 == rhs.m23 &&
                lhs.m31 == rhs.m31 &&
                lhs.m32 == rhs.m32 &&
                lhs.m33 == rhs.m33 ;

        }

        public static bool operator !=(Matrix3x3 lhs, Matrix3x3 rhs)
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
            var rhs = obj as Matrix3x3;
            if (rhs == null) return false;
            return this == rhs;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
