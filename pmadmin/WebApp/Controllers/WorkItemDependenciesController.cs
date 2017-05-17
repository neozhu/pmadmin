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
	public class WorkItemDependenciesController : Controller
	{
		
		//Please RegisterType UnityConfig.cs
		//container.RegisterType<IRepositoryAsync<WorkItemDependency>, Repository<WorkItemDependency>>();
		//container.RegisterType<IWorkItemDependencyService, WorkItemDependencyService>();
		
		//private StoreContext db = new StoreContext();
		private readonly IWorkItemDependencyService  _workItemDependencyService;
		private readonly IUnitOfWorkAsync _unitOfWork;

		public WorkItemDependenciesController (IWorkItemDependencyService  workItemDependencyService, IUnitOfWorkAsync unitOfWork)
		{
			_workItemDependencyService  = workItemDependencyService;
			_unitOfWork = unitOfWork;
		}

		// GET: WorkItemDependencies/Index
		public async Task<ActionResult> Index()
		{
			
			var workitemdependency  = _workItemDependencyService.Queryable();
			return View(await workitemdependency.ToListAsync()  );
			
		}

		// Get :WorkItemDependencies/PageList
		// For Index View Boostrap-Table load  data 
		[HttpGet]
				 public async Task<ActionResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
				{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var totalCount = 0;
			//int pagenum = offset / limit +1;
											var workitemdependency  = await  _workItemDependencyService
						.Query(new WorkItemDependencyQuery().Withfilter(filters))
							.OrderBy(n=>n.OrderBy(sort,order))
							.SelectPageAsync(page, rows, out totalCount);
							 
				
						var datarows = workitemdependency .Select(  n => new {  Id = n.Id , WorkItemId = n.WorkItemId , DependentId = n.DependentId }).ToList();
			var pagelist = new { total = totalCount, rows = datarows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
				public async Task<ActionResult> SaveData(WorkItemDependencyChangeViewModel workitemdependency)
		{
			if (workitemdependency.updated != null)
			{
				foreach (var item in workitemdependency.updated)
				{
					_workItemDependencyService.Update(item);
				}
			}
			if (workitemdependency.deleted != null)
			{
				foreach (var item in workitemdependency.deleted)
				{
					_workItemDependencyService.Delete(item);
				}
			}
			if (workitemdependency.inserted != null)
			{
				foreach (var item in workitemdependency.inserted)
				{
					_workItemDependencyService.Insert(item);
				}
			}
			await _unitOfWork.SaveChangesAsync();

			return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
		}
		
		
		
	   
		// GET: WorkItemDependencies/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var  workItemDependency = await _workItemDependencyService.FindAsync(id);

			if (workItemDependency == null)
			{
				return HttpNotFound();
			}
			return View(workItemDependency);
		}
		

		// GET: WorkItemDependencies/Create
				public ActionResult Create()
				{
			var workItemDependency = new WorkItemDependency();
			//set default value
			return View(workItemDependency);
		}

		// POST: WorkItemDependencies/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,WorkItemId,DependentId")] WorkItemDependency workItemDependency)
		{
			if (ModelState.IsValid)
			{
			 				_workItemDependencyService.Insert(workItemDependency);
		   				await _unitOfWork.SaveChangesAsync();
				if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				DisplaySuccessMessage("Has append a WorkItemDependency record");
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
			
			return View(workItemDependency);
		}

		// GET: WorkItemDependencies/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var workItemDependency = await _workItemDependencyService.FindAsync(id);
			if (workItemDependency == null)
			{
				return HttpNotFound();
			}
			return View(workItemDependency);
		}

		// POST: WorkItemDependencies/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,WorkItemId,DependentId")] WorkItemDependency workItemDependency)
		{
			if (ModelState.IsValid)
			{
				workItemDependency.ObjectState = ObjectState.Modified;
								_workItemDependencyService.Update(workItemDependency);
								
				await   _unitOfWork.SaveChangesAsync();
				if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				DisplaySuccessMessage("Has update a WorkItemDependency record");
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
			
			return View(workItemDependency);
		}

		// GET: WorkItemDependencies/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var workItemDependency = await _workItemDependencyService.FindAsync(id);
			if (workItemDependency == null)
			{
				return HttpNotFound();
			}
			return View(workItemDependency);
		}

		// POST: WorkItemDependencies/Delete/5
		[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			var workItemDependency = await  _workItemDependencyService.FindAsync(id);
			 _workItemDependencyService.Delete(workItemDependency);
			await _unitOfWork.SaveChangesAsync();
		   if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
			DisplaySuccessMessage("Has delete a WorkItemDependency record");
			return RedirectToAction("Index");
		}


       

 

		//导出Excel
		[HttpPost]
		public ActionResult ExportExcel( string filterRules = "",string sort = "Id", string order = "asc")
		{
			var fileName = "workitemdependency_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream=  _workItemDependencyService.ExportExcel(filterRules,sort, order );
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
