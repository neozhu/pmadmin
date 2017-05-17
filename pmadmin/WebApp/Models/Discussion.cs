using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Discussion
    {
        public int Id { get; set; }
        [MaxLength(255)]
        [Display(Name = "主题", Description = "主题")]
        public string Title { get; set; }
        public int? ParentId { get; set; }
        [Display(Name = "项目", Description = "项目")]
        public int WorkItemId { get; set; }
       
        [Display(Name = "内容", Description = "内容")]
        public string Content { get; set; }
        [MaxLength(255)]
        [Display(Name = "标记", Description = "标记")]
        public string Tags { get; set; }
        [Display(Name = "点赞", Description = "点赞")]
        public int Like { get; set; }
        [MaxLength(20)]
        [Display(Name = "创建人", Description = "创建人")]
        public string Creator { get; set; }
        [MaxLength(20)]
        [Display(Name = "指派给", Description = "指派给")]
        public string AssignTo { get; set; }
        [Display(Name = "创建时间", Description = "创建时间")]
        public DateTime Created { get; set; }
    }
}