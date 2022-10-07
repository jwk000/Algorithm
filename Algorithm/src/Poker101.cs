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
            //思路：穷举所有情况，判断最大牌型
            //先排序，方便查找相邻数字牌
            //取cards[i]判断是否组成刻子K，是则取rk = K+max(k-Rest)
            //取cards[i]判断是否组成顺子S，是则取rs = S+max(s-Rest)
            //max(cards) = max(rk,rs)

            cards.Sort();

            int s = cards[0];


            return null;
        }

        Dictionary<HandCardStatus, Result> mCache = new Dictionary<HandCardStatus, Result>();


        class Result
        {
            public int value;
            public List<List<int>> combo = new List<List<int>>();
            public HandCardStatus status;
        }


        class HandCardStatus
        {
            long Low;
            long High;


            public HandCardStatus(List<int> cards)
            {
                foreach (int card in cards)
                {
                    int idx = (card / 100 - 1) * 13 + (card % 100 - 1);
                    if ((Low & (1 << idx)) == 0)
                    {
                        Low |= 1 << idx;
                    }
                    else
                    {
                        High |= 1 << idx;
                    }
                }
            }

            public override int GetHashCode()
            {
                int v = (int)Low;
                v ^= (int)(Low >> 32) * 13;
                v ^= (int)(High) * 13;
                v ^= (int)(High >> 32) * 13;
                return v;
            }
        }
    }
}
