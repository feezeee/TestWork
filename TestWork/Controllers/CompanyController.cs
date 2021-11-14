using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Data;
using TestWork.Models;

namespace TestWork.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public CompanyController(IConfiguration config)
        {
            _config = config;
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<IActionResult> List()
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);

            var company = await myDbConnection.CompaniesList();
            foreach(var el in company)
            {
                el.Workers.AddRange(myDbConnection.WorkersList($"WHERE workers.CompanyId = {el.Id}").Result);
            }

            return View(company);
        }

        [HttpGet]
        public ViewResult Create()
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);

            ViewBag.FormTypes = new SelectList(myDbConnection.FormTypesList().Result, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (ModelState.IsValid)
            {
                await myDbConnection.CompanyCreate(company);

                return RedirectToAction("List");
            }
            ViewBag.FormTypes = new SelectList(myDbConnection.FormTypesList().Result, "Id", "Name");
            return View(company);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);

            if (id != null)
            {
                var company = myDbConnection.CompanyById(id.Value).Result.FirstOrDefault();
                if (company == null)
                {
                    return RedirectToAction("List");
                }
                    
                company.Workers.AddRange(myDbConnection.WorkersList($"WHERE workers.CompanyId = {company.Id}").Result);
                
                ViewBag.FormTypes = new SelectList(myDbConnection.FormTypesList().Result, "Id", "Name");
                return View(company);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Company company)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (ModelState.IsValid)
            {
                await myDbConnection.CompanyUpdate(company);
                return RedirectToAction("List");
            }
            ViewBag.FormTypes = new SelectList(myDbConnection.FormTypesList().Result, "Id", "Name");
            return View(company);
        }

        public IActionResult CheckId(int? preId, int id)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (preId != null)
            {
                var res1 = myDbConnection.CompaniesList($"WHERE companies.Id = {preId}").Result.FirstOrDefault();
                var res2 = myDbConnection.CompaniesList($"WHERE companies.Id = {id}").Result.FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = myDbConnection.CompaniesList($"WHERE companies.Id = {id}").Result.FirstOrDefault();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (id != null)
            {
                var company = myDbConnection.CompanyById(id.Value).Result.FirstOrDefault();
                if (company == null)
                {
                    return RedirectToAction("List");
                }

                company.Workers.AddRange(myDbConnection.WorkersList($"WHERE workers.CompanyId = {company.Id}").Result);
                if (company.Workers?.Count == 0)
                {
                    return View(company);
                }
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (id != null)
            {
                var company = myDbConnection.CompanyById(id.Value).Result.FirstOrDefault();
                if (company == null)
                {
                    return RedirectToAction("List");
                }

                company.Workers.AddRange(myDbConnection.WorkersList($"WHERE workers.CompanyId = {company.Id}").Result);

                if (company.Workers?.Count == 0)
                {
                    await myDbConnection.CompanyDelete(id.Value);
                }
                else
                {
                    return RedirectToAction("List");
                }
            }
            return RedirectToAction("List");
        }
    }
}
