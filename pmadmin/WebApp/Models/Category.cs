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
        public string Name { get; set; }
        [MaxLength(255)]
        public string Desc { get; set; }
        public int CompanyId { get; set;}

        [ForeignKey("CompanyId")]
   
        public virtual Company Company { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
    }
}