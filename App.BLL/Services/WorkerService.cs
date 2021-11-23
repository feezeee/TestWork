using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.Interfaces;
using App.DAL.Models;
using System.Collections.Generic;
using System;
using App.BLL.DTOToDAL;
using App.BLL.DALToDTO;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class WorkerService : IWorkerService
    {

        IUnitOfWork Database { get; set; }

        public WorkerService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public void AddWorker(WorkerDTO item)
        {
            WorkerDAL worker = item.ToDAL();

            Database.Workers.Create(worker);            
            
        }

        public void DeleteWorker(int id)
        {
            if (Database.Workers.Find(new WorkerDAL { Id = id }) != null)
            {
                Database.Workers.Delete(id);
            }             
            
        }       

        public IEnumerable<WorkerDTO> GetWorkers()
        {
            var workers = Database.Workers.GetAll();
            foreach (var worker in workers)
            {            
                yield return worker.ToDTO();
            }
        }

        public void UpdateWorker(WorkerDTO item)
        {
            WorkerDAL worker = item.ToDAL();

            Database.Workers.Update(worker);
        }

        public IEnumerable<WorkerDTO> GetWorkerBy(int id = 0, string lastName = "", string firstName = "", string middleName = "", int positionId = 0, int companyId = 0)
        {
            WorkerDAL item = null;
            if (id != 0)
            {
                if(item == null)
                {
                    item = new WorkerDAL();
                }
                item.Id = id;
            }
            if (!String.IsNullOrEmpty(lastName))
            {
                if (item == null)
                {
                    item = new WorkerDAL();
                }
                item.LastName = lastName;
            }
            if (!String.IsNullOrEmpty(firstName))
            {
                if (item == null)
                {
                    item = new WorkerDAL();
                }
                item.FirstName = firstName;
            }
            if (!String.IsNullOrEmpty(middleName))
            {
                if (item == null)
                {
                    item = new WorkerDAL();
                }
                item.MiddleName = middleName;
            }
            if (positionId != 0)
            {
                if (item == null)
                {
                    item = new WorkerDAL();
                }
                item.PositionId = positionId;
            }
            if (companyId != 0)
            {
                if (item == null)
                {
                    item = new WorkerDAL();
                }
                item.CompanyId = companyId;
            }

            var workers = Database.Workers.Find(item);
            foreach (var worker in workers)
            {
                WorkerDTO myworker = worker.ToDTO(); 

                yield return myworker;
            }
        }

        public async Task AddWorkerAsync(WorkerDTO item)
        {
            await Task.Run(() => AddWorker(item));
        }

        public async Task<IEnumerable<WorkerDTO>> GetWorkersAsync()
        {
            return await Task.Run(() => GetWorkers());
        }

        public async Task<IEnumerable<WorkerDTO>> GetWorkerByAsync(int id = 0, string lastName = "", string firstName = "", string middleName = "", int positionId = 0, int companyId = 0)
        {
            return await Task.Run(() => GetWorkerBy(id, lastName, firstName, middleName, positionId, companyId)); 
        }

        public async Task UpdateWorkerAsync(WorkerDTO item)
        {
            await Task.Run(() => UpdateWorker(item));
        }

        public async Task DeleteWorkerAsync(int id)
        {
            await Task.Run(() => DeleteWorker(id));
        }
    }
}
