using App.BLL.DTO;
using TestWork.Models;

namespace TestWork.ViewModelsToDTO
{
    public static class ViewModelToDTO
    {
        public static WorkerDTO ToDTO(this WorkerViewModel workerViewModel)
        {
            if (workerViewModel != null)
            {
                WorkerDTO myworker = new WorkerDTO();
                myworker.Id = workerViewModel.Id;
                myworker.FirstName = workerViewModel.FirstName;
                myworker.LastName = workerViewModel.LastName;
                myworker.MiddleName = workerViewModel.MiddleName;
                myworker.DateEmployment = workerViewModel.DateEmployment;
                myworker.Position = workerViewModel.Position?.ToDTO();
                myworker.PositionId = workerViewModel.PositionId;
                myworker.Company = workerViewModel.Company?.ToDTO();
                myworker.CompanyId = workerViewModel.CompanyId;
                return myworker;

            }
            return null;
        }
        public static PositionDTO ToDTO(this PositionViewModel positionViewModel)
        {
            if (positionViewModel != null)
            {
                PositionDTO position = new PositionDTO();
                position.Id = positionViewModel.Id;
                position.Name = positionViewModel.Name;
                return position;
            }
            return null;
        }
        public static CompanyDTO ToDTO(this CompanyViewModel companyViewModel)
        {
            if (companyViewModel != null)
            {
                CompanyDTO company = new CompanyDTO();
                company.Id = companyViewModel.Id;
                company.Name = companyViewModel.Name;
                company.FormTypeId = companyViewModel.FormTypeId;
                company.FormType = companyViewModel.FormType?.ToDTO();
                return company;
            }
            return null;
        }
        public static FormTypeDTO ToDTO(this FormTypeViewModel formTypeViewModel)
        {
            if (formTypeViewModel != null)
            {
                FormTypeDTO formType = new FormTypeDTO();
                formType.Id = formTypeViewModel.Id;
                formType.Name = formTypeViewModel.Name;
                return formType;
            }
            return null;
        }
    }
}
