// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：MemoryCacheExtensions.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System.Runtime.Caching;

#endregion

namespace YoYo.Common.Extensions
{
    /// <summary>
    ///     内存缓存扩展操作类
    /// </summary>
    public static class MemoryCacheExtensions
    {
        /// <summary>
        ///     获取指定键值的强类型数据
        /// </summary>
        /// <typeparam name="T">强类型</typeparam>
        /// <param name="cache"></param>
        /// <param name="key">缓存键值</param>
        /// <param name="regionName">区域名称，默认不支持</param>
        /// <returns></returns>
        public static T Get<T>(this MemoryCache cache, string key, string regionName = null)
        {
            var value = cache.Get(key, regionName);
            if (value is T)
                return (T) value;
            return default(T);
        }
    }
}