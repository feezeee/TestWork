using App.BLL.DTO;

using System.Collections.Generic;

using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    /// <summary>
    /// Сервис для работы со соответствующим репозиторием
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Добавление компании в репозиторий
        /// </summary>
        /// <param name="item"></param>
        void AddCompany(CompanyDTO item);

        /// <summary>
        /// Получение выборки компаний по параметрам из репозитория
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        IEnumerable<CompanyDTO> GetCompanyBy(int id = 0, string name = "", int formTypeId = 0);

        /// <summary>
        ///  Получение выборки всех компаний из репозитория
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompanyDTO> GetCompanies();

        /// <summary>
        /// Обновление информации о компании в репозитории
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        void UpdateCompany(CompanyDTO item, int? id = null);

        /// <summary>
        /// Удаление компании из репозитория по Id
        /// </summary>
        /// <param name="id"></param>
        void DeleteCompany(int id);

        /// <summary>
        /// Async добавление компании в репзиторий
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task AddCompanyAsync(CompanyDTO item);
        /// <summary>
        /// Async получение выборки всех компаний из репозитория
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CompanyDTO>> GetCompaniesAsync();

        /// <summary>
        /// Async получение выборки компаний из репозитория по указанным параметрам
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        Task<IEnumerable<CompanyDTO>> GetCompanyByAsync(int id = 0, string name = "", int formTypeId = 0);
        /// <summary>
        /// Async обновление данных о компании в репозитории
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task UpdateCompanyAsync(CompanyDTO item, int? id = null);
        /// <summary>
        /// Async удаление компании из репозитория по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteCompanyAsync(int id);


    }
}
