using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{

    /*
     * 101理牌算法
     * 
     * 一局101使用两副牌，共106张，每副牌53张，牌型如下：
     * 1-13张数字牌，每个数字4种颜色，记为
     *    101 102 103 。。。113
     *    201 202 203 。。。213
     *    301 302 303 。。。313
     *    401 402 403 。。。413
     * 一张王牌，记为 0 可以作为任意数字牌使用
     * 
     * 牌型组合
     * 刻子：3张以上同数字不同色的牌
     * 顺子：3张以上同色连续数字的牌
     * 
     * 手牌点数：手牌中所有刻子和顺子牌的点数和
     * 
     * 给定一副手牌，包含1~21张牌，求使手牌点数最大的牌型组合。
     * 
     */
    public class Poker101
    {
        
        public List<List<int>> BestHandCard(List<int> cards)
        {
            return null;
        }
    }
}
