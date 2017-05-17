     
 
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
using System.IO;

namespace WebApp.Services
{
    public interface IWorkItemService:IService<WorkItem>
    {

                  IEnumerable<WorkItem> GetByTypeId(int  typeid);
                 IEnumerable<WorkItem> GetByCompanyId(int  companyid);
                 IEnumerable<WorkItem> GetByCategoryId(int  categoryid);
        
         
 
		void ImportDataTable(DataTable datatable);
		Stream ExportExcel( string filterRules = "",string sort = "Id", string order = "asc");
	}
}