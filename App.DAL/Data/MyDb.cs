using App.DAL.Data.DbSet;
using App.DAL.Models;

namespace App.DAL.Data
{
    /// <summary>
    /// Представляет DbContext мой собственный
    /// </summary>
    public class MyDb
    {
        private readonly string _connectionString;

        public MyDb(string connectionString)
        {
            _connectionString = connectionString;
            Workers = new WorkerDbSet(_connectionString);
            Positions = new PositionDbSet(_connectionString);
            FormTypes = new FormTypeDbSet(_connectionString);
            Companies = new CompanyDbSet(_connectionString);

        }
        public IDbSet<WorkerDAL> Workers { get; }
        public IDbSet<PositionDAL> Positions { get; }
        public IDbSet<FormTypeDAL> FormTypes { get; }
        public IDbSet<CompanyDAL> Companies { get; }

    }
}
