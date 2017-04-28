// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：SortCondition.cs
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
    ///     列表字段排序条件
    /// </summary>
    public class SortCondition
    {
        /// <summary>
        ///     构造一个指定字段名称的升序排序的排序条件
        /// </summary>
        /// <param name="sortField">字段名称</param>
        public SortCondition(string sortField)
            : this(sortField, ListSortDirection.Ascending)
        {
        }

        /// <summary>
        ///     构造一个排序字段名称和排序方式的排序条件
        /// </summary>
        /// <param name="sortField">字段名称</param>
        /// <param name="listSortDirection">排序方式</param>
        public SortCondition(string sortField, ListSortDirection listSortDirection)
        {
            SortField = sortField;
            ListSortDirection = listSortDirection;
        }

        /// <summary>
        ///     获取或设置 排序字段名称
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        ///     获取或设置 排序方向
        /// </summary>
        public ListSortDirection ListSortDirection { get; set; }
    }
}