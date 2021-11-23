using App.DAL.Data;
using App.DAL.Interfaces;
using App.DAL.Models;
using System.Collections.Generic;
using System;

namespace App.DAL.Repositories
{
    public class WorkerRepository : IRepository<WorkerDAL>
    {
        private MyDb db;

        public WorkerRepository(MyDb context)
        {
            this.db = context;
        }

        public void Create(WorkerDAL item)
        {
            if (db.Workers.TableExist())
            {
                db.Workers.Create(item);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public void Delete(int id)
        {
            if (db.Workers.TableExist())
            {
                db.Workers.Delete(id);               
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public IEnumerable<WorkerDAL> Find(WorkerDAL item)
        {
            if (db.Workers.TableExist())
            {
                return db.Workers.Find(item);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public IEnumerable<WorkerDAL> GetAll()
        {
            if (db.Workers.TableExist())
            {
                return db.Workers.GetAll();
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public WorkerDAL GetById(int id)
        {
            if (db.Workers.TableExist())
            {
                var workers = db.Workers.Find(new WorkerDAL { Id = id });
                foreach (var worker in workers)
                {
                    return worker;
                }
                return null;
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }            
        }

        public void Update(WorkerDAL item, int? id = null)
        {

            if (db.Workers.TableExist())
            {
                db.Workers.Update(item, id);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }
    }
}
