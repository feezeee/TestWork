using App.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IWorkerService
    {
        void AddWorker(WorkerDTO item);

        IEnumerable<WorkerDTO> GetWorkerBy(int id = 0, string lastName = "", string firstName = "", string middleName = "", int positionId = 0, int companyId = 0);

        IEnumerable<WorkerDTO> GetWorkers();

        void UpdateWorker(WorkerDTO item);

        void DeleteWorker(int id);


        Task AddWorkerAsync(WorkerDTO item);
        Task<IEnumerable<WorkerDTO>> GetWorkersAsync();
        Task<IEnumerable<WorkerDTO>> GetWorkerByAsync(int id = 0, string lastName = "", string firstName = "", string middleName = "", int positionId = 0, int companyId = 0);        
        Task UpdateWorkerAsync(WorkerDTO item);
        Task DeleteWorkerAsync(int id); 

    }
}
