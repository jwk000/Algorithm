using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAlgorithm
{
    public static class IntArrayExt
    {
        public static string ToString2(this int[] arr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in arr)
            {
                sb.Append(i).Append(' ');
            }
            sb.Length--;
            return sb.ToString();

        }
    }
}
