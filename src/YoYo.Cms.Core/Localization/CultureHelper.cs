using System.Threading;

namespace YoYo.Cms.Localization
{
    public class CultureHelper
    {
        public static bool IsRtl => Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
    }
}