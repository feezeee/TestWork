
namespace App.BLL.Interfaces
{
    /// <summary>
    /// Менеджер, хранящий все сервисы для работы с репозиториями
    /// </summary>
    public interface IManagerServices
    {
        /// <summary>
        /// Сервис для работы со соответствующим репозиторием
        /// </summary>
        IWorkerService WorkerService { get; }
        /// <summary>
        /// Сервис для работы со соответствующим репозиторием
        /// </summary>
        IPositionService PositionService { get; }
        /// <summary>
        /// Сервис для работы со соответствующим репозиторием
        /// </summary>
        ICompanyService CompanyService { get; }
        /// <summary>
        /// Сервис для работы со соответствующим репозиторием
        /// </summary>
        IFormTypeService FormTypeService { get; }
    }
}
