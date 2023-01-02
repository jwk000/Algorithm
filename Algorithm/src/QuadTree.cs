using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 四叉树：2D空间划分数据结构，可用于稀疏图存储、碰撞检测；
     * 0. 四叉树存储空间节点和空间内的所有对象，既然是碰撞检测，应假设对象有尺寸，使用矩形包围盒最好算；且应假设对象可移动，如果对象占据了四叉树的边界则被多个节点共享；
     * 1. 四叉树的构建：
     *      初始只有根节点，往根节点添加对象，对象数量达到M就分裂为4个子空间；
     *      由于空间不能无限分裂，需要设计一个maxLevel作为树的最大深度，空间应该划分到对碰撞检测有意义的最小尺寸；
     *      继续往根节点添加对象，根节点查询对象所在的子节点，递归到子节点添加对象；
     *      情况1.对象会不断移动到地图的任意位置，则四叉树应该提前构建完整的结构，对象移动时仅变更所属节点；
     *      情况2.移动对象较少，固定对象较多，此时四叉树应该基于固定对象构建，而且不需要是完全四叉树；移动对象不会引发四叉树结构的变化，只需要修改对象所属的节点；
     * 2. 对象移动：移动后需要更新四叉树。
     * 3. 松散四叉树，对象移出四叉树节点边界的判断条件是2倍节点尺寸，对象移入节点则，避免对象在边界反复横跳导致频繁更新；
     * 4. 查找对象A的潜在碰撞对象，找到A所在的最小节点（对象有大小，可以占据多个节点），遍历节点内的其他对象；
     */
    public class QuadTree
    {
        public class QTNode
        {
            public int level;
            public int Left, Right, Up, Down;//边界坐标
            public QTNode[] SubNodes;//4个子节点
            public Dictionary<int, QTObject> Objects;//节点内的对象
            public static int M = 10;//空间分裂阈值
            public static int MaxLevel = 5;//树最大深度

            public QTNode(int level)
            {
                this.level = level;
                Objects = new Dictionary<int, QTObject>();
            }
            public void AddObject(QTObject obj)
            {
                if (SubNodes != null)
                {
                    foreach (var node in SubNodes)
                    {
                        node.AddObject(obj);
                    }
                }
                else
                {
                    if (Objects.ContainsKey(obj.ID))
                    {
                        return;
                    }

                    if (Contains(obj))
                    {
                        Objects.Add(obj.ID, obj);
                        obj.Nodes.Add(this);
                    }
                    //分裂
                    if (Objects.Count > M && level < MaxLevel)
                    {
                        Splite();
                    }

                }

            }

            void Splite()
            {
                SubNodes = new QTNode[4];
                for (int i = 0; i < 4; i++)
                {
                    SubNodes[i] = new QTNode(level+1);
                    bool L = (i & 1) == 0;
                    bool U = i < 2;
                    int MidH = (Left + Right) / 2;
                    int MidV = (Up + Down) / 2;
                    SubNodes[i].Left = L?Left:MidH;
                    SubNodes[i].Right = L?MidH:Right;
                    SubNodes[i].Up = U ? Up : MidV;
                    SubNodes[i].Down = U ? MidV : Down;

                    foreach (var o in Objects.Values)
                    {
                        SubNodes[i].AddObject(o);
                    }
                }
                Objects.Clear();
            }

            bool Contains(QTObject obj)
            {

                bool L = (obj.X > Left && obj.X < Right);
                bool R = (obj.X + obj.W > Left && obj.X + obj.W < Right);
                bool U = (obj.Y > Up && obj.Y < Down);
                bool D = (obj.Y + obj.H > Up && obj.Y + obj.H < Down);

                //四个角至少有一个在节点范围内
                return L && U || L && D || R && U || R && D;
            }

            public void RemoveObject(QTObject obj)
            {
                foreach(var node in obj.Nodes)
                {
                    node.Objects.Remove(obj.ID);
                }
                obj.Nodes.Clear();
            }

            public List<QTObject> GetIntreastObjects(QTObject obj)
            {
                List<QTObject> ret = new List<QTObject>();
                foreach(var node in obj.Nodes)
                {
                    ret.AddRange(node.Objects.Values);
                }
                ret.RemoveAll(o=>o==obj);
                
                return ret;
            }
        }

        public class QTObject
        {
            public static int UUID = 0;
            public int ID;
            public int X, Y, W, H;//xy是左上角，wh是宽高
            public List<QTNode> Nodes = new List<QTNode>();
            public QTObject(int x, int y, int w, int h)
            {
                ID = ++UUID;
                X = x; Y = y; W = w; H = h;
            }
        }

        public QTNode Root = new QTNode(0);
        public Dictionary<int, QTObject> AllObjects = new Dictionary<int, QTObject>();

        public QuadTree(int m,int maxlevel,int left,int right,int up,int down)
        {
            QTNode.M = m;
            QTNode.MaxLevel = maxlevel;
            Root.Left = left;
            Root.Right = right;
            Root.Up = up;
            Root.Down = down;
        }


        //添加对象
        public void AddObject(QTObject obj)
        {
            AllObjects.TryAdd(obj.ID, obj);
            Root.AddObject(obj);
        }

        //移除对象
        public void RemoveObject(QTObject obj)
        {
            AllObjects.Remove(obj.ID);
            Root.RemoveObject(obj);
        }

        //对象位置变化
        public void UpdateObject(QTObject obj)
        {
            RemoveObject(obj);
            AddObject(obj);
        }

        public List<QTObject> GetIntreastObjects(QTObject obj)
        {
            return Root.GetIntreastObjects(obj);
        }
    }
}
