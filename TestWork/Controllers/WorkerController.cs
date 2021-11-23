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
            var workers = await _managerServices.WorkerService.GetWorkerByAsync(worker.Id, worker.LastName, worker.FirstName, worker.MiddleName);

            List<WorkerViewModel> myWorkers = workers.ToList();

            return View(myWorkers);
        }

       
        /// <summary>
        /// Вызов форма добавления сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ViewBag.Positions = new SelectList((await _managerServices.PositionService.GetPositionsAsync()).ToList(), "Id", "Name");
            ViewBag.Companies = new SelectList((await _managerServices.CompanyService.GetCompaniesAsync()).ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkerViewModel worker)
        {            
            if (ModelState.IsValid)
            {
                await _managerServices.WorkerService.AddWorkerAsync(worker.ToDTO());
                return RedirectToAction("List");
            }
            ViewBag.Positions = new SelectList((await _managerServices.PositionService.GetPositionsAsync()).ToList(), "Id", "Name");
            ViewBag.Companies = new SelectList((await _managerServices.CompanyService.GetCompaniesAsync()).ToList(), "Id", "Name");
            return View(worker);
        }
        
       /// <summary>
       /// Вызов формы редактирования сотрудника
       /// </summary>
       /// <param name="id">Индентификатор сотрудника</param>
       /// <returns></returns>
       [HttpGet]
       public async Task<IActionResult> Edit(int? id)
       {
           if (id != null)
           {
                var worker = (await _managerServices.WorkerService.GetWorkerByAsync(id.Value)).GetFirst().ToViewModel();

                if (worker == null)
                {
                    return RedirectToAction("List");
                }
                ViewBag.Positions = new SelectList((await _managerServices.PositionService.GetPositionsAsync()).ToList(), "Id", "Name");
                ViewBag.Companies = new SelectList((await _managerServices.CompanyService.GetCompaniesAsync()).ToList(), "Id", "Name");
                return View(worker);
           }
           return RedirectToAction("List");
       }

       [HttpPost]
       public async Task<IActionResult> Edit(WorkerViewModel worker)
       {
            if (ModelState.IsValid)
            {
                await _managerServices.WorkerService.UpdateWorkerAsync(worker.ToDTO());
                return RedirectToAction("List");
            }
            ViewBag.Positions = new SelectList((await _managerServices.PositionService.GetPositionsAsync()).ToList(), "Id", "Name");
            ViewBag.Companies = new SelectList((await _managerServices.CompanyService.GetCompaniesAsync()).ToList(), "Id", "Name");
            return View(worker);
       }
        
       /// <summary>
       /// Вызов формы удаления сотрудника
       /// </summary>
       /// <param name="id">Индентификатор сотрудника</param>
       /// <returns></returns>
       [HttpGet]
       public async Task<IActionResult> Delete(int? id)
       {           
           if (id != null)
           {
                var worker = (await _managerServices.WorkerService.GetWorkerByAsync(id.Value)).GetFirst().ToViewModel();
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
                var worker = (await _managerServices.WorkerService.GetWorkerByAsync(id.Value)).GetFirst().ToViewModel();
                if (worker == null)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    await _managerServices.WorkerService.DeleteWorkerAsync(id.Value);
                }
           }
            return RedirectToAction("List");
       }
    }
}
