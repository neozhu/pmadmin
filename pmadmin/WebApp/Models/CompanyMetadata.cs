using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApp.Models
{
    
    public partial class Company
    {
    }

     




	public class CompanyChangeViewModel
    {
        public IEnumerable<Company> inserted { get; set; }
        public IEnumerable<Company> deleted { get; set; }
        public IEnumerable<Company> updated { get; set; }
    }

}
