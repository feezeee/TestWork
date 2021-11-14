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
    public class WorkerController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public WorkerController(IConfiguration config)
        {
            _config = config;
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<IActionResult> List(Worker worker)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            string where = "";
            if (worker?.Id != 0)
            {
                if(where.Length == 0)
                {
                    where += $"WHERE workers.Id = {worker.Id} ";
                }
                else
                {
                    where += $"or workers.Id = {worker.Id} ";
                }
            }
            if (worker?.LastName != null)
            {
                if (where.Length == 0)
                {
                    where += $"WHERE workers.LastName = '{worker.LastName}' ";
                }
                else
                {
                    where += $"or workers.LastName = '{worker.LastName}' ";
                }
            }
            if (worker?.FirstName != null)
            {
                if (where.Length == 0)
                {
                    where += $"WHERE workers.FirstName = '{worker.FirstName}' ";
                }
                else
                {
                    where += $"or workers.FirstName = '{worker.FirstName}' ";
                }
            }
            if (worker?.MiddleName != null)
            {
                if (where.Length == 0)
                {
                    where += $"WHERE workers.MiddleName = '{worker.MiddleName}' ";
                }
                else
                {
                    where += $"or workers.MiddleName = '{worker.MiddleName}' ";
                }
            }            

            var workers = await myDbConnection.WorkersList(where);

            return View(workers);
        }

        [HttpGet]
        public ViewResult Create()
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);

            ViewBag.Positions = new SelectList(myDbConnection.PositionsList().Result, "Id", "Name");
            ViewBag.Companies = new SelectList(myDbConnection.CompaniesList().Result, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Worker worker)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (ModelState.IsValid)
            {           
                await myDbConnection.WorkerCreate(worker);

                return RedirectToAction("List");
            }
            ViewBag.Positions = new SelectList(myDbConnection.PositionsList().Result, "Id", "Name");
            ViewBag.Companies = new SelectList(myDbConnection.CompaniesList().Result, "Id", "Name");
            return View(worker);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);

            if (id != null)
            {
                var worker = myDbConnection.WorkerById(id.Value).Result.FirstOrDefault();
                if (worker == null)
                {
                    return RedirectToAction("List");
                }
                ViewBag.Positions = new SelectList(myDbConnection.PositionsList().Result, "Id", "Name");
                ViewBag.Companies = new SelectList(myDbConnection.CompaniesList().Result, "Id", "Name");
                return View(worker);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Worker worker)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (ModelState.IsValid)
            {
                await myDbConnection.WorkerUpdate(worker);
                return RedirectToAction("List");
            }
            ViewBag.Positions = new SelectList(myDbConnection.PositionsList().Result, "Id", "Name");
            ViewBag.Companies = new SelectList(myDbConnection.CompaniesList().Result, "Id", "Name");
            return View(worker);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (id != null)
            {
                var worker = myDbConnection.WorkerById(id.Value).Result.FirstOrDefault();
                if (worker == null)
                {
                    return RedirectToAction("List");
                }
                return View(worker);
            }
            return RedirectToAction("List");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (id != null)
            {
                var worker = myDbConnection.WorkerById(id.Value).Result.FirstOrDefault();
                if (worker == null)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    await myDbConnection.WorkerDelete(id.Value);
                }
            }
            return RedirectToAction("List");
        }
    }
}
