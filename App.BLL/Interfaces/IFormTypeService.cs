using App.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    /// <summary>
    /// Сервис для работы со соответствующим репозиторием
    /// </summary>
    public interface IFormTypeService
    {
        void AddFormType(FormTypeDTO item);

        IEnumerable<FormTypeDTO> GetFormTypeBy(int id = 0, string name = "");

        IEnumerable<FormTypeDTO> GetFormTypes();

        void UpdateFormType(FormTypeDTO item);

        void DeleteFormType(int id);


        Task AddFormTypeAsync(FormTypeDTO item);
        Task<IEnumerable<FormTypeDTO>> GetFormTypesAsync();
        Task<IEnumerable<FormTypeDTO>> GetFormTypeByAsync(int id = 0, string name = "");
        Task UpdateFormTypeAsync(FormTypeDTO item);
        Task DeleteFormTypeAsync(int id);


    }
}
