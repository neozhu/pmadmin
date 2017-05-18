﻿             
           
 
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
    public class WorkItemDependencyService : Service< WorkItemDependency >, IWorkItemDependencyService
    {

        private readonly IRepositoryAsync<WorkItemDependency> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  WorkItemDependencyService(IRepositoryAsync< WorkItemDependency> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                  
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                WorkItemDependency item = new WorkItemDependency();
				var mapping = _mappingservice.Queryable().Where(x => x.EntitySetName == "WorkItemDependency").ToList();

                foreach (var field in mapping)
                {
                 
						var defval = field.DefaultValue;
						var contation = datatable.Columns.Contains((field.SourceFieldName == null ? "" : field.SourceFieldName));
						if (contation && row[field.SourceFieldName] != DBNull.Value)
						{
							Type workitemdependencytype = item.GetType();
							PropertyInfo propertyInfo = workitemdependencytype.GetProperty(field.FieldName);
							propertyInfo.SetValue(item, Convert.ChangeType(row[field.SourceFieldName], propertyInfo.PropertyType), null);
						}
						else if (!string.IsNullOrEmpty(defval))
						{
							Type workitemdependencytype = item.GetType();
							PropertyInfo propertyInfo = workitemdependencytype.GetProperty(field.FieldName);
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
                                   var workitemdependency  = this.Query(new WorkItemDependencyQuery().Withfilter(filters)).OrderBy(n=>n.OrderBy(sort,order)).Select().ToList();
                        var datarows = workitemdependency .Select(  n => new {  Id = n.Id , WorkItemId = n.WorkItemId , DependentId = n.DependentId }).ToList();
           
            return ExcelHelper.ExportExcel(typeof(WorkItemDependency), datarows);

        }
    }
}


