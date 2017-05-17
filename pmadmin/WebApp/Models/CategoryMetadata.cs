using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApp.Models
{
    //[MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
    }

    public partial class CategoryMetadata
    {
         

    }




	public class CategoryChangeViewModel
    {
        public IEnumerable<Category> inserted { get; set; }
        public IEnumerable<Category> deleted { get; set; }
        public IEnumerable<Category> updated { get; set; }
    }

}
