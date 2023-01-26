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
     * 红   101 102 103 。。。113
     * 橙   201 202 203 。。。213
     * 蓝   301 302 303 。。。313
     * 黑   401 402 403 。。。413
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


    public class Card
    {
        public int color;
        public int number;

        public Card(int c, int n)
        {
            color = c;
            number = n;
        }
        public override string ToString()
        {
            return $"{color * 100 + number}";
        }
    }
    public class Combo
    {
        public List<Card> cards = new List<Card>();
        public int comboType;//0刻子 1顺子
        public int comboValue;//面值

        public Combo(int type)
        {
            comboType = type;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Card v in cards) { sb.Append(v).Append(' '); }
            sb.Length--;
            return sb.ToString();
        }
    }

    public class ComboSolution
    {
        public List<Combo> combos = new List<Combo>();
        public int value;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(value).Append(" {");
            foreach (Combo combo in combos)
            {
                sb.Append($"[{combo}] ");
            }
            sb.Append("}");
            return sb.ToString();
        }

        public ComboSolution Clone()
        {
            var s = new ComboSolution();
            s.value = this.value;
            s.combos.AddRange(this.combos.GetRange(0, combos.Count));
            return s;
        }
    }

    public static class CardCache
    {
        static Card[,] Cache = new Card[5, 14];
        static CardCache()
        {
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    Cache[i, j] = new Card(i, j);
                }
            }
        }

        public static Card GetCard(int color, int number)
        {
            if (color < 1 || color > 4 || number < 1 || number > 13)
            {
                return null;
            }
            return Cache[color, number];
        }
    }

    public class Poker101
    {
        //记录每种颜色每种数字牌型数量
        int[,] HandCards = new int[5, 14];
        List<ComboSolution> Solutions = new List<ComboSolution>();
        ComboSolution Result = null;

        public ComboSolution BestHandCard(List<int> handcards)
        {
            int joker = 0;
            foreach (int h in handcards)
            {
                if (h == 0) joker++;
                else HandCards[h / 100, h % 100]++;
            }

            ComboSolution solution = new ComboSolution();
            //寻找combo
            SearchCombo2(solution, 1);
            Console.WriteLine("All result:----------------------");
            foreach (var s in Solutions)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("Best result: {0}", Result.ToString());

            return Result;
        }


        //全排列版本，会包含结果相同，顺序不同的combo
        bool Forward = true;//递归方向，true递归，false回溯
        void SearchCombo(ComboSolution solution)
        {
            Forward = true;

            //刻子
            for (int n = 1; n <= 13; n++)
            {
                List<int> colors = new List<int>(4);
                for (int c = 1; c <= 4; c++)
                {
                    if (HandCards[c, n] > 0)
                    {
                        colors.Add(c);
                    }
                }
                if (colors.Count == 3)
                {
                    Combo combo = new Combo(0);
                    foreach (int c in colors)
                    {
                        var card = CardCache.GetCard(c, n);
                        combo.cards.Add(card);
                        combo.comboValue += n;
                        HandCards[c, n]--;
                    }
                    solution.combos.Add(combo);
                    solution.value += combo.comboValue;
                    SearchCombo(solution);

                    //还原
                    foreach (int c in colors)
                    {
                        HandCards[c, n]++;
                    }
                    solution.value -= combo.comboValue;
                    solution.combos.RemoveAt(solution.combos.Count - 1);

                }
                else if (colors.Count == 4)
                {
                    //1234 123 124 134 234
                    for (int c = 0; c <= 4; c++)
                    {
                        Combo combo = new Combo(0);
                        for (int j = 1; j <= 4; j++)
                        {
                            if (c == j) continue;
                            var card = CardCache.GetCard(j, n);
                            combo.cards.Add(card);
                            combo.comboValue += n;
                            HandCards[j, n]--;
                        }

                        solution.combos.Add(combo);
                        solution.value += combo.comboValue;
                        SearchCombo(solution);

                        //还原
                        for (int j = 1; j <= 4; j++)
                        {
                            if (c == j) continue;
                            HandCards[j, n]++;
                        }
                        solution.value -= combo.comboValue;
                        solution.combos.RemoveAt(solution.combos.Count - 1);

                    }
                }
            }

            //顺子
            for (int c = 1; c <= 4; c++)
            {
                for (int j = 1; j <= 11; j++)
                {
                    int cnt = 0;
                    for (int k = j; k <= 13; k++)
                    {
                        if (HandCards[c, k] > 0)
                        {
                            cnt++;
                        }
                        else
                        {
                            break;
                        }
                        if (cnt >= 3)
                        {
                            Combo combo = new Combo(1);
                            for (int n = j; n <= k; n++)
                            {
                                var card = CardCache.GetCard(c, n);
                                combo.cards.Add(card);
                                combo.comboValue += n;
                                HandCards[c, n]--;
                            }

                            solution.combos.Add(combo);
                            solution.value += combo.comboValue;
                            SearchCombo(solution);

                            //还原
                            for (int n = j; n <= k; n++)
                            {
                                HandCards[c, n]++;
                            }
                            solution.value -= combo.comboValue;
                            solution.combos.RemoveAt(solution.combos.Count - 1);
                        }
                    }
                }
            }


            //结果
            if (Forward && solution.value > 0)
            {
                var clone = solution.Clone();
                Solutions.Add(clone);
                if (Result == null)
                {
                    Result = clone;
                }
                if (clone.value > Result.value)
                {
                    Result = clone;
                }
            }

            Forward = false;

        }


        //优化版本，每次迭代只计算从n开始的combo，避免重复计算
        void SearchCombo2(ComboSolution solution, int n)
        {
            if (n > 13)
            {
                //结果
                if (solution.value > 0)
                {
                    var clone = solution.Clone();
                    Solutions.Add(clone);
                    if (Result == null)
                    {
                        Result = clone;
                    }
                    if (clone.value > Result.value)
                    {
                        Result = clone;
                    }
                }
                return;
            }

            //n的刻子
            List<int> colors = new List<int>(4);
            for (int c = 1; c <= 4; c++)
            {
                if (HandCards[c, n] > 0)
                {
                    colors.Add(c);
                }
            }
            if (colors.Count == 3)
            {
                Combo combo = new Combo(0);
                foreach (int c in colors)
                {
                    var card = CardCache.GetCard(c, n);
                    combo.cards.Add(card);
                    combo.comboValue += n;
                    HandCards[c, n]--;
                }
                solution.combos.Add(combo);
                solution.value += combo.comboValue;
                SearchCombo2(solution, n);

                //还原
                foreach (int c in colors)
                {
                    HandCards[c, n]++;
                }
                solution.value -= combo.comboValue;
                solution.combos.RemoveAt(solution.combos.Count - 1);

            }
            else if (colors.Count == 4)
            {
                //1234 123 124 134 234
                for (int c = 0; c <= 4; c++)
                {
                    Combo combo = new Combo(0);
                    for (int j = 1; j <= 4; j++)
                    {
                        if (c == j) continue;
                        var card = CardCache.GetCard(j, n);
                        combo.cards.Add(card);
                        combo.comboValue += n;
                        HandCards[j, n]--;
                    }

                    solution.combos.Add(combo);
                    solution.value += combo.comboValue;
                    SearchCombo2(solution, n);

                    //还原
                    for (int j = 1; j <= 4; j++)
                    {
                        if (c == j) continue;
                        HandCards[j, n]++;
                    }
                    solution.value -= combo.comboValue;
                    solution.combos.RemoveAt(solution.combos.Count - 1);

                }
            }
            else //n没有刻子
            {
                SearchCombo2(solution, n + 1);
            }


            //从n开始的顺子
            if (n <= 11)
            {
                for (int c = 1; c <= 4; c++)
                {
                    int cnt = 0;
                    for (int k = n; k <= 13; k++)
                    {
                        if (HandCards[c, k] > 0)
                        {
                            cnt++;
                        }
                        else
                        {
                            break;
                        }
                        if (cnt >= 3)
                        {
                            Combo combo = new Combo(1);
                            for (int j = n; j <= k; j++)
                            {
                                var card = CardCache.GetCard(c, j);
                                combo.cards.Add(card);
                                combo.comboValue += j;
                                HandCards[c, j]--;
                            }

                            solution.combos.Add(combo);
                            solution.value += combo.comboValue;
                            SearchCombo2(solution, n + 1);

                            //还原
                            for (int j = n; j <= k; j++)
                            {
                                HandCards[c, j]++;
                            }
                            solution.value -= combo.comboValue;
                            solution.combos.RemoveAt(solution.combos.Count - 1);
                        }
                    }

                }
            }
        }

        //剩余手牌
        List<int> RestCards = new List<int>();
        int[,] BottomCards = new int[5, 14];

        public void InitBottomCards()
        {
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    BottomCards[i, j] = 2;
                    RestCards.Add(i * 100 + j);
                }
            }
        }
        //根据期望值计算一副手牌，要考虑剩余底牌，生成的牌型要有随机性
        //王牌简单处理：一张王牌抵20分
        Random rander = new Random();
        public List<int> GenHandCard_Random(int expect)
        {

            List<int> ret = new List<int>();
            //21张手牌
            for (int cnt = 0; cnt < 21; cnt++)
            {
                int r = rander.Next(0, RestCards.Count);
                int card = RestCards[r];
                RestCards.RemoveAt(r);
                ret.Add(card);
            }

            var sol = BestHandCard(ret);
            for(int i=0;i<10;i++)
            {
                int diff = Math.Abs(sol.value - expect);
                if (diff < 10)
                {
                    return ret;
                }

                if (sol.value < expect)
                {
                    //提高面值

                }
                else
                {
                    //拆除combo
                }

            }

            return ret;
        }

        //洗牌
        void shuffle(int[] arr)
        {
            for (int n = 0; n < arr.Length - 1; n++)
            {
                int i = rander.Next(0, arr.Length - n);
                int v = arr[arr.Length - n];
                arr[arr.Length - n] = arr[i];
                arr[i] = v;
            }
        }
    }



}
