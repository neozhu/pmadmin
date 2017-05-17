using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApp.Models
{
    
    public partial class WorkItemType
    {
    }

    




	public class WorkItemTypeChangeViewModel
    {
        public IEnumerable<WorkItemType> inserted { get; set; }
        public IEnumerable<WorkItemType> deleted { get; set; }
        public IEnumerable<WorkItemType> updated { get; set; }
    }

}
