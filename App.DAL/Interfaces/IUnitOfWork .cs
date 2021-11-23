using App.DAL.Models;

namespace App.DAL.Interfaces
{
    /// <summary>
    /// Данный интерфейс содержит в себе все репозитории для удобного пользования
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Репозиторий работников
        /// </summary>
        public IRepository<WorkerDAL> Workers { get; }
        /// <summary>
        /// Репозиторий должностей
        /// </summary>
        public IRepository<PositionDAL> Positions { get; }
        /// <summary>
        /// Репозиторий ОПФ
        /// </summary>
        public IRepository<FormTypeDAL> FormTypes { get; }
        /// <summary>
        /// Репозиторий компаний
        /// </summary>
        public IRepository<CompanyDAL> Companies { get; }
    }
}
