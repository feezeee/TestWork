using App.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    /// <summary>
    /// Сервис для работы со соответствующим репозиторием
    /// </summary>
    public interface IPositionService
    {
        void AddPosition(PositionDTO item);

        IEnumerable<PositionDTO> GetPositionBy(int id = 0, string name = "");

        IEnumerable<PositionDTO> GetPositions();

        void UpdatePosition(PositionDTO item);

        void DeletePosition(int id);

        Task AddPositionAsync(PositionDTO item);
        Task<IEnumerable<PositionDTO>> GetPositionsAsync();
        Task<IEnumerable<PositionDTO>> GetPositionByAsync(int id = 0, string name = "");
        Task UpdatePositionAsync(PositionDTO item);
        Task DeletePositionAsync(int id);

    }
}
