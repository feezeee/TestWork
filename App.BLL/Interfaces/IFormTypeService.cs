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
        void AddFormType(FormTypeDTO item);

        IEnumerable<FormTypeDTO> GetFormTypeBy(int id = 0, string name = "");

        IEnumerable<FormTypeDTO> GetFormTypes();

        void UpdateFormType(FormTypeDTO item);

        void DeleteFormType(int id);


    }
}
