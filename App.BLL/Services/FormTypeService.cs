using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.Interfaces;
using App.DAL.Models;
using System.Collections.Generic;
using System;
using App.BLL.DTOToDAL;
using App.BLL.DALToDTO;

namespace App.BLL.Services
{
    public class FormTypeService : IFormTypeService
    {
        IUnitOfWork Database { get; set; }

        public FormTypeService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public void AddFormType(FormTypeDTO item)
        {
            FormTypeDAL form = item.ToDAL();

            Database.FormTypes.Create(form);
        }

        public void DeleteFormType(int id)
        {
            if (Database.FormTypes.Find(new FormTypeDAL { Id = id }) != null)
            {
                Database.FormTypes.Delete(id);
            }
        }

        public IEnumerable<FormTypeDTO> GetFormTypeBy(int id = 0, string name = "")
        {
            FormTypeDAL item = null;
            if (id != 0)
            {
                if (item == null)
                {
                    item = new FormTypeDAL();
                }
                item.Id = id;
            }
            if (!String.IsNullOrEmpty(name))
            {
                if (item == null)
                {
                    item = new FormTypeDAL();
                }
                item.Name = name;
            }

            var formTypes = Database.FormTypes.Find(item);

            foreach (var form in formTypes)
            {
                FormTypeDTO myForm = form.ToDTO();

                yield return myForm;
            }
        }

        public IEnumerable<FormTypeDTO> GetFormTypes()
        {
            var formTypes = Database.FormTypes.GetAll();
            foreach (var form in formTypes)
            {
                yield return form.ToDTO();
            }
        }

        public void UpdateFormType(FormTypeDTO item)
        {
            Database.FormTypes.Update(item.ToDAL());
        }
    }
}
