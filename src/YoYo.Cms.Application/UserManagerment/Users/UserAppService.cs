


// 项目展示地址:"http://www.ddxc.org/"
// 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-04-24T21:56:56. All Rights Reserved.
//<生成时间>2017-04-24T21:56:56</生成时间>

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using YoYo.Cms.UserManagerment.Users.Dtos;
using Abp.Extensions;
using System.Linq;
using YoYo.Cms.Authorization.Roles;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using System.Collections.ObjectModel;
using Abp.Authorization.Users;
using Abp.UI;

namespace YoYo.Cms.UserManagerment.Users
{
    /// <summary>
    /// 用户信息服务实现
    /// </summary>
    //[AbpAuthorize(UserAppPermissions.User)]
    public class UserAppService : CmsAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly RoleManager _roleManager;

        /// <summary>
        /// 构造方法
        /// </summary>
        public UserAppService(IRepository<User, long> userRepository,
             RoleManager roleManager)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
        }

        #region 用户信息管理

        /// <summary>
        /// 根据查询条件获取用户信息分页列表
        /// </summary>
        public async Task<PagedResultDto<UserListDto>> GetPagedUsersAsync(GetUserInput input)
        {

            var query = _userRepository.GetAll()
                  .Include(u => u.Roles)
                .WhereIf(input.Role.HasValue, u => u.Roles.Any(r => r.RoleId == input.Role.Value))
                .WhereIf(
                    !StringExtensions.IsNullOrWhiteSpace(input.FilterText),
                    u =>
                        u.UserName.Contains(input.FilterText) ||
                        u.EmailAddress.Contains(input.FilterText)
                ); ;
            //TODO:根据传入的参数添加过滤条件


            var userCount = await query.CountAsync();

            var users = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var userListDtos = users.MapTo<List<UserListDto>>();
            return new PagedResultDto<UserListDto>(
            userCount,
            userListDtos
            );
        }

        /// <summary>
        /// 通过Id获取用户信息信息进行编辑或修改 
        /// </summary>
        public async Task<GetUserForEditOutput> GetUserForEditAsync(NullableIdDto<long> input)
        {
            var output = new GetUserForEditOutput();

            UserEditDto userEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _userRepository.GetAsync(input.Id.Value);
                userEditDto = entity.MapTo<UserEditDto>();
            }
            else
            {
                userEditDto = new UserEditDto();
            }

            output.User = userEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取用户信息ListDto信息
        /// </summary>
        public async Task<UserListDto> GetUserByIdAsync(EntityDto<long> input)
        {
            var entity = await _userRepository.GetAsync(input.Id);

            return entity.MapTo<UserListDto>();
        }

        /// <summary>
        /// 新增或更改用户信息
        /// </summary>
        public async Task CreateOrUpdateUserAsync(CreateOrUpdateUserInput input)
        {
            if (input.UserEditDto.Id.HasValue)
            {
                await UpdateUserAsync(input.UserEditDto);
            }
            else
            {
                await CreateUserAsync(input.UserEditDto);
            }
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        public virtual async Task<UserEditDto> CreateUserAsync(UserEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<User>();

            entity = await _userRepository.InsertAsync(entity);
            return entity.MapTo<UserEditDto>();
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        public virtual async Task UpdateUserAsync(UserEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _userRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _userRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        public async Task DeleteUserAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _userRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除用户信息
        /// </summary>
        public async Task BatchDeleteUserAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _userRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input)
        {
            //Getting all available roles
            var userRoleDtos = await _roleManager.Roles
                .OrderBy(r => r.DisplayName)
                .Select(r => new UserRoleDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDisplayName = r.DisplayName
                })
                .ToArrayAsync();

            var output = new GetUserForEditOutput
            {
                Roles = userRoleDtos
            };

            if (!input.Id.HasValue)
            {
                //Creating a new user
                output.User = new UserEditDto { IsActive = true, ShouldChangePasswordOnNextLogin = true };

                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    var defaultUserRole = userRoleDtos.FirstOrDefault(ur => ur.RoleName == defaultRole.Name);
                    if (defaultUserRole != null)
                        defaultUserRole.IsAssigned = true;
                }
            }
            else
            {
                //Editing an existing user
                var user = await UserManager.GetUserByIdAsync(input.Id.Value);

                output.User = user.MapTo<UserEditDto>();
                //output.ProfilePictureId = user.ProfilePictureId; 合并代码后生产文件再启用

                foreach (var userRoleDto in userRoleDtos)
                    userRoleDto.IsAssigned = await UserManager.IsInRoleAsync(input.Id.Value, userRoleDto.RoleName);
            }

            return output;
        }

        public async Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            if (input.UserEditDto.Id.HasValue)
                await UpdateUserAsync(input);
            else
                await CreateUserAsync(input);
        }

        protected virtual async Task UpdateUserAsync(CreateOrUpdateUserInput input)
        {
            Debug.Assert(input.UserEditDto.Id != null, "input.User.Id should be set.");

            var user = await UserManager.FindByIdAsync(input.UserEditDto.Id.Value);

            //Update user properties
            input.UserEditDto.MapTo(user); //Passwords is not mapped (see mapping configuration)

            if (!StringExtensions.IsNullOrEmpty(input.UserEditDto.Password))
                CheckErrors(await UserManager.ChangePasswordAsync(user, input.UserEditDto.Password));
            if(input.UserEditDto.ShouldResetPassword)
                user.Password = new PasswordHasher().HashPassword("123qwe");
            CheckErrors(await UserManager.UpdateAsync(user));

            //Update roles
            CheckErrors(await UserManager.SetRoles(user, input.AssignedRoleNames));
        }

        protected  async Task CreateUserAsync(CreateOrUpdateUserInput input)
        {
            var user = input.UserEditDto.MapTo<User>(); //Passwords is not mapped (see mapping configuration)
            user.TenantId = AbpSession.TenantId;

            //Set password
            if (!StringExtensions.IsNullOrEmpty(input.UserEditDto.Password))
                CheckErrors(await UserManager.PasswordValidator.ValidateAsync(input.UserEditDto.Password));
            else
                input.UserEditDto.Password = User.CreateRandomPassword();

            user.Password = new PasswordHasher().HashPassword(input.UserEditDto.Password);
            //user.ShouldChangePasswordOnNextLogin = input.User.ShouldChangePasswordOnNextLogin; 合并代码生产后启用

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.AssignedRoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            CheckErrors(await UserManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new user's Id.

        }

        public async Task<string> ResetPassword(NullableIdDto<long> input)
        {
            var user = await _userRepository.GetAsync(input.Id.Value);
            if (user == null)
                throw new UserFriendlyException("用户异常");
            
            user.Password = new PasswordHasher().HashPassword("123qwe");
            return "123qwe";
        }

  


        #endregion
    }
}
