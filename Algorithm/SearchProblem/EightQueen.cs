using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //在8×8格的国际象棋上摆放八个皇后，使其不能互相攻击，即任意两个皇后都不能处于同一行、同一列或同一斜线上，问一共有多少种摆法。
    public class EightQueen
    {
        struct Offset
        {
            public int x;
            public int y;
            public Offset(int a,int b) { x = a;y = b; }
        }
        int[,] board = new int[8, 8];
        List<Offset> offsets = new List<Offset>();

        void InitOffset()
        {
            offsets.Add(new Offset(0, 0));
            for(int i = -8;i<8;i++)
            {
                if (i == 0) continue;
                offsets.Add(new Offset(0, i));
                offsets.Add(new Offset(i, 0));
                offsets.Add(new Offset(i, i));
                offsets.Add(new Offset(i, -i));
            }
        }

   
        bool CanAddQueen(int i, int j)
        {
            foreach (Offset p in offsets)
            {
                int a = i + p.x; 
                int b = j + p.y;
                if (a >= 0 && a < 8 && b >= 0 && b < 8)
                {
                    if(board[a, b] > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        int ans = 0;
        public void AddQueen(int row)
        {
            for (int i = 0; i < 8; i++)
            {
                if (CanAddQueen(row, i))
                {
                    if (row == 7)
                    {
                        ans++;
                        return;
                    }
                    board[row, i] = 1;
                    AddQueen(row + 1);
                    board[row, i] = 0;
                }
            }
        }

        public int CalcEightQueen()
        {
            InitOffset();
            ans = 0;
            AddQueen(0);
            return ans;
        }

    }
}
