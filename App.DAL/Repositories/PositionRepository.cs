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
    public class PositionRepository : IRepository<PositionDAL>
    {
        private MyDb db;

        public PositionRepository(MyDb context)
        {
            this.db = context;
        }

        public void Create(PositionDAL item)
        {
            if (db.Positions.TableExist())
            {
                db.Positions.Create(item);
            }
            else
            {
                throw new NullReferenceException("Таблица должностей не существует");
            }
        }

        public void Delete(int id)
        {
            if (db.Positions.TableExist())
            {
                db.Positions.Delete(id);
            }
            else
            {
                throw new NullReferenceException("Таблица должностей не существует");
            }
        }

        public IEnumerable<PositionDAL> Find(PositionDAL item)
        {
            if (db.Positions.TableExist())
            {
                return db.Positions.Find(item);
            }
            else
            {
                throw new NullReferenceException("Таблица должностей не существует");
            }
        }

        public IEnumerable<PositionDAL> GetAll()
        {
            if (db.Positions.TableExist())
            {
                return db.Positions.GetAll();
            }
            else
            {
                throw new NullReferenceException("Таблица должностей не существует");
            }
        }

        public PositionDAL GetById(int id)
        {
            if (db.Positions.TableExist())
            {
                var positions = db.Positions.Find(new PositionDAL { Id = id });
                foreach (var position in positions)
                {
                    return position;
                }
                return null;
            }
            else
            {
                throw new NullReferenceException("Таблица должностей не существует");
            }
        }

        public void Update(PositionDAL item, int? id = null)
        {

            if (db.Positions.TableExist())
            {
                db.Positions.Update(item, id);
            }
            else
            {
                throw new NullReferenceException("Таблица должностей не существует");
            }
        }
    }
}
