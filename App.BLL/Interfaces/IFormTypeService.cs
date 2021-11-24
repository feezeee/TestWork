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
        /// <summary>
        /// Добавление в репозиторий ОПФ
        /// </summary>
        /// <param name="item"></param>
        void AddFormType(FormTypeDTO item);

        /// <summary>
        /// Выборка ОПФ по параметрам из репозитория
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerable<FormTypeDTO> GetFormTypeBy(int id = 0, string name = "");

        /// <summary>
        /// Выборка всех ОПФ из репозитория
        /// </summary>
        /// <returns></returns>
        IEnumerable<FormTypeDTO> GetFormTypes();

        /// <summary>
        /// Обновление данных об ОПФ в репозитории
        /// </summary>
        /// <param name="item"></param>
        void UpdateFormType(FormTypeDTO item);


        /// <summary>
        /// Удаление по Id ОПФ из репозитория
        /// </summary>
        /// <param name="id"></param>
        void DeleteFormType(int id);


        /// <summary>
        /// Async добавление ОПФ в репозиторий
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task AddFormTypeAsync(FormTypeDTO item);

        /// <summary>
        /// Async получение выборки всех ОПФ из репозитория
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FormTypeDTO>> GetFormTypesAsync();

        /// <summary>
        /// Async получение выборки ОПФ по параметрам
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<FormTypeDTO>> GetFormTypeByAsync(int id = 0, string name = "");

        /// <summary>
        /// Async обновление данных ОПФ в репозитории
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpdateFormTypeAsync(FormTypeDTO item);

        /// <summary>
        /// Async удаление ОПФ по Id из репозитория
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteFormTypeAsync(int id);


    }
}
