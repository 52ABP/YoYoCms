using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Notifications;

namespace YoYo.Cms.Notifications.Dto
{
    public class GetNotificationsOutput : PagedResultDto<UserNotification>
    {
        /// <summary>
        /// 未阅读消息数量
        /// </summary>
        public int UnreadCount { get; set; }

        public GetNotificationsOutput(int totalCount, int unreadCount, List<UserNotification> notifications)
            :base(totalCount, notifications)
        {
            UnreadCount = unreadCount;
        }
    }
}