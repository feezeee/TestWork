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
        void AddCompany(CompanyDTO item);

        IEnumerable<CompanyDTO> GetCompanyBy(int id = 0, string name = "", int formTypeId = 0);

        IEnumerable<CompanyDTO> GetCompanies();

        void UpdateCompany(CompanyDTO item, int? id = null);

        void DeleteCompany(int id);


        Task AddCompanyAsync(CompanyDTO item);
        Task<IEnumerable<CompanyDTO>> GetCompaniesAsync();
        Task<IEnumerable<CompanyDTO>> GetCompanyByAsync(int id = 0, string name = "", int formTypeId = 0);
        Task UpdateCompanyAsync(CompanyDTO item, int? id = null);
        Task DeleteCompanyAsync(int id);


    }
}
