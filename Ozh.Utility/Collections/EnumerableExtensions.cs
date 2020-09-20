using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ozh.Utility.Collections
{
    public static class EnumerableExtensions
    {
        private static readonly Random random = new Random();

        public static T GetRandomElement<T>(this IEnumerable<T> collection)
        {
            var count = collection.Count();
            if(count == 0)
            {
                return default;
            }
            return collection.ToArray()[random.Next(0, count)];
        }
    }
}
