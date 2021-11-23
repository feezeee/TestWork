
namespace App.BLL.Interfaces
{
    public interface IManagerServices
    {
        IWorkerService WorkerService { get; }
        IPositionService PositionService { get; }
        ICompanyService CompanyService { get; }
        IFormTypeService FormTypeService { get; }
    }
}
