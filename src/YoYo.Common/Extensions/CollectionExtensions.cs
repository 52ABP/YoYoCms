﻿// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：CollectionExtensions.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#endregion

namespace YoYo.Common.Extensions
{
    /// <summary>
    ///     集合扩展方法类
    /// </summary>
    public static class CollectionExtensions
    {
        #region IEnumerable的扩展

        /// <summary>
        ///     将集合展开并分别转换成字符串，再以指定的分隔符衔接，拼成一个字符串返回。默认分隔符为逗号
        /// </summary>
        /// <param name="collection"> 要处理的集合 </param>
        /// <param name="separator"> 分隔符，默认为逗号 </param>
        /// <returns> 拼接后的字符串 </returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection, string separator = ",")
        {
            return collection.ExpandAndToString(t => t.ToString(), separator);
        }

        /// <summary>
        ///     循环集合的每一项，调用委托生成字符串，返回合并后的字符串。默认分隔符为逗号
        /// </summary>
        /// <param name="collection">待处理的集合</param>
        /// <param name="itemFormatFunc">单个集合项的转换委托</param>
        /// <param name="separetor">分隔符，默认为逗号</param>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <returns></returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection, Func<T, string> itemFormatFunc,
            string separetor = ",")
        {
            collection = collection as IList<T> ?? collection.ToList();
            itemFormatFunc.CheckNotNull(nameof(itemFormatFunc));
            if (!collection.Any())
                return null;
            var sb = new StringBuilder();
            var i = 0;
            var count = collection.Count();
            foreach (var t in collection)
            {
                if (i == count - 1)
                    sb.Append(itemFormatFunc(t));
                else
                    sb.Append(itemFormatFunc(t) + separetor);
                i++;
            }
            return sb.ToString();
        }

        /// <summary>
        ///     集合是否为空
        /// </summary>
        /// <param name="collection"> 要处理的集合 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 为空返回True，不为空返回False </returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            collection = collection as IList<T> ?? collection.ToList();
            return !collection.Any();
        }

        /// <summary>
        ///     根据第三方条件是否为真来决定是否执行指定条件的查询
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            predicate.CheckNotNull(nameof(predicate));
            source = source as IList<T> ?? source.ToList();

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        ///     根据指定条件返回集合中不重复的元素
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <typeparam name="TKey">动态筛选条件类型</typeparam>
        /// <param name="source">要操作的源</param>
        /// <param name="keySelector">重复数据筛选条件</param>
        /// <returns>不重复元素的集合</returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            keySelector.CheckNotNull(nameof(keySelector));
            source = source as IList<T> ?? source.ToList();

            return source.GroupBy(keySelector).Select(group => group.First());
        }

        #endregion IEnumerable的扩展

        #region IQueryable的扩展

        /// <summary>
        ///     根据第三方条件是否为真来决定是否执行指定条件的查询
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate,
            bool condition)
        {
            source.CheckNotNull(nameof(source));
            predicate.CheckNotNull(nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }


        private static Action<T> Fix<T>(Func<Action<T>, Action<T>> f)
        {
            return x => f(Fix(f))(x);
        }

        private static void Render<T>(T model, Func<Action<T>, Action<T>> f)
        {
            Fix(f)(model);
        }

        /// <summary>
        ///     从指定集合TSource递归出无限级TResult集合、常用于树形数据
        /// </summary>
        /// <typeparam name="TResult">返回的集合类型</typeparam>
        /// <typeparam name="TSource">原始类型</typeparam>
        /// <param name="sources">原始集合数据</param>
        /// <param name="roots">原始根节点数据</param>
        /// <param name="mapper">类型映射mapper</param>
        /// <param name="getchilden">子集合表达式</param>
        /// <param name="chidenway">集合的处理方式</param>
        /// <returns>TResult集合</returns>
        public static ICollection<TResult> RenderTree<TSource, TResult>(this List<TSource> sources,
            List<TSource> roots, Func<TSource, int, TResult> mapper, Func<TSource, List<TSource>
                , List<TSource>> getchilden, Action<TResult, TResult> chidenway) where TResult : new()
        {
            var Items = new List<TResult>();
            var parentnode = default(TResult);
            var level = 0;
            Render(roots, render => categories =>
            {
                foreach (var cat in categories)
                {
                    var item = mapper(cat, level);
                    var childens = getchilden(cat, sources);
                    if (roots.Contains(cat))
                    {
                        parentnode = default(TResult);
                        level = 0;
                        Items.Add(item);
                        parentnode = item;
                    }
                    else
                    {
                        chidenway(parentnode, item);
                        if (childens.Count > 0)
                        {
                            level++;
                            parentnode = item;
                        }
                    }
                    render(childens);
                }
            });
            return Items;
        }

        public static ICollection<TResult> RenderTree<TSource, TResult>(this List<TSource> sources,
            List<TSource> roots, Func<TSource, int, TResult> mapper, Func<TSource, List<TSource>
                , List<TSource>> getchilden, Action<TResult, TResult, List<TResult>> chidenway) where TResult : new()
        {
            var Items = new List<TResult>();
            var parentnode = default(TResult);
            var level = 0;
            Render(roots, render => categories =>
            {
                foreach (var cat in categories)
                {
                    var item = mapper(cat, level);
                    var childens = getchilden(cat, sources);
                    if (roots.Contains(cat))
                    {
                        parentnode = default(TResult);
                        level = 0;
                        Items.Add(item);
                        parentnode = item;
                    }
                    else
                    {
                        chidenway(parentnode, item, Items);
                        if (childens.Count > 0)
                        {
                            level++;
                            parentnode = item;
                        }
                    }
                    render(childens);
                }
            });
            return Items;
        }

        /*/// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中筛选指定键范围内的子数据集
        /// </summary>
        /// <typeparam name="TSource">集合元素类型</typeparam>
        /// <typeparam name="TKey">筛选键类型</typeparam>
        /// <param name="source">要筛选的数据源</param>
        /// <param name="keySelector">筛选键的范围表达式</param>
        /// <param name="start">筛选范围起始值</param>
        /// <param name="end">筛选范围结束值</param>
        /// <param name="startEqual">是否等于起始值</param>
        /// <param name="endEqual">是否等于结束集</param>
        /// <returns></returns>
        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            TKey start,
            TKey end,
            bool startEqual = false,
            bool endEqual = false) where TKey : IComparable<TKey>
        {
            Expression[] paramters = keySelector.Parameters.Cast<Expression>().ToArray();
            Expression key = Expression.Invoke(keySelector, paramters);
            Expression startBound = startEqual
                ? Expression.GreaterThanOrEqual(key, Expression.Constant(start))
                : Expression.GreaterThan(key, Expression.Constant(start));
            Expression endBound = endEqual
                ? Expression.LessThanOrEqual(key, Expression.Constant(end))
                : Expression.LessThan(key, Expression.Constant(end));
            Expression and = Expression.AndAlso(startBound, endBound);
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);
            return source.Where(lambda);
        }*/

        #endregion IQueryable的扩展
    }
}