// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：SingletonList.cs
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
    ///     创建一个类型列表的单例，该实例的生命周期将跟随整个应用程序
    /// </summary>
    /// <typeparam name="T">要创建的列表元素的类型</typeparam>
    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        /// <summary>
        ///     获取指定类型的列表的单例实例
        /// </summary>
        public new static IList<T> Instance
        {
            get { return Singleton<IList<T>>.Instance; }
        }
    }
}