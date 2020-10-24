using System.Collections.Generic;
using System.Linq;

namespace sorovi.Validation.Tests.Helper
{
    public static class ObjectHelper
    {
        public static object[][] InverseBool(this IEnumerable<object[]> v, int atIndex = 1) => v
            .Select(item =>
            {
                var t = item.Clone() as object[];
                t[atIndex] = !((bool) t[atIndex]);
                return t;
            })
            .ToArray();
    }
}