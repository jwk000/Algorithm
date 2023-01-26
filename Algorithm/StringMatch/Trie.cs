using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * Trie树，又叫字典树、前缀树（Prefix Tree）、单词查找树 或 键树，是一种多叉树结构。
     * 根节点不包含字符，除根节点外的每一个子节点都包含一个字符。
     * 从根节点到某一个节点，路径上经过的字符连接起来，为该节点对应的字符串。
     * 通常在实现的时候，会在节点结构中设置一个标志，用来标记该结点处是否构成一个单词。
     * 用途：字符串检索，词频统计，前缀匹配，字符串排序；
     * ref:https://blog.csdn.net/lisonglisonglisong/article/details/45584721
     */
    public class Trie
    {
        const int MaxKeyNum = 128;//ascii
        class TrieNode
        {
            public bool IsKey;
            public TrieNode[] Children;
        }

        TrieNode Root = new TrieNode();

        public void Add(string str)
        {
            TrieNode node = Root;
            for (int i = 0; i < str.Length; i++)
            {
                if (node.Children == null)
                {
                    node.Children = new TrieNode[MaxKeyNum];
                }
                var child = node.Children[str[i]];
                if (child == null)
                {
                    child = new TrieNode();
                    node.Children[str[i]] = child;
                }
                node = child;
                if (i == str.Length - 1)
                {
                    node.IsKey = true;
                }
            }
        }

        public bool HasString(string str)
        {
            TrieNode node = Root;
            for (int i = 0; i < str.Length; i++)
            {
                if (node.Children == null)
                {
                    return false;
                }
                node = node.Children[str[i]];
                if (node == null)
                {
                    return false;
                }
            }
            return node.IsKey;
        }

        public bool Match(string str)
        {
            TrieNode node = Root;
            for (int i = 0; i < str.Length; i++)
            {
                if (node.Children == null || node.Children[str[i]] == null)
                {
                    continue;
                }
                node = node.Children[str[i]];

                if (node.IsKey)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
