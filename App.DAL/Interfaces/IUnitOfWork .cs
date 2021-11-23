using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<WorkerDAL> Workers { get; }
        public IRepository<PositionDAL> Positions { get; }
        public IRepository<FormTypeDAL> FormTypes { get; }
        public IRepository<CompanyDAL> Companies { get; }
    }
}
