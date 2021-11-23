using App.DAL.Models;

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
