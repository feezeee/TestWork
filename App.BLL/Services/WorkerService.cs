using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.Interfaces;
using App.DAL.Models;
using System.Collections.Generic;
using System;

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
                Worker worker = new Worker
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MiddleName = item.MiddleName,
                    PositionId = item.PositionId,
                    CompanyId = item.CompanyId,
                    DateEmployment = item.DateEmployment
                };

                Database.Workers.Create(worker);
            
            
        }

        public void DeleteWorker(int id)
        {
                if (Database.Workers.Find(new Worker { Id = id }) != null)
                {
                    Database.Workers.Delete(id);
                }              
            
        }

       

        public IEnumerable<WorkerDTO> GetWorkers()
        {
            var workers = Database.Workers.GetAll();
            foreach (var worker in workers)
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

        public void UpdateWorker(WorkerDTO item)
        {
            Worker worker = new Worker
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                MiddleName = item.MiddleName,
                PositionId = item.PositionId,
                CompanyId = item.CompanyId,
                DateEmployment = item.DateEmployment
            };

            Database.Workers.Update(worker);
        }

        public IEnumerable<WorkerDTO> GetWorkerBy(int id, string lastName, string firstName, string middleName)
        {
            Worker item = null;
            if (id != 0)
            {
                if(item == null)
                {
                    item = new Worker();
                }
                item.Id = id;
            }
            if (!String.IsNullOrEmpty(lastName))
            {
                if (item == null)
                {
                    item = new Worker();
                }
                item.LastName = lastName;
            }
            if (!String.IsNullOrEmpty(firstName))
            {
                if (item == null)
                {
                    item = new Worker();
                }
                item.FirstName = firstName;
            }
            if (!String.IsNullOrEmpty(middleName))
            {
                if (item == null)
                {
                    item = new Worker();
                }
                item.MiddleName = middleName;
            }
            var workers = Database.Workers.Find(item);
            foreach (var worker in workers)
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

                myworker.CompanyId = worker.CompanyId;


                yield return myworker;
            }
        }
    }
}
