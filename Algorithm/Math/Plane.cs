using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //3D平面
    public class Plane
    {
        public float D;
        public Vector3 Normal;

        public Plane(Vector4 value)
        {

        }
        public Plane(Vector3 normal, float d)
        {

        }
        public Plane(float x, float y, float z, float d)
        {

        }

        //public static Plane CreateFromVertices(Vector3 point1, Vector3 point2, Vector3 point3)
        //{

        //}
        //public static float Dot(Plane plane, Vector4 value)
        //{

        //}
        //public static float DotCoordinate(Plane plane, Vector3 value)
        //{

        //}
        //public static float DotNormal(Plane plane, Vector3 value)
        //{

        //}
        //public static Plane Normalize(Plane value)
        //{

        //}
        //public static Plane Transform(Plane plane, Matrix4x4 matrix)
        //{

        //}
        //public static Plane Transform(Plane plane, Quaternion rotation)
        //{

        //}
        //public readonly bool Equals(Plane other)
        //{

        //}
        //public readonly override bool Equals([NotNullWhen(true)] object? obj)
        //{

        //}
        //public readonly override int GetHashCode()
        //{

        //}
        //public readonly override string ToString()
        //{

        //}

        //public static bool operator ==(Plane value1, Plane value2)
        //{

        //}
        //public static bool operator !=(Plane value1, Plane value2)
        //{

        //}
    }
}
