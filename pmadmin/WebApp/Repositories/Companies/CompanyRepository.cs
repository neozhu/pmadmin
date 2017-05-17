                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using WebApp.Models;

namespace WebApp.Repositories
{
  public static class CompanyRepository  
    {
 
        
                public static IEnumerable<Department>   GetDepartmentsByCompanyId (this IRepositoryAsync<Company> repository,int companyid)
        {
			var departmentRepository = repository.GetRepository<Department>(); 
            return departmentRepository.Queryable().Include(x => x.Company).Where(n => n.CompanyId == companyid);
        }
                public static IEnumerable<Employee>   GetEmployeeByCompanyId (this IRepositoryAsync<Company> repository,int companyid)
        {
			var employeeRepository = repository.GetRepository<Employee>(); 
            return employeeRepository.Queryable().Include(x => x.Company).Where(n => n.CompanyId == companyid);
        }
         
	}
}



