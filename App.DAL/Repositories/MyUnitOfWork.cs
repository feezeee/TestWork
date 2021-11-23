using App.DAL.Data;
using App.DAL.Interfaces;
using App.DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Repositories
{
    public class MyUnitOfWork : IUnitOfWork
    {
        private MyDb db;
        private WorkerRepository workerRepository;

        public MyUnitOfWork(IConfiguration configuration)
        {
            db = new MyDb(configuration.GetConnectionString("DefaultConnection"));
        }

        public IRepository<Worker> Workers
        {
            get
            {
                if (workerRepository == null)
                    workerRepository = new WorkerRepository(db);
                return workerRepository;
            }
        }

        public IRepository<Position> Positions => throw new NotImplementedException();

        public IRepository<FormType> FormTypes => throw new NotImplementedException();

        public IRepository<Company> Companies => throw new NotImplementedException();
    }
}
