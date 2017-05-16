using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
    public partial class Favorites:Entity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string User { get; set; }
        public int WorkItemId { get; set; }
    }
}