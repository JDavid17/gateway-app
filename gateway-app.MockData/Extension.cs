using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gateway_app.MockData
{
    public static class Extension
    {
        public static Task<List<T>> AsListTask<T>(this T value)
            => new List<T> { value }.AsTask();

        public static Task<T> AsTask<T>(this T value)
            => Task.FromResult(value);
    }
}
