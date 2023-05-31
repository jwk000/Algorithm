using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    // 中缀表达式转后缀表达式
    // 计算四则运算 1+(2+3)*4-5 --> 123+4*+5-
    public class AffixExp
    {
        //中缀表达式转后缀表达式
        //实现思路：
        //用一个栈保存操作符，按优先级入栈出栈即可调整计算顺序
        //每种操作符出现时判断前面的操作符优先级<=自己则出栈
        //操作数不会变化前后顺序，直接进入后缀表达式
        public static string Convert(string exp)
        {
            Stack<char> stack = new Stack<char>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < exp.Length; i++)
            {
                char c = exp[i];
                if (c == '+' || c == '-')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        sb.Append(stack.Pop());
                    }
                    stack.Push(c);
                }
                else if (c == '*' || c == '/')
                {
                    while (stack.Count > 0 && (stack.Peek() == '*' || stack.Peek() == '/'))
                    {
                        sb.Append(stack.Pop());
                    }
                    stack.Push(c);
                }
                else if (c == '(')
                {
                    stack.Push(c);
                }
                else if (c == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        sb.Append(stack.Pop());
                    }
                    stack.Pop();
                }
                else
                {
                    sb.Append(c);
                }
            }
            while (stack.Count > 0)
            {
                sb.Append(stack.Pop());
            }
            return sb.ToString();
        }

        //后缀表达式求解
        public static int Calculate(string exp)
        {
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < exp.Length; i++)
            {
                char c = exp[i];
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    int b = stack.Pop();
                    int a = stack.Pop();
                    int r = 0;
                    switch (c)
                    {
                        case '+':
                            r = a + b;
                            break;
                        case '-':
                            r = a - b;
                            break;
                        case '*':
                            r = a * b;
                            break;
                        case '/':
                            r = a / b;
                            break;
                    }
                    stack.Push(r);
                }
                else
                {
                    stack.Push(c - '0');
                }
            }
            return stack.Pop();
        }
    }
}
