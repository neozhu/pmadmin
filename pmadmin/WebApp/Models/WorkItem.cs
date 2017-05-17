using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
    public partial class WorkItem : Entity
    {
        public WorkItem()
        {

        }

        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        [Display(Name = "主题", Description = "主题")]
        public string Title { get; set; }
        [Display(Name = "描述", Description = "描述")]
        public string Desc { get; set; }
        [MaxLength(255)]
        [Display(Name = "标记", Description = "标记")]
        public string Tags { get; set; }
        [MaxLength(255)]
        [Display(Name = "备注", Description = "备注")]
        public string Notes { get; set;}
        [Display(Name = "优先级", Description = "优先级")]
        public int Priority { get; set; }
        [Display(Name = "工作量", Description = "估计实现需要付出的努力")]
        public int Effort { get; set; }
        [Display(Name = "业务价值", Description = "业务价值")]
        public int BusinessValue { get; set; }
        [Display(Name = "时间紧迫性", Description = "时间紧迫性")]
        public int TimeCriticality { get; set; }
        [Display(Name = "风险", Description = "风险")]
        public int Risk { get; set; }
        [Display(Name = "状态", Description = "状态")]
        public int Status { get; set; }
        [Display(Name = "开始时间", Description = "开始时间")]
        public DateTime FromDate { get; set; }
        [Display(Name = "结束时间", Description = "结束时间")]
        public DateTime EndDate { get; set; }
        [Display(Name = "结案时间", Description = "结案时间")]
        public DateTime? ClosedDate { get; set; }
        [MaxLength(20)]
        [Display(Name = "责任人", Description = "责任人")]
        public string Owner { get; set; }
        [MaxLength(20)]
        [Display(Name = "创建人", Description = "创建人")]
        public string Creator { get; set; }
        [MaxLength(20)]
        [Display(Name = "指派给", Description = "指派给")]
        public string AssignTo { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual WorkItemType WorkItemType { get; set; }
        [Display(Name = "是否可见", Description = "是否可见")]
        public int Visibility { get; set; }
        [Display(Name = "企业", Description = "企业")]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [Display(Name = "所在公司")]
        public virtual Company Company { get; set; }
        [Display(Name = "战略类别")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [Display(Name = "战略类别")]
        public virtual Category Category { get; set; }



        [Display(Name = "创建时间")]
        public DateTime Created { get; set; }
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdated {get;set;}


        public int? ParentId { get; set; }

    }
}