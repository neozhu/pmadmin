using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApp.Models
{
    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department
    {
    }

    public partial class DepartmentMetadata
    {
        [Display(Name = "Company")]
        public Company Company { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Manager")]
        public string Manager { get; set; }

        [Display(Name = "CompanyId")]
        public int CompanyId { get; set; }

    }




	public class DepartmentChangeViewModel
    {
        public IEnumerable<Department> inserted { get; set; }
        public IEnumerable<Department> deleted { get; set; }
        public IEnumerable<Department> updated { get; set; }
    }

}
