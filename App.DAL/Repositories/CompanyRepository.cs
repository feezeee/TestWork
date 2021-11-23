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
    public class CompanyRepository : IRepository<Company>
    {
        private MyDb db;

        public CompanyRepository(MyDb context)
        {
            this.db = context;
        }

        public void Create(Company item)
        {
            if (db.Companies.TableExist())
            {
                db.Companies.Create(item);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
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
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public IEnumerable<Company> Find(Company item)
        {
            if (db.Companies.TableExist())
            {
                return db.Companies.Find(item);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public IEnumerable<Company> GetAll()
        {
            if (db.Companies.TableExist())
            {
                return db.Companies.GetAll();
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public Company GetById(int id)
        {
            if (db.Companies.TableExist())
            {
                var companies = db.Companies.Find(new Company { Id = id });
                foreach (var company in companies)
                {
                    return company;
                }
                return null;
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }

        public void Update(Company item)
        {

            if (db.Companies.TableExist())
            {
                db.Companies.Update(item);
            }
            else
            {
                throw new NullReferenceException("Таблица работников не существует");
            }
        }
    }
}
