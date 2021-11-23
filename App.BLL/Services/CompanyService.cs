using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.Interfaces;
using App.DAL.Models;
using System.Collections.Generic;
using System;
using App.BLL.DTOToDAL;
using App.BLL.DALToDTO;

namespace App.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        IUnitOfWork Database { get; set; }

        public CompanyService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
        public void AddCompany(CompanyDTO item)
        {
            CompanyDAL company = item.ToDAL();

            Database.Companies.Create(company);
        }

        public void DeleteCompany(int id)
        {
            if (Database.Companies.Find(new CompanyDAL { Id = id }) != null)
            {
                Database.Companies.Delete(id);
            }
        }

        public IEnumerable<CompanyDTO> GetCompanies()
        {
            var companies = Database.Companies.GetAll();
            foreach (var company in companies)
            {
                yield return company.ToDTO();
            }
        }

        public IEnumerable<CompanyDTO> GetCompanyBy(int id = 0, string name = "", int formTypeId = 0)
        {
            CompanyDAL item = null;
            if (id != 0)
            {
                if (item == null)
                {
                    item = new CompanyDAL();
                }
                item.Id = id;
            }
            if (!String.IsNullOrEmpty(name))
            {
                if (item == null)
                {
                    item = new CompanyDAL();
                }
                item.Name = name;
            }
            if (formTypeId != 0)
            {
                if (item == null)
                {
                    item = new CompanyDAL();
                }
                item.FormTypeId = formTypeId;
            }

            var companies = Database.Companies.Find(item);

            foreach (var company in companies)
            {
                CompanyDTO myCompany = company.ToDTO();

                yield return myCompany;
            }
        }

        public void UpdateCompany(CompanyDTO item, int? id = null)
        {
            CompanyDAL company = item.ToDAL();
            Database.Companies.Update(company, id);
        }
    }
}
