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

        public int WorkItemId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Desc { get; set; }
        [MaxLength(20)]
        public string From { get; set; }
        [MaxLength(20)]
        public string To { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }

    }
}