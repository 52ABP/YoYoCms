// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：PageCondition.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

namespace YoYo.Common.Data
{
    /// <summary>
    ///     分页查询条件信息
    /// </summary>
    public class PageCondition
    {
        /// <summary>
        ///     初始化一个 默认参数（第1页，每页20，排序条件为空）的分页查询条件信息类 的新实例
        /// </summary>
        public PageCondition()
        {
            PageIndex = 1;
            PageSize = 20;
            SortConditions = new SortCondition[] {};
        }

        /// <summary>
        ///     初始化一个 指定页索引与页大小的分页查询条件信息类 的新实例
        /// </summary>
        /// <param name="pageIndex"> 页索引 </param>
        /// <param name="pageSize"> 页大小 </param>
        public PageCondition(int pageIndex, int pageSize)
            : this()
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
        ///     获取或设置 页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     获取或设置 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     获取或设置 排序条件组
        /// </summary>
        public SortCondition[] SortConditions { get; set; }
    }
}