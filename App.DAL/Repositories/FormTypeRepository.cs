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
    public class FormTypeRepository : IRepository<FormTypeDAL>
    {
        private MyDb db;

        public FormTypeRepository(MyDb context)
        {
            this.db = context;
        }

        public void Create(FormTypeDAL item)
        {
            if (db.FormTypes.TableExist())
            {
                db.FormTypes.Create(item);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public void Delete(int id)
        {
            if (db.FormTypes.TableExist())
            {
                db.FormTypes.Delete(id);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public IEnumerable<FormTypeDAL> Find(FormTypeDAL item)
        {
            if (db.FormTypes.TableExist())
            {
                return db.FormTypes.Find(item);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public IEnumerable<FormTypeDAL> GetAll()
        {
            if (db.FormTypes.TableExist())
            {
                return db.FormTypes.GetAll();
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public FormTypeDAL GetById(int id)
        {
            if (db.FormTypes.TableExist())
            {
                var formTypes = db.FormTypes.Find(new FormTypeDAL { Id = id });
                foreach (var form in formTypes)
                {
                    return form;
                }
                return null;
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public void Update(FormTypeDAL item, int? id = null)
        {

            if (db.FormTypes.TableExist())
            {
                db.FormTypes.Update(item, id);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }
    }
}
