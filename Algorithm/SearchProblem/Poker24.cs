//输入4个扑克牌数字（1-13）输出一个算式使4个数字计算得到24，可以用+-*/和()，无解返回-1

using System.Text;
namespace Algorithm;

class Poker24
{
    static char[] opchar = new char[] { '+', '-', '*', '/' };
    static Func<int, int, int>[] opfunc = new Func<int, int, int>[]
    {
    (a,b)=>a+b,
    (a,b)=>a-b,
    (a,b)=>a*b,
    (a,b)=>a/b
    };

    class Node
    {
        public Node Left;
        public Node Right;
        public int Op = -1;
        public int Num;

        public override string ToString()
        {
            if (Op == -1)
            {
                return Num.ToString();
            }
            StringBuilder sb = new StringBuilder();


            if (Op > 1 && (Left.Op == 0 || Left.Op == 1))
            {
                sb.Append("(");
            }

            sb.Append(Left.ToString());

            if (Op > 1 && (Left.Op == 0 || Left.Op == 1))
            {
                sb.Append(")");
            }
            sb.Append(opchar[Op]);

            if (Op > 1 && (Right.Op == 0 || Right.Op == 1))
            {
                sb.Append("(");
            }

            sb.Append(Right.ToString());

            if (Op > 1 && (Right.Op == 0 || Right.Op == 1))
            {
                sb.Append(")");
            }
            return sb.ToString();
        }
    }

    void dfs(List<Node> arr)
    {
        if (arr.Count == 1)
        {
            if (arr[0].Num == 24)
            {
                Console.WriteLine(arr[0].ToString());
            }
            return;
        }
        for (int i = 0; i < arr.Count - 1; i++)
        {
            for (int j = i + 1; j < arr.Count; j++)
            {
                List<Node> list = new List<Node>();
                for (int k = 0; k < arr.Count; k++)
                {
                    if (k != i && k != j) list.Add(arr[k]);
                }

                Node node = new Node();
                node.Left = arr[i];
                node.Right = arr[j];
                list.Add(node);
                for (int x = 0; x < 4; x++)
                {
                    node.Op = x;
                    bool valid = true;
                    if (x == 3 && (node.Right.Num == 0 || node.Left.Num % node.Right.Num != 0))
                    {
                        valid = false;
                    }
                    if (valid)
                    {
                        node.Num = opfunc[x](node.Left.Num, node.Right.Num);
                        dfs(list);
                    }
                    if (node.Left != node.Right)
                    {
                        node.Left = arr[j];
                        node.Right = arr[i];

                        if (x == 3 && (node.Right.Num == 0 || node.Left.Num % node.Right.Num != 0))
                        {
                            valid = false;
                        }
                        if (valid)
                        {

                            node.Num = opfunc[x](node.Left.Num, node.Right.Num);
                            dfs(list);
                        }
                    }

                }
            }
        }
    }

    public void Test()
    {

        Poker24 p = new Poker24();
        int[] arr = new[] { 2, 3, 12, 12 };
        List<Node> list = new List<Node>();
        foreach (int a in arr)
        {
            list.Add(new Node { Num = a });
        }
        p.dfs(list);
        Console.Read();

    }
}



