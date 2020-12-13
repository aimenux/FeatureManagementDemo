using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Helpers
{
    public static class Randomize
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        public static int RandomNumber() => Random.Next();

        public static int RandomNumber(int max) => Random.Next(max);

        public static T RandomFrom<T>(ICollection<T> objects)
        {
            if (objects == null) return default;
            var index = Random.Next(objects.Count);
            return objects.ElementAt(index);
        }
    }
}
