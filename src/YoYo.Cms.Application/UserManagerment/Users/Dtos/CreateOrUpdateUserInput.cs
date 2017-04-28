// 项目展示地址:"http://www.ddxc.org/"
// 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-04-24T21:56:55. All Rights Reserved.
//<生成时间>2017-04-24T21:56:55</生成时间>

using System.ComponentModel.DataAnnotations;

namespace YoYo.Cms.UserManagerment.Users.Dtos
{
    /// <summary>
    /// 用户信息新增和编辑时用Dto
    /// </summary>

    public class CreateOrUpdateUserInput
    {
        /// <summary>
        /// 用户信息编辑Dto
        /// </summary>
        public UserEditDto UserEditDto { get; set; }

        [Required]
        public string[] AssignedRoleNames { get; set; }
    }
}
