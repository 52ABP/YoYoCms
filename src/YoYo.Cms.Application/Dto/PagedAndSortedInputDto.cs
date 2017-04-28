using Abp.Application.Services.Dto;

namespace YoYo.Cms.Dto
{
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {
        public string Sorting { get; set; }

        public PagedAndSortedInputDto()
        {
            MaxResultCount = CmsConsts.DefaultPageSize;
        }
    }
}