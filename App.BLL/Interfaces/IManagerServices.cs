using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
