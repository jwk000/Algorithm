using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //三阶贝塞尔曲线，需要4个控制点
    //b10=(1-t)^2*b0+2*(1-t)*t*b1+t^2(b2)
    public class BezierCurve
    {
        //输入控制点，输出结果
        public List<Vector2> DrawBezier(List<Vector2> points)
        {
            float minX = 1000;
            float maxX = 0;
            float minY = 1000;
            float maxY = 0;
            float min = 0;
            float max = 0;
            foreach (Vector2 p in points)
            {
                if (p.X < minX) minX = p.X;
                if (p.Y < minY) minY = p.Y;
                if (p.X > maxX) maxX = p.X;
                if (p.Y > maxY) maxY = p.Y;
            }
            if (maxX - minX > maxY - minY)
            {
                min = minX;
                max = maxX;
            }
            else
            {
                min = minY;
                max = maxY;
            }

            List<Vector2> ans = new List<Vector2>();
            for (float d = min; d <= max; d++)
            {
                float t = (d - min) / (max - min);
                ans.Add(Draw(points, t));
            }
            return ans;
        }

        Vector2 Draw(List<Vector2> points, float t)
        {
            if (points.Count == 1)
            {
                return points[0];
            }

            List<Vector2> ans = new List<Vector2>();

            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector2 p = points[i] + (points[i + 1] - points[i]) * t;
                ans.Add(p);
            }
            return Draw(ans, t);
        }

    }
}
