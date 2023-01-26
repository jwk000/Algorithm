using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * JPS+ 本质上也是 JPS寻路，只是加上了预处理来改进，从而使寻路更加快速。
     * 首先对地图每个节点进行跳点判断，找出所有主要跳点;
     * 然后对每个节点进行跳点的直线可达性判断，并记录好跳点直线可达性;
     * 若可达还需记录号跳点直线距离;
     * 类似地，我们对每个节点进行跳点斜向距离的记录;
     * 剩余各个方向如果不可到达跳点的数据记为0或负数距离。如果在对应的方向上移动1步后碰到障碍（或边界）则记为0，如果移动n+1步后会碰到障碍（或边界）的数据记为负数距离-n
     * 最后每个节点的8个方向都记录完毕，我们便完成了JPS+的预处理过程
     * 
     * 
     */
    public class JPSPlus
    {
    }
}
