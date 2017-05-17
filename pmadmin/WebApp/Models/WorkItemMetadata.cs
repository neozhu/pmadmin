using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApp.Models
{
   // [MetadataType(typeof(WorkItemMetadata))]
    public partial class WorkItem
    {
    }

    




	public class WorkItemChangeViewModel
    {
        public IEnumerable<WorkItem> inserted { get; set; }
        public IEnumerable<WorkItem> deleted { get; set; }
        public IEnumerable<WorkItem> updated { get; set; }
    }

}
