// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：CheckExistsType.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

namespace YoYo.Common.Data
{
    /// <summary>
    ///     指定可用于表数据存在性检查类型的值
    /// </summary>
    public enum CheckExistsType
    {
        /// <summary>
        ///     插入数据时重复性检查
        /// </summary>
        Insert,

        /// <summary>
        ///     编辑数据时重复性检查
        /// </summary>
        Update
    }
}