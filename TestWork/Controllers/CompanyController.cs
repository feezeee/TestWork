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
    public class CompanyController : Controller
    {
        private readonly IManagerServices _managerServices;

        public CompanyController(IManagerServices managerServices)
        {
            _managerServices = managerServices;
        }


        /// <summary>
        /// Вывод списка компаний
        /// </summary>
        /// <param name="company">Фильтрующие параметры</param>
        /// <returns></returns>
        public async Task<IActionResult> List(CompanyViewModel company)
        {
            var companies = _managerServices.CompanyService.GetCompanyBy(id: company.Id, name: company.Name);

            List<CompanyViewModel> myCompanies = companies.ToList();
            foreach(CompanyViewModel compan in myCompanies)
            {
                compan.Workers.AddRange(_managerServices.WorkerService.GetWorkerBy(companyId: compan.Id).ToList());
            }

            return View(myCompanies);
        }


        /// <summary>
        /// Вызов формы добавления новой компании
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Create()
        {
            ViewBag.FormTypes = new SelectList(_managerServices.FormTypeService.GetFormTypes().ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyViewModel company)
        {
            if (ModelState.IsValid)
            {
                _managerServices.CompanyService.AddCompany(company.ToDTO());

                return RedirectToAction("List");
            }
            ViewBag.FormTypes = new SelectList(_managerServices.FormTypeService.GetFormTypes().ToList(), "Id", "Name");
            return View(company);
        }


        /// <summary>
        /// Вызов формы редактирования информации о компании
        /// </summary>
        /// <param name="id">Индентификатор компании</param>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var company = _managerServices.CompanyService.GetCompanyBy(id: id.Value).GetFirst().ToViewModel();
                if (company == null)
                {
                    return RedirectToAction("List");
                }

                company.Workers.AddRange(_managerServices.WorkerService.GetWorkerBy(companyId: company.Id).ToList());
                
                ViewBag.FormTypes = new SelectList(_managerServices.FormTypeService.GetFormTypes().ToList(), "Id", "Name");
                return View(company);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyViewModel company, int? predId)
        {
            if (ModelState.IsValid)
            {
                if(_managerServices.WorkerService.GetWorkerBy(companyId: predId.Value).ToList().Count == 0)
                {
                    _managerServices.CompanyService.UpdateCompany(company.ToDTO(), predId);
                    return RedirectToAction("List");
                }
                else
                {
                    _managerServices.CompanyService.UpdateCompany(company.ToDTO());
                    return RedirectToAction("List");
                }
            }

            ViewBag.FormTypes = new SelectList(_managerServices.FormTypeService.GetFormTypes().ToList(), "Id", "Name");
            return View(company);
        }


        /// <summary>
        /// Проверка индентификатора компании на уникальность
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckId(int? predId, int id)
        {
            if (predId != null)
            {
                var res1 = _managerServices.CompanyService.GetCompanyBy(id: predId.Value).GetFirst();
                var res2 = _managerServices.CompanyService.GetCompanyBy(id: id).GetFirst();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _managerServices.CompanyService.GetCompanyBy(id: id).GetFirst();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }


        /// <summary>
        /// Вызов формы удаления компании
        /// </summary>
        /// <param name="id">Индентификатор компании</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var company = _managerServices.CompanyService.GetCompanyBy(id: id.Value).GetFirst().ToViewModel();
                if (company == null)
                {
                    return RedirectToAction("List");
                }

                company.Workers.AddRange(_managerServices.WorkerService.GetWorkerBy(companyId: company.Id).ToList());
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
            if (id != null)
            {
                var company = _managerServices.CompanyService.GetCompanyBy(id: id.Value).GetFirst().ToViewModel();
                if (company == null)
                {
                    return RedirectToAction("List");
                }

                company.Workers.AddRange(_managerServices.WorkerService.GetWorkerBy(companyId: company.Id).ToList());

                if (company.Workers?.Count == 0)
                {
                    _managerServices.WorkerService.DeleteWorker(id.Value);
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
