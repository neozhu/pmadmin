using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApp.Models
{
     
    public partial class Department
    {
    }

     




	public class DepartmentChangeViewModel
    {
        public IEnumerable<Department> inserted { get; set; }
        public IEnumerable<Department> deleted { get; set; }
        public IEnumerable<Department> updated { get; set; }
    }

}
