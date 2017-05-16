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

        public string Name { get; set; }
    }
}