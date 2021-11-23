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
    public class PositionController : Controller
    {
        private readonly IManagerServices _managerServices;

        public PositionController(IManagerServices managerServices)
        {
            _managerServices = managerServices;
        }

        /// <summary>
        /// Отображение списка должностей
        /// </summary>
        /// <param name="position">Фильтрующие параметры</param>
        /// <returns></returns>
        public async Task<IActionResult> List(PositionViewModel position)
        {
            var positions = _managerServices.PositionService.GetPositionBy(position.Id, position.Name);

            List<PositionViewModel> myPositions = positions.ToList();
            
            return View(myPositions);
        }


        /// <summary>
        /// Проверка на уникальность Id и(или) наименования должности
        /// </summary>
        /// <param name="Id">Индентификатор должности</param>
        /// <param name="Name">Наименование должности</param>
        /// <returns></returns>
        public IActionResult CheckPosition(int? Id, string Name)
        {
            if (Id != null)
            {
                var res1 = _managerServices.PositionService.GetPositionBy(id: Id.Value).GetFirst();
                var res2 = _managerServices.PositionService.GetPositionBy(name: Name).GetFirst();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _managerServices.PositionService.GetPositionBy(name: Name).GetFirst();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }

        /// <summary>
        /// Вызов формы добавления новой должности
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionViewModel position)
        {
            if (ModelState.IsValid)
            {
                _managerServices.PositionService.AddPosition(position.ToDTO());

                return RedirectToAction("List");
            }
            return View(position);
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
                var position = _managerServices.PositionService.GetPositionBy(id.Value).GetFirst().ToViewModel();
                if (position == null)
                {
                    return RedirectToAction("List");
                }
                position.Workers.AddRange(_managerServices.WorkerService.GetWorkerBy(positionId: position.Id).ToList());
                return View(position);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PositionViewModel position)
        {
            if (ModelState.IsValid)
            {
                _managerServices.PositionService.UpdatePosition(position.ToDTO());
                return RedirectToAction("List");
            }

            position.Workers.AddRange(_managerServices.WorkerService.GetWorkerBy(positionId: position.Id).ToList());

            return View(position);
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
                var position = _managerServices.PositionService.GetPositionBy(id: id.Value).GetFirst().ToViewModel();
                if (position == null)
                {
                    return RedirectToAction("List");
                }

                position.Workers.AddRange(_managerServices.WorkerService.GetWorkerBy(positionId: position.Id).ToList());

                if (position.Workers?.Count == 0)
                {
                    return View(position);
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
                var position = _managerServices.PositionService.GetPositionBy(id: id.Value).GetFirst().ToViewModel();
                if (position == null)
                {
                    return RedirectToAction("List");
                }

                position.Workers.AddRange(_managerServices.WorkerService.GetWorkerBy(positionId: position.Id).ToList());

                if (position.Workers?.Count == 0)
                {                    
                    _managerServices.PositionService.DeletePosition(id.Value);
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
