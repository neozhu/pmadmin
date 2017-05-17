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
	public class WorkItemTypesController : Controller
	{
		
		//Please RegisterType UnityConfig.cs
		//container.RegisterType<IRepositoryAsync<WorkItemType>, Repository<WorkItemType>>();
		//container.RegisterType<IWorkItemTypeService, WorkItemTypeService>();
		
		//private StoreContext db = new StoreContext();
		private readonly IWorkItemTypeService  _workItemTypeService;
		private readonly IUnitOfWorkAsync _unitOfWork;

		public WorkItemTypesController (IWorkItemTypeService  workItemTypeService, IUnitOfWorkAsync unitOfWork)
		{
			_workItemTypeService  = workItemTypeService;
			_unitOfWork = unitOfWork;
		}

		// GET: WorkItemTypes/Index
		public async Task<ActionResult> Index()
		{
			
			var workitemtypes  = _workItemTypeService.Queryable();
			return View(await workitemtypes.ToListAsync()  );
			
		}

		// Get :WorkItemTypes/PageList
		// For Index View Boostrap-Table load  data 
		[HttpGet]
				 public async Task<ActionResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
				{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var totalCount = 0;
			//int pagenum = offset / limit +1;
											var workitemtypes  = await  _workItemTypeService
						.Query(new WorkItemTypeQuery().Withfilter(filters))
							.OrderBy(n=>n.OrderBy(sort,order))
							.SelectPageAsync(page, rows, out totalCount);
							 
				
						var datarows = workitemtypes .Select(  n => new {  Id = n.Id , Name = n.Name }).ToList();
			var pagelist = new { total = totalCount, rows = datarows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
				public async Task<ActionResult> SaveData(WorkItemTypeChangeViewModel workitemtypes)
		{
			if (workitemtypes.updated != null)
			{
				foreach (var item in workitemtypes.updated)
				{
					_workItemTypeService.Update(item);
				}
			}
			if (workitemtypes.deleted != null)
			{
				foreach (var item in workitemtypes.deleted)
				{
					_workItemTypeService.Delete(item);
				}
			}
			if (workitemtypes.inserted != null)
			{
				foreach (var item in workitemtypes.inserted)
				{
					_workItemTypeService.Insert(item);
				}
			}
			await _unitOfWork.SaveChangesAsync();

			return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
		}
		
		
		
	   
		// GET: WorkItemTypes/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var  workItemType = await _workItemTypeService.FindAsync(id);

			if (workItemType == null)
			{
				return HttpNotFound();
			}
			return View(workItemType);
		}
		

		// GET: WorkItemTypes/Create
				public ActionResult Create()
				{
			var workItemType = new WorkItemType();
			//set default value
			return View(workItemType);
		}

		// POST: WorkItemTypes/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,Name")] WorkItemType workItemType)
		{
			if (ModelState.IsValid)
			{
			 				_workItemTypeService.Insert(workItemType);
		   				await _unitOfWork.SaveChangesAsync();
				if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				DisplaySuccessMessage("Has append a WorkItemType record");
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
			
			return View(workItemType);
		}

		// GET: WorkItemTypes/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var workItemType = await _workItemTypeService.FindAsync(id);
			if (workItemType == null)
			{
				return HttpNotFound();
			}
			return View(workItemType);
		}

		// POST: WorkItemTypes/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] WorkItemType workItemType)
		{
			if (ModelState.IsValid)
			{
				workItemType.ObjectState = ObjectState.Modified;
								_workItemTypeService.Update(workItemType);
								
				await   _unitOfWork.SaveChangesAsync();
				if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				DisplaySuccessMessage("Has update a WorkItemType record");
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
			
			return View(workItemType);
		}

		// GET: WorkItemTypes/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var workItemType = await _workItemTypeService.FindAsync(id);
			if (workItemType == null)
			{
				return HttpNotFound();
			}
			return View(workItemType);
		}

		// POST: WorkItemTypes/Delete/5
		[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			var workItemType = await  _workItemTypeService.FindAsync(id);
			 _workItemTypeService.Delete(workItemType);
			await _unitOfWork.SaveChangesAsync();
		   if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
			DisplaySuccessMessage("Has delete a WorkItemType record");
			return RedirectToAction("Index");
		}


       

 

		//导出Excel
		[HttpPost]
		public ActionResult ExportExcel( string filterRules = "",string sort = "Id", string order = "asc")
		{
			var fileName = "workitemtypes_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream=  _workItemTypeService.ExportExcel(filterRules,sort, order );
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
