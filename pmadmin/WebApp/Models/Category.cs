using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public partial class Category:Entity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name ="战略类型",Description = "包括营销战略、发展战略、品牌战略、融资战略、技术开发战略、人才开发战略、资源开发战略")]
        public string Name { get; set; }
        [MaxLength(255)]
        [Display(Name = "战略描述", Description = "包括营销战略、发展战略、品牌战略、融资战略、技术开发战略、人才开发战略、资源开发战略")]
        public string Desc { get; set; }
        [Display(Name = "企业", Description = "企业战略")]
        public int CompanyId { get; set;}

        [ForeignKey("CompanyId")]
        [Display(Name = "企业", Description = "企业战略")]
        public virtual Company Company { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
    }
}