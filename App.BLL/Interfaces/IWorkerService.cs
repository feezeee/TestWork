using App.BLL.DTO;
using System.Collections.Generic;

namespace App.BLL.Interfaces
{
    public interface IWorkerService
    {
        void AddWorker(WorkerDTO item);

        IEnumerable<WorkerDTO> GetWorkerBy(int id = 0, string lastName = "", string firstName = "", string middleName = "", int positionId = 0, int companyId = 0);

        IEnumerable<WorkerDTO> GetWorkers();

        void UpdateWorker(WorkerDTO item);

        void DeleteWorker(int id);
        

    }
}
