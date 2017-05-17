using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public partial class Company : Entity
    {
        public Company()
        {
            Departments = new HashSet<Department>();
            Employee = new HashSet<Employee>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "企业名称", Description = "企业名称")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(20)]
        [Display(Name = "企业标志", Description = "企业标志")]
        public string Logo { get; set; }
        [MaxLength(255)]
        [Display(Name = "域名", Description = "域名")]
        public string Domain { get; set; }
        [MaxLength(255)]
        [Display(Name = "地址", Description = "地址")]
        public string Address {get;set;}
        [MaxLength(50)]
        [Display(Name = "所在城市", Description = "所在城市")]
        public string City { get; set; }
        [MaxLength(50)]
        [Display(Name = "省", Description = "省")]
        public string Province { get; set; }
        [Display(Name = "注册日期", Description = "注册日期")]
        public DateTime RegisterDate { get; set; }
        //public int Employees { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}