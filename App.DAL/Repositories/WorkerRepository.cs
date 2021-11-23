using App.DAL.Data;
using App.DAL.Interfaces;
using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Repositories
{
    public class WorkerRepository : IRepository<Worker>
    {
        private MyDb db;

        public WorkerRepository(MyDb context)
        {
            this.db = context;
        }

        public void Create(Worker item)
        {
            db.Workers.Create(item);
        }

        public void Delete(int id)
        {
            db.Workers.Delete(id);
        }

        public IEnumerable<Worker> Find(Func<Worker, bool> predicate)
        {
            return db.Workers.Find(predicate);
        }

        public IEnumerable<Worker> GetAll()
        {
            return db.Workers.GetAll();
        }

        public Worker GetById(int id)
        {
            return db.Workers.GetById(id);
        }

        public void Update(Worker item)
        {
            db.Workers.Update(item);
        }
    }
}
