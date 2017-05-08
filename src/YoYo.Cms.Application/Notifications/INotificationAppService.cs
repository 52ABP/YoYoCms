using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YoYo.Cms.Notifications.Dto;

namespace YoYo.Cms.Notifications
{
    /// <summary>
    /// 消息通知系统服务
    /// </summary>
    public interface INotificationAppService : IApplicationService
    {
        /// <summary>
        /// 获取用户通知信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetNotificationsOutput> GetPagedUserNotificationsAsync(GetUserNotificationsInput input);
        /// <summary>
        /// 设置所有通知为已阅读状态
        /// </summary>
        /// <returns></returns>
        Task MakeAllUserNotificationsAsRead();
        /// <summary>
        /// 标记某条通知为已读
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task MakeNotificationAsRead(EntityDto<Guid> input);
        /// <summary>
        /// 获取消息设置
        /// </summary>
        /// <returns></returns>
        Task<GetNotificationSettingsOutput> GetNotificationSettings();
        /// <summary>
        /// 更新消息设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateNotificationSettings(UpdateNotificationSettingsInput input);
    }
}