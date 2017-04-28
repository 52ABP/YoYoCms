// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：SingletonDictionary.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System.Collections.Generic;

#endregion

namespace YoYo.Common.Data
{
    /// <summary>
    ///     创建一个单例字典，该实例的生命周期将跟随整个应用程序
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        static SingletonDictionary()
        {
            Singleton<IDictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        ///     获取指定类型的字典的单例实例
        /// </summary>
        public new static IDictionary<TKey, TValue> Instance
        {
            get { return Singleton<IDictionary<TKey, TValue>>.Instance; }
        }
    }
}