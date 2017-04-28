// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：QueryablePropertySorter.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System.Collections.Concurrent;
using System.Linq.Expressions;

#endregion

namespace YoYo.Common.Filter
{
    /// <summary>
    ///     <see cref="IQueryable{T}" />类型字符串排序操作类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class QueryablePropertySorter<T>
    {
// ReSharper disable StaticFieldInGenericType
        private static readonly ConcurrentDictionary<string, LambdaExpression> Cache =
            new ConcurrentDictionary<string, LambdaExpression>();
    }
}