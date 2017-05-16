using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public partial class Company : Entity
    {
        public Company()
        {
            Departments = new HashSet<Department>();
            Employee = new HashSet<Employee>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Logo { get; set; }
        [MaxLength(255)]
        public string Domain { get; set; }
        [MaxLength(255)]
        public string Address {get;set;}
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Province { get; set; }
        public DateTime RegisterDate { get; set; }
        //public int Employees { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}