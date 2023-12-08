using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{

    //正则表达式语法 /pattern/flags

    /***********flags*********************************************************
     * 
     * i ignore 不区分大小写
     * g global 全局匹配
     * m multiline 多行匹配，修改^和$的含义，匹配整个字符串的开头和结尾
     * s singleline 单行匹配，修改.的含义，匹配\n
     * 
     * ************************************************************************

     ************pattern*******************************************************
     *
     * \d 数字[0-9]
     * \D 非数字
     * \w 单词 数字字母下划线 [0-9a-zA-Z_]
     * \W 非单词
     * \s 空白 [ \t\r\n\f\v]
     * \S 非空白
     * \b 单词边界 匹配"never"中的"er"，但不能匹配"verb"中的"er"。
     * \B 非单词边界 	匹配"verb"中的"er"，但不能匹配"never"中的"er"。
     * [abc] 字符集
     * [^abc] 逆字符集
     * [a-g] 区间字符集
     * X{m} 量词 m个X，贪婪
     * X{m,n} m-n个X，贪婪
     * X{m,} 至少m个X，贪婪
     * () 分组并捕获
     * (?:) 分组非捕获 industr(?:y|ies) 等价于 industry|industries
     * exp1(?=exp2) 查找exp2前面的exp1 前向断言 lookahead
     * exp1(?!exp2) 查找后面不是exp2的exp1
     * (?<=exp1)exp2 查找前面是exp1的exp2 后向断言 lookbehind
     * (?<!exp1)exp2 查找前面不是exp1的exp2
     * |  或 'z|food' 能匹配 "z" 或 "food"。'(z|f)ood' 则匹配 "zood" 或 "food"
     * .  任意字符，不含\n
     * X*  任意多个X {0,} 贪婪 A\s*=\s*B
     * X+  至少一个X {1,} 贪婪
     * X?  0到1个X {0,1} 贪婪
     * X*? X+? X?? X{}? 非贪婪
     * ^  行首
     * $  行尾不含\n
     * \1...\9 反向引用捕获的文本 (\d)\1 匹配2个连续数字
     * 
     * ******************************************************************************
     */

    internal class Regex
    {
    }
}
