using System;
using System.Collections.Generic;
using System.Text;

namespace Ozh.Utility.Collections
{
    public static class MathUtils
    {
        public static int Clamp(int value, int min, int max)
        {
            if(value < min)
            {
                value = min;
            }
            if(value > max)
            {
                value = max;
            }
            return value;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (value > max)
            {
                return max;
            }

            return value;
        }
    }
}
