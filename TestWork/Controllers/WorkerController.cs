using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestWork.Data;
using TestWork.DTOToModels;
using TestWork.Models;
using System.Linq;

namespace TestWork.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        private IWorkerService workerService;

        public WorkerController(IConfiguration config, IWorkerService workerService)
        {
            _config = config;
            connectionString = config.GetConnectionString("DefaultConnection");
            this.workerService = workerService;
        }

        /// <summary>
        /// Отображение списка сотрудников
        /// </summary>
        /// <param name="worker">Фильтрующие данные</param>
        /// <returns></returns>
        public async Task<IActionResult> List(Worker worker)
        {
            var test = workerService.GetWorkerBy(worker.Id, worker.LastName, worker.FirstName, worker.MiddleName);

            List<Worker> workers = new List<Worker>();
            foreach(var el in test)
            {
                workers.Add(el.Worker());
            }      

            return View(workers);
        }


        /// <summary>
        /// Вызов форма добавления сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Create()
        {            

            //ViewBag.Positions = new SelectList(myDbConnection.PositionsList().Result, "Id", "Name");
            //ViewBag.Companies = new SelectList(myDbConnection.CompaniesList().Result, "Id", "Name");
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

        /// <summary>
        /// Вызов формы редактирования сотрудника
        /// </summary>
        /// <param name="id">Индентификатор сотрудника</param>
        /// <returns></returns>
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

        /// <summary>
        /// Вызов формы удаления сотрудника
        /// </summary>
        /// <param name="id">Индентификатор сотрудника</param>
        /// <returns></returns>
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
