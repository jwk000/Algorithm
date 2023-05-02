using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class BrainFuck
    {
        byte[] mem = new byte[1024];//内存
        int ptr = 0;//当前指针
        public void Do(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '[')
                {
                    int loopBegin = i;//循环指令地址
                    int loopEnd = -1;
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        if (input[j] == ']')
                        {
                            loopEnd = j;
                            break;
                        }
                    }
                    if (loopEnd == -1)
                    {
                        Console.WriteLine("not find ']' !!");
                        return;
                    }
                    string loop = input.Substring(loopBegin + 1, loopEnd - loopBegin - 1);
                    while (mem[ptr] != 0)
                    {
                        Do(loop);
                    }
                    i = loopEnd;
                }
                else if (c == '<')
                {
                    ptr--;
                }
                else if (c == '>')
                {
                    ptr++;
                }
                else if (c == '+')
                {
                    mem[ptr]++;
                }
                else if (c == '-')
                {
                    mem[ptr]--;
                }
                else if (c == '.')
                {
                    Console.Write((char)mem[ptr]);
                }
                else if (c == ',')
                {
                    mem[ptr] = (byte)Console.Read();
                }
            }
        }
    }
}
