                    
      
     
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
   public class WorkItemQuery:QueryObject<WorkItem>
    {
        public WorkItemQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Id.ToString().Contains(search) || x.Title.Contains(search) || x.Desc.Contains(search) || x.Tags.Contains(search) || x.Notes.Contains(search) || x.Priority.ToString().Contains(search) || x.Effort.ToString().Contains(search) || x.BusinessValue.ToString().Contains(search) || x.TimeCriticality.ToString().Contains(search) || x.Risk.ToString().Contains(search) || x.Status.ToString().Contains(search) || x.FromDate.ToString().Contains(search) || x.EndDate.ToString().Contains(search) || x.ClosedDate.ToString().Contains(search) || x.Owner.Contains(search) || x.Creator.Contains(search) || x.AssignTo.Contains(search) || x.TypeId.ToString().Contains(search) || x.Visibility.ToString().Contains(search) || x.CompanyId.ToString().Contains(search) || x.CategoryId.ToString().Contains(search) || x.Created.ToString().Contains(search) || x.LastUpdated.ToString().Contains(search) || x.ParentId.ToString().Contains(search) );
            return this;
        }

		public WorkItemQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Id.ToString().Contains(search) || x.Title.Contains(search) || x.Desc.Contains(search) || x.Tags.Contains(search) || x.Notes.Contains(search) || x.Priority.ToString().Contains(search) || x.Effort.ToString().Contains(search) || x.BusinessValue.ToString().Contains(search) || x.TimeCriticality.ToString().Contains(search) || x.Risk.ToString().Contains(search) || x.Status.ToString().Contains(search) || x.FromDate.ToString().Contains(search) || x.EndDate.ToString().Contains(search) || x.ClosedDate.ToString().Contains(search) || x.Owner.Contains(search) || x.Creator.Contains(search) || x.AssignTo.Contains(search) || x.TypeId.ToString().Contains(search) || x.Visibility.ToString().Contains(search) || x.CompanyId.ToString().Contains(search) || x.CategoryId.ToString().Contains(search) || x.Created.ToString().Contains(search) || x.LastUpdated.ToString().Contains(search) || x.ParentId.ToString().Contains(search) );
            return this;
        }

		public WorkItemQuery Withfilter(IEnumerable<filterRule> filters)
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
				    
					
					
				    				
											if (rule.field == "Title"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Title.Contains(rule.value));
						}
				    
				    
					
					
				    				
											if (rule.field == "Desc"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Desc.Contains(rule.value));
						}
				    
				    
					
					
				    				
											if (rule.field == "Tags"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Tags.Contains(rule.value));
						}
				    
				    
					
					
				    				
											if (rule.field == "Notes"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Notes.Contains(rule.value));
						}
				    
				    
					
					
				    				
					
				    						if (rule.field == "Priority" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Priority == val);
						}
				    
					
					
				    				
					
				    						if (rule.field == "Effort" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Effort == val);
						}
				    
					
					
				    				
					
				    						if (rule.field == "BusinessValue" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.BusinessValue == val);
						}
				    
					
					
				    				
					
				    						if (rule.field == "TimeCriticality" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.TimeCriticality == val);
						}
				    
					
					
				    				
					
				    						if (rule.field == "Risk" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Risk == val);
						}
				    
					
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				    
					
					
				    				
					
				    
					
											if (rule.field == "FromDate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDateTime())
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.FromDate)>=0);
						}
				   
				    				
					
				    
					
											if (rule.field == "EndDate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDateTime())
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.EndDate)>=0);
						}
				   
				    				
					
				    
					
											if (rule.field == "ClosedDate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDateTime())
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ClosedDate)>=0);
						}
				   
				    				
											if (rule.field == "Owner"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Owner.Contains(rule.value));
						}
				    
				    
					
					
				    				
											if (rule.field == "Creator"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Creator.Contains(rule.value));
						}
				    
				    
					
					
				    				
											if (rule.field == "AssignTo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.AssignTo.Contains(rule.value));
						}
				    
				    
					
					
				    				
					
				    						if (rule.field == "TypeId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.TypeId == val);
						}
				    
					
					
				    				
					
				    						if (rule.field == "Visibility" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Visibility == val);
						}
				    
					
					
				    				
					
				    						if (rule.field == "CompanyId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.CompanyId == val);
						}
				    
					
					
				    				
					
				    						if (rule.field == "CategoryId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.CategoryId == val);
						}
				    
					
					
				    				
					
				    
					
											if (rule.field == "Created" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDateTime())
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.Created)>=0);
						}
				   
				    				
					
				    
					
											if (rule.field == "LastUpdated" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDateTime())
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.LastUpdated)>=0);
						}
				   
				    				
					
				    						if (rule.field == "ParentId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ParentId == val);
						}
				    
					
					
				    									
                   
               }
           }
            return this;
        }
    }
}



