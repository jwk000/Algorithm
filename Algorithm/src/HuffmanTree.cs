using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 哈夫曼树是带权路径长度最短的树，权值较大的结点离根较近。
     * 给定N个权值作为N个叶子结点，构造一棵二叉树，若该树的带权路径长度达到最小，称这样的二叉树为最优二叉树，也称为哈夫曼树(Huffman Tree)。
     * 树的带权路径长度规定为所有叶子结点的带权路径长度之和，记为WPL。
     */
    public class HuffmanTree
    {

        class HNode
        {
            public bool isleaf = true;
            public int priority;//权值
            public char value;//处理字符串压缩问题
            public HNode left;
            public HNode right;

            public HNode(char c, int prio)
            {
                priority = prio;
                value = c;
            }

            public override string ToString()
            {
                return $"{value}|{priority}";
            }
        }

        HNode Root;

        /*
         * 哈夫曼树的构造规则为：
         * (1) 将w1、w2、…，wn看成是有n 棵树的森林(每棵树仅有一个结点)；
         * (2) 在森林中选出两个根结点的权值最小的树合并，作为一棵新树的左、右子树，且新树的根结点权值为其左、右子树根结点权值之和；
         * (3)从森林中删除选取的两棵树，并将新树加入森林；
         * (4)重复(2)、(3)步，直到森林中只剩一棵树为止，该树即为所求得的哈夫曼树。
         */
        public void Build(string input)
        {
            Dictionary<char, int> totals = new Dictionary<char, int>();
            foreach (char c in input)
            {
                if (totals.ContainsKey(c) == false) { totals.Add(c, 0); }
                totals[c]++;
            }

            List<HNode> list = new List<HNode>();
            foreach (var kv in totals)
            {
                list.Add(new HNode(kv.Key, kv.Value));
            }

            build(list);

            doEncode(Root,0,0);
        }

        void build(List<HNode> list)
        {
            if (list.Count == 1)
            {
                Root = list[0];
                return;
            }
            HNode left = FetchMinNode(list);

            HNode right = FetchMinNode(list);


            HNode root = new HNode('*', left.priority + right.priority);
            root.left = left;
            root.right = right;
            root.isleaf = false;

            list.Add(root);

            build(list);
        }

        HNode FetchMinNode(List<HNode> list)
        {

            HNode min = list[0];
            int pos = 0;
            for (int i = 1; i < list.Count; i++)
            {
                HNode node = list[i];
                if (node.priority < min.priority)
                {
                    min = node;
                    pos = i;
                }
            }

            list.RemoveAt(pos);
            return min;
        }

        /*
         * 将二叉树分支中的左分支编为 0，右分支编为 1；
         * 可以发现每个字符都在树的叶子节点上，因此要获取每个字符的哈夫曼编码，就通过根节点遍历到对应的子节点所经历的路径就是这个字符的编码；
         * 可以发现使用频率高的字符e 其编码长度是比出现频率低的字符c 编码长度要少。
         */

        struct HEncode
        {
            public int code;
            public int bitlen;
        }

        Dictionary<char, HEncode> EncodeDict = new Dictionary<char, HEncode>();
        void doEncode(HNode node, int code, int bitlen)
        {
            if (node.isleaf)
            {
                EncodeDict.Add(node.value, new HEncode { code=code,bitlen=bitlen});
            }
            else
            {
                doEncode(node.left, code << 1,bitlen+1);
                doEncode(node.right, (code << 1) + 1,bitlen+1);
            }

        }

        public byte[] Encode(string input,out int bitlen)
        {
            Build(input);

            BitArray arr = new BitArray(0);
            foreach(char c in input)
            {
                HEncode e = EncodeDict[c];
                for(int i = 0; i < e.bitlen; i++)
                {
                    arr[arr.Length++] = ((e.code >> (e.bitlen - i -1)) & 1) == 1;
                }
            }
            bitlen = arr.Length;
            byte[] ret = new byte[arr.Length / 8 + 1];
            arr.CopyTo(ret, 0);
            return ret;
        }

        public string Decode(byte[] input,int bitlen)
        {
            string ret = "";
            BitArray arr = new BitArray(input);
            HNode p = Root;
            for(int i=0;i<=bitlen;i++)
            {
                if (p.isleaf)
                {
                    ret+=p.value;
                    p = Root;
                }
                if (arr[i])//1
                {
                    p = p.right;
                }
                else
                {
                    p = p.left;
                }
            }

            return ret;
        }
    }
}
