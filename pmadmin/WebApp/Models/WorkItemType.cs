using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
    public partial class WorkItemType:Entity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "工作项类型",Description ="战略,项目，任务")]
        public string Name { get; set; }
    }
}