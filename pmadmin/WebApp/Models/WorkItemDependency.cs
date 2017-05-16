using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class WorkItemDependency
    {
        [Key]
        public int Id { get; set;}
        [Index("Idx_Dependency", IsClustered =false, IsUnique =true)]
        public int WorkItemId { get; set; }
        [Index("Idx_Dependency", IsClustered = false, IsUnique = true)]
        public int DependentId { get; set; }
    }
}