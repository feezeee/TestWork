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
    public class CompanyRepository : IRepository<CompanyDAL>
    {
        private MyDb db;

        public CompanyRepository(MyDb context)
        {
            this.db = context;
        }

        public void Create(CompanyDAL item)
        {
            if (db.Companies.TableExist())
            {
                db.Companies.Create(item);
            }
            else
            {
                throw new NullReferenceException("Таблица компаний не существует");
            }
        }

        public void Delete(int id)
        {
            if (db.Companies.TableExist())
            {
                db.Companies.Delete(id);
            }
            else
            {
                throw new NullReferenceException("Таблица компаний не существует");
            }
        }

        public IEnumerable<CompanyDAL> Find(CompanyDAL item)
        {
            if (db.Companies.TableExist())
            {
                return db.Companies.Find(item);
            }
            else
            {
                throw new NullReferenceException("Таблица компаний не существует");

            }
        }

        public IEnumerable<CompanyDAL> GetAll()
        {
            if (db.Companies.TableExist())
            {
                return db.Companies.GetAll();
            }
            else
            {
                throw new NullReferenceException("Таблица компаний не существует");
            }
        }

        public CompanyDAL GetById(int id)
        {
            if (db.Companies.TableExist())
            {
                var companies = db.Companies.Find(new CompanyDAL { Id = id });
                foreach (var company in companies)
                {
                    return company;
                }
                return null;
            }
            else
            {
                throw new NullReferenceException("Таблица компаний не существует");
            }
        }

        public void Update(CompanyDAL item, int? id = null)
        {

            if (db.Companies.TableExist())
            {
                db.Companies.Update(item, id);
            }
            else
            {
                throw new NullReferenceException("Таблица компаний не существует");
            }
        }
    }
}
