using System;
using System.Collections.Generic;

namespace Maze
{
    public static class Min
    {
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) => MinBy(source, keySelector, comparer: null);

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            comparer ??= Comparer<TKey>.Default;

            var key = default(TKey);
            var value = default(TSource);
            using var e = source.GetEnumerator();
            if (key == null)
            {
                do
                {
                    if (!e.MoveNext())
                        return value;

                    value = e.Current;
                    key = keySelector(value);
                } while (key == null);

                while (e.MoveNext())
                {
                    var nextValue = e.Current;
                    var nextKey = keySelector(nextValue);
                    if (nextKey != null && comparer.Compare(nextKey, key) < 0)
                    {
                        key = nextKey;
                        value = nextValue;
                    }
                }
            }
            else
            {
                if (!e.MoveNext())
                    throw new InvalidOperationException("No elements.");

                value = e.Current;
                key = keySelector(value);
                if (comparer == Comparer<TKey>.Default)
                {
                    while (e.MoveNext())
                    {
                        var nextValue = e.Current;
                        var nextKey = keySelector(nextValue);
                        if (Comparer<TKey>.Default.Compare(nextKey, key) < 0)
                        {
                            key = nextKey;
                            value = nextValue;
                        }
                    }
                }
                else
                {
                    while (e.MoveNext())
                    {
                        var nextValue = e.Current;
                        var nextKey = keySelector(nextValue);
                        if (comparer.Compare(nextKey, key) < 0)
                        {
                            key = nextKey;
                            value = nextValue;
                        }
                    }
                }
            }

            return value;
        }
    }
}