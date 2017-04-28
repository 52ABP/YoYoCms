using System.Threading.Tasks;
using Abp.Application.Services;
using YoYo.Cms.Sessions.Dto;

namespace YoYo.Cms.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
