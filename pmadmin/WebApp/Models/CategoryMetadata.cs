using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApp.Models
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
    }

    public partial class CategoryMetadata
    {
        [Display(Name = "Company")]
        public Company Company { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Desc")]
        public string Desc { get; set; }

        [Display(Name = "CompanyId")]
        public int CompanyId { get; set; }

    }




	public class CategoryChangeViewModel
    {
        public IEnumerable<Category> inserted { get; set; }
        public IEnumerable<Category> deleted { get; set; }
        public IEnumerable<Category> updated { get; set; }
    }

}
