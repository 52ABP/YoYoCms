// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：OperationResultType.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System.ComponentModel;

#endregion

namespace YoYo.Common.Data
{
    /// <summary>
    ///     表示业务操作结果的枚举
    /// </summary>
    public enum OperationResultType
    {
        /// <summary>
        ///     输入信息验证失败
        /// </summary>
        [Description("输入信息验证失败。")] ValidError,

        /// <summary>
        ///     指定参数的数据不存在
        /// </summary>
        [Description("指定参数的数据不存在。")] QueryNull,

        /// <summary>
        ///     操作取消或操作没引发任何变化
        /// </summary>
        [Description("操作没有引发任何变化，提交取消。")] NoChanged,

        /// <summary>
        ///     操作成功
        /// </summary>
        [Description("操作成功。")] Success,

        /// <summary>
        ///     操作引发错误
        /// </summary>
        [Description("操作引发错误。")] Error
    }
}