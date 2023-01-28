using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 盖尔-沙普利算法(Gale-Shapley)
     *   简称“GS算法”，也称为延迟接受算法。是盖尔和沙普利为了寻找一个稳定匹配而设计出的市场机制。
     *   稳定婚配问题是GS算法主要的应用领域，假设现在有五位男士（ABCDE），四位女士（abcd），他们参加一档相亲节目，如何配对能保证最终的相亲结果是稳定的；
     *   稳定婚姻就是不存在两个人都认为对方比自己现在的婚配对象更好，例如A与a婚配，B与b婚配，但是A认为b比a更适合自己，b也认为A比B更适合自己，这就是一种不稳定的婚姻关系。
     *   而Gale-Shapley算法得到的最终结果一定是一组稳定的婚配关系
     *   
     *   盖尔沙普利算法的其他应用：高考录取机制，工作分配机制。
     *   
     *   ref：https://blog.csdn.net/renboyu010214/article/details/106896818
     * 
     */
    public class GaleShapley
    {
        class Man
        {
            public int ID;
            public string Name;
            public int[] FavorWoman;//喜欢的女人排序，数量<=全部女人数量
            public Woman MatchedWoman;//当前匹配的女人

        }

        class Woman
        {
            public int ID;
            public string Name;
            public int[] FavorMan;//喜欢的男人排序，数量<=全部男人数量
            public Man MatchedMan;//当前匹配的男人

            public int GetManFavor(Man m)
            {
                for(int i = 0; i < FavorMan.Length; i++)
                {
                    if (m.ID == FavorMan[i])
                    {
                        return i;
                    }
                }
                return int.MaxValue;
            }
        }

        Man[] AllMen ;
        Woman[] AllWomen ;

        public void Init(int manCnt, int womanCnt)
        {
            AllMen = new Man[manCnt];
            AllWomen = new Woman[womanCnt];
        }

        public void AddMan(int id,string name, int[] favor)
        {
            AllMen[id] = new Man();
            AllMen[id].ID = id;
            AllMen[id].Name = name;
            AllMen[id].FavorWoman = favor;
        }

        public void AddWoman(int id,string name, int[] favor)
        {
            AllWomen[id] = new Woman();
            AllWomen[id].ID = id;
            AllWomen[id].Name = name;
            AllWomen[id].FavorMan = favor;
        }

        public void Match()
        {
            Man m = FetchFreeMan();
            while (m != null)
            {
                for(int i = 0; i < m.FavorWoman.Length; i++)
                {
                    Woman w = AllWomen[m.FavorWoman[i]];
                    if (w.MatchedMan == null)
                    {
                        m.MatchedWoman = w;
                        w.MatchedMan = m;
                        break;
                    }
                    else if(w.GetManFavor(m)<w.GetManFavor(w.MatchedMan))
                    {
                        w.MatchedMan.MatchedWoman = null;
                        w.MatchedMan = m;
                        m.MatchedWoman = w;
                        break;
                    }
                }
                m = FetchFreeMan();
            }
        }

        Man FetchFreeMan()
        {
            foreach(Man m in AllMen)
            {
                if (m.MatchedWoman == null)
                {
                    return m;
                }
            }
            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var m in AllMen)
            {
                sb.Append($"{m.ID}:{m.MatchedWoman.ID} ");
            }
            return sb.ToString();
        }

    }
}
