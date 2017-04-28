// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：OperateCodeAttribute.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System;

#endregion

namespace YoYo.Common.Filter
{
    /// <summary>
    ///     表示查询操作的前台操作码
    /// </summary>
    public class OperateCodeAttribute : Attribute
    {
        /// <summary>
        ///     初始化一个<see cref="OperateCodeAttribute" />类型的新实例
        /// </summary>
        public OperateCodeAttribute(string code)
        {
            Code = code;
        }

        /// <summary>
        ///     初始化一个<see cref="OperateCodeAttribute" />类型的新实例
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        public OperateCodeAttribute(string code, string name)
        {
            Code = code;
            Name = name;
        }

        /// <summary>
        ///     获取 属性名称
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        ///     前台名称
        /// </summary>
        public string Name { get; private set; }
    }
}