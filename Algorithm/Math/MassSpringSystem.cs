using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class MassPoint
    {
        public float mass;
        public Vector2 position;
        public bool pin;//是否固定
        public Vector2 speed;
        public Vector2 force;
        public MassPoint(float mass, Vector2 position, bool pin)
        {
            this.mass = mass;
            this.position = position;
            this.pin = pin;
            speed = Vector2.Zero;
            force = Vector2.Zero;
        }
    }

    public class Spring
    {
        public int a;
        public int b;
        public float ks;//劲度系数
        public float restLength;
        public float realLength;

        public Spring(int a, int b, float ks, float rest)
        {
            this.a = a;
            this.b = b;
            this.ks = ks;
            this.restLength = rest;
            this.realLength = rest;
        }
    }

    public class MassSpringSystem
    {
        public List<MassPoint> massPoints = new List<MassPoint>();
        public List<Spring> springs = new List<Spring>();
        const float Ox = 500;
        const float Oy = 100;
        const float Rest = 50;
        const float Ks = 800;
        const float Kd = 0.8f;//内部阻尼
        const float Damping = 0.5f;//空气阻尼
        public void Init()
        {

            massPoints.Add(new MassPoint(10, new Vector2(Ox + Rest * 0, Oy), true));
            massPoints.Add(new MassPoint(10, new Vector2(Ox + Rest * 1, Oy), false));
            massPoints.Add(new MassPoint(10, new Vector2(Ox + Rest * 2, Oy), false));
            massPoints.Add(new MassPoint(10, new Vector2(Ox + Rest * 3, Oy), false));
            massPoints.Add(new MassPoint(10, new Vector2(Ox + Rest * 4, Oy), false));
            massPoints.Add(new MassPoint(10, new Vector2(Ox + Rest * 5, Oy), false));
            massPoints.Add(new MassPoint(10, new Vector2(Ox + Rest * 6, Oy), false));
            massPoints.Add(new MassPoint(10, new Vector2(Ox + Rest * 7, Oy), false));

            springs.Add(new Spring(0, 1, Ks, Rest));
            springs.Add(new Spring(1, 2, Ks, Rest));
            springs.Add(new Spring(2, 3, Ks, Rest));
            springs.Add(new Spring(3, 4, Ks, Rest));
            springs.Add(new Spring(4, 5, Ks, Rest));
            springs.Add(new Spring(5, 6, Ks, Rest));
            springs.Add(new Spring(6, 7, Ks, Rest));

        }

        public void Update(float t)
        {
            //重力
            foreach (MassPoint p in massPoints)
            {
                p.force = new Vector2(0, p.mass * 9.8f);
            }

            //弹力
            foreach (Spring s in springs)
            {
                MassPoint Pa = massPoints[s.a];
                MassPoint Pb = massPoints[s.b];
                Vector2 ab = Pb.position - Pa.position;//a->b
                //b给a的力
                Vector2 Fa = Ks * ab.Normalize() * (ab.Length() - s.restLength);
                //内部阻力
                Vector2 Fd = Kd * ab.Normalize() *MathF.Abs( Vector2.Dot((Pb.speed - Pa.speed), ab.Normalize()));
                Fa = Fa - Fd;
                //a给b的力
                Vector2 Fb = Vector2.Zero - Fa;
                Pa.force = Pa.force + Fa;
                Pb.force = Pb.force + Fb;
            }



            //速度和位移
            foreach (MassPoint p in massPoints)
            {
                if (p.pin)
                {
                    continue;
                }
                Vector2 acc = (p.force - p.speed * Damping) / p.mass;//加速度
                p.speed = p.speed + acc * t;
                p.position = p.position + p.speed * t;

            }
        }

        public List<Vector2> GetPoints()
        {
            List<Vector2> list = new List<Vector2>();
            foreach (MassPoint p in massPoints)
            {
                list.Add(p.position);
            }
            return list;
        }


    }
}
