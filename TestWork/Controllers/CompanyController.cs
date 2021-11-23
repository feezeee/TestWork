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
            var companies = await _managerServices.CompanyService.GetCompanyByAsync(id: company.Id, name: company.Name);

            List<CompanyViewModel> myCompanies = companies.ToList();
            foreach(CompanyViewModel compan in myCompanies)
            {
                compan.Workers.AddRange((await _managerServices.WorkerService.GetWorkerByAsync(companyId: compan.Id)).ToList());
            }

            return View(myCompanies);
        }


        /// <summary>
        /// Вызов формы добавления новой компании
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.FormTypes = new SelectList((await _managerServices.FormTypeService.GetFormTypesAsync()).ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyViewModel company)
        {
            if (ModelState.IsValid)
            {
                await _managerServices.CompanyService.AddCompanyAsync(company.ToDTO());

                return RedirectToAction("List");
            }
            ViewBag.FormTypes = new SelectList((await _managerServices.FormTypeService.GetFormTypesAsync()).ToList(), "Id", "Name");
            return View(company);
        }


        /// <summary>
        /// Вызов формы редактирования информации о компании
        /// </summary>
        /// <param name="id">Индентификатор компании</param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var company = (await _managerServices.CompanyService.GetCompanyByAsync(id: id.Value)).GetFirst().ToViewModel();
                if (company == null)
                {
                    return RedirectToAction("List");
                }

                company.Workers.AddRange((await _managerServices.WorkerService.GetWorkerByAsync(companyId: company.Id)).ToList());
                
                ViewBag.FormTypes = new SelectList((await _managerServices.FormTypeService.GetFormTypesAsync()).ToList(), "Id", "Name");
                return View(company);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyViewModel company, int? predId)
        {
            if (ModelState.IsValid)
            {
                if((await _managerServices.WorkerService.GetWorkerByAsync(companyId: predId.Value)).ToList().Count == 0)
                {
                    await _managerServices.CompanyService.UpdateCompanyAsync(company.ToDTO(), predId);
                    return RedirectToAction("List");
                }
                else
                {
                    await _managerServices.CompanyService.UpdateCompanyAsync(company.ToDTO());
                    return RedirectToAction("List");
                }
            }

            ViewBag.FormTypes = new SelectList((await _managerServices.FormTypeService.GetFormTypesAsync()).ToList(), "Id", "Name");
            return View(company);
        }


        /// <summary>
        /// Проверка индентификатора компании на уникальность
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CheckId(int? predId, int id)
        {
            if (predId != null)
            {
                var res1 = (await _managerServices.CompanyService.GetCompanyByAsync(id: predId.Value)).GetFirst();
                var res2 = (await _managerServices.CompanyService.GetCompanyByAsync(id: id)).GetFirst();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = (await _managerServices.CompanyService.GetCompanyByAsync(id: id)).GetFirst();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var company = (await _managerServices.CompanyService.GetCompanyByAsync(id: id.Value)).GetFirst().ToViewModel();
                if (company == null)
                {
                    return RedirectToAction("List");
                }

                company.Workers.AddRange((await _managerServices.WorkerService.GetWorkerByAsync(companyId: company.Id)).ToList());
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
                var company = (await _managerServices.CompanyService.GetCompanyByAsync(id: id.Value)).GetFirst().ToViewModel();
                if (company == null)
                {
                    return RedirectToAction("List");
                }

                company.Workers.AddRange((await _managerServices.WorkerService.GetWorkerByAsync(companyId: company.Id)).ToList());

                if (company.Workers?.Count == 0)
                {
                    await _managerServices.WorkerService.DeleteWorkerAsync(id.Value);
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
