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
        int[] good;//下标是好后缀的长度，值是好后缀在模式串上次出现的位置
        int[] bad;
        string pattern;

        public BM(string pattern)
        {
            this.pattern = pattern;
            buildGoodTable(pattern);
            buildBadTable(pattern);
        }

        void buildGoodTable(string pattern)
        {
            good = new int[pattern.Length];
            
            good[0] = 1;//匹配好后缀长度=0则移动一步


            for(int len = 1; len < pattern.Length - 1; len++)
            {
                int idx = -1;
                for(int i = pattern.Length - 2; i >= len; i--)
                {
                    bool match = true;
                    for(int j = 0; j < len; j++)
                    {

                        if(pattern[i-j] != pattern[pattern.Length - 1 - j])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        idx = i;
                        break;
                    }
                }
                if (idx >= 0)
                {
                    good[len] = pattern.Length- idx-1;
                }
            }

            int lastk = 0;
            for(int k = 0; k < pattern.Length; k++)
            {
                if (good[k] == 0)
                {
                    good[k] = good[lastk];//k是pattern和text的匹配后缀长度，如果k不是好后缀则取上一个好后缀
                }
                else
                {
                    lastk = k;
                }
            }
        }

  
        //只有最后一个字符不匹配的时候坏字符规则才有意义
        void buildBadTable(string pattern)
        {

            bad = new int[26];//取一个字节的所有情况
            for(int i = 0; i < 26; i++)
            {
                bad[i] = pattern.Length;//如果坏字节没有在pattern出现，跳过length
            }
            for(int i = 0; i < pattern.Length; i++)
            {
                bad[pattern[i]-'a'] = pattern.Length - i - 1;//坏字符最右出现的位置
            }
        }

        public int Search(string text)
        {
            int skip = 0;
            for(int i = pattern.Length-1; i < text.Length ; i += skip)
            {
                int j = pattern.Length - 1;
                int k = 0;
                for (; j >= 0; j--,k++)
                {
                    if (pattern[j] != text[i-k])
                    {
                        if (j == pattern.Length - 1)
                        {
                            skip = bad[text[j] - 'a'];
                        }
                        else
                        {
                            skip = good[k];
                        }
                        break;
                    }
                    if (j == 0) return i-k;
                }
            }
            return -1;
        }
    }
}
