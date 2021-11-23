using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestWork.DTOToViewModels;
using TestWork.IEnumerableExtension;
using TestWork.Models;
using TestWork.ViewModelsToDTO;


namespace TestWork.Controllers
{
    public class FormTypeController : Controller
    {
        private readonly IManagerServices _managerServices;

        public FormTypeController(IManagerServices managerServices)
        {
            _managerServices = managerServices;
        }

        /// <summary>
        /// Вывод информации о всех ОПФ
        /// </summary>
        /// <param name="formType">Фильтрующие параметры</param>
        /// <returns></returns>
        public async Task<IActionResult> List(FormTypeViewModel formType)
        {
            var forms  = _managerServices.FormTypeService.GetFormTypeBy(formType.Id, formType.Name);

            List<FormTypeViewModel> myForms = forms.ToList();

            return View(myForms);
        }


        /// <summary>
        /// Вызов формы добавления новой ОПФ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Create()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FormTypeViewModel formType)
        {            
            if (ModelState.IsValid)
            {
                _managerServices.FormTypeService.AddFormType(formType.ToDTO());

                return RedirectToAction("List");
            }
            return View(formType);
        }


        /// <summary>
        /// Вызов формы редактирования ОПФ
        /// </summary>
        /// <param name="id">Индентификатор ОПФ</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var formType = _managerServices.FormTypeService.GetFormTypeBy(id: id.Value).GetFirst().ToViewModel();
                if (formType == null)
                {
                    return RedirectToAction("List");
                }

                formType.Companies.AddRange(_managerServices.CompanyService.GetCompanyBy(formTypeId: id.Value).ToList());
                return View(formType);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FormTypeViewModel formType)
        {
            if (ModelState.IsValid)
            {
                _managerServices.FormTypeService.UpdateFormType(formType.ToDTO());
                return RedirectToAction("List");
            }
            formType.Companies.AddRange(_managerServices.CompanyService.GetCompanyBy(formTypeId: formType.Id).ToList());
            return View(formType);
        }


        /// <summary>
        /// Проверка на уникальность наименования ОПФ
        /// </summary>
        /// <param name="Id">Индентификатор ОПФ</param>
        /// <param name="Name">Наименование ОПФ</param>
        /// <returns></returns>
        public IActionResult CheckFormType(int? Id, string Name)
        {
            if (Id != null)
            {
                var res1 = _managerServices.FormTypeService.GetFormTypeBy(id: Id.Value).GetFirst();
                var res2 = _managerServices.FormTypeService.GetFormTypeBy(name: Name).GetFirst();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _managerServices.FormTypeService.GetFormTypeBy(name: Name).GetFirst();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }

        /// <summary>
        /// Вызов формы удаления ОПФ
        /// </summary>
        /// <param name="id">Индентификатор ОПФ</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var formType = _managerServices.FormTypeService.GetFormTypeBy(id: id.Value).GetFirst().ToViewModel();
                if (formType == null)
                {
                    return RedirectToAction("List");
                }

                formType.Companies.AddRange(_managerServices.CompanyService.GetCompanyBy(formTypeId: formType.Id).ToList());
                if (formType.Companies?.Count == 0)
                {
                    return View(formType);
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
                var formType = _managerServices.FormTypeService.GetFormTypeBy(id: id.Value).GetFirst().ToViewModel();
                if (formType == null)
                {
                    return RedirectToAction("List");
                }

                formType.Companies.AddRange(_managerServices.CompanyService.GetCompanyBy(formTypeId: formType.Id).ToList());

                if (formType.Companies?.Count == 0)
                {
                    _managerServices.FormTypeService.DeleteFormType(formType.Id);
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
