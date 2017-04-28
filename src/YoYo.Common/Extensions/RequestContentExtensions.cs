// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：RequestContentExtensions.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System.Net.Http;
using System.Web;

#endregion

namespace YoYo.Common.Extensions
{
    public static class RequestContentExtensions
    {
        /// <summary>
        ///     将API httpRequestMessage对象转换为传统HttpContent对象
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <returns></returns>
        public static HttpContextBase GetContextBase(this HttpRequestMessage httpRequestMessage)
        {
            return (HttpContextBase) httpRequestMessage.Properties["MS_HttpContext"];
        }
    }
}