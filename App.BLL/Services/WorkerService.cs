using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.Interfaces;
using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void AddWorker(WorkerDTO orderDto)
        {
            Worker worker = new Worker
            {
                FirstName = orderDto.FirstName,
                LastName = orderDto.LastName,
                MiddleName = orderDto.MiddleName,
                PositionId = orderDto.PositionId,
                CompanyId = orderDto.CompanyId,
                DateEmployment = orderDto.DateEmployment
            };

            Database.Workers.Create(worker);
        }

        public void DeleteWorker(int? id)
        {
            throw new NotImplementedException();
        }

        public WorkerDTO GetWorkerByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public WorkerDTO GetWorkerById(int? id)
        {
            throw new NotImplementedException();
        }

        public WorkerDTO GetWorkerByLastName(string firstName)
        {
            throw new NotImplementedException();
        }

        public WorkerDTO GetWorkerByMiddleName(string firstName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkerDTO> GetWorkers()
        {
            var workers = Database.Workers.GetAll();
            foreach(var worker in workers)
            {
                WorkerDTO myworker = new WorkerDTO();
                myworker.Id = worker.Id;
                myworker.FirstName = worker.FirstName;
                myworker.LastName = worker.LastName;
                myworker.MiddleName = worker.MiddleName;
                myworker.DateEmployment = worker.DateEmployment;
                

                myworker.Position = new PositionDTO
                {
                    Id = worker.Position.Id,
                    Name = worker.Position.Name,
                };

                //foreach (var _workers in worker.Position?.Workers)
                //{
                //    WorkerDTO __worker = new WorkerDTO
                //    {
                //        FirstName = _workers.FirstName,
                //        LastName = _workers.LastName,
                //        MiddleName = _workers.MiddleName,
                //        PositionId = _workers.PositionId,
                //        CompanyId = _workers.CompanyId,
                //        DateEmployment = _workers.DateEmployment
                //    };
                //    myworker.Position?.Workers?.Add(__worker);
                //}
                
                myworker.PositionId = worker.PositionId;



                myworker.Company = new CompanyDTO
                {
                    Id = worker.Company.Id,
                    Name = worker.Company?.Name,
                    FormType = new FormTypeDTO
                    {
                        Id = worker.Company.FormType.Id,
                        Name = worker.Company?.FormType?.Name
                    },
                    FormTypeId = worker.Company.FormTypeId,
                };

                //foreach (var _workers in worker.Position?.Workers)
                //{
                //    WorkerDTO __worker = new WorkerDTO
                //    {
                //        FirstName = _workers.FirstName,
                //        LastName = _workers.LastName,
                //        MiddleName = _workers.MiddleName,
                //        PositionId = _workers.PositionId,
                //        CompanyId = _workers.CompanyId,
                //        DateEmployment = _workers.DateEmployment
                //    };
                //    myworker.Company?.Workers?.Add(__worker);
                //}

                myworker.CompanyId = worker.CompanyId;


                yield return myworker;
            }
        }

        public void UpdateWorker(WorkerDTO worker)
        {
            throw new NotImplementedException();
        }
    }
}
