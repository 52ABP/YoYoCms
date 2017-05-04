using System.Threading.Tasks;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.Notifications
{
    /// <summary>
    /// 通知管理领域服务
    /// </summary>
    public interface INotificationManager
    {
        Task WelcomeToCmsAsync(User user);
    }
}