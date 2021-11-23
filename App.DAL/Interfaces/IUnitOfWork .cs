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
        public IRepository<Worker> Workers { get; }
        public IRepository<Position> Positions { get; }
        public IRepository<FormType> FormTypes { get; }
        public IRepository<Company> Companies { get; }
    }
}
