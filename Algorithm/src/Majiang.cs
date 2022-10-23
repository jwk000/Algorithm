using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     *   麻将里有4副牌，每副34张，共136张，牌面如下：
     *   东西南北中发白 Do Xi Na Be Zh Fa Ba
     *   一筒-九筒 1T~9T
     *   一索-九索 1S~9S
     *   一万-九万 1W~9W
     *
     *   2张一样的牌叫对子，3+张一样的牌叫刻子，3+张连续的牌叫顺子
     *   胡牌牌型：m个顺子，n个刻子，1个对子 (m和n可以为0)
     *      m*ABC + n*DDD + EE
     *
     *  给定一副手牌，14张，判断其是否胡牌。
     */
    public class Majiang
    {
        int[] HandCards = new int[34];

        static string[] CardNames = new string[]
        {
            "Do","Xi","Na","Be","Zh","Fa","Ba",
            "1T","2T","3T","4T","5T","6T","7T","8T","9T",
            "1S","2S","3S","4S","5S","6S","7S","8S","9S",
            "1W","2W","3W","4W","5W","6W","7W","8W","9W",
        };

        class Combo
        {
            public int comboType;//0对子 1刻子 3顺子
            public List<int> cards = new List<int>();
            public Combo(int t)
            {
                comboType = t;
            }
            public override string ToString()
            {
                string s = "";
                foreach(int c in cards)
                {
                    s += CardNames[c] + ",";
                }
                return s.Substring(0,s.Length-1);
            }
        }

        class Solution
        {
            public List<Combo> combos = new List<Combo>();
        }
        public bool CheckHu(List<string> input)
        {
            foreach (var card in input)
            {
                for (int i = 0; i < CardNames.Length; i++)
                {
                    if (CardNames[i] == card)
                    {
                        HandCards[i]++;
                        break;
                    }
                }
            }
            List<Combo> combos = new List<Combo>();

            bool ret = Search(combos, 0);
            if (ret)
            {
                foreach(var combo in combos)
                {
                    Console.Write($"[{combo.ToString()}] ");
                }
                Console.WriteLine();
            }

            return ret;
        }

        bool Search(List<Combo> combos, int use)
        {
            if (use == 14)
            {
                return true;
            }
            for (int i = 0; i < 34; i++)
            {
                //对子
                if (combos.Count == 0 && HandCards[i] >= 2)
                {
                    Combo combo = new Combo(0);
                    combo.cards.Add(i);
                    combo.cards.Add(i);
                    combos.Add(combo);
                    HandCards[i] -= 2;
                    if (Search(combos, 2))
                    {
                        return true;
                    }
                    HandCards[i] += 2;
                    combos.RemoveAt(combos.Count - 1);
                }
                //刻子
                if (HandCards[i] >= 3)
                {
                    Combo combo = new Combo(1);
                    combo.cards.Add(i);
                    combo.cards.Add(i);
                    combo.cards.Add(i);
                    combos.Add(combo);
                    HandCards[i] -= 3;
                    if (Search(combos, use + 3))
                    {
                        return true;
                    }
                    HandCards[i] += 3;
                    combos.RemoveAt(combos.Count - 1);
                }

                //顺子
                int shun = 0;
                for (int j = i; j < i + 9; j++)
                {
                    if (j >= 7 && j <= 13 || j >= 16 && j <= 22 || j >= 25 && j <= 31)
                    {
                        if (HandCards[j] > 0)
                        {
                            shun++;
                            if (shun >= 3)
                            {
                                Combo combo = new Combo(2);
                                for (int k = i; k <= j; k++)
                                {
                                    combo.cards.Add(k);
                                    HandCards[k]--;
                                }
                                if (Search(combos, use + shun))
                                {
                                    return true;
                                }
                                for (int k = i; k <= j; k++)
                                {
                                    HandCards[k]++;
                                }
                                combos.RemoveAt(combos.Count - 1);
                            }
                        }
                    }

                }
            }


            return false;
        }
    }
}
