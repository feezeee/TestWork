using App.BLL.DTO;
using System.Collections.Generic;

namespace App.BLL.Interfaces
{
    public interface IWorkerService
    {
        void AddWorker(WorkerDTO orderDto);

        WorkerDTO GetWorkerById(int? id);
        WorkerDTO GetWorkerByFirstName(string firstName);
        WorkerDTO GetWorkerByLastName(string firstName);
        WorkerDTO GetWorkerByMiddleName(string firstName);

        IEnumerable<WorkerDTO> GetWorkers();

        void UpdateWorker(WorkerDTO worker);

        void DeleteWorker(int? id);
        

    }
}
