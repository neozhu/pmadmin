using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
    public partial class Attachment:Entity
    {
        [Key]
        public int Id { get; set; }
        public int WorkItemId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }

        public string Type { get; set; }
        public string Creator { get; set; }
        public DateTime Created { get; set; }
    }
}