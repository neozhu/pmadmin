             
           
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Service.Pattern;
using WebApp.Models;
using WebApp.Repositories;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using WebApp.Extensions;
using System.IO;

namespace WebApp.Services
{
    public class WorkItemService : Service< WorkItem >, IWorkItemService
    {

        private readonly IRepositoryAsync<WorkItem> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  WorkItemService(IRepositoryAsync< WorkItem> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                 public  IEnumerable<WorkItem> GetByTypeId(int  typeid)
         {
            return _repository.GetByTypeId(typeid);
         }
                  public  IEnumerable<WorkItem> GetByCompanyId(int  companyid)
         {
            return _repository.GetByCompanyId(companyid);
         }
                  public  IEnumerable<WorkItem> GetByCategoryId(int  categoryid)
         {
            return _repository.GetByCategoryId(categoryid);
         }
                   
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                WorkItem item = new WorkItem();
				var mapping = _mappingservice.Queryable().Where(x => x.EntitySetName == "WorkItem").ToList();

                foreach (var field in mapping)
                {
                 
						var defval = field.DefaultValue;
						var contation = datatable.Columns.Contains((field.SourceFieldName == null ? "" : field.SourceFieldName));
						if (contation && row[field.SourceFieldName] != DBNull.Value)
						{
							Type workitemtype = item.GetType();
							PropertyInfo propertyInfo = workitemtype.GetProperty(field.FieldName);
							propertyInfo.SetValue(item, Convert.ChangeType(row[field.SourceFieldName], propertyInfo.PropertyType), null);
						}
						else if (!string.IsNullOrEmpty(defval))
						{
							Type workitemtype = item.GetType();
							PropertyInfo propertyInfo = workitemtype.GetProperty(field.FieldName);
							if (defval.ToLower() == "now" && propertyInfo.PropertyType ==typeof(DateTime))
                            {
                                propertyInfo.SetValue(item, Convert.ChangeType(DateTime.Now, propertyInfo.PropertyType), null);
                            }
                            else
                            {
                                propertyInfo.SetValue(item, Convert.ChangeType(defval, propertyInfo.PropertyType), null);
                            }
						}
                }
                
                this.Insert(item);
               

            }
        }
		
		public Stream ExportExcel(string filterRules = "",string sort = "Id", string order = "asc")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
                       			 
            var workitems  = this.Query(new WorkItemQuery().Withfilter(filters)).Include(p => p.Category).Include(p => p.Company).Include(p => p.WorkItemType).OrderBy(n=>n.OrderBy(sort,order)).Select().ToList();
            
                        var datarows = workitems .Select(  n => new { CategoryName = (n.Category==null?"": n.Category.Name) ,CompanyName = (n.Company==null?"": n.Company.Name) ,WorkItemTypeName = (n.WorkItemType==null?"": n.WorkItemType.Name) , Id = n.Id , Title = n.Title , Desc = n.Desc , Tags = n.Tags , Notes = n.Notes , Priority = n.Priority , Effort = n.Effort , BusinessValue = n.BusinessValue , TimeCriticality = n.TimeCriticality , Risk = n.Risk , Status = n.Status , FromDate = n.FromDate , EndDate = n.EndDate , ClosedDate = n.ClosedDate , Owner = n.Owner , Creator = n.Creator , AssignTo = n.AssignTo , TypeId = n.TypeId , Visibility = n.Visibility , CompanyId = n.CompanyId , CategoryId = n.CategoryId , Created = n.Created , LastUpdated = n.LastUpdated , ParentId = n.ParentId }).ToList();
           
            return ExcelHelper.ExportExcel(typeof(WorkItem), datarows);

        }
    }
}



