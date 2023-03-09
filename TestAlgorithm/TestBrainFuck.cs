using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestBrainFuck
    {
        [TestMethod]
        public void Test()
        {
            string str = "++++++++[>+>++>+++>++++>+++++>++++++>+++++++>++++++++>+++++++++>++++++++++>+++++++++++>++++++++++++>+++++++++++++>++++++++++++++>+++++++++++++++>++++++++++++++++<<<<<<<<<<<<<<<<-]>>>>>>>>>>>>>>>-.+<<<<<<<<<<<<<<<>>>>>>>>>>>>>---.+++<<<<<<<<<<<<<>>>>>>>>>>>>>>----.++++<<<<<<<<<<<<<<>>>>>>>>>>>>+++.---<<<<<<<<<<<<>>>>>>>>>>>>>>-.+<<<<<<<<<<<<<<>>>>>>>>>>>>>>---.+++<<<<<<<<<<<<<<>>>>>>>>>>>>>---.+++<<<<<<<<<<<<<>>>>>>--.++<<<<<<>>>>>>>>>>>>>.<<<<<<<<<<<<<>>>>>>>>>>>>>>>----.++++<<<<<<<<<<<<<<<>>>>>>>>>>>>>>---.+++<<<<<<<<<<<<<<>>>>>>>>>>>>>>----.++++<<<<<<<<<<<<<<.";

            BrainFuck bf = new BrainFuck();
            bf.Do(str);

        }

        [TestMethod]
        public void Keyboard()
        {
            char[] map = new char[128];
            for (int i = 0; i < 128; i++) { map[i] = (char)i; }
            map['\''] = 'q';
            map[','] = 'w';
            map['.'] = 'e';
            map['p'] = 'r';
            map['y'] = 't';
            map['f'] = 'y';
            map['g'] = 'u';
            map['c'] = 'i';
            map['r'] = 'o';
            map['l'] = 'p';
            map['/'] = '[';
            map['='] = ']';
            map['['] = '-';
            map[']'] = '=';
            map['o'] = 's';
            map['e'] = 'd';
            map['u'] = 'f';
            map['i'] = 'g';
            map['d'] = 'h';
            map['h'] = 'j';
            map['t'] = 'k';
            map['n'] = 'l';
            map['s'] = ';';
            map['-'] = '\'';
            map[';'] = 'z';
            map['q'] = 'x';
            map['j'] = 'c';
            map['k'] = 'v';
            map['x'] = 'b';
            map['b'] = 'n';
            map['m'] = 'm';
            map['w'] = ',';
            map['v'] = '.';
            map['z'] = '/';
            map['?'] = '{';
            map['+'] = '}';
            map['}'] = '+';
            map['_'] = '"';

            string code = "macb() ? lpcbyu(&gbcq/_17%ocq10_=w(gbcq)/_dak._=}_ugb_[0q60)s+";
            StringBuilder sb = new StringBuilder();
            foreach (char c in code)
            {
                sb.Append(map[c]);
            }
            string s = sb.ToString();
            Console.WriteLine(s);
            // main() { printf(&unix["\021%six\012\0"],(unix)["have"]+"fun"-0x60);}
            //unix
        }

        [TestMethod]
        public void mapkey()
        {
            char[] m = new char[128];
            string mm = "pvwdgazxubqfsnrhocitlkeymj";
            string s = "Wxgcg txgcg ui p ixgff, txgcg ui p epm. I gyhgwt mrl lig txg ixgff wrsspnd tr irfkg txui hcrvfgs, nre, hfgpig tcm liunz txg crt13 ra \"ixgff\" tr gntgc ngyt fgkgf.";
            for(int i = 0; i < 26; i++)
            {
                m[mm[i]] =(char)( 'a' + i);
            }
            StringBuilder sb = new StringBuilder();
            foreach (char c in s)
            {
                if (c >= 'a' && c <= 'z')
                {
                    sb.Append(m[c]);
                }
                else
                {
                    sb.Append(c);
                }
            }
            Console.WriteLine(sb.ToString());
            // Where there is a shell, there is a way. I expect you use the shell command to solve this problem, now, please try using the rot13 of "shell" to enter next level.
        }
    }
}
