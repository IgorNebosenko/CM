using System.Collections.Generic;

namespace CM.Core.Utils
{
    public static class EnumeratorUtil
    {
        public static IEnumerator<T> Single<T>(T value)
        {
            while (true)
                yield return value;
        }
    }
}