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
        private PositionRepository positionRepository;
        private FormTypeRepository formTypeRepository;
        private CompanyRepository companyRepository;


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

        public IRepository<Position> Positions
        {
            get
            {
                if (positionRepository == null)
                    positionRepository = new PositionRepository(db);
                return positionRepository;
            }
        }

        public IRepository<FormType> FormTypes
        {
            get
            {
                if (formTypeRepository == null)
                    formTypeRepository = new FormTypeRepository(db);
                return formTypeRepository;
            }
        }

        public IRepository<Company> Companies
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new CompanyRepository(db);
                return companyRepository;
            }
        }
    }
}
