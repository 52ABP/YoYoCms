// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：ValidateCodeType.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

namespace YoYo.Common.Drawing
{
    /// <summary>
    ///     验证码类型
    /// </summary>
    public enum ValidateCodeType
    {
        /// <summary>
        ///     纯数值
        /// </summary>
        Number,

        /// <summary>
        ///     数值与字母的组合
        /// </summary>
        NumberAndLetter,

        /// <summary>
        ///     汉字
        /// </summary>
        Hanzi
    }
}