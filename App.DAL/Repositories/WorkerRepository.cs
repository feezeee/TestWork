using App.DAL.Data;
using App.DAL.Interfaces;
using App.DAL.Models;
using System.Collections.Generic;
using System;

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

        public IEnumerable<Worker> Find(Worker worker)
        {
            if (db.Workers.TableExist())
            {
                return db.Workers.Find(worker);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public IEnumerable<Worker> GetAll()
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

        public Worker GetById(int id)
        {
            if (db.Workers.TableExist())
            {
                var workers = db.Workers.Find(new Worker { Id = id });
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

        public void Update(Worker item)
        {

            if (db.Workers.TableExist())
            {
                db.Workers.Update(item);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }
    }
}
