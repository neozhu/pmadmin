using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
    public partial class Favorites:Entity
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int WorkItemId { get; set; }
    }
}