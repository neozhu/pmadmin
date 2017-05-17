using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
    public partial class CheckList:Entity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        [Display(Name = "主题", Description = "主题")]
        public string Title { get; set; }
        [Display(Name = "项目", Description = "项目")]
        public int WorkItemId { get; set; }
       
        [MaxLength(255)]
        [Display(Name = "描述", Description = "描述")]
        public string Desc { get; set; }
        [MaxLength(20)]
        [Display(Name = "创建人", Description = "创建人")]
        public string From { get; set; }
        [MaxLength(20)]
        [Display(Name = "指派给", Description = "指派给")]
        public string To { get; set; }
        [Display(Name = "状态", Description = "状态")]
        public int Status { get; set; }
        [Display(Name = "创建时间", Description = "创建时间")]
        public DateTime Created { get; set; }
        [Display(Name = "修改时间", Description = "修改时间")]
        public DateTime? LastUpdated { get; set; }

    }
}