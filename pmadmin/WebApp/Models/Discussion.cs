using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Discussion
    {
        public int Id { get; set; }

        public int WorkItemId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public string Tags { get; set; }
        
        public int Like { get; set; }

        public string Creator { get; set; }
        public string AssignTo { get; set; }
        
        public DateTime Created { get; set; }
    }
}