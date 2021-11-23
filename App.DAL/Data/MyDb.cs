using App.DAL.Data.DbSet;
using App.DAL.Models;

namespace App.DAL.Data
{
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
        public IDbSet<Worker> Workers { get; }
        public IDbSet<Position> Positions { get; }
        public IDbSet<FormType> FormTypes { get; }
        public IDbSet<Company> Companies { get; }

    }
}
