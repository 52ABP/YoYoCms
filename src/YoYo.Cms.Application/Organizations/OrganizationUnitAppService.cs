using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Abp.UI;
using Abp.Linq.Extensions;
using YoYo.Cms.Organizations.Dto;

namespace YoYo.Cms.Organizations
{
    //[AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits)]
    public class OrganizationUnitAppService : CmsAppServiceBase, IOrganizationUnitAppService
    {

        #region 初始化

        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;

        public OrganizationUnitAppService(
            OrganizationUnitManager organizationUnitManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository)
        {
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
        }

        #endregion

        public async Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync()
        {
            var query =
                from ou in _organizationUnitRepository.GetAll()
                join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g
                select new { ou, memberCount = g.Count() };

            var items = await query.ToListAsync();

            return new ListResultDto<OrganizationUnitDto>(
                items.Select(item =>
                {
                    var dto = item.ou.MapTo<OrganizationUnitDto>();
                    dto.MemberCount = item.memberCount;
                    return dto;
                }).ToList());
        }

        public async Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsersAsync(GetOrganizationUnitUsersInput input)
        {
            try
            {
                var query = from uou in _userOrganizationUnitRepository.GetAll()
                            join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                            join user in UserManager.Users on uou.UserId equals user.Id
                            where uou.OrganizationUnitId == input.Id
                            select new { uou, user } ;

                var totalCount = await query.CountAsync();
                var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<OrganizationUnitUserListDto>(
                    totalCount,
                    items.Select(item =>
                    {
                        var dto = item.user.MapTo<OrganizationUnitUserListDto>();
                        dto.AddedTime = item.uou.CreationTime;
                        return dto;
                    }).ToList());
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> CreateOrganizationUnitAsync(CreateOrganizationUnitInput input)
        {
                var organizationUnit = new OrganizationUnit(AbpSession.TenantId, input.DisplayName, input.ParentId);
                await _organizationUnitManager.CreateAsync(organizationUnit);
                await CurrentUnitOfWork.SaveChangesAsync();

                return organizationUnit.MapTo<OrganizationUnitDto>();
        }

        //[AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnitAsync(UpdateOrganizationUnitInput input)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(input.Id);

            if (organizationUnit == null)
                throw new UserFriendlyException("指定用户不存在");

            organizationUnit.DisplayName = input.DisplayName;

            await _organizationUnitManager.UpdateAsync(organizationUnit);

            return await CreateOrganizationUnitDto(organizationUnit);
        }

        //[AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnitAsync(MoveOrganizationUnitInput input)
        {
            await _organizationUnitManager.MoveAsync(input.Id, input.NewParentId);

            return await CreateOrganizationUnitDto(
                await _organizationUnitRepository.GetAsync(input.Id)
                );
        }

        //[AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task DeleteOrganizationUnitAsync(EntityDto<long> input)
        {
            await _organizationUnitManager.DeleteAsync(input.Id);
        }

        //[AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task AddUserToOrganizationUnitAsync(UserToOrganizationUnitInput input)
        {
            await UserManager.AddToOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }

        //[AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task RemoveUserFromOrganizationUnitAsync(UserToOrganizationUnitInput input)
        {
            await UserManager.RemoveFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }

        //[AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task<bool> IsInOrganizationUnitAsync(UserToOrganizationUnitInput input)
        {
            return await UserManager.IsInOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }

        private async Task<OrganizationUnitDto> CreateOrganizationUnitDto(OrganizationUnit organizationUnit)
        {
            var dto = organizationUnit.MapTo<OrganizationUnitDto>();
            dto.MemberCount = await _userOrganizationUnitRepository.CountAsync(uou => uou.OrganizationUnitId == organizationUnit.Id);
            return dto;
        }
    }
}