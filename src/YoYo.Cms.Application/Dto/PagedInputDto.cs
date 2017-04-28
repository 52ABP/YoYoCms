using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YoYo.Cms.Dto
{
    /// <summary>
    /// 分页接收信息
    /// </summary>
    public class PagedInputDto : IPagedResultRequest
    {
        /// <summary>
        /// 每页的数据条数
        /// </summary>
        [Range(1, CmsConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }
        /// <summary>
        /// 跨数据条数
        /// </summary>
        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }
        /// <summary>
        /// 每页的数据条数(默认10条)
        /// </summary>
        public PagedInputDto()
        {
            MaxResultCount = CmsConsts.DefaultPageSize;
        }
    }
}