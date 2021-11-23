using App.BLL.DTO;
using App.DAL.Models;

namespace App.BLL.DALToDTO
{
    public static class DALToDTO
    {
        public static WorkerDTO ToDTO(this WorkerDAL workerDAL)
        {
            if (workerDAL != null)
            {
                WorkerDTO myworker = new WorkerDTO();
                myworker.Id = workerDAL.Id;
                myworker.FirstName = workerDAL.FirstName;
                myworker.LastName = workerDAL.LastName;
                myworker.MiddleName = workerDAL.MiddleName;
                myworker.DateEmployment = workerDAL.DateEmployment;

                myworker.Position = workerDAL.Position?.ToDTO();
                myworker.PositionId = workerDAL.PositionId;


                myworker.Company = workerDAL.Company?.ToDTO();
                myworker.CompanyId = workerDAL.CompanyId;


                return myworker;
            }
            return null;
        }
        public static PositionDTO ToDTO(this PositionDAL positionDAL)
        {
            if (positionDAL != null)
            {
                PositionDTO position = new PositionDTO();
                position.Id = positionDAL.Id;
                position.Name = positionDAL.Name;
                return position;
            }
            return null;
        }
        public static CompanyDTO ToDTO(this CompanyDAL companyDAL)
        {
            if (companyDAL != null)
            {
                CompanyDTO company = new CompanyDTO();
                company.Id = companyDAL.Id;
                company.Name = companyDAL.Name;
                company.FormTypeId = companyDAL.FormTypeId;
                company.FormType = companyDAL.FormType?.ToDTO();
                return company;
            }
            return null;
        }
        public static FormTypeDTO ToDTO(this FormTypeDAL formTypeDAL)
        {
            if (formTypeDAL != null)
            {
                FormTypeDTO formType = new FormTypeDTO();
                formType.Id = formTypeDAL.Id;
                formType.Name = formTypeDAL.Name;
                return formType;
            }
            return null;
        }
    }
}
