                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using WebApp.Models;

namespace WebApp.Repositories
{
  public static class WorkItemRepository  
    {
 
                 public static IEnumerable<WorkItem> GetByTypeId(this IRepositoryAsync<WorkItem> repository, int typeid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.TypeId==typeid);
             return query;

         }
             
                 public static IEnumerable<WorkItem> GetByCompanyId(this IRepositoryAsync<WorkItem> repository, int companyid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.CompanyId==companyid);
             return query;

         }
             
                 public static IEnumerable<WorkItem> GetByCategoryId(this IRepositoryAsync<WorkItem> repository, int categoryid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.CategoryId==categoryid);
             return query;

         }
             
        
         
	}
}



