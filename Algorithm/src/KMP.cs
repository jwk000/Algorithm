using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class KMP
    {
        int[] next;
        string pattern;
        public KMP(string pattern)
        {
            next = new int[pattern.Length];
            this.pattern = pattern;

            next[0] = -1;
            for (int i = 1; i < pattern.Length; i++)
            {
                int t = next[i - 1];
                while (true)
                {
                    if (t==-1||pattern[t] == pattern[i - 1])//t回退不匹配最终回退到-1，这时候pattern[i]的回退位置next[i]=0
                    {
                        if (pattern[i] != pattern[t + 1]) //优化：i失配的时候回退到next[i]，如果p[i]==p[next[i]]则必然继续失配，此时需回退到next[next[i]]
                        {
                            next[i] = t + 1;
                        }
                        else
                        {
                            next[i] = next[t + 1];
                        }
                        break;
                    }
                    else
                    {
                        t = next[t]; //next构造的关键在于t回退，退到上一个可能匹配的位置
                    }
                }
            }
        }

        public int Search(string text)
        {
            int j = 0;
            for (int i = 0; i < text.Length; )
            {
                if (j == -1 || text[i] == pattern[j])//j回退不匹配会退到-1，这时候text[i]和pattern没法匹配，只能i++
                {
                    i++; j++;
                }
                else
                {
                    j = next[j]; //查找的关键在j回退到下个可能匹配的位置
                }
                if (j == pattern.Length)
                {
                    return i - pattern.Length;
                }
            }
            return -1;
        }
    }
}
