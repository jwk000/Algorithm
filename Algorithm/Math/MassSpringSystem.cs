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
        public Edge edge;//接触的边缘
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

    public class Edge
    {
        public Vector2 c;
        public Vector2 d;

        public Edge(Vector2 c, Vector2 d)
        {
            this.c = c;
            this.d = d;
        }
    }

    public class MassSpringSystem
    {
        public List<Edge> edges = new List<Edge>();//边缘
        public List<MassPoint> massPoints = new List<MassPoint>();
        public List<Spring> springs = new List<Spring>();
        const float Ks = 300;
        const float Kd = 0.8f;//内部阻尼
        const float Damping = 0.1f;//空气阻尼
        public void MakeRope()
        {
            const float Ox = 500;
            const float Oy = 100;
            const float Rest = 50;

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

        public void MakeBall()
        {
            const float Ox = 300;
            const float Oy = 200;
            const float Rest = 50;
            massPoints.Clear();
            springs.Clear();
            edges.Clear();

            massPoints.Add(new MassPoint(10, new Vector2(Ox, Oy), false));
            for (int i = 0; i < 6; i++)
            {
                float x = Ox + MathF.Sin(i * 60f / 180f * MathF.PI) * Rest;
                float y = Oy + MathF.Cos(i * 60f / 180f * MathF.PI) * Rest;

                massPoints.Add(new MassPoint(10, new Vector2(x, y), false));
                springs.Add(new Spring(0, i + 1, Ks, Rest));
                int j = i + 2;
                if (j == 7) j = 1;
                springs.Add(new Spring(i + 1, j, Ks, Rest));
            }

            var e0 = new Vector2(100, 400);
            var e1 = new Vector2(600, 600);
            var e2 = new Vector2(600, 300);
            edges.Add(new Edge(e0,e1));
            edges.Add(new Edge(e1,e2));
        }

        public void Update(float t)
        {
            foreach (MassPoint p in massPoints)
            {
                //重力
                p.force = new Vector2(0, p.mass * 9.8f);
                //空气阻力
                p.force = p.force - p.speed * Damping;
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
                Vector2 Fd = Kd * ab.Normalize() * MathF.Abs(Vector2.Dot((Pb.speed - Pa.speed), ab.Normalize()));
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
                Vector2 acc = p.force / p.mass;//加速度
                p.speed = p.speed + acc * t; //下一帧的速度
                Vector2 pos = p.position + p.speed * t;//下一帧的位置

                //边缘求交点
                for(int i = 0; i < edges.Count - 1; i++)
                {
                    Vector2 q;
                    if (MyMath.line2CrossPoint(p.position,pos, edges[i].c, edges[i].d, out q))
                    {
                        p.position = q;
                        p.speed = Vector2.Zero;
                        p.edge = edges[i];
                        return;
                    }
                }

                //没有到边缘
                p.position = pos;
                
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

        public List<Spring> GetSprings()
        {
            return springs;
        }

        public List<Edge> GetEdges()
        {
            return edges;
        }
    }
}
