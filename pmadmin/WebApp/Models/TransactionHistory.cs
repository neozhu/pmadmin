using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
    public partial class TransactionHistory:Entity
    {
        [Key]
        public int Id { get; set; }
   
        [Required]
        public int WorkItemId { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name ="状态")]
        public string Status { get; set; }
        [MaxLength(255)]
        [Display(Name = "描述")]
        public string Desc { get; set; }
        [MaxLength(255)]
        [Display(Name = "备注")]
        public string Notes { get; set; }
        [Display(Name = "操作时间")]
        public DateTime TransactionDateTime { get; set; }
        [MaxLength(20)]
        [Display(Name = "操作人")]
        public string Operator { get; set; }
        [MaxLength(50)]
        [Display(Name = "关键字")]
        public string PublicKey { get; set; }
        [MaxLength(30)]
        [Display(Name = "事件")]
        public string Event { get; set; }
        [MaxLength(30)]
        [Display(Name = "表名")]
        public string TableName { get; set; }
        [Display(Name = "标记")]
        public int Flag { get; set; }
        [MaxLength(20)]
        public string Creator { get; set; }
        public DateTime Created { get; set; }

    }
}