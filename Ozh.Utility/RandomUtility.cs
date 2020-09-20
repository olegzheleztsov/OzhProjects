using System;
using System.Collections.Generic;
using System.Text;

namespace Ozh.Utility
{
    public static class RandomUtility
    {
        private static readonly Random random = new Random();

        public static int InRange(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
