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
    public class FormTypeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public FormTypeController(IConfiguration config)
        {
            _config = config;
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<IActionResult> List(FormType formType)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            string where = "";
            if (formType?.Id != 0)
            {
                if (where.Length == 0)
                {
                    where += $"WHERE form_types.form_type_id = {formType.Id} ";
                }
                else
                {
                    where += $"and form_types.form_type_id = {formType.Id} ";
                }
            }
            if (formType?.Name != null)
            {
                if (where.Length == 0)
                {
                    where += $"WHERE form_types.form_type_name = '{formType.Name}' ";
                }
                else
                {
                    where += $"and form_types.form_type_name = '{formType.Name}' ";
                }
            }            

            var formTypes = await myDbConnection.FormTypesList(where);

            return View(formTypes);
        }

        [HttpGet]
        public ViewResult Create()
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FormType formType)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (ModelState.IsValid)
            {
                await myDbConnection.FormTypeCreate(formType);

                return RedirectToAction("List");
            }
            return View(formType);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);

            if (id != null)
            {
                var formType = myDbConnection.FormTypeById(id.Value).Result.FirstOrDefault();
                if (formType == null)
                {
                    return RedirectToAction("List");
                }

                formType.Companies.AddRange(myDbConnection.CompaniesList($"WHERE companies.form_type_id = {formType.Id}").Result);
                return View(formType);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FormType formType)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (ModelState.IsValid)
            {
                await myDbConnection.FormTypeUpdate(formType);
                return RedirectToAction("List");
            }
            formType.Companies.AddRange(myDbConnection.CompaniesList($"WHERE companies.form_type_id = {formType.Id}").Result);
            return View(formType);
        }

        public IActionResult CheckFormType(int? Id, string Name)
        {
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (Id != null)
            {
                var res1 = myDbConnection.FormTypesList($"WHERE form_types.form_type_id = {Id}").Result.FirstOrDefault();
                var res2 = myDbConnection.FormTypesList($"WHERE form_types.form_type_name = '{Name}'").Result.FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = myDbConnection.FormTypesList($"WHERE form_types.form_type_name = '{Name}'").Result.FirstOrDefault();
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
                var formType = myDbConnection.FormTypeById(id.Value).Result.FirstOrDefault();
                if (formType == null)
                {
                    return RedirectToAction("List");
                }

                formType.Companies.AddRange(myDbConnection.CompaniesList($"WHERE companies.form_type_id = {formType.Id}").Result);
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
            MyDbConnection myDbConnection = new MyDbConnection(_config);
            if (id != null)
            {
                var formType = myDbConnection.FormTypeById(id.Value).Result.FirstOrDefault();
                if (formType == null)
                {
                    return RedirectToAction("List");
                }

                formType.Companies.AddRange(myDbConnection.CompaniesList($"WHERE companies.form_type_id = {formType.Id}").Result);

                if (formType.Companies?.Count == 0)
                {
                    await myDbConnection.FormTypeDelete(id.Value);
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
