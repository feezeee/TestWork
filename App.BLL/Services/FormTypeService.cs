using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.Interfaces;
using App.DAL.Models;
using System.Collections.Generic;
using System;
using App.BLL.DTOToDAL;
using App.BLL.DALToDTO;
using System.Threading.Tasks;

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

        public async Task AddFormTypeAsync(FormTypeDTO item)
        {
            await Task.Run(() => AddFormType(item));
        }

        public async Task<IEnumerable<FormTypeDTO>> GetFormTypesAsync()
        {
            return await Task.Run(() => GetFormTypes());
        }

        public async Task<IEnumerable<FormTypeDTO>> GetFormTypeByAsync(int id = 0, string name = "")
        {
            return await Task.Run(() => GetFormTypeBy(id, name));
        }

        public async Task UpdateFormTypeAsync(FormTypeDTO item)
        {
            await Task.Run(() => UpdateFormType(item));
        }

        public async Task DeleteFormTypeAsync(int id)
        {
            await Task.Run(() => DeleteFormType(id));
        }
    }
}
