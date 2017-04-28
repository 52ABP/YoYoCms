// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：UnitConversionHelper.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System;

#endregion

namespace YoYo.Common.UnitConversion
{
    /// <summary>
    ///     单位换算帮助类
    /// </summary>
    public static class UnitConversionHelper
    {
        /// <summary>
        ///     字节转换大小方法  (单位换算)
        /// </summary>
        /// <param name="size">bypes字节值</param>
        /// <returns></returns>
        public static string StorageUnitConversion(double size)
        {
            var units = new[] {"B", "KB", "MB", "GB", "TB", "PB"};
            var mod = 1024.0;
            var i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }
    }
}