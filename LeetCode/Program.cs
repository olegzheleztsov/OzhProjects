using System;

namespace LeetCode
{
    class Program
    {

        static void Main(string[] args)
        {
            int[] arr = { 17, 18, 5, 4, 6, 1 };
            Console.WriteLine(string.Join(", ", new Solution().ReplaceElements(arr)));


            //Console.WriteLine(string.Join(',', arr));
        }
    }
}
