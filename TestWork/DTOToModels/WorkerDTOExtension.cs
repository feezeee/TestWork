using App.BLL.DTO;
using TestWork.Models;

namespace TestWork.DTOToModels
{
    public static class WorkerDTOExtension
    {
        public static Worker Worker(this WorkerDTO workerDTO)
        {
            Worker myworker = new Worker();
            myworker.Id = workerDTO.Id;
            myworker.FirstName = workerDTO.FirstName;
            myworker.LastName = workerDTO.LastName;
            myworker.MiddleName = workerDTO.MiddleName;
            myworker.DateEmployment = workerDTO.DateEmployment;


            myworker.Position = new Position
            {
                Id = workerDTO.Position.Id,
                Name = workerDTO.Position.Name,
            };

            myworker.PositionId = workerDTO.PositionId;



            myworker.Company = new Company
            {
                Id = workerDTO.Company.Id,
                Name = workerDTO.Company?.Name,
                FormType = new FormType
                {
                    Id = workerDTO.Company.FormType.Id,
                    Name = workerDTO.Company?.FormType?.Name
                },
                FormTypeId = workerDTO.Company.FormTypeId,
            };

            myworker.CompanyId = workerDTO.CompanyId;
            return myworker;
        }
    }
}
