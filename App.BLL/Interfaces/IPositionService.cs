using App.BLL.DTO;
using System.Collections.Generic;

namespace App.BLL.Interfaces
{
    public interface IPositionService
    {
        void AddPosition(WorkerDTO orderDto);

        IEnumerable<PositionDTO> GetPositionBy(int id, string name);

        IEnumerable<PositionDTO> GetPositions();

        void UpdatePosition(PositionDTO worker);

        void DeletePosition(int id);
        

    }
}
