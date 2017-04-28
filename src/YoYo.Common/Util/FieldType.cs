// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：FieldType.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System.Collections.Generic;

#endregion

namespace YoYo.Common.Util
{
    public class FieldType
    {
        /// <summary>
        ///     获取字段分类
        /// </summary>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, string> GetFieldType()
        {
            var dic = new Dictionary<string, string>
            {
                {"single-text", "单行文本"},
                {"password", "密码"},
                {"multi-text", "密码"},
                {"editor", "多行文本"},
                {"images", "图片上传"},
                {"video", "视频上传"},
                {"number", "数字"},
                {"datetime", "时间"},
                {"multi-radio", "多项单选"},
                {"multi-checkbox", "多项多选"}
            };
            return dic;
        }
    }
}