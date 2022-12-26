using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * BM算法定义了两个规则：
     * 坏字符规则：  当文本串中的某个字符跟模式串的某个字符不匹配时，我们称文本串中的这个失配字符为坏字符，此时模式串需要向右移动，
     *              移动的位数 = 坏字符在模式串中的位置 - 坏字符在模式串中最右出现的位置。如果"坏字符"不包含在模式串之中，则最右出现位置为-1。
     * 好后缀规则：  当字符失配时，后移位数 = 好后缀在模式串中的位置 - 好后缀在模式串上一次出现的位置。如果好后缀在模式串中没有再次出现，则上次出现的位置为-1。
     * 从模式串的尾部向前匹配，失配时根据上面两个规则计算后移位置，取较大的值右移。算法复杂度O(n)，比kmp算法快3-5倍。
     */
    public class BM
    {
        int[] good;
        int[] bad;
        string pattern;

        public BM(string pattern)
        {
            calcGoodTable(pattern);
            calcBadTable(pattern);
        }

        void calcGoodTable(string pattern)
        {

        }

        void calcBadTable(string pattern)
        {

        }

        public int Search(string text)
        {
            int skip = 0;
            for(int i = 0; i < text.Length - pattern.Length; i += skip)
            {
                int j = pattern.Length - 1;
                for (; j >= 0; j--)
                {
                    if (pattern[j] != text[i + j])
                    {
                        skip = Math.Max(good[j], bad[j]);
                    }
                }
                if (j == 0) return i;
            }
            return -1;
        }
    }
}
