using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using YoYo.Cms.Dto;

namespace YoYo.Cms.Organizations.Dto
{
    public class GetOrganizationUnitUsersInput : PagedAndSortedInputDto, IShouldNormalize
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = " user.Id ";
            }
            else if (Sorting.Contains("userName"))
            {
                Sorting = Sorting.Replace("userName", "user.userName");
            }
            else if (Sorting.Contains("addedTime"))
            {
                Sorting = Sorting.Replace("addedTime", "uou.creationTime");
            }
        }
    }
}