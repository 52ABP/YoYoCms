namespace YoYo.Cms
{/// <summary>
/// Cms的常量信息
/// </summary>
    public class CmsConsts
    {
        /// <summary>
        /// 默认一页显示多少条数据
        /// </summary>
        public const int DefaultPageSize = 10;

        /// <summary>
        /// 一次性最多可以分多少页
        /// </summary>
        public const int MaxPageSize = 1000;

        /// <summary>
        /// 本地化多语言源
        /// </summary>
        public const string LocalizationSourceName = "Cms";
        /// <summary>
        /// 是否启用多租户
        /// </summary>
        public const bool MultiTenancyEnabled = false;





        public class SchemaName
        {
            public const string Basic = "Basic";

            public const string ABP = "ABP";

            public const string Bus = "Bus";

            public const string Galaxy = "Galaxy";


        }
        /// <summary>
        /// 通知系统的常量管理
        /// </summary>
        public static class NotificationConstNames
        {
            /// <summary>
            /// 欢迎语
            /// </summary>
            public const string WelcomeToCms = "App.WelcomeToCms";
            /// <summary>
            /// 发送消息信息
            /// </summary>
            public const string SendMessageAsync = "App.SendMessageAsync";
        }




    }
}