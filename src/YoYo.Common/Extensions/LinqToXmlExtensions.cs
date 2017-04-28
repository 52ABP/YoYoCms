// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：LinqToXmlExtensions.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System.Xml;
using System.Xml.Linq;

#endregion

namespace YoYo.Common.Extensions
{
    /// <summary>
    ///     Xml 扩展操作类
    /// </summary>
    public static class LinqToXmlExtensions
    {
        #region 公共方法

        /// <summary>
        ///     将XmlNode转换为XElement
        /// </summary>
        /// <returns> XElment对象 </returns>
        public static XElement ToXElement(this XmlNode node)
        {
            var xdoc = new XDocument();
            using (var xmlWriter = xdoc.CreateWriter())
            {
                node.WriteTo(xmlWriter);
            }
            return xdoc.Root;
        }

        /// <summary>
        ///     将XElement转换为XmlNode
        /// </summary>
        /// <returns> 转换后的XmlNode </returns>
        public static XmlNode ToXmlNode(this XElement element)
        {
            using (var xmlReader = element.CreateReader())
            {
                var xml = new XmlDocument();
                xml.Load(xmlReader);
                return xml;
            }
        }

        #endregion 公共方法
    }
}