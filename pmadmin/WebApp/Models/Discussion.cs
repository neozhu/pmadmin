using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Discussion
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int WorkItemId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public string Content { get; set; }
        [MaxLength(255)]
        public string Tags { get; set; }
        
        public int Like { get; set; }
        [MaxLength(20)]
        public string Creator { get; set; }
        [MaxLength(20)]
        public string AssignTo { get; set; }
        
        public DateTime Created { get; set; }
    }
}