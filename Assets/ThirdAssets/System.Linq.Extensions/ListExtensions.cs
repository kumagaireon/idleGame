using System;
using System.Collections.Generic;

namespace IdolGame.Linq.Extensions
{
    public static class ListExtensions
    {
        public static void ForEach<TSource, TArg>(this List<TSource> source, TArg arg, Action<TSource, TArg> action)
        {
            for (var i = 0; i < source.Count; i++)
            {
                action(source[i], arg);
            }
        }
    }
}
