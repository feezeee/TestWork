using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Data;
using TestWork.Models;

namespace TestWork.Controllers
{
    public class PositionController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public PositionController(IConfiguration config)
        {
            _config = config;
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Отображение списка должностей
        /// </summary>
        /// <param name="position">Фильтрующие параметры</param>
        /// <returns></returns>
        public async Task<IActionResult> List(Position position)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            string where = "";
            if (position?.Id != 0)
            {
                if (where.Length == 0)
                {
                    where += $"WHERE positions.position_id = {position.Id} ";
                }
                else
                {
                    where += $"and positions.position_id = {position.Id} ";
                }
            }
            if (position?.Name != null)
            {
                if (where.Length == 0)
                {
                    where += $"WHERE positions.position_name = '{position.Name}' ";
                }
                else
                {
                    where += $"and positions.position_name = '{position.Name}' ";
                }
            }            

            var positions = await myDbConnection.PositionsList(where);

            return View(positions);
        }


        /// <summary>
        /// Проверка на уникальность Id и(или) наименования должности
        /// </summary>
        /// <param name="Id">Индентификатор должности</param>
        /// <param name="Name">Наименование должности</param>
        /// <returns></returns>
        public IActionResult CheckPosition(int? Id, string Name)
        {

            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (Id != null)
            {
                var res1 = myDbConnection.PositionsList($"WHERE positions.position_id = {Id}").Result.FirstOrDefault();
                var res2 = myDbConnection.PositionsList($"WHERE positions.position_name = '{Name}'").Result.FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = myDbConnection.PositionsList($"WHERE positions.position_name = '{Name}'").Result.FirstOrDefault();
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
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Position position)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (ModelState.IsValid)
            {
                await myDbConnection.PositionCreate(position);

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
            MyDbConnection myDbConnection = new MyDbConnection(_config);

            if (id != null)
            {
                var position = myDbConnection.PositionById(id.Value).Result.FirstOrDefault();
                if (position == null)
                {
                    return RedirectToAction("List");
                }
                position.Workers.AddRange(myDbConnection.WorkersList($"WHERE workers.position_id = {position.Id}").Result);
                return View(position);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Position position)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (ModelState.IsValid)
            {
                await myDbConnection.PositionUpdate(position);
                return RedirectToAction("List");
            }
            position.Workers.AddRange(myDbConnection.WorkersList($"WHERE workers.position_id = {position.Id}").Result);
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
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (id != null)
            {
                var position = myDbConnection.PositionById(id.Value).Result.FirstOrDefault();
                if (position == null)
                {
                    return RedirectToAction("List");
                }

                position.Workers.AddRange(myDbConnection.WorkersList($"WHERE workers.position_id = {position.Id}").Result);
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
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (id != null)
            {
                var position = myDbConnection.PositionById(id.Value).Result.FirstOrDefault();
                if (position == null)
                {
                    return RedirectToAction("List");
                }

                position.Workers.AddRange(myDbConnection.WorkersList($"WHERE workers.position_id = {position.Id}").Result);

                if (position.Workers?.Count == 0)
                {
                    await myDbConnection.PositionDelete(id.Value);
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
