using System;
using System.Collections.Generic;
using Baymax.Entity;
using Baymax.Entity.Interface;

namespace Baymax.Extension.Entity
{
    public static class IEnumerablePagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom = 0)
        {
            return new PagedList<T>(source, pageIndex, pageSize, indexFrom);
        }

        public static IPagedList<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize, int indexFrom = 0)
        {
            return new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize, indexFrom);
        }
    }
}