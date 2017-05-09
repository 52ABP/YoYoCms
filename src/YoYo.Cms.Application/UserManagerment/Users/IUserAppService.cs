

// 项目展示地址:"http://www.ddxc.org/"
// 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-04-24T21:56:56. All Rights Reserved.
//<生成时间>2017-04-24T21:56:56</生成时间>

using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YoYo.Cms.UserManagerment.Users.Dtos;

namespace YoYo.Cms.UserManagerment.Users
{
    /// <summary>
    /// 用户信息服务接口
    /// </summary>
    public interface IUserAppService : IApplicationService
    {
        #region 用户信息管理

        /// <summary>
        /// 根据查询条件获取用户信息分页列表
        /// </summary>
        Task<PagedResultDto<UserListDto>> GetPagedUsersAsync(GetUserInput input);

        /// <summary>
        /// 通过Id获取用户信息信息进行编辑或修改 
        /// </summary>
        Task<GetUserForEditOutput> GetUserForEditAsync(NullableIdDto<long> input);

        /// <summary>
        /// 通过指定id获取用户信息ListDto信息
        /// </summary>
        Task<UserListDto> GetUserByIdAsync(EntityDto<long> input);

        /// <summary>
        /// 新增或更改用户信息
        /// </summary>
        Task CreateOrUpdateUserAsync(CreateOrUpdateUserInput input);

        /// <summary>
        /// 新增用户信息
        /// </summary>
        Task<UserEditDto> CreateUserAsync(UserEditDto input);

        ///// <summary>
        ///// 更新用户信息
        ///// </summary>
        //Task UpdateUserAsync(UserEditDto input);

        /// <summary>
        /// 删除用户信息
        /// </summary>
        Task DeleteUserAsync(EntityDto<long> input);

        ///// <summary>
        ///// 批量删除用户信息
        ///// </summary>
        //Task BatchDeleteUserAsync(List<long> input);

        #endregion


        #region 修改用户资料

        Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);

        Task CreateOrUpdateUser(CreateOrUpdateUserInput input);

        Task<string> ResetPassword(NullableIdDto<long> input);

        #endregion




    }
}
