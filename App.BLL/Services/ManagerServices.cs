using App.BLL.Interfaces;
using App.DAL.Interfaces;

namespace App.BLL.Services
{
    public class ManagerServices : IManagerServices
    {
        private WorkerService workerService;
        private PositionService positionService;
        private CompanyService companyService;
        private FormTypeService formTypeService;
        private readonly IUnitOfWork _unitOfWork;


        public ManagerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IWorkerService WorkerService
        {
            get
            {
                if (workerService == null)
                    workerService = new WorkerService(_unitOfWork);
                return workerService;
            }
        }

        public IPositionService PositionService
        {
            get
            {
                if (positionService == null)
                    positionService = new PositionService(_unitOfWork);
                return positionService;
            }
        }

        public ICompanyService CompanyService
        {
            get
            {
                if (companyService == null)
                    companyService = new CompanyService(_unitOfWork);
                return companyService;
            }
        }

        public IFormTypeService FormTypeService
        {
            get
            {
                if (formTypeService == null)
                    formTypeService = new FormTypeService(_unitOfWork);
                return formTypeService;
            }
        }
    }
}
