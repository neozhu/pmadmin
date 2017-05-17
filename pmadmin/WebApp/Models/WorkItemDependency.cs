using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
    public partial class WorkItemDependency:Entity
    {
        [Key]
        public int Id { get; set;}
        [Index("Idx_Dependency", IsClustered =false, IsUnique =true, Order =1)]
        public int WorkItemId { get; set; }
        [Index("Idx_Dependency", IsClustered = false, IsUnique = true, Order = 2)]
        public int DependentId { get; set; }
    }
}