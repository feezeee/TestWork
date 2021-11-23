using App.BLL.DTO;
using App.DAL.Models;

namespace App.BLL.DTOToDAL
{
    public static class DTOToDAL
    {

        public static WorkerDAL ToDAL(this WorkerDTO workerDTO)
        {
            if (workerDTO != null)
            {
                WorkerDAL myworker = new WorkerDAL();
                myworker.Id = workerDTO.Id;
                myworker.FirstName = workerDTO.FirstName;
                myworker.LastName = workerDTO.LastName;
                myworker.MiddleName = workerDTO.MiddleName;
                myworker.DateEmployment = workerDTO.DateEmployment;

                myworker.Position = workerDTO.Position?.ToDAL();
                myworker.PositionId = workerDTO.PositionId;


                myworker.Company = workerDTO.Company?.ToDAL();
                myworker.CompanyId = workerDTO.CompanyId;


                return myworker;
            }
            return null;
        }
        public static PositionDAL ToDAL(this PositionDTO positionDTO)
        {
            if (positionDTO != null)
            {
                PositionDAL position = new PositionDAL();
                position.Id = positionDTO.Id;
                position.Name = positionDTO.Name;
                return position;
            }
            return null;
        }
        public static CompanyDAL ToDAL(this CompanyDTO companyDTO)
        {
            if (companyDTO != null)
            {
                CompanyDAL company = new CompanyDAL();
                company.Id = companyDTO.Id;
                company.Name = companyDTO.Name;
                company.FormTypeId = companyDTO.FormTypeId;
                company.FormType = companyDTO.FormType?.ToDAL();
                return company;
            }
            return null;
        }
        public static FormTypeDAL ToDAL(this FormTypeDTO formTypeDTO)
        {
            if (formTypeDTO != null)
            {
                FormTypeDAL formType = new FormTypeDAL();
                formType.Id = formTypeDTO.Id;
                formType.Name = formTypeDTO.Name;
                return formType;
            }
            return null;
        }
    }
}
