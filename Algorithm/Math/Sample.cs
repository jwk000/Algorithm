using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Algorithm
{

    //在w*h的矩形里采样n个点
    public class Sample
    {
        Random_MersenneTwister mRandor = new Random_MersenneTwister();

        //均匀随机采样
        public List<Point> RandomSample(int width, int height, int num)
        {
            List<Point> ret = new List<Point>();

            for (int i = 0; i < num; i++)
            {
                ret.Add(new Point(mRandor.Next(1, width), mRandor.Next(1, height)));
            }
            return ret;
        }



        //米切尔采样
        //（1）全屏随机一个点p，找到距离p最近的已采样点q，p和q的距离=d
        //（2）重复第一步k次，把d最大的点加入采样结果
        public List<Point> MichellSample(int width, int height, int num)
        {
            List<Point> ret = new List<Point>();
            //第一个点
            ret.Add(new Point(mRandor.Next(1, width), mRandor.Next(1, height)));

            for (int i = 0; i < num-1; i++)
            {
                Point p = new Point();
                int distance = 0;
                for (int n = 0; n < 30; n++)
                {
                    Point c = new Point(mRandor.Next(1, width), mRandor.Next(1, height));
                    int d = FindClosest(ret, c);
                    if (d > distance)
                    {
                        distance = d;
                        p = c;
                    }
                }
                ret.Add(p);
            }
            return ret;
        }

        //TODO K-D树优化
        int FindClosest(List<Point> samples, Point p)
        {
            Point closest = samples[0];
            int distance = int.MaxValue;
            foreach (Point q in samples)
            {
                int x = p.X - q.X;
                int y = p.Y - q.Y;
                int d = x * x + y * y;
                if (d < distance)
                {
                    distance = d;
                    closest = q;
                }
            }
            return distance;
        }

        /*
         * 泊松圆盘采样
         * 假设我们需要在一个宽高为(width,height)的平面内平均生成一堆的点，且这些点之间的距离不能小于r。
         * 我们可以先从平面内随机选一个点，然后在这个点附近随机找一些点，并判断这些点是否合法，合法的话则在这些点附近继续随机寻找，直到找不到合法点为止。
         * 算法简单说起来就是这样，下面详细说下细节。
         * 为了保证能尽量填满整个平面， 随机找点时，采用与中心点距离为[r,2r)的圆环内找，这个距离能保证找的点距离自身大于r，且不会离得太远，能填满整个平面。
         * (1)怎么判断一个点的附近已经找不到合法点了？算法定义了一个常量k，对于每个点，我们尝试在它附近随机找k次，如果都找不到，那么就认为这个点附近已经没有合法点。
         * (2)怎么快速判定随机找出的点是否合法？这个是算法的关键，可以采用一些空间划分方法来做（游戏场景也会经常用到），首先将平面划分成m行n列的格子，每个格子都保存了格子内部的点。这样当我需要判断一个点是否合法时，我只要和附近的格子内的点做判断即可。
         * (3)那怎么确定每个格子的大小？我们尽量让每个格子内部最多只能有1个点，这样数据结构就会简单很多。怎么做到呢？我们假设每个格子都是正方形，那正方形内部距离最远的点就是对角线的2个点，所以我们只要保证正方形的对角线长度等于r，则正方形内部任意2个点之间的距离肯定小于r，从而保证每个正方形内部肯定最多只能有1个点。假设正方形边长为a，对角线长度为r，那么有：a^2+a^2=r^2 
         * (4)要判断xk的合法性，就是要判断附近有没有点与xk的距离小于r，由于cell的边长小于r，所以只测试xk所在的cell的九宫格是不够的（考虑xk正好处于cell的边缘的情况），正确做法是以xk为中心，做一个边长为2r的正方形，测试这个正方形覆盖到的所有cell
         */

        //泊松圆盘采样
        public List<Point> PoissonDiscSample(int width, int height, int num)
        {
            int r = (int)Math.Sqrt(width * height / num);
            int d = (int)(r / 1.414f);//格子边长
            int k = 30; //尝试次数

            //划分格子
            int nx = width / d + 1;
            int ny = height / d + 1;

            //记录格子占用状态 0是未占用 >0是已采样点的下标+1
            int[,] occupied = new int[nx, ny];

            //活动点
            List<Point> activeList = new List<Point>();
            List<Point> sampled = new List<Point>();

            //判断合法性，只找周围2圈的格子（2r）即可
            Point[] relative = new Point[]
            {
                new Point(-1,2),new Point(0,2),new Point(1,2),
                new Point(-2,1),new Point(-1,-1),new Point(0,1),new Point(1,1),new Point(2,1),
                new Point(-2,0),new Point(-1,0),new Point(1,0),new Point(2,0),
                new Point(-2,-1),new Point(-1,-1),new Point(0,-1),new Point(1,-1),new Point(2,-1),
                new Point(-1,-2),new Point(0,-2),new Point(1,-2)
            };

            //随机第一个点
            {
                int x = mRandor.Next(0, width);
                int y = mRandor.Next(0, height);
                Point p = new Point(x, y);
                activeList.Add(p);
                sampled.Add(p);

                //点在哪个格子
                int X = x / d;
                int Y = y / d;
                occupied[X, Y] = sampled.Count;

            }


            while (activeList.Count > 0)
            {
                int idx = mRandor.Next(0, activeList.Count);
                Point activePoint = activeList[idx];

                //尝试k次寻找合法点
                bool active = false;
                for (int i = 0; i < k; i++)
                {
                    int radius = (mRandor.Next(0, r) + 1) * r;
                    double theta = mRandor.NextDouble() * Math.PI * 2;
                    Point candidate = new Point((int)(radius * Math.Cos(theta) + activePoint.X), (int)(radius * Math.Sin(theta) + activePoint.Y));
                    int cX = candidate.X / d;
                    int cY = candidate.Y / d;
                    if (cX < 0 || cX >= nx || cY < 0 || cY >= ny)
                    {
                        continue;
                    }
                    if (occupied[cX, cY] > 0)
                    {
                        continue;
                    }

                    bool valid = true;
                    foreach (Point t in relative)
                    {
                        if (cX + t.X < 0 || cX + t.X >= nx || cY + t.Y < 0 || cY + t.Y >= ny)
                        {
                            valid = false;
                            break;
                        }
                        int oidx = occupied[cX + t.X, cY + t.Y];
                        if (oidx > 0)
                        {
                            var p = sampled[oidx - 1];
                            int offsetx = candidate.X - p.X;
                            int offsety = candidate.Y - p.Y;
                            if (offsetx * offsetx + offsety * offsety < r * r)
                            {
                                valid = false;
                                break;
                            }
                        }
                    }

                    if (valid)
                    {
                        active = true;
                        activeList.Add(candidate);
                        sampled.Add(candidate);
                        occupied[cX, cY] = sampled.Count;
                        break;
                    }
                }
                if (active == false)
                {
                    activeList.RemoveAt(idx);
                }
            }


            return sampled;
        }

    }
}
