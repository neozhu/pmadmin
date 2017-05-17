using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using WebApp.Models;
using WebApp.Services;
using WebApp.Repositories;
using WebApp.Extensions;


namespace WebApp.Controllers
{
	public class WorkItemsController : Controller
	{
		
		//Please RegisterType UnityConfig.cs
		//container.RegisterType<IRepositoryAsync<WorkItem>, Repository<WorkItem>>();
		//container.RegisterType<IWorkItemService, WorkItemService>();
		
		//private StoreContext db = new StoreContext();
		private readonly IWorkItemService  _workItemService;
		private readonly IUnitOfWorkAsync _unitOfWork;

		public WorkItemsController (IWorkItemService  workItemService, IUnitOfWorkAsync unitOfWork)
		{
			_workItemService  = workItemService;
			_unitOfWork = unitOfWork;
		}

		// GET: WorkItems/Index
		public async Task<ActionResult> Index()
		{
			
			var workitems  = _workItemService.Queryable().Include(w => w.Category).Include(w => w.Company).Include(w => w.WorkItemType);
			
		  
			return View(await workitems.ToListAsync());
			
		}

		// Get :WorkItems/PageList
		// For Index View Boostrap-Table load  data 
		[HttpGet]
				 public async Task<ActionResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
				{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var totalCount = 0;
			//int pagenum = offset / limit +1;
											var workitems  = await _workItemService
						.Query(new WorkItemQuery().Withfilter(filters)).Include(w => w.Category).Include(w => w.Company).Include(w => w.WorkItemType)
							.OrderBy(n=>n.OrderBy(sort,order))
							.SelectPageAsync(page, rows, out totalCount);
							 
			
				
						var datarows = workitems .Select(  n => new { CategoryName = (n.Category==null?"": n.Category.Name) ,CompanyName = (n.Company==null?"": n.Company.Name) ,WorkItemTypeName = (n.WorkItemType==null?"": n.WorkItemType.Name) , Id = n.Id , Title = n.Title , Desc = n.Desc , Tags = n.Tags , Notes = n.Notes , Priority = n.Priority , Effort = n.Effort , BusinessValue = n.BusinessValue , TimeCriticality = n.TimeCriticality , Risk = n.Risk , Status = n.Status , FromDate = n.FromDate , EndDate = n.EndDate , ClosedDate = n.ClosedDate , Owner = n.Owner , Creator = n.Creator , AssignTo = n.AssignTo , TypeId = n.TypeId , Visibility = n.Visibility , CompanyId = n.CompanyId , CategoryId = n.CategoryId , Created = n.Created , LastUpdated = n.LastUpdated , ParentId = n.ParentId }).ToList();
			var pagelist = new { total = totalCount, rows = datarows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
				public async Task<ActionResult> SaveData(WorkItemChangeViewModel workitems)
		{
			if (workitems.updated != null)
			{
				foreach (var item in workitems.updated)
				{
					_workItemService.Update(item);
				}
			}
			if (workitems.deleted != null)
			{
				foreach (var item in workitems.deleted)
				{
					_workItemService.Delete(item);
				}
			}
			if (workitems.inserted != null)
			{
				foreach (var item in workitems.inserted)
				{
					_workItemService.Insert(item);
				}
			}
			await _unitOfWork.SaveChangesAsync();

			return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
		}
		
						public async Task<ActionResult> GetCategories()
		{
			var categoryRepository = _unitOfWork.RepositoryAsync<Category>();
			var data = await categoryRepository.Queryable().ToListAsync();
			var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
								public async Task<ActionResult> GetCompanies()
		{
			var companyRepository = _unitOfWork.RepositoryAsync<Company>();
			var data = await companyRepository.Queryable().ToListAsync();
			var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
								public async Task<ActionResult> GetWorkItemTypes()
		{
			var workitemtypeRepository = _unitOfWork.RepositoryAsync<WorkItemType>();
			var data = await workitemtypeRepository.Queryable().ToListAsync();
			var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
				
		
	   
		// GET: WorkItems/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var  workItem = await _workItemService.FindAsync(id);

			if (workItem == null)
			{
				return HttpNotFound();
			}
			return View(workItem);
		}
		

		// GET: WorkItems/Create
				public ActionResult Create()
				{
			var workItem = new WorkItem();
			//set default value
			var categoryRepository = _unitOfWork.RepositoryAsync<Category>();
		   			ViewBag.CategoryId = new SelectList(categoryRepository.Queryable(), "Id", "Name");
		   			var companyRepository = _unitOfWork.RepositoryAsync<Company>();
		   			ViewBag.CompanyId = new SelectList(companyRepository.Queryable(), "Id", "Name");
		   			var workitemtypeRepository = _unitOfWork.RepositoryAsync<WorkItemType>();
		   			ViewBag.TypeId = new SelectList(workitemtypeRepository.Queryable(), "Id", "Name");
		   			return View(workItem);
		}

		// POST: WorkItems/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Category,Company,WorkItemType,Id,Title,Desc,Tags,Notes,Priority,Effort,BusinessValue,TimeCriticality,Risk,Status,FromDate,EndDate,ClosedDate,Owner,Creator,AssignTo,TypeId,Visibility,CompanyId,CategoryId,Created,LastUpdated,ParentId")] WorkItem workItem)
		{
			if (ModelState.IsValid)
			{
			 				_workItemService.Insert(workItem);
		   				await _unitOfWork.SaveChangesAsync();
				if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				DisplaySuccessMessage("Has append a WorkItem record");
				return RedirectToAction("Index");
			}
			else {
			 var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			 if (Request.IsAjaxRequest())
			 {
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			 }
			 DisplayErrorMessage(modelStateErrors);
			}
						var categoryRepository = _unitOfWork.RepositoryAsync<Category>();
						ViewBag.CategoryId = new SelectList(await categoryRepository.Queryable().ToListAsync(), "Id", "Name", workItem.CategoryId);
									var companyRepository = _unitOfWork.RepositoryAsync<Company>();
						ViewBag.CompanyId = new SelectList(await companyRepository.Queryable().ToListAsync(), "Id", "Name", workItem.CompanyId);
									var workitemtypeRepository = _unitOfWork.RepositoryAsync<WorkItemType>();
						ViewBag.TypeId = new SelectList(await workitemtypeRepository.Queryable().ToListAsync(), "Id", "Name", workItem.TypeId);
						
			return View(workItem);
		}

		// GET: WorkItems/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var workItem = await _workItemService.FindAsync(id);
			if (workItem == null)
			{
				return HttpNotFound();
			}
			var categoryRepository = _unitOfWork.RepositoryAsync<Category>();
			ViewBag.CategoryId = new SelectList(categoryRepository.Queryable(), "Id", "Name", workItem.CategoryId);
			var companyRepository = _unitOfWork.RepositoryAsync<Company>();
			ViewBag.CompanyId = new SelectList(companyRepository.Queryable(), "Id", "Name", workItem.CompanyId);
			var workitemtypeRepository = _unitOfWork.RepositoryAsync<WorkItemType>();
			ViewBag.TypeId = new SelectList(workitemtypeRepository.Queryable(), "Id", "Name", workItem.TypeId);
			return View(workItem);
		}

		// POST: WorkItems/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Category,Company,WorkItemType,Id,Title,Desc,Tags,Notes,Priority,Effort,BusinessValue,TimeCriticality,Risk,Status,FromDate,EndDate,ClosedDate,Owner,Creator,AssignTo,TypeId,Visibility,CompanyId,CategoryId,Created,LastUpdated,ParentId")] WorkItem workItem)
		{
			if (ModelState.IsValid)
			{
				workItem.ObjectState = ObjectState.Modified;
								_workItemService.Update(workItem);
								
				await   _unitOfWork.SaveChangesAsync();
				if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				DisplaySuccessMessage("Has update a WorkItem record");
				return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			if (Request.IsAjaxRequest())
			{
				return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			}
			DisplayErrorMessage(modelStateErrors);
			}
						var categoryRepository = _unitOfWork.RepositoryAsync<Category>();
						ViewBag.CategoryId = new SelectList( await categoryRepository.Queryable().ToListAsync(), "Id", "Name", workItem.CategoryId);
									var companyRepository = _unitOfWork.RepositoryAsync<Company>();
						ViewBag.CompanyId = new SelectList( await companyRepository.Queryable().ToListAsync(), "Id", "Name", workItem.CompanyId);
									var workitemtypeRepository = _unitOfWork.RepositoryAsync<WorkItemType>();
						ViewBag.TypeId = new SelectList( await workitemtypeRepository.Queryable().ToListAsync(), "Id", "Name", workItem.TypeId);
						
			return View(workItem);
		}

		// GET: WorkItems/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var workItem = await _workItemService.FindAsync(id);
			if (workItem == null)
			{
				return HttpNotFound();
			}
			return View(workItem);
		}

		// POST: WorkItems/Delete/5
		[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			var workItem = await  _workItemService.FindAsync(id);
			 _workItemService.Delete(workItem);
			await _unitOfWork.SaveChangesAsync();
		   if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
			DisplaySuccessMessage("Has delete a WorkItem record");
			return RedirectToAction("Index");
		}


       

 

		//导出Excel
		[HttpPost]
		public ActionResult ExportExcel( string filterRules = "",string sort = "Id", string order = "asc")
		{
			var fileName = "workitems_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream=  _workItemService.ExportExcel(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
	  
		}
		


		private void DisplaySuccessMessage(string msgText)
		{
			TempData["SuccessMessage"] = msgText;
		}

		private void DisplayErrorMessage(string msgText)
		{
			TempData["ErrorMessage"] = msgText;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_unitOfWork.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
