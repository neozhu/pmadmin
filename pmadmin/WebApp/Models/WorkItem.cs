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

        public string Title { get; set; }
        public string Desc { get; set; }

        public int Priority { get; set; }
        public int Effort { get; set; }
        public int BusinessValue { get; set; }
        public int TimeCriticality { get; set; }
        public int Risk { get; set; }
        public int Status { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Owner { get; set; }
        public string Creator { get; set; }
        public string AssignTo { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual WorkItemType WorkItemType { get; set; }
        public int Visibility { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [Display(Name = "所在公司")]
        public virtual Company Company { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [Display(Name = "战略类别")]
        public virtual Category Category { get; set; }


       

        public DateTime Created { get; set; }
        public DateTime? LastUpdated {get;set;}


        public int? ParentId { get; set; }

    }
}