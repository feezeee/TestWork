using App.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    /// <summary>
    /// Сервис для работы со соответствующим репозиторием
    /// </summary>
    public interface IWorkerService
    {
        /// <summary>
        /// Добавление работника в репозиторий
        /// </summary>
        /// <param name="item"></param>
        void AddWorker(WorkerDTO item);

        /// <summary>
        /// Выборка из репозитория нужных работников
        /// </summary>
        IEnumerable<WorkerDTO> GetWorkerBy(int id = 0, string lastName = "", string firstName = "", string middleName = "", int positionId = 0, int companyId = 0);

        /// <summary>
        /// Выборка всех работников
        /// </summary>
        IEnumerable<WorkerDTO> GetWorkers();

        /// <summary>
        /// Обновление данных о работнике в репозитории
        /// </summary>
        /// <param name="item"></param>
        void UpdateWorker(WorkerDTO item);

        /// <summary>
        /// Удаление из репозитория по Id
        /// </summary>
        /// <param name="id"></param>
        void DeleteWorker(int id);



        /// <summary>
        /// Async добавление работника в репозиторий
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task AddWorkerAsync(WorkerDTO item);

        /// <summary>
        /// Async Выборка всех работников
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<WorkerDTO>> GetWorkersAsync();

        /// <summary>
        /// Async Выборка из репозитория нужных работников
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<WorkerDTO>> GetWorkerByAsync(int id = 0, string lastName = "", string firstName = "", string middleName = "", int positionId = 0, int companyId = 0);

        /// <summary>
        /// Async Обновление данных о работнике в репозитории
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpdateWorkerAsync(WorkerDTO item);

        /// <summary>
        /// Async Удаление из репозитория по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteWorkerAsync(int id); 

    }
}
