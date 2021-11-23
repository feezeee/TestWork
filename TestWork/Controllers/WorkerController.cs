using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestWork.DTOToViewModels;
using TestWork.IEnumerableExtension;
using TestWork.Models;
using TestWork.ViewModelsToDTO;

namespace TestWork.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IManagerServices _managerServices;

        public WorkerController(IManagerServices managerServices)
        {
            _managerServices = managerServices;
        }

        /// <summary>
        /// Отображение списка сотрудников
        /// </summary>
        /// <param name="worker">Фильтрующие данные</param>
        /// <returns></returns>
        public async Task<IActionResult> List(WorkerViewModel worker)
        {
            var workers = _managerServices.WorkerService.GetWorkerBy(worker.Id, worker.LastName, worker.FirstName, worker.MiddleName);

            List<WorkerViewModel> myWorkers = workers.ToList();

            return View(myWorkers);
        }

       
        /// <summary>
        /// Вызов форма добавления сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Create()
        {

            ViewBag.Positions = new SelectList(_managerServices.PositionService.GetPositions(), "Id", "Name");
            ViewBag.Companies = new SelectList(_managerServices.CompanyService.GetCompanies(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkerViewModel worker)
        {            
            if (ModelState.IsValid)
            {
                _managerServices.WorkerService.AddWorker(worker.ToDTO());
                return RedirectToAction("List");
            }
            ViewBag.Positions = new SelectList(_managerServices.PositionService.GetPositions(), "Id", "Name");
            ViewBag.Companies = new SelectList(_managerServices.CompanyService.GetCompanies(), "Id", "Name");
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
           if (id != null)
           {                               
                var worker = _managerServices.WorkerService.GetWorkerBy(id.Value).GetFirst().ToViewModel();

                if (worker == null)
                {
                    return RedirectToAction("List");
                }
                ViewBag.Positions = new SelectList(_managerServices.PositionService.GetPositions().ToList(), "Id", "Name");
                ViewBag.Companies = new SelectList(_managerServices.CompanyService.GetCompanies().ToList(), "Id", "Name");
                return View(worker);
           }
           return RedirectToAction("List");
       }

       [HttpPost]
       public async Task<IActionResult> Edit(WorkerViewModel worker)
       {
            if (ModelState.IsValid)
            {
                _managerServices.WorkerService.UpdateWorker(worker.ToDTO());
                return RedirectToAction("List");
            }
            ViewBag.Positions = new SelectList(_managerServices.PositionService.GetPositions().ToList(), "Id", "Name");
            ViewBag.Companies = new SelectList(_managerServices.CompanyService.GetCompanies().ToList(), "Id", "Name");
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
           if (id != null)
           {
                var worker = _managerServices.WorkerService.GetWorkerBy(id.Value).GetFirst().ToViewModel();
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
           if (id != null)
           {
                var worker = _managerServices.WorkerService.GetWorkerBy(id.Value).GetFirst().ToViewModel();
                if (worker == null)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    _managerServices.WorkerService.DeleteWorker(id.Value);
                }
           }
            return RedirectToAction("List");
       }
    }
}
