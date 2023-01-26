using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * AC自动机
     * 
     */
    public class ACMachine
    {
        class ACNode
        {
            public ACNode ParentNode;
            public ACNode[] NextNode;
            public ACNode FailNode;
            public int MatchLength; //匹配模式长度
            public char Character;//字符

            public override string ToString()
            {
                return $"{Character}|{FailNode}";
            }
        }

        const int N = 128;
        ACNode Root = new ACNode();

        public void Init(List<string> patterns)
        {
            //构建trie树
            foreach (string pattern in patterns)
            {
                ACNode node = Root;
                for (int i = 0; i < pattern.Length; i++)
                {
                    char c = pattern[i];
                    if (node.NextNode == null)
                    {
                        node.NextNode = new ACNode[N];
                    }
                    var next = node.NextNode[c];
                    if (next == null)
                    {
                        next = new ACNode();
                        next.Character = c;
                        next.ParentNode = node;
                        node.NextNode[c] = next;
                    }
                    node = next;
                    if (i == pattern.Length - 1)
                    {

                        node.MatchLength = pattern.Length;
                    }
                }
            }


            //层序遍历树，记录fail指针
            NQueue<ACNode> nq = new NQueue<ACNode>();
            nq.Enqueue(Root);
            while (nq.Size > 0)
            {
                ACNode node = nq.Dequeue();
                if (node.ParentNode != null)
                {
                    var pfail = node.ParentNode.FailNode;
                    if (pfail != null && pfail.NextNode != null && pfail.NextNode[node.Character] != null)
                    {
                        node.FailNode = pfail.NextNode[node.Character];
                    }
                    else
                    {
                        node.FailNode = Root;
                    }
                }
                if (node.NextNode != null)
                {
                    for (int i = 0; i < N; i++)
                    {
                        var next = node.NextNode[i];
                        if (next != null)
                        {
                            nq.Enqueue(next);
                        }
                    }
                }
            }
        }

        //找出text文本中所有匹配的模式串
        public List<string> Match(string text)
        {
            List<string> ret = new List<string>();
            var p = Root;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                while (p != Root && (p.NextNode == null || p.NextNode[c] == null))
                {
                    p = p.FailNode;
                    if (p.MatchLength > 0)
                    {
                        ret.Add(text.Substring(i - p.MatchLength, p.MatchLength));
                    }
                }
                if (p.NextNode[c] != null)
                {
                    p = p.NextNode[c];
                    if (p.MatchLength > 0)
                    {
                        ret.Add(text.Substring(i - p.MatchLength + 1, p.MatchLength));
                    }
                }
            }

            return ret;
        }
    }
}
