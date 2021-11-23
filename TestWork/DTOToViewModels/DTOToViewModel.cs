using App.BLL.DTO;
using TestWork.Models;

namespace TestWork.DTOToViewModels
{
    public static class DTOToViewModel
    {
        public static WorkerViewModel ToViewModel(this WorkerDTO workerDTO)
        {
            if (workerDTO != null)
            {
                WorkerViewModel myworker = new WorkerViewModel();
                myworker.Id = workerDTO.Id;
                myworker.FirstName = workerDTO.FirstName;
                myworker.LastName = workerDTO.LastName;
                myworker.MiddleName = workerDTO.MiddleName;
                myworker.DateEmployment = workerDTO.DateEmployment;
                myworker.Position = workerDTO.Position?.ToViewModel();
                myworker.PositionId = workerDTO.PositionId;
                myworker.Company = workerDTO.Company?.ToViewModel();
                myworker.CompanyId = workerDTO.CompanyId;


                return myworker;
            }
            return null;
        }
        public static PositionViewModel ToViewModel(this PositionDTO positionDTO)
        {
            if (positionDTO != null)
            {
                PositionViewModel position = new PositionViewModel();
                position.Id = positionDTO.Id;
                position.Name = positionDTO.Name;
                return position;
            }
            return null;
        }
        public static CompanyViewModel ToViewModel(this CompanyDTO companyDTO)
        {
            if (companyDTO != null)
            {
                CompanyViewModel company = new CompanyViewModel();
                company.Id = companyDTO.Id;
                company.Name = companyDTO.Name;
                company.FormTypeId = companyDTO.FormTypeId;
                company.FormType = companyDTO.FormType?.ToViewModel();
                return company;
            }
            return null;
        }
        public static FormTypeViewModel ToViewModel(this FormTypeDTO formTypeDTO)
        {
            if (formTypeDTO != null)
            {
                FormTypeViewModel formType = new FormTypeViewModel();
                formType.Id = formTypeDTO.Id;
                formType.Name = formTypeDTO.Name;
                return formType;
            }
            return null;
        }
    }

}
