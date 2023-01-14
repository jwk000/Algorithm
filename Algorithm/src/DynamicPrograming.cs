using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * 十大动态规划问题
     * 动态规划（Dynamic Programming）是求多阶段决策过程（Multistep Decision Process）最优化的一种数学方法。
     * 它将问题的整体按时间或空间的特征分成若干个前后衔接的时空阶段，把多阶段决策问题表示为前后有关的一系列单阶段决策问题，然后逐个求解，从而求出整个问题的最有决策序列。
     * 
     *  -- 最大子序列和
     *  -- 零钱兑换
     *  -- 最长上升子序列
     *  -- 堆盒子
     *  -- 01背包问题
     *  -- 完全背包
     *  -- 数组均衡划分
     *  -- 编辑距离
     *  -- 逻辑符处理
     *  -- 博弈游戏
     * 
     * ref :https://zhuanlan.zhihu.com/p/107501014
     * 
     */
    public class DynamicPrograming
    {
        /*
         * 最大子序列和
         * 给定一个整数数组 nums ，找到一个具有最大和的连续子数组（子数组最少包含一个元素），返回其最大和
         * 输入: [-2,1,-3,4,-1,2,1,-5,4] 输出：6 连续子数组 [4,-1,2,1] 的和最大，为 6。
         */
        public static int MaxContiguousSubsequence(int[] nums)
        {

        }

        /*
         * 零钱兑换
         * 给定不同面额的硬币 coins 和一个总金额 amount。编写一个函数来计算可以凑成总金额所需的最少的硬币个数。如果没有任何一种硬币组合能组成总金额，返回 -1。
         * 输入: coins = [1, 2, 5], amount = 11 输出: 3 解释: 11 = 5 + 5 + 1
         */

        public static int MakeChange(int[] coins, int amount)
        {

        }

        /*
         * 最长上升子序列（简称LIS问题）
         * 给定一个无序的整数数组，找到其中最长上升子序列的长度。
         * 输入: [10,9,2,5,3,7,101,18] 输出: 4 解释: 最长的上升子序列是 [2,3,7,101]，它的长度是 4。
         */

        public static int LongestIncreasingSubsequence(int[] arr)
        {

        }

        /*
         * 堆盒子
         * 给出了一组n种类型的矩形三维长方体，其中第i种盒子具有高度h（i）、宽度w（i）和深度D（i）。
         * 希望创建一个尽可能高的盒子堆，要求下一个盒子的底部的尺寸都严格大于上一个盒子的底部的尺寸。
         * 当然，可以旋转一个长方体，使任何边都作为其底部。还允许使用同一类型盒子的多个实例。
         * 
         */
        public static int BoxStacking(int[] H, int[] W, int[] D)
        {

        }


        /*
         * 01背包问题
         * 有 n 个物品和一个大小为 m 的背包. 给定数组 A 表示每个物品的大小和数组 V 表示每个物品的价值.问最多能装入背包的总价值是多大?
         * 
         */
        public static int BackPackI(int[] A,int[] V, int M)
        {

        }

        /*
         * 完全背包
         * 有 n 个物品和一个大小为 m 的背包. 给定数组 A 表示每个物品的大小和数组 V 表示每个物品的价值.物品是不限使用次数的.问最多能装入背包的总价值是多大?
         */
        public static int BackPackII(int[] A, int[] V, int M)
        {

        }

        /*
         * 数组均衡划分
         * 给定一组整数，任务是将其分为两组S1和S2，使得它们的和之间的绝对差最小。
         * 输入 arr[] = {1, 6, 11, 5} 输出: 1 解释: s1 = {1, 5, 6}, sum = 12 s2 = {11}, sum = 11
         */

        public static int BalancedPartition(int[] arr)
        {

        }

        /*
         * 编辑距离
         * 给定两个单词 word1 和 word2，计算出将 word1 转换成 word2 所使用的最少操作数 。
         * 你可以对一个单词进行如下三种操作： 插入一个字符 删除一个字符 替换一个字符
         * 输入: word1 = "intention", word2 = "execution"
         * 输出: 5
         * 解释:
         * intention -> inention (删除 't')
         * inention -> enention (将 'i' 替换为 'e')
         * enention -> exention (将 'n' 替换为 'x')
         * exention -> exection (将 'n' 替换为 'c')
         * exection -> execution (插入 'u')
         */
        public static int EditDistance()
        {

        }


        /*
         * 逻辑符处理
         * 给出了一个布尔表达式，该表达式由一个符号字符串“true”、“false”、“and”、“or”和“xor”组成。计算将表达式括起来以使其计算结果为true的方法数。
         * 例如，有两种方法将“true和false xor true”括起来，使其计算结果为true。
         * 
         */
        public static int BooleanParenthesizations(string exp)
        {

        }

        /*
         * 博弈游戏
         * 考虑一行n个硬币的值v1。vn，其中n为偶数。我们轮流与对手比赛。在每个回合中，玩家从一行中选择第一个或最后一个硬币，将其永久地从一行中移除，并接收硬币的价值。如果我们先走，确定我们能赢得的最大金额。
         * 注：对手和用户一样聪明。
         * 
         */
        public static int OptimalStrategyForGame(int[] arr)
        {

        }
    }
}
