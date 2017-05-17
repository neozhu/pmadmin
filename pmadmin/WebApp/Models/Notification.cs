using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
    public partial class Notification:Entity
    {
        public int Id { get; set; }
        [MaxLength(255)]
        [Display(Name = "主题", Description = "主题")]
        public string Title { get; set; }
        public int Type { get; set; }
        [MaxLength(255)]
        [Display(Name = "内容", Description = "内容")]
        public string Message { get; set; }
        [MaxLength(255)]
        [Display(Name = "链接", Description = "链接")]
        public string Link { get; set; }
        [Display(Name = "状态", Description = "状态")]
        public int  Status { get; set; }
        [Display(Name = "给", Description = "给")]
        [MaxLength(20)]
        public string To { get; set; }
        [Display(Name = "创建人", Description = "创建人")]
        [MaxLength(20)]
        public string Creator { get; set; }
        [Display(Name = "创建时间", Description = "创建时间")]
       
        public DateTime Created { get; set; }
    }
}