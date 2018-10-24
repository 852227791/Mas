using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mas.Domain.Common.User
{
    public class Sys_User : BaseEntity
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        [MaxLength(30)]
        [Column(Order = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(30)]
        [Column(Order = 3)]
        public string UserName { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [MaxLength(30)]
        [Column(Order = 4)]
        public string LoginName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [MaxLength(18)]
        [Column(Order = 5)]
        public string IdCard { get; set;}

        /// <summary>
        /// 微信号
        /// </summary>
        [Column(Order = 6)]
        public string WeChatNum { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(16)]
        public string Mobile { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Column(Order = 7)]
        public string HeadPhoto { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(32)]
        [Column(Order = 8)]
        public string Password { get; set; }
       
    }
}
