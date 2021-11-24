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
        /// <summary>
        /// Добавление должности в репозиторий
        /// </summary>
        /// <param name="item"></param>
        void AddPosition(PositionDTO item);

        /// <summary>       
        /// Выборка из репозитория нужных работников       
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerable<PositionDTO> GetPositionBy(int id = 0, string name = "");


        /// <summary>
        /// Выборка из репозитория всех должностей
        /// </summary>
        /// <returns></returns>
        IEnumerable<PositionDTO> GetPositions();

        /// <summary>
        /// Обновление данных о должности в репозитории
        /// </summary>
        /// <param name="item"></param>
        void UpdatePosition(PositionDTO item);

        /// <summary>
        /// Удаление должности из репозитория по Id
        /// </summary>
        /// <param name="id"></param>
        void DeletePosition(int id);


        /// <summary>
        /// Async добавление должности в репозиторий
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task AddPositionAsync(PositionDTO item);
        /// <summary>
        /// Async выборка из репозитория всех должностей
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PositionDTO>> GetPositionsAsync();

        /// <summary>
        /// Async Выборка из репозитория должностей по параметрам
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<PositionDTO>> GetPositionByAsync(int id = 0, string name = "");
        /// <summary>
        /// Async обновление данных о должности в репозитории
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpdatePositionAsync(PositionDTO item);

        /// <summary>
        /// Async удаление должности по id из репозитория
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeletePositionAsync(int id);

    }
}
