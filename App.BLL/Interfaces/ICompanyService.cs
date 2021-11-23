using App.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface ICompanyService
    {
        void AddCompany(CompanyDTO orderDto);

        IEnumerable<CompanyDTO> GetCompanyBy(int id, string name);

        IEnumerable<CompanyDTO> GetCompanies();

        void UpdateCompany(CompanyDTO worker);

        void DeleteCompany(int id);


    }
}
