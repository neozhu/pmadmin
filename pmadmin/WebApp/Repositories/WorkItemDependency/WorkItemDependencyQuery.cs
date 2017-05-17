                    
      
     
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.SqlServer;
using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using System.Web.WebPages;
using WebApp.Models;
using WebApp.Extensions;

namespace WebApp.Repositories
{
   public class WorkItemDependencyQuery:QueryObject<WorkItemDependency>
    {
        public WorkItemDependencyQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Id.ToString().Contains(search) || x.WorkItemId.ToString().Contains(search) || x.DependentId.ToString().Contains(search) );
            return this;
        }

		public WorkItemDependencyQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Id.ToString().Contains(search) || x.WorkItemId.ToString().Contains(search) || x.DependentId.ToString().Contains(search) );
            return this;
        }

		public WorkItemDependencyQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
                  
					
				    						if (rule.field == "Id" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Id == val);
						}
				    
					
					
				    				
					
				    						if (rule.field == "WorkItemId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.WorkItemId == val);
						}
				    
					
					
				    				
					
				    						if (rule.field == "DependentId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.DependentId == val);
						}
				    
					
					
				    									
                   
               }
           }
            return this;
        }
    }
}



