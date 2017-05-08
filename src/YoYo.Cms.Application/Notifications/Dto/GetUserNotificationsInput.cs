using Abp.Notifications;
using YoYo.Cms.Dto;

namespace YoYo.Cms.Notifications.Dto
{
    /// <summary>
    /// 获取通知信息的参数
    /// </summary>
    public class GetUserNotificationsInput : PagedInputDto
    {
        /// <summary>
        /// 是否阅读枚举 0是未读 1是已经阅读
        /// </summary>
        public UserNotificationState? State { get; set; }
    }
}