using App.BLL.DTO;
using System.Collections.Generic;

namespace App.BLL.Interfaces
{
    public interface IWorkerService
    {
        void AddWorker(WorkerDTO orderDto);

        IEnumerable<WorkerDTO> GetWorkerBy(int? id, string lastName, string firstName, string middleName);

        IEnumerable<WorkerDTO> GetWorkers();

        void UpdateWorker(WorkerDTO worker);

        void DeleteWorker(int id);
        

    }
}
