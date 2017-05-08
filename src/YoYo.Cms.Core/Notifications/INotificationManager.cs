using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.Notifications
{
    /// <summary>
    /// 通知管理领域服务
    /// </summary>
    public interface INotificationManager
    {
        /// <summary>
        /// 欢迎使用CMS消息通知
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task WelcomeToCmsAsync(User user);

        /// <summary>
        /// 发送自定义消息通知
        /// </summary>
        /// <param name="user">用户身份</param>
        /// <param name="messager">消息内容</param>
        /// <param name="severity">消息严重等级</param>
        /// <returns></returns>
        Task SendMessageAsync(UserIdentifier user, string messager,
            NotificationSeverity severity = NotificationSeverity.Info);



    }
}