using App.BLL.DTO;
using System.Collections.Generic;

namespace App.BLL.Interfaces
{
    public interface IPositionService
    {
        void AddPosition(PositionDTO item);

        IEnumerable<PositionDTO> GetPositionBy(int id = 0, string name = "");

        IEnumerable<PositionDTO> GetPositions();

        void UpdatePosition(PositionDTO item);

        void DeletePosition(int id);
        

    }
}
