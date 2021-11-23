using App.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IFormTypeService
    {
        void AddFormType(FormTypeDTO formTypeDTO);

        IEnumerable<FormTypeDTO> GetFormTypeBy(int id, string name);

        IEnumerable<FormTypeDTO> GetFormTypes();

        void UpdateFormType(FormTypeDTO formTypeDTO);

        void DeleteFormType(int id);


    }
}
