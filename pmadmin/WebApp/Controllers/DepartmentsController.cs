﻿using System;
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
	public class DepartmentsController : Controller
	{
		
		//Please RegisterType UnityConfig.cs
		//container.RegisterType<IRepositoryAsync<Department>, Repository<Department>>();
		//container.RegisterType<IDepartmentService, DepartmentService>();
		
		//private StoreContext db = new StoreContext();
		private readonly IDepartmentService  _departmentService;
		private readonly IUnitOfWorkAsync _unitOfWork;

		public DepartmentsController (IDepartmentService  departmentService, IUnitOfWorkAsync unitOfWork)
		{
			_departmentService  = departmentService;
			_unitOfWork = unitOfWork;
		}

		// GET: Departments/Index
		public async Task<ActionResult> Index()
		{
			
			var departments  = _departmentService.Queryable().Include(d => d.Company);
			
		  
			return View(await departments.ToListAsync());
			
		}

		// Get :Departments/PageList
		// For Index View Boostrap-Table load  data 
		[HttpGet]
				 public async Task<ActionResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
				{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var totalCount = 0;
			//int pagenum = offset / limit +1;
											var departments  = await _departmentService
						.Query(new DepartmentQuery().Withfilter(filters)).Include(d => d.Company)
							.OrderBy(n=>n.OrderBy(sort,order))
							.SelectPageAsync(page, rows, out totalCount);
							 
			
				
						var datarows = departments .Select(  n => new { CompanyName = (n.Company==null?"": n.Company.Name) , Id = n.Id , Name = n.Name , Manager = n.Manager , CompanyId = n.CompanyId }).ToList();
			var pagelist = new { total = totalCount, rows = datarows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
				public async Task<ActionResult> SaveData(DepartmentChangeViewModel departments)
		{
			if (departments.updated != null)
			{
				foreach (var updated in departments.updated)
				{
					_departmentService.Update(updated);
				}
			}
			if (departments.deleted != null)
			{
				foreach (var deleted in departments.deleted)
				{
					_departmentService.Delete(deleted);
				}
			}
			if (departments.inserted != null)
			{
				foreach (var inserted in departments.inserted)
				{
					_departmentService.Insert(inserted);
				}
			}
			await _unitOfWork.SaveChangesAsync();

			return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
		}
		
						public async Task<ActionResult> GetCompanies()
		{
			var companyRepository = _unitOfWork.RepositoryAsync<Company>();
			var data = await companyRepository.Queryable().ToListAsync();
			var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
				
		
	   
		// GET: Departments/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var  department = await _departmentService.FindAsync(id);

			if (department == null)
			{
				return HttpNotFound();
			}
			return View(department);
		}
		

		// GET: Departments/Create
				public ActionResult Create()
				{
			var department = new Department();
			//set default value
			var companyRepository = _unitOfWork.RepositoryAsync<Company>();
		   			ViewBag.CompanyId = new SelectList(companyRepository.Queryable(), "Id", "Name");
		   			return View(department);
		}

		// POST: Departments/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Company,Id,Name,Manager,CompanyId")] Department department)
		{
			if (ModelState.IsValid)
			{
			 				_departmentService.Insert(department);
		   				await _unitOfWork.SaveChangesAsync();
				if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				DisplaySuccessMessage("Has append a Department record");
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
						var companyRepository = _unitOfWork.RepositoryAsync<Company>();
						ViewBag.CompanyId = new SelectList(await companyRepository.Queryable().ToListAsync(), "Id", "Name", department.CompanyId);
						
			return View(department);
		}

		// GET: Departments/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var department = await _departmentService.FindAsync(id);
			if (department == null)
			{
				return HttpNotFound();
			}
			var companyRepository = _unitOfWork.RepositoryAsync<Company>();
			ViewBag.CompanyId = new SelectList(companyRepository.Queryable(), "Id", "Name", department.CompanyId);
			return View(department);
		}

		// POST: Departments/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Company,Id,Name,Manager,CompanyId")] Department department)
		{
			if (ModelState.IsValid)
			{
				department.ObjectState = ObjectState.Modified;
								_departmentService.Update(department);
								
				await   _unitOfWork.SaveChangesAsync();
				if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				DisplaySuccessMessage("Has update a Department record");
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
						var companyRepository = _unitOfWork.RepositoryAsync<Company>();
						ViewBag.CompanyId = new SelectList( await companyRepository.Queryable().ToListAsync(), "Id", "Name", department.CompanyId);
						
			return View(department);
		}

		// GET: Departments/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var department = await _departmentService.FindAsync(id);
			if (department == null)
			{
				return HttpNotFound();
			}
			return View(department);
		}

		// POST: Departments/Delete/5
		[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			var department = await  _departmentService.FindAsync(id);
			 _departmentService.Delete(department);
			await _unitOfWork.SaveChangesAsync();
		   if (Request.IsAjaxRequest())
				{
					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
			DisplaySuccessMessage("Has delete a Department record");
			return RedirectToAction("Index");
		}

        public async Task<ActionResult> GetByCompanyId(int companyId = 0) {

            var data = await _departmentService.Queryable().Where(x => x.CompanyId == companyId).ToListAsync();
            var rows = data.Select(x => new { Id = x.Id, Name = x.Name });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
       

 

		//导出Excel
		[HttpPost]
		public ActionResult ExportExcel( string filterRules = "",string sort = "Id", string order = "asc")
		{
			var fileName = "departments_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream=  _departmentService.ExportExcel(filterRules,sort, order );
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
