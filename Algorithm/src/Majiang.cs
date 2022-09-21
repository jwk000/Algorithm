using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.src
{
    /*
     *   麻将里有4副牌，每副牌面如下：
     *   东西南北中发白 1 2 3 4 5 6 7
     *   一筒-九筒 11 12 13 14 15 16 17 18 19
     *   一条-九条 21 22 23 24 25 26 27 28 29
     *   一万-九万 31 32 33 34 35 36 37 38 39
     *
     *   2张一样的牌叫对子，3+张一样的牌叫刻子，3+张连续的牌叫顺子
     *   胡牌牌型：m个顺子，n个刻子，1个对子 (m和n可以为0)
     *      m*ABC + n*DDD + EE
     *
     *  给定一副手牌，13张，判断其是否胡牌。
     */
    public class Majiang
    {
        public bool IsWin(List<int> cards)
        {
            return false;
        }
    }
}
