using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApp.Models
{
     
    public partial class WorkItemDependency
    {
    }

     




	public class WorkItemDependencyChangeViewModel
    {
        public IEnumerable<WorkItemDependency> inserted { get; set; }
        public IEnumerable<WorkItemDependency> deleted { get; set; }
        public IEnumerable<WorkItemDependency> updated { get; set; }
    }

}
